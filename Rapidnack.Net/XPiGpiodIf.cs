using System;
using System.Linq;
using System.Threading;

namespace Rapidnack.Net
{
	public class XPigpiodIf
	{
		#region # private const

		private const int GPIO = 5;

		#endregion


		#region # public method

		public void t0(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			Console.WriteLine("\r\nTesting pigpiod C# I/F");

			Console.WriteLine("pigpio version {0}.", pigpiodIf.get_pigpio_version());

			Console.WriteLine("Hardware revision {0}.", pigpiodIf.get_hardware_revision());
		}

		public void t1(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			int v;

			Console.WriteLine("\r\nMode/PUD/read/write tests.");

			pigpiodIf.set_mode(GPIO, PigpiodIf.PI_INPUT);
			v = pigpiodIf.get_mode(GPIO);
			CHECK(1, 1, v, 0, 0, "set mode, get mode", ct);

			pigpiodIf.set_pull_up_down(GPIO, PigpiodIf.PI_PUD_UP);
			v = pigpiodIf.gpio_read(GPIO);
			CHECK(1, 2, v, 1, 0, "set pull up down, read", ct);

			pigpiodIf.set_pull_up_down(GPIO, PigpiodIf.PI_PUD_DOWN);
			v = pigpiodIf.gpio_read(GPIO);
			CHECK(1, 3, v, 0, 0, "set pull up down, read", ct);

			pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_LOW);
			v = pigpiodIf.get_mode(GPIO);
			CHECK(1, 4, v, 1, 0, "write, get mode", ct);

			v = pigpiodIf.gpio_read(GPIO);
			CHECK(1, 5, v, 0, 0, "read", ct);

			pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_HIGH);
			v = pigpiodIf.gpio_read(GPIO);
			CHECK(1, 6, v, 1, 0, "write, read", ct);
		}

		public void t2(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			PigpiodIf.Callback callback = null;
			try
			{
				int dc, f, r, rr, oc;

				Console.WriteLine("\r\nPWM dutycycle/range/frequency tests.");

				pigpiodIf.set_PWM_range(GPIO, 255);
				pigpiodIf.set_PWM_frequency(GPIO, 0);
				f = pigpiodIf.get_PWM_frequency(GPIO);
				CHECK(2, 1, f, 10, 0, "set PWM range, set/get PWM frequency", ct);

				int t2_count = 0;
				callback = pigpiodIf.callback(GPIO, PigpiodIf.EITHER_EDGE, (gpio, level, tick, user) =>
				{
					t2_count++;
				});

				pigpiodIf.set_PWM_dutycycle(GPIO, 0);
				dc = pigpiodIf.get_PWM_dutycycle(GPIO);
				CHECK(2, 2, dc, 0, 0, "get PWM dutycycle", ct);

				pigpiodIf.time_sleep(0.5); /* allow old notifications to flush */
				oc = t2_count;
				pigpiodIf.time_sleep(2);
				f = t2_count - oc;
				CHECK(2, 3, f, 0, 0, "set PWM dutycycle, callback", ct);

				pigpiodIf.set_PWM_dutycycle(GPIO, 128);
				dc = pigpiodIf.get_PWM_dutycycle(GPIO);
				CHECK(2, 4, dc, 128, 0, "get PWM dutycycle", ct);

				pigpiodIf.time_sleep(0.2);
				oc = t2_count;
				pigpiodIf.time_sleep(2);
				f = t2_count - oc;
				CHECK(2, 5, f, 40, 5, "set PWM dutycycle, callback", ct);

				pigpiodIf.set_PWM_frequency(GPIO, 100);
				f = pigpiodIf.get_PWM_frequency(GPIO);
				CHECK(2, 6, f, 100, 0, "set/get PWM frequency", ct);

				pigpiodIf.time_sleep(0.2);
				oc = t2_count;
				pigpiodIf.time_sleep(2);
				f = t2_count - oc;
				CHECK(2, 7, f, 400, 1, "callback", ct);

				pigpiodIf.set_PWM_frequency(GPIO, 1000);
				f = pigpiodIf.get_PWM_frequency(GPIO);
				CHECK(2, 8, f, 1000, 0, "set/get PWM frequency", ct);

				pigpiodIf.time_sleep(0.2);
				oc = t2_count;
				pigpiodIf.time_sleep(2);
				f = t2_count - oc;
				CHECK(2, 9, f, 4000, 1, "callback", ct);

				r = pigpiodIf.get_PWM_range(GPIO);
				CHECK(2, 10, r, 255, 0, "get PWM range", ct);

				rr = pigpiodIf.get_PWM_real_range(GPIO);
				CHECK(2, 11, rr, 200, 0, "get PWM real range", ct);

				pigpiodIf.set_PWM_range(GPIO, 2000);
				r = pigpiodIf.get_PWM_range(GPIO);
				CHECK(2, 12, r, 2000, 0, "set/get PWM range", ct);

				rr = pigpiodIf.get_PWM_real_range(GPIO);
				CHECK(2, 13, rr, 200, 0, "get PWM real range", ct);
			}
			finally
			{
				pigpiodIf.set_PWM_dutycycle(GPIO, 0);

				pigpiodIf.callback_cancel(callback);
			}
		}

		public void t3(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			PigpiodIf.Callback callback = null;
			try
			{
				int[] pw = new int[] { 500, 1500, 2500 };
				int[] dc = new int[] { 20, 40, 60, 80 };

				int f, rr, v;
				float on, off;

				int t;

				Console.WriteLine("\r\nPWM/Servo pulse accuracy tests.");

				int t3_reset = 1;
				int t3_count = 0;
				UInt32 t3_tick = 0;
				float t3_on = 0.0f;
				float t3_off = 0.0f;
				callback = pigpiodIf.callback(GPIO, PigpiodIf.EITHER_EDGE, (gpio, level, tick, user) =>
				{
					UInt32 td;

					//Console.WriteLine("pi={0} g{1} l={2} t={3}", pi, gpio, level, tick);
					if (t3_reset != 0)
					{
						t3_count = 0;
						t3_on = 0.0f;
						t3_off = 0.0f;
						t3_reset = 0;
					}
					else
					{
						td = tick - t3_tick;

						if (level == 0) t3_on += td;
						else t3_off += td;
					}

					t3_count++;
					t3_tick = tick;
				});

				for (t = 0; t < 3; t++)
				{
					pigpiodIf.set_servo_pulsewidth(GPIO, (UInt32)pw[t]);
					v = pigpiodIf.get_servo_pulsewidth(GPIO);
					CHECK(3, t + t + 1, v, pw[t], 0, "get servo pulsewidth", ct);

					pigpiodIf.time_sleep(1);
					t3_reset = 1;
					pigpiodIf.time_sleep(4);
					on = t3_on;
					off = t3_off;
					CHECK(3, t + t + 2, (int)((1000.0 * (on + off)) / on), (int)(20000000.0 / pw[t]), 1,
					   "set servo pulsewidth", ct);
				}

				pigpiodIf.set_servo_pulsewidth(GPIO, 0);
				pigpiodIf.set_PWM_frequency(GPIO, 1000);
				f = pigpiodIf.get_PWM_frequency(GPIO);
				CHECK(3, 7, f, 1000, 0, "set/get PWM frequency", ct);

				rr = pigpiodIf.set_PWM_range(GPIO, 100);
				CHECK(3, 8, rr, 200, 0, "set PWM range", ct);

				for (t = 0; t < 4; t++)
				{
					pigpiodIf.set_PWM_dutycycle(GPIO, (UInt32)dc[t]);
					v = pigpiodIf.get_PWM_dutycycle(GPIO);
					CHECK(3, t + t + 9, v, dc[t], 0, "get PWM dutycycle", ct);

					pigpiodIf.time_sleep(1);
					t3_reset = 1;
					pigpiodIf.time_sleep(2);
					on = t3_on;
					off = t3_off;
					CHECK(3, t + t + 10, (int)((1000.0 * on) / (on + off)), (int)(10.0 * dc[t]), 1,
					   "set PWM dutycycle", ct);
				}
			}
			finally
			{
				pigpiodIf.set_PWM_dutycycle(GPIO, 0);

				pigpiodIf.callback_cancel(callback);
			}
		}

		public void t5(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			PigpiodIf.Callback callback = null;
			try
			{
				int BAUD = 4800;

				string TEXT = @"
Now is the winter of our discontent
Made glorious summer by this sun of York;
And all the clouds that lour'd upon our house
In the deep bosom of the ocean buried.
Now are our brows bound with victorious wreaths;
Our bruised arms hung up for monuments;
Our stern alarums changed to merry meetings,
Our dreadful marches to delightful measures.
Grim - visaged war hath smooth'd his wrinkled front;
And now, instead of mounting barded steeds
To fright the souls of fearful adversaries,
He capers nimbly in a lady's chamber
To the lascivious pleasing of a lute.
";

				TEXT = TEXT.Replace("\r\n", "\n");

				PigpiodIf.GpioPulse[] wf = new PigpiodIf.GpioPulse[]
				{
				new PigpiodIf.GpioPulse() { gpioOn = 1 << GPIO, gpioOff = 0, usDelay = 10000 },
				new PigpiodIf.GpioPulse() { gpioOn = 0, gpioOff = 1 << GPIO, usDelay = 30000 },
				new PigpiodIf.GpioPulse() { gpioOn = 1 << GPIO, gpioOff = 0,  usDelay = 60000 },
				new PigpiodIf.GpioPulse() { gpioOn = 0, gpioOff = 1 << GPIO, usDelay = 100000 },
				};

				int e, oc, c, wid;

				byte[] recv = new byte[2048];

				Console.WriteLine("\r\nWaveforms & serial read/write tests.");

				int t5_count = 0;
				callback = pigpiodIf.callback(GPIO, PigpiodIf.FALLING_EDGE, (gpio, level, tick, user) =>
				{
					t5_count++;
				});

				pigpiodIf.set_mode(GPIO, PigpiodIf.PI_OUTPUT);

				e = pigpiodIf.wave_clear();
				CHECK(5, 1, e, 0, 0, "callback, set mode, wave clear", ct);

				e = pigpiodIf.wave_add_generic(wf);
				CHECK(5, 2, e, 4, 0, "pulse, wave add generic", ct);

				wid = pigpiodIf.wave_create();
				e = pigpiodIf.wave_send_repeat((UInt32)wid);
				if (e < 14) CHECK(5, 3, e, 9, 0, "wave tx repeat", ct);
				else CHECK(5, 3, e, 19, 0, "wave tx repeat", ct);

				oc = t5_count;
				pigpiodIf.time_sleep(5.05);
				c = t5_count - oc;
				CHECK(5, 4, c, 50, 2, "callback", ct);

				e = pigpiodIf.wave_tx_stop();
				CHECK(5, 5, e, 0, 0, "wave tx stop", ct);

				e = pigpiodIf.bb_serial_read_open(GPIO, (UInt32)BAUD, 8);
				CHECK(5, 6, e, 0, 0, "serial read open", ct);

				pigpiodIf.wave_clear();
				var bytes = System.Text.Encoding.UTF8.GetBytes(TEXT);
				e = pigpiodIf.wave_add_serial(GPIO, (UInt32)BAUD, 8, 2, 5000000, bytes);
				CHECK(5, 7, e, 3405, 0, "wave clear, wave add serial", ct);

				wid = pigpiodIf.wave_create();
				e = pigpiodIf.wave_send_once((UInt32)wid);
				if (e < 6964) CHECK(5, 8, e, 6811, 0, "wave tx start", ct);
				else CHECK(5, 8, e, 7116, 0, "wave tx start", ct);

				oc = t5_count;
				pigpiodIf.time_sleep(3);
				c = t5_count - oc;
				CHECK(5, 9, c, 0, 0, "callback", ct);

				oc = t5_count;
				while (pigpiodIf.wave_tx_busy() != 0) pigpiodIf.time_sleep(0.1);
				pigpiodIf.time_sleep(0.1);
				c = t5_count - oc;
				CHECK(5, 10, c, 1702, 0, "wave tx busy, callback", ct);

				c = pigpiodIf.bb_serial_read(GPIO, recv);
				recv = recv.Take(c).ToArray();
				string text = System.Text.Encoding.UTF8.GetString(recv);
				CHECK(5, 11, string.Compare(TEXT, text), 0, 0, "wave tx busy, serial read", ct);

				e = pigpiodIf.bb_serial_read_close(GPIO);
				CHECK(5, 12, e, 0, 0, "serial read close", ct);

				c = pigpiodIf.wave_get_micros();
				CHECK(5, 13, c, 6158148, 0, "wave get micros", ct);

				c = pigpiodIf.wave_get_high_micros();
				if (c > 6158148) c = 6158148;
				CHECK(5, 14, c, 6158148, 0, "wave get high micros", ct);

				c = pigpiodIf.wave_get_max_micros();
				CHECK(5, 15, c, 1800000000, 0, "wave get max micros", ct);

				c = pigpiodIf.wave_get_pulses();
				CHECK(5, 16, c, 3405, 0, "wave get pulses", ct);

				c = pigpiodIf.wave_get_high_pulses();
				CHECK(5, 17, c, 3405, 0, "wave get high pulses", ct);

				c = pigpiodIf.wave_get_max_pulses();
				CHECK(5, 18, c, 12000, 0, "wave get max pulses", ct);

				c = pigpiodIf.wave_get_cbs();
				if (c < 6963) CHECK(5, 19, c, 6810, 0, "wave get cbs", ct);
				else CHECK(5, 19, c, 7115, 0, "wave get cbs", ct);

				c = pigpiodIf.wave_get_high_cbs();
				if (c < 6963) CHECK(5, 20, c, 6810, 0, "wave get high cbs", ct);
				else CHECK(5, 20, c, 7115, 0, "wave get high cbs", ct);

				c = pigpiodIf.wave_get_max_cbs();
				CHECK(5, 21, c, 25016, 0, "wave get max cbs", ct);
			}
			finally
			{
				pigpiodIf.callback_cancel(callback);
			}
		}

		public void t6(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			PigpiodIf.Callback callback = null;
			try
			{
				int tp, t, p;

				Console.WriteLine("\r\nTrigger tests.");

				pigpiodIf.gpio_write(GPIO, PigpiodIf.PI_LOW);

				tp = 0;

				int t6_count = 0;
				int t6_on = 0;
				UInt32 t6_on_tick = 0;
				callback = pigpiodIf.callback(GPIO, PigpiodIf.EITHER_EDGE, (gpio, level, tick, user) =>
				{
					if (level == 1)
					{
						t6_on_tick = tick;
						t6_count++;
					}
					else
					{
						if (t6_on_tick != 0) t6_on += (int)(tick - t6_on_tick);
					}
				});

				pigpiodIf.time_sleep(0.2);

				for (t = 0; t < 5; t++)
				{
					pigpiodIf.time_sleep(0.1);
					p = 10 + (t * 10);
					tp += p;
					pigpiodIf.gpio_trigger(GPIO, (UInt32)p, 1);
				}

				pigpiodIf.time_sleep(0.5);

				CHECK(6, 1, t6_count, 5, 0, "gpio trigger count", ct);

				CHECK(6, 2, t6_on, tp, 25, "gpio trigger pulse length", ct);
			}
			finally
			{
				pigpiodIf.callback_cancel(callback);
			}
		}

		public void t7(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			PigpiodIf.Callback callback = null;
			try
			{
				int c, oc;

				Console.WriteLine("\r\nWatchdog tests.");

				int t7_count = 0;
				/* type of edge shouldn't matter for watchdogs */
				callback = pigpiodIf.callback(GPIO, PigpiodIf.FALLING_EDGE, (gpio, level, tick, user) =>
				{
					if (level == PigpiodIf.PI_TIMEOUT) t7_count++;
				});

				pigpiodIf.set_watchdog(GPIO, 50); /* 50 ms, 20 per second */
				pigpiodIf.time_sleep(0.5);
				oc = t7_count;
				pigpiodIf.time_sleep(2);
				c = t7_count - oc;
				CHECK(7, 1, c, 39, 5, "set watchdog on count", ct);

				pigpiodIf.set_watchdog(GPIO, 0); /* 0 switches watchdog off */
				pigpiodIf.time_sleep(0.5);
				oc = t7_count;
				pigpiodIf.time_sleep(2);
				c = t7_count - oc;
				CHECK(7, 2, c, 0, 1, "set watchdog off count", ct);
			}
			finally
			{
				pigpiodIf.callback_cancel(callback);
			}
		}

		public void t8(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			int v;

			Console.WriteLine("\r\nBank read/write tests.");

			pigpiodIf.gpio_write(GPIO, 0);
			v = (int)pigpiodIf.read_bank_1() & (1 << GPIO);
			CHECK(8, 1, v, 0, 0, "read bank 1", ct);

			pigpiodIf.gpio_write(GPIO, 1);
			v = (int)pigpiodIf.read_bank_1() & (1 << GPIO);
			CHECK(8, 2, v, (1 << GPIO), 0, "read bank 1", ct);

			pigpiodIf.clear_bank_1(1 << GPIO);
			v = pigpiodIf.gpio_read(GPIO);
			CHECK(8, 3, v, 0, 0, "clear bank 1", ct);

			pigpiodIf.set_bank_1(1 << GPIO);
			v = pigpiodIf.gpio_read(GPIO);
			CHECK(8, 4, v, 1, 0, "set bank 1", ct);

			v = (int)pigpiodIf.read_bank_2();

			if (v != 0) v = 0; else v = 1;

			CHECK(8, 5, v, 0, 0, "read bank 2", ct);

			v = pigpiodIf.clear_bank_2(0);
			CHECK(8, 6, v, 0, 0, "clear bank 2", ct);

			v = pigpiodIf.clear_bank_2(0xffffff);
			CHECK(8, 7, v, -42, 0, "clear bank 2", ct);

			v = pigpiodIf.set_bank_2(0);
			CHECK(8, 8, v, 0, 0, "set bank 2", ct);

			v = pigpiodIf.set_bank_2(0xffffff);
			CHECK(8, 9, v,-42, 0, "set bank 2", ct);
		}

		public void t9(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			PigpiodIf.Callback callback = null;
			try
			{
				int s, oc, c, e;
				UInt32[] p = new UInt32[10];

				Console.WriteLine("\r\nScript store/run/status/stop/delete tests.");

				pigpiodIf.gpio_write(GPIO, 0); /* need known state */

				/*
				100 loops per second
				p0 number of loops
				p1 GPIO
				*/
				string script = @"

   ld p9 p0
   tag 0
   w p1 1
   mils 5
   w p1 0
   mils 5
   dcr p9
   jp 0";

				int t9_count = 0;
				callback = pigpiodIf.callback(GPIO, PigpiodIf.RISING_EDGE, (gpio, level, tick, user) =>
				{
					t9_count++;
				});

				var bytes = System.Text.Encoding.UTF8.GetBytes(script);
				s = pigpiodIf.store_script(bytes);

				/* Wait for script to finish initing. */
				while (true)
				{
					pigpiodIf.time_sleep(0.1);
					e = pigpiodIf.script_status((UInt32)s, p);
					if (e != PigpiodIf.PI_SCRIPT_INITING) break;
				}

				oc = t9_count;
				pigpiodIf.run_script((UInt32)s, new UInt32[] { 99, GPIO });
				pigpiodIf.time_sleep(2);
				c = t9_count - oc;
				CHECK(9, 1, c, 100, 0, "store/run script", ct);

				oc = t9_count;
				pigpiodIf.run_script((UInt32)s, new UInt32[] { 200, GPIO });
				while (true)
				{
					pigpiodIf.time_sleep(0.1);
					e = pigpiodIf.script_status((UInt32)s, p);
					if (e != PigpiodIf.PI_SCRIPT_RUNNING) break;
				}
				c = t9_count - oc;
				pigpiodIf.time_sleep(0.1);
				CHECK(9, 2, c, 201, 0, "run script/script status", ct);

				oc = t9_count;
				pigpiodIf.run_script((UInt32)s, new UInt32[] { 2000, GPIO });
				while (true)
				{
					pigpiodIf.time_sleep(0.1);
					e = pigpiodIf.script_status((UInt32)s, p);
					if (e != PigpiodIf.PI_SCRIPT_RUNNING) break;
					if (p[9] < 1600) pigpiodIf.stop_script((UInt32)s);
				}
				c = t9_count - oc;
				pigpiodIf.time_sleep(0.1);
				CHECK(9, 3, c, 410, 10, "run/stop script/script status", ct);

				e = pigpiodIf.delete_script((UInt32)s);
				CHECK(9, 4, e, 0, 0, "delete script", ct);
			}
			finally
			{
				pigpiodIf.callback_cancel(callback);
			}
		}

		public void ta(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			int h, b, e;
			string TEXT;
			byte[] recv = new byte[2048];

			Console.WriteLine("\r\nSerial link tests.");

			/* this test needs RXD and TXD to be connected */

			h = pigpiodIf.serial_open("/dev/ttyAMA0", 57600, 0);

			CHECK(10, 1, h, 0, 0, "serial open", ct);

			pigpiodIf.time_sleep(0.1); /* allow time for transmission */

			b = pigpiodIf.serial_read((UInt32)h, recv); /* flush buffer */

			b = pigpiodIf.serial_data_available((UInt32)h);
			CHECK(10, 2, b, 0, 0, "serial data available", ct);

			TEXT = @"

		To be, or not to be, that is the question -
Whether 'tis Nobler in the mind to suffer

		The Slings and Arrows of outrageous Fortune,
Or to take Arms against a Sea of troubles,
";
			var bytes = System.Text.Encoding.UTF8.GetBytes(TEXT);
			e = pigpiodIf.serial_write((UInt32)h, bytes);
			CHECK(10, 3, e, 0, 0, "serial write", ct);

			e = pigpiodIf.serial_write_byte((UInt32)h, 0xAA);
			e = pigpiodIf.serial_write_byte((UInt32)h, 0x55);
			e = pigpiodIf.serial_write_byte((UInt32)h, 0x00);
			e = pigpiodIf.serial_write_byte((UInt32)h, 0xFF);

			CHECK(10, 4, e, 0, 0, "serial write byte", ct);

			pigpiodIf.time_sleep(0.1); /* allow time for transmission */

			b = pigpiodIf.serial_data_available((UInt32)h);
			CHECK(10, 5, b, bytes.Length + 4, 0, "serial data available", ct);

			recv = new byte[bytes.Length];
			b = pigpiodIf.serial_read((UInt32)h, recv);
			CHECK(10, 6, b, bytes.Length, 0, "serial read", ct);
			recv = recv.Take(b).ToArray();
			string text = System.Text.Encoding.UTF8.GetString(recv);
			CHECK(10, 7, string.Compare(TEXT, text), 0, 0, "serial read", ct);

			b = pigpiodIf.serial_read_byte((UInt32)h);
			CHECK(10, 8, b, 0xAA, 0, "serial read byte", ct);

			b = pigpiodIf.serial_read_byte((UInt32)h);
			CHECK(10, 9, b, 0x55, 0, "serial read byte", ct);

			b = pigpiodIf.serial_read_byte((UInt32)h);
			CHECK(10, 10, b, 0x00, 0, "serial read byte", ct);

			b = pigpiodIf.serial_read_byte((UInt32)h);
			CHECK(10, 11, b, 0xFF, 0, "serial read byte", ct);

			b = pigpiodIf.serial_data_available((UInt32)h);
			CHECK(10, 12, b, 0, 0, "serial data availabe", ct);

			e = pigpiodIf.serial_close((UInt32)h);
			CHECK(10, 13, e, 0, 0, "serial close", ct);
		}

		public void tb(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			int h, e, b;
			string exp;
			string buf;
			byte[] bytes = new byte[128];

			Console.WriteLine("\r\nSMBus / I2C tests.");

			/* this test requires an ADXL345 on I2C bus 1 addr 0x53 */

			h = pigpiodIf.i2c_open(1, 0x53, 0);
			CHECK(11, 1, h, 0, 0, "i2c open", ct);

			e = pigpiodIf.i2c_write_device((UInt32)h, new byte[] { 0x00 }); /* move to known register */
			CHECK(11, 2, e, 0, 0, "i2c write device", ct);

			bytes = new byte[1];
			b = pigpiodIf.i2c_read_device((UInt32)h, bytes);
			CHECK(11, 3, b, 1, 0, "i2c read device", ct);
			CHECK(11, 4, bytes[0], 0xE5, 0, "i2c read device", ct);

			b = pigpiodIf.i2c_read_byte((UInt32)h);
			CHECK(11, 5, b, 0xE5, 0, "i2c read byte", ct);

			b = pigpiodIf.i2c_read_byte_data((UInt32)h, 0);
			CHECK(11, 6, b, 0xE5, 0, "i2c read byte data", ct);

			b = pigpiodIf.i2c_read_byte_data((UInt32)h, 48);
			CHECK(11, 7, b, 2, 0, "i2c read byte data", ct);

			exp = "\x1D[aBcDeFgHjKM]";
			bytes = System.Text.Encoding.UTF8.GetBytes(exp);

			e = pigpiodIf.i2c_write_device((UInt32)h, bytes);
			CHECK(11, 8, e, 0, 0, "i2c write device", ct);

			e = pigpiodIf.i2c_write_device((UInt32)h, new byte[] { 0x1D });
			bytes = new byte[bytes.Length - 1];
			b = pigpiodIf.i2c_read_device((UInt32)h, bytes);
			buf = System.Text.Encoding.UTF8.GetString(bytes);
			CHECK(11, 9, b, buf.Length, 0, "i2c read device", ct);
			CHECK(11, 10, string.Compare(buf, 0, exp, 1, buf.Length - 1), 0, 0, "i2c read device", ct);

			if (string.Compare(buf, 0, exp, 1, buf.Length - 1) != 0)
				Console.WriteLine("got [{0}] expected [{1}]", buf, exp.Substring(1));

			e = pigpiodIf.i2c_write_byte_data((UInt32)h, 0x1d, 0xAA);
			CHECK(11, 11, e, 0, 0, "i2c write byte data", ct);

			b = pigpiodIf.i2c_read_byte_data((UInt32)h, 0x1d);
			CHECK(11, 12, b, 0xAA, 0, "i2c read byte data", ct);

			e = pigpiodIf.i2c_write_byte_data((UInt32)h, 0x1d, 0x55);
			CHECK(11, 13, e, 0, 0, "i2c write byte data", ct);

			b = pigpiodIf.i2c_read_byte_data((UInt32)h, 0x1d);
			CHECK(11, 14, b, 0x55, 0, "i2c read byte data", ct);

			exp = "[1234567890#]";
			bytes = System.Text.Encoding.UTF8.GetBytes(exp);

			e = pigpiodIf.i2c_write_block_data((UInt32)h, 0x1C, bytes);
			CHECK(11, 15, e, 0, 0, "i2c write block data", ct);

			e = pigpiodIf.i2c_write_device((UInt32)h, new byte[] { 0x1D });
			b = pigpiodIf.i2c_read_device((UInt32)h, bytes);
			buf = System.Text.Encoding.UTF8.GetString(bytes);
			CHECK(11, 16, b, buf.Length, 0, "i2c read device", ct);
			CHECK(11, 17, string.Compare(buf, 0, exp, 0, buf.Length), 0, 0, "i2c read device", ct);

			if (string.Compare(buf, 0, exp, 0, buf.Length) != 0)
				Console.WriteLine("got [{0}] expected [{1}]", buf, exp);

			b = pigpiodIf.i2c_read_i2c_block_data((UInt32)h, 0x1D, bytes);
			buf = System.Text.Encoding.UTF8.GetString(bytes);
			CHECK(11, 18, b, buf.Length, 0, "i2c read i2c block data", ct);
			CHECK(11, 19, string.Compare(buf, 0, exp, 0, buf.Length), 0, 0, "i2c read i2c block data", ct);

			if (string.Compare(buf, 0, exp, 0, buf.Length) != 0)
				Console.WriteLine("got [{0}] expected [{1}]", buf, exp);

			exp = "(-+=;:,<>!%)";
			bytes = System.Text.Encoding.UTF8.GetBytes(exp);

			e = pigpiodIf.i2c_write_i2c_block_data((UInt32)h, 0x1D, bytes);
			CHECK(11, 20, e, 0, 0, "i2c write i2c block data", ct);

			b = pigpiodIf.i2c_read_i2c_block_data((UInt32)h, 0x1D, bytes);
			buf = System.Text.Encoding.UTF8.GetString(bytes);
			CHECK(11, 21, b, buf.Length, 0, "i2c read i2c block data", ct);
			CHECK(11, 22, string.Compare(buf, 0, exp, 0, buf.Length), 0, 0, "i2c read i2c block data", ct);

			if (string.Compare(buf, 0, exp, 0, buf.Length) != 0)
				Console.WriteLine("got [{0}] expected [{1}]", buf, exp);

			e = pigpiodIf.i2c_close((UInt32)h);
			CHECK(11, 23, e, 0, 0, "i2c close", ct);
		}

		public void tc(PigpiodIf pigpiodIf, CancellationToken ct)
		{
			int h, x, b, e;
			byte[] buf = new byte[128];

			Console.WriteLine("\r\nSPI tests.");

			/* this test requires a MCP3202 on SPI channel 1 */

			h = pigpiodIf.spi_open(1, 50000, 0);
			CHECK(12, 1, h, 0, 0, "spi open", ct);


			for (x = 0; x < 5; x++)
			{
				buf = new byte[] { 0x01, 0x80, 0x00 };
				b = pigpiodIf.spi_xfer((UInt32)h, buf, buf);
				CHECK(12, 2, b, 3, 0, "spi xfer", ct);
				if (b == 3)
				{
					pigpiodIf.time_sleep(1.0);
					Console.WriteLine("{0} ", ((buf[1] & 0x0F) * 256) | buf[2]);
				}
			}

			e = pigpiodIf.spi_close((UInt32)h);
			CHECK(12, 99, e, 0, 0, "spi close", ct);
		}

		#endregion


		#region # private method

		private void CHECK(int t, int st, int got, int expect, int pc, string desc, CancellationToken ct)
		{
			if ((got >= (((1E2 - pc) * expect) / 1E2)) && (got <= (((1E2 + pc) * expect) / 1E2)))
			{
				Console.WriteLine("TEST {0}.{1} PASS ({2}: {3})", t, st, desc, expect);
			}
			else
			{
				Console.WriteLine("TEST {0}.{1} FAILED got {2} ({3}: {4})", t, st, got, desc, expect);
			}

			if (ct.IsCancellationRequested)
			{
				throw new Exception("Canceled by user.");
			}
		}

		#endregion
	}
}
