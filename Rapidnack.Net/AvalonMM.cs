using System;
using System.IO;

namespace Rapidnack.Net
{
	public class AvalonMM
	{
		#region # event

		public event EventHandler StreamChanged;

		#endregion


		#region # public property

		private object _lockObject = new object();
		public object LockObject
		{
			get
			{
				return _lockObject;
			}
		}

		private Stream _stream = null;
		public Stream Stream
		{
			get
			{
				return _stream;
			}
			set
			{
				lock (LockObject)
				{
					_stream = value;
				}

				if (StreamChanged != null)
				{
					StreamChanged.Invoke(this, new EventArgs());
				}
			}
		}

		public bool CanRead
		{
			get
			{
				if (Stream == null)
					return false;
				return Stream.CanRead;
			}
		}

		public bool CanWrite
		{
			get
			{
				if (Stream == null)
					return false;
				return Stream.CanWrite;
			}
		}

		#endregion


		#region # public method

		public byte[] WriteBytePacket(UInt32 addr, byte[] dataBytes, bool isIncremental = false, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.WritePacket(Stream, addr, dataBytes, isIncremental, timeoutInSec);
			}
		}

		public byte[] WriteBytePacket(UInt32 addr, byte data, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.WritePacket(Stream, addr, data, timeoutInSec);
			}
		}

		public byte[] WriteUInt16Packet(UInt32 addr, UInt16 data, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.WritePacket(Stream, addr, data, timeoutInSec);
			}
		}

		public byte[] WriteUInt32Packet(UInt32 addr, UInt32 data, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.WritePacket(Stream, addr, data, timeoutInSec);
			}
		}

		public byte[] WriteUInt16Packet(UInt32 addr, UInt16[] dataArray, bool isIncremental = false, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.WritePacket(Stream, addr, dataArray, isIncremental, timeoutInSec);
			}
		}

		public byte[] WriteUInt32Packet(UInt32 addr, UInt32[] dataArray, bool isIncremental = false, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.WritePacket(Stream, addr, dataArray, isIncremental, timeoutInSec);
			}
		}

		public byte[] ReadBytePacket(UInt16 size, UInt32 addr, bool isIncremental = false, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.ReadBytePacket(Stream, size, addr, isIncremental, timeoutInSec);
			}
		}

		public UInt16[] ReadUInt16Packet(UInt16 size, UInt32 addr, bool isIncremental = false, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.ReadUInt16Packet(Stream, size, addr, isIncremental, timeoutInSec);
			}
		}

		public UInt32[] ReadUInt32Packet(UInt16 size, UInt32 addr, bool isIncremental = false, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.ReadUInt32Packet(Stream, size, addr, isIncremental, timeoutInSec);
			}
		}

		public byte ReadBytePacket(UInt32 addr, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.ReadBytePacket(Stream, addr, timeoutInSec);
			}
		}

		public UInt16 ReadUInt16Packet(UInt32 addr, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.ReadUInt16Packet(Stream, addr, timeoutInSec);
			}
		}

		public UInt32 ReadUInt32Packet(UInt32 addr, int timeoutInSec = AvalonPacket.DefaultTimeoutInSec)
		{
			lock (LockObject)
			{
				return AvalonPacket.ReadUInt32Packet(Stream, addr, timeoutInSec);
			}
		}

		#endregion
	}
}
