using Rapidnack.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigpiodIfTest
{
	public partial class MainForm : Form
	{
		private PigpiodIf pigpiodIf = new PigpiodIf();
		private CancellationTokenSource cts;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			buttonClose.Enabled = false;
			buttonOff.Enabled = false;
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
			buttonOff.Enabled = true;

			var callback = pigpiodIf.callback(25, PigpiodIf.EITHER_EDGE, (gpio, level, tick, user) =>
			{
				Console.WriteLine("callback: {0}, {1}, {2}, {3}", gpio, level, tick, user);
				Invoke(new Action(() =>
				{
					textBoxAddress.Enabled = (level == PigpiodIf.PI_LOW);
				}));
			});
			try
			{
				cts = new CancellationTokenSource();
				await Task.Run(async () =>
				{
					while (cts.Token.IsCancellationRequested == false)
					{
						pigpiodIf.gpio_write(25, PigpiodIf.PI_HIGH);
						await Task.Delay(500);
						pigpiodIf.gpio_write(25, PigpiodIf.PI_LOW);
						await Task.Delay(500);
					}
				});
			}
			finally
			{
				pigpiodIf.callback_cancel(callback);
				textBoxAddress.Enabled = true;
			}

			buttonOn.Enabled = true;
			buttonOff.Enabled = false;
		}

		private void buttonOff_Click(object sender, EventArgs e)
		{
			cts.Cancel();
		}

		private async void buttonTest_Click(object sender, EventArgs e)
		{
			buttonTest.Enabled = false;

			await Task.Run(() =>
			{
				var xPiGpiodIf = new XPigpiodIf();
				xPiGpiodIf.t0(pigpiodIf);
				xPiGpiodIf.t1(pigpiodIf);
				xPiGpiodIf.t2(pigpiodIf);
				xPiGpiodIf.t3(pigpiodIf);
				//xPiGpiodIf.t4(pigpiodIf);
				xPiGpiodIf.t5(pigpiodIf);
				xPiGpiodIf.t6(pigpiodIf);
				xPiGpiodIf.t7(pigpiodIf);
				xPiGpiodIf.t8(pigpiodIf);
				xPiGpiodIf.t9(pigpiodIf);
				xPiGpiodIf.ta(pigpiodIf);
				xPiGpiodIf.tb(pigpiodIf);
				xPiGpiodIf.tc(pigpiodIf);
			});

			buttonTest.Enabled = true;
		}
	}
}
