using System;
using System.IO;

namespace Rapidnack.Net
{
	public class SpiStream : Stream
	{
		private const int CS = 16;

		private PigpiodIf pigpiodIf;
		private int handle = -1;
		private byte[] rxBuf = new byte[0];


		public SpiStream(PigpiodIf pigpiodIf, UInt32 channel, UInt32 speed, UInt32 flags)
		{
			this.pigpiodIf = pigpiodIf;

			for (int i = 0; i < 3; i++)
			{
				handle = pigpiodIf.spi_open(channel, speed, flags);
				//Console.WriteLine("spi_open: {0}", handle);
				if (handle >= 0)
					break;
				System.Threading.Thread.Sleep(100);
			}
			if (handle < 0)
			{
				throw new PigpiodIfException(handle, "PigpiodIf: " + pigpiodIf.pigpio_error(handle));
			}
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (handle >= 0)
				{
					pigpiodIf.spi_close((UInt32)handle);
					//Console.WriteLine("spi_close: {0}", handle);
					handle = -1;
				}
			}
			base.Dispose(disposing);
		}


		#region # Implements of Stream

		public override bool CanRead
		{
			get
			{
				return rxBuf.Length > 0;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return this.pigpiodIf.CanWrite && handle >= 0;
			}
		}

		public override long Length
		{
			get
			{
				return rxBuf.Length;
			}
		}

		public override long Position
		{
			get
			{
				return 0;
			}

			set
			{
				// nothing to do
			}
		}

		public override void Flush()
		{
			// nothing to do
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int n = 0;
			for (int i = 0; i < count && i < rxBuf.Length; i++)
			{
				buffer[offset + i] = rxBuf[i];
				n++;
			}
			byte[] newBuf = new byte[rxBuf.Length - n];
			for (int i = n; i < rxBuf.Length; i++)
			{
				newBuf[i - n] = rxBuf[i];
			}
			rxBuf = newBuf;
			return n;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0;
		}

		public override void SetLength(long value)
		{
			// nothing to do
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (handle < 0)
				return;

			byte[] txBuf = new byte[count];
			for (int i = 0; i < txBuf.Length; i++)
			{
				txBuf[i] = buffer[offset + i];
			}
			rxBuf = new byte[count];

			//Console.Write("txBuf[{0}]: ", txBuf.Length);
			//for (int i = 0; i < txBuf.Length; i++)
			//{
			//	Console.Write(" {0:x2}", txBuf[i]);
			//}
			//Console.WriteLine("\r\n");

			pigpiodIf.spi_xfer((UInt32)handle, txBuf, rxBuf);

			//Console.Write("rxBuf[{0}]: ", rxBuf.Length);
			//for (int i = 0; i < rxBuf.Length; i++)
			//{
			//	Console.Write(" {0:x2}", rxBuf[i]);
			//}
			//Console.WriteLine("\r\n");
		}

		#endregion
	}
}
