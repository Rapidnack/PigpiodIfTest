using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Rapidnack.Net;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Max10Graph
{
	public partial class Form1 : Form
	{
		private PigpiodIf pigpiodIf;
		private AvalonMM avalonMM = new AvalonMM();

		private const int SERVO_1 = 12;
		private const int SERVO_2 = 13;

		private const int COMPLETE = 26;
		private const int NUM_CHANNELS = 2;
		private const int NUM_SAMPLES = 512;
		private const int AUTO_TRIGGER_MS = 100;
		private CancellationTokenSource cts;
		private bool repeatRequested;
		private PlotModel plotModel;
		private LineSeries[] series;
		private int horizontal = 9;
		private int positionPct = 50;
		private int source = 0;
		private int polarity = 0;
		private int levelPct = 50;

		public Form1()
		{
			InitializeComponent();
		}

		private void CloseConnection()
		{
			if (cts != null)
			{
				cts.Cancel();
				while (cts != null)
				{
					Application.DoEvents();
				}
			}

			if (avalonMM.Stream != null)
			{
				avalonMM.WriteUInt32Packet(0x10, 0);  // Stop sequencer

				avalonMM.Stream.Dispose();
				avalonMM.Stream = null;
			}

			pigpiodIf.pigpio_stop();

			panelOperation.Enabled = false;
			buttonOpen.Enabled = true;
			buttonClose.Enabled = false;
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			pigpiodIf = new PigpiodIf();
			pigpiodIf.StreamChanged += (s, evt) =>
			{
				if (pigpiodIf.CommandStream != null && pigpiodIf.NotifyStream != null)
				{
					Invoke(new Action(() =>
					{
						panelOperation.Enabled = true;

						checkBoxServo1_CheckedChanged(checkBoxServo1, new EventArgs());
						checkBoxServo2_CheckedChanged(checkBoxServo2, new EventArgs());
					}));

					try
					{
						// CS2, 20MHz, Auxiliary SPI + Mode1
						avalonMM.Stream = new SpiStream(pigpiodIf, 2, 20 * 1000000, 256 + 1);

						avalonMM.WriteUInt32Packet(0x10, 1); // Start sequencer
					}
					catch (PigpiodIfException ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
			};

			plotModel = new PlotModel();
			series = new LineSeries[NUM_CHANNELS];
			for (int ch = 0; ch < NUM_CHANNELS; ch++)
			{
				series[ch] = new LineSeries();
				series[ch].Title = string.Format("CH{0}", ch);
				plotModel.Series.Add(series[ch]);
			}
			double fullScale = 3.3;
			double margin = fullScale * 0.05;
			plotModel.Axes.Add(
				new LinearAxis() { Position = AxisPosition.Left, Minimum = 0 - margin, Maximum = fullScale + margin }
			);

			panelOperation.Enabled = false;
			buttonClose.Enabled = false;
			buttonStop.Enabled = false;
			trackBarHor.Value = horizontal;
			trackBarPos.Value = positionPct;
			foreach (var s in series)
			{
				comboBoxSource.Items.Add(s.Title);
			}
			comboBoxSource.SelectedIndex = source;
			comboBoxPol.SelectedIndex = polarity;
			trackBarLevel.Value = levelPct;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseConnection();
		}

		private void buttonOpen_Click(object sender, System.EventArgs e)
		{
			pigpiodIf.pigpio_start(textBoxAddress.Text, "8888");

			buttonOpen.Enabled = false;
			buttonClose.Enabled = true;
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			CloseConnection();
		}

		private async void buttonStart_Click(object sender, EventArgs e)
		{
			do
			{
				repeatRequested = false;

				buttonStart.Enabled = false;
				buttonStop.Enabled = true;

				bool complete = false;
				PigpiodIf.Callback callback = pigpiodIf.callback(COMPLETE, PigpiodIf.RISING_EDGE, (gpio, level, tick, user) =>
				{
					complete = true;
				});
				try
				{
					cts = new CancellationTokenSource();
					var ct = cts.Token;
					await Task.Run(async () =>
					{
						uint addr = 0x10000;

						int[] divs = new int[] { 1000, 500, 200, 100, 50, 20, 10, 5, 2, 1 };
						int div = divs[horizontal];
						int position = ((NUM_SAMPLES - 1) * positionPct) / 100;
						int level = (4095 * levelPct) / 100;
						avalonMM.WriteUInt32Packet(addr + 0x201 * 4, (uint)div);
						avalonMM.WriteUInt32Packet(addr + 0x202 * 4, (uint)position);
						avalonMM.WriteUInt32Packet(addr + 0x203 * 4, (uint)source);
						avalonMM.WriteUInt32Packet(addr + 0x204 * 4, (uint)polarity);
						avalonMM.WriteUInt32Packet(addr + 0x205 * 4, (uint)level);

						complete = false;
						avalonMM.WriteUInt32Packet(addr + 0x200 * 4, 1);
						DateTime lastAccessTime = DateTime.Now;

						List<DataPoint>[] dataPoints = new List<DataPoint>[NUM_CHANNELS];
						for (int ch = 0; ch < NUM_CHANNELS; ch++)
						{
							dataPoints[ch] = new List<DataPoint>();
						}

						while (!ct.IsCancellationRequested)
						{
							double deltaT = (1.0 / 0.5e6) * div;

							while (!ct.IsCancellationRequested && !complete)
							{
								int leftInMS = (int)(deltaT * 1000 * NUM_SAMPLES + AUTO_TRIGGER_MS) - (int)(DateTime.Now - lastAccessTime).TotalMilliseconds;
								if (leftInMS <= 0)
									break;
								await Task.Delay(Math.Min(leftInMS, 10), ct);
							}

							uint[] res;
							lock (avalonMM.LockObject)
							{
								avalonMM.WriteUInt32Packet(addr + 0x200 * 4, 0);
								res = avalonMM.ReadUInt32Packet(NUM_SAMPLES, addr, true);
								complete = false;
								avalonMM.WriteUInt32Packet(addr + 0x200 * 4, 1);
								lastAccessTime = DateTime.Now;
							}
							if (res.Length != NUM_SAMPLES)
							{
								Console.WriteLine("the response size {0} not equal to the requested size {1}", res.Length, NUM_SAMPLES);
								continue;
							}

							for (int ch = 0; ch < NUM_CHANNELS; ch++)
							{
								dataPoints[ch].Clear();
							}
							for (int i = 0; i < res.Length; i++)
							{
								int d1 = (int)(res[i] & 0xffff);
								int d2 = (int)((res[i] >> 16) & 0xffff);
								double v1 = 3.3 * (d1 / 4096.0);
								double v2 = 3.3 * (d2 / 4096.0);
								dataPoints[0].Add(new DataPoint(deltaT * (i - position), v1));
								dataPoints[1].Add(new DataPoint(deltaT * (i - position) + 1e-6, v2));
							}

							Invoke(new Action(() =>
							{
								for (int ch = 0; ch < NUM_CHANNELS; ch++)
								{
									series[ch].Points.Clear();
									series[ch].Points.AddRange(dataPoints[ch]);
								}
								plotModel.InvalidatePlot(true);
								if (plotView1.Model != plotModel)
								{
									plotView1.Model = plotModel;
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
				finally
				{
					cts = null;

					buttonStart.Enabled = true;
					buttonStop.Enabled = false;
				}
			} while (repeatRequested);
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			cts.Cancel();
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

		private void trackBarHor_Scroll(object sender, EventArgs e)
		{
			if (cts == null)
				return;

			horizontal = trackBarHor.Value;
			repeatRequested = true;
			cts.Cancel();
		}

		private void trackBarPos_Scroll(object sender, EventArgs e)
		{
			if (cts == null)
				return;

			positionPct = trackBarPos.Value;
			repeatRequested = true;
			cts.Cancel();
		}

		private void comboBoxSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cts == null)
				return;

			source = comboBoxSource.SelectedIndex;
			repeatRequested = true;
			cts.Cancel();
		}

		private void comboBoxPol_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cts == null)
				return;

			polarity = comboBoxPol.SelectedIndex;
			repeatRequested = true;
			cts.Cancel();
		}

		private void trackBarLevel_Scroll(object sender, EventArgs e)
		{
			if (cts == null)
				return;

			levelPct = trackBarLevel.Value;
			repeatRequested = true;
			cts.Cancel();
		}
	}
}
