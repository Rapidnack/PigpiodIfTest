using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Rapidnack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdcGraph
{
	public partial class Form1 : Form
	{
		private PigpiodIf pigpiodIf;

		private const int GPIO = 5;
		private CancellationTokenSource ledCts;

		private const int SERVO_1 = 26;
		private const int SERVO_2 = 13;

		private const int NUM_ROLL_CHANNELS = 8;
		private const int NUM_ROLL_SAMPLES = 100;
		private CancellationTokenSource rollCts;
		private PlotModel rollPlotModel;
		private LineSeries[] rollSeries;

		private enum EState
		{
			Idle,
			Arming,
			WaitingForTrigger1,
			WaitingForTrigger2,
			Triggered
		}
		private const int NUM_FAST_CHANNELS = 2;
		private const int NUM_FAST_SAMPLES = 100;
		private const double AUTO_TRIGGER = 0.1;
		private const int INTERVAL_IN_MS = 33;
		private CancellationTokenSource fastCts;
		private PlotModel fastPlotModel;
		private LineSeries[] fastSeries;
		private DateTime lastDispleyTime = DateTime.MinValue;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			pigpiodIf = new PigpiodIf();
			pigpiodIf.StreamChanged += (s, evt) =>
			{
				if (pigpiodIf.CommandStream != null && pigpiodIf.NotifyStream != null)
				{
					Invoke(new Action(() =>
					{
						checkBoxServo1_CheckedChanged(checkBoxServo1, new EventArgs());
						checkBoxServo2_CheckedChanged(checkBoxServo2, new EventArgs());
					}));
				}
			};

			rollPlotModel = new PlotModel();
			rollSeries = new LineSeries[NUM_ROLL_CHANNELS];
			for (int ch = 0; ch < NUM_ROLL_CHANNELS; ch++)
			{
				rollSeries[ch] = new LineSeries();
				rollSeries[ch].Title = string.Format("CH{0}", ch);
				rollPlotModel.Series.Add(rollSeries[ch]);
			}
			rollPlotModel.Axes.Add(
				new LinearAxis() { Position = AxisPosition.Left, Minimum = 0 - 0.1, Maximum = 3.3 + 0.1 }
			);

			fastPlotModel = new PlotModel();
			fastSeries = new LineSeries[NUM_FAST_CHANNELS];
			for (int ch = 0; ch < NUM_FAST_CHANNELS; ch++)
			{
				fastSeries[ch] = new LineSeries();
				fastSeries[ch].Title = string.Format("CH{0}", ch);
				fastPlotModel.Series.Add(fastSeries[ch]);
			}
			fastPlotModel.Axes.Add(
				new LinearAxis() { Position = AxisPosition.Left, Minimum = 0 - 0.1, Maximum = 3.3 + 0.1 }
			);

			buttonClose.Enabled = false;
			buttonLedStop.Enabled = false;
			buttonRollStop.Enabled = false;
			buttonFastStop.Enabled = false;
		}

		private void buttonOpen_Click(object sender, EventArgs e)
		{
			pigpiodIf.pigpio_start(textBoxAddress.Text, "8888");

			buttonOpen.Enabled = false;
			buttonClose.Enabled = true;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			pigpiodIf.pigpio_stop();

			buttonOpen.Enabled = true;
			buttonClose.Enabled = false;
		}

		private async void buttonLedStart_Click(object sender, EventArgs e)
		{
			buttonLedStart.Enabled = false;
			buttonLedStop.Enabled = true;
			try
			{
				ledCts = new CancellationTokenSource();
				var ct = ledCts.Token;
				await Task.Run(async () =>
				{
					while (!ct.IsCancellationRequested)
					{
						pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_HIGH);
						await Task.Delay(500, ct);
						pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_LOW);
						await Task.Delay(500, ct);
					}
				}, ct);
			}
			catch (OperationCanceledException)
			{
				// nothing to do
			}
			finally
			{
				buttonLedStart.Enabled = true;
				buttonLedStop.Enabled = false;
			}
		}

		private void buttonLedStop_Click(object sender, EventArgs e)
		{
			ledCts.Cancel();
		}

		private void checkBoxServo1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxServo1.Checked)
			{
				pigpiodIf.set_servo_pulsewidth(SERVO_1, (UInt32)trackBarServo1.Value);
			}
			else
			{
				pigpiodIf.set_servo_pulsewidth(SERVO_1, 0);
			}
		}

		private void trackBarServo1_Scroll(object sender, EventArgs e)
		{
			if (checkBoxServo1.Checked)
			{
				pigpiodIf.set_servo_pulsewidth(SERVO_1, (UInt32)trackBarServo1.Value);
			}
			else
			{
				checkBoxServo1.Checked = true;
			}
		}

		private void checkBoxServo2_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxServo2.Checked)
			{
				pigpiodIf.set_servo_pulsewidth(SERVO_2, (UInt32)trackBarServo2.Value);
			}
			else
			{
				pigpiodIf.set_servo_pulsewidth(SERVO_2, 0);
			}
		}

		private void trackBarServo2_Scroll(object sender, EventArgs e)
		{
			if (checkBoxServo2.Checked)
			{
				pigpiodIf.set_servo_pulsewidth(SERVO_2, (UInt32)trackBarServo2.Value);
			}
			else
			{
				checkBoxServo2.Checked = true;
			}
		}

		private async void buttonRollStart_Click(object sender, EventArgs e)
		{
			buttonRollStart.Enabled = false;
			buttonFastStart.Enabled = false;
			buttonRollStop.Enabled = true;

			int h = -1;
			try
			{
				rollCts = new CancellationTokenSource();
				var ct = rollCts.Token;
				await Task.Run(() =>
				{
					h = pigpiodIf.spi_open(2, 1000000, 256 + 0);
					if (h < 0)
					{
						throw new PigpiodIfException(h, "PigpiodIf: " + pigpiodIf.pigpio_error(h));
					}

					for (int ch = 0; ch < NUM_ROLL_CHANNELS; ch++)
					{
						rollSeries[ch].Points.Clear();
					}
					DateTime start = DateTime.Now;
					while (!ct.IsCancellationRequested)
					{
						double[] volts = new double[NUM_ROLL_CHANNELS];
						DataPoint[] dataPoints = new DataPoint[NUM_ROLL_CHANNELS];
						for (int ch = 0; ch < NUM_ROLL_CHANNELS; ch++)
						{
							byte[] buf = new byte[] { 0x01, (byte)(0x80 + (ch << 4)), 0x00 };
							int b = pigpiodIf.spi_xfer((UInt32)h, buf, buf);
							if (b == 3)
							{
								TimeSpan ts = DateTime.Now - start;
								double volt = 3.3 * (((buf[1] & 0x0f) * 256) + buf[2]) / 1024.0;
								volts[ch] = volt;
								dataPoints[ch] = new DataPoint(ts.TotalSeconds, volt);
							}
						}

						string s = volts[0].ToString("0.0");
						for (int ch = 1; ch < NUM_ROLL_CHANNELS; ch++)
						{
							s += string.Format(", {0:0.0}", volts[ch]);
						}
						Console.WriteLine("{0}", s);

						Invoke(new Action(() =>
						{
							for (int ch = 0; ch < NUM_ROLL_CHANNELS; ch++)
							{
								if (rollSeries[ch].Points.Count >= NUM_ROLL_SAMPLES)
								{
									rollSeries[ch].Points.RemoveAt(0);
								}
								rollSeries[ch].Points.Add(dataPoints[ch]);
							}
							rollPlotModel.InvalidatePlot(true);
							if (plotView1.Model != rollPlotModel)
							{
								plotView1.Model = rollPlotModel;
							}
							else
							{
								plotView1.Invalidate();
							}
						}));
					}
				}, ct);
			}
			catch (OperationCanceledException)
			{
				// nothing to do
			}
			catch (PigpiodIfException ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				if (h >= 0)
				{
					pigpiodIf.spi_close((UInt32)h);
				}

				buttonRollStart.Enabled = true;
				buttonFastStart.Enabled = true;
				buttonRollStop.Enabled = false;
			}
		}

		private void buttonRollStop_Click(object sender, EventArgs e)
		{
			rollCts.Cancel();
		}

		private async void buttonFastStart_Click(object sender, EventArgs e)
		{
			buttonRollStart.Enabled = false;
			buttonFastStart.Enabled = false;
			buttonFastStop.Enabled = true;

			int h = -1;
			try
			{
				fastCts = new CancellationTokenSource();
				var ct = fastCts.Token;
				await Task.Run(async () =>
				{
					h = pigpiodIf.spi_open(2, 1000000, 256 + 0);
					if (h < 0)
					{
						throw new PigpiodIfException(h, "PigpiodIf: " + pigpiodIf.pigpio_error(h));
					}

					List<DataPoint>[] dataPoints = new List<DataPoint>[NUM_FAST_CHANNELS];
					for (int ch = 0; ch < NUM_FAST_CHANNELS; ch++)
					{
						dataPoints[ch] = new List<DataPoint>();
					}

					while (!ct.IsCancellationRequested)
					{
						EState state = EState.Arming;
						for (int ch = 0; ch < NUM_FAST_CHANNELS; ch++)
						{
							dataPoints[ch].Clear();
						}
						DateTime start = DateTime.Now;
						while (true)
						{
							double[] volts = new double[NUM_FAST_CHANNELS];
							for (int ch = 0; ch < NUM_FAST_CHANNELS; ch++)
							{
								byte[] buf = new byte[] { 0x01, (byte)(0x80 + (ch << 4)), 0x00 };
								int b = pigpiodIf.spi_xfer((UInt32)h, buf, buf);
								if (b == 3)
								{
									TimeSpan ts = DateTime.Now - start;
									volts[ch] = 3.3 * (((buf[1] & 0x0f) * 256) + buf[2]) / 1024.0;
									if (EState.Arming < state && state < EState.Triggered)
									{
										dataPoints[ch].RemoveAt(0);
									}
									dataPoints[ch].Add(new DataPoint(ts.TotalSeconds, volts[ch]));
								}
							}

							if (dataPoints[0].Count >= NUM_FAST_SAMPLES / 2)
							{
								switch (state)
								{
									case EState.Arming:
										state = EState.WaitingForTrigger1;
										break;
									case EState.WaitingForTrigger1:
										if (dataPoints[0].Last().Y < 3.3 / 2)
										{
											state = EState.WaitingForTrigger2;
										}
										else if ((DateTime.Now - start) >= TimeSpan.FromSeconds(AUTO_TRIGGER))
										{
											state = EState.Triggered;
										}
										break;
									case EState.WaitingForTrigger2:
										if (dataPoints[0].Last().Y > 3.3 / 2)
										{
											state = EState.Triggered;
										}
										else if ((DateTime.Now - start) >= TimeSpan.FromSeconds(AUTO_TRIGGER))
										{
											state = EState.Triggered;
										}
										break;
								}
							}

							if (dataPoints[0].Count >= NUM_FAST_SAMPLES)
							{
								if (state == EState.Triggered)
								{
									state = EState.Idle;
									break;
								}
							}
						}
						for (int ch = 0; ch < NUM_FAST_CHANNELS; ch++)
						{
							double x0 = dataPoints[ch][0].X;
							for (int sample = 0; sample < dataPoints[ch].Count; sample++)
							{
								var dp = dataPoints[ch][sample];
								dataPoints[ch][sample] = new DataPoint(dp.X - x0, dp.Y);
							}
						}

						int leftInMS = INTERVAL_IN_MS - (int)(DateTime.Now - lastDispleyTime).TotalMilliseconds;
						if (leftInMS > 0)
						{
							//Console.WriteLine("spentTime: {0}", leftInMS);
							await Task.Delay(leftInMS, ct);
						}
						lastDispleyTime = DateTime.Now;

						Invoke(new Action(() =>
						{
							for (int ch = 0; ch < NUM_FAST_CHANNELS; ch++)
							{
								fastSeries[ch].Points.Clear();
								fastSeries[ch].Points.AddRange(dataPoints[ch]);
							}
							fastPlotModel.InvalidatePlot(true);
							if (plotView1.Model != fastPlotModel)
							{
								plotView1.Model = fastPlotModel;
							}
							else
							{
								plotView1.Invalidate();
							}
						}));
					}
				}, ct);
			}
			catch (OperationCanceledException)
			{
				// nothing to do
			}
			catch (PigpiodIfException ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				if (h >= 0)
				{
					pigpiodIf.spi_close((UInt32)h);
				}

				buttonRollStart.Enabled = true;
				buttonFastStart.Enabled = true;
				buttonFastStop.Enabled = false;
			}
		}

		private void buttonFastStop_Click(object sender, EventArgs e)
		{
			fastCts.Cancel();
		}
	}
}
