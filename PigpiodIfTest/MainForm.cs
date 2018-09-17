using Rapidnack.Common;
using Rapidnack.Net;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigpiodIfTest
{
	public partial class MainForm : Form
	{
		private const int GPIO = 5;

		private LogWriter logWriter;
		private PigpiodIf pigpiodIf = new PigpiodIf();
		private CancellationTokenSource cts;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			logWriter = new LogWriter();
			System.Console.SetOut(logWriter);
			System.Console.SetError(logWriter);
			logWriter.TextChanged += (s, evt) =>
			{
				Invoke(new Action(() =>
				{
					textBoxLog.Text = logWriter.Text;
					textBoxLog.SelectionStart = textBoxLog.Text.Length;
					textBoxLog.ScrollToCaret();
				}));
			};

			buttonClose.Enabled = false;
			buttonOff.Enabled = false;
			buttonCancel.Enabled = false;
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

		private async void buttonOn_Click(object sender, EventArgs e)
		{
			buttonOn.Enabled = false;
			buttonTest.Enabled = false;
			buttonOff.Enabled = true;
			PigpiodIf.Callback callback = null;
			try
			{
				callback = pigpiodIf.callback(GPIO, PigpiodIf.EITHER_EDGE, (gpio, level, tick, user) =>
				{
					Console.WriteLine("callback: {0}, {1}, {2}, {3}", gpio, level, tick, user);
					Invoke(new Action(() =>
					{
						bool isLow = (level == PigpiodIf.PI_LOW);
						textBoxAddress.Enabled = isLow;
						textBoxAddress.BackColor = isLow ? Color.Lime : Color.Aqua;
					}));
				});

				cts = new CancellationTokenSource();
				var ct = cts.Token;
				await Task.Run(async () =>
				{
					while (ct.IsCancellationRequested == false)
					{
						pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_HIGH);
						await Task.Delay(500);
						pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_LOW);
						await Task.Delay(500);
					}
				});
			}
			finally
			{
				pigpiodIf.callback_cancel(callback);
				textBoxAddress.Enabled = true;
				textBoxAddress.BackColor = SystemColors.Window;

				buttonOn.Enabled = true;
				buttonTest.Enabled = true;
				buttonOff.Enabled = false;
			}
		}

		private void buttonOff_Click(object sender, EventArgs e)
		{
			cts.Cancel();
		}

		private async void buttonTest_Click(object sender, EventArgs e)
		{
			buttonOn.Enabled = false;
			buttonTest.Enabled = false;
			buttonCancel.Enabled = true;
			try
			{
				cts = new CancellationTokenSource();
				var ct = cts.Token;
				await Task.Run(() =>
				{
					var xPiGpiodIf = new XPigpiodIf();
					xPiGpiodIf.t0(pigpiodIf, ct);
					xPiGpiodIf.t1(pigpiodIf, ct);
					xPiGpiodIf.t2(pigpiodIf, ct);
					xPiGpiodIf.t3(pigpiodIf, ct);
					//xPiGpiodIf.t4(pigpiodIf, ct);
					xPiGpiodIf.t5(pigpiodIf, ct);
					xPiGpiodIf.t6(pigpiodIf, ct);
					xPiGpiodIf.t7(pigpiodIf, ct);
					xPiGpiodIf.t8(pigpiodIf, ct);
					xPiGpiodIf.t9(pigpiodIf, ct);
					//xPiGpiodIf.ta(pigpiodIf, ct);
					//xPiGpiodIf.tb(pigpiodIf, ct);
					//xPiGpiodIf.tc(pigpiodIf, ct);
				});
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.Message);
			}
			finally
			{
				buttonOn.Enabled = true;
				buttonTest.Enabled = true;
				buttonCancel.Enabled = false;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			cts.Cancel();
		}
	}
}
