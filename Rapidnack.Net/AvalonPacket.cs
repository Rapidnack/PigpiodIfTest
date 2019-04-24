using System;
using System.Collections.Generic;
using System.IO;

namespace Rapidnack.Net
{
	public class AvalonPacket
	{
		#region # private field

		private static bool byteCh = false;
		private static bool bytesEscape = false;
		private static bool bitsEscape = false;
		private static int waitMs = 100;

		#endregion


		#region # public const

		public const int DefaultTimeoutInSec = 3;

		#endregion


		#region # public method

		public static byte[] WritePacket(Stream stream, UInt32 addr, byte[] array, bool isIncremental = false, int timeoutInSec = DefaultTimeoutInSec)
		{
			byte[] packet = new byte[8 + array.Length];
			byte[] sizeBytes = BitConverter.GetBytes((UInt16)array.Length);
			byte[] addrBytes = BitConverter.GetBytes(addr);
			packet[0] = (byte)(isIncremental ? 0x04 : 0x00);
			packet[1] = 0;
			packet[2] = sizeBytes[1];
			packet[3] = sizeBytes[0];
			packet[4] = addrBytes[3];
			packet[5] = addrBytes[2];
			packet[6] = addrBytes[1];
			packet[7] = addrBytes[0];
			for (int i = 0; i < array.Length; i++)
			{
				packet[8 + i] = array[i];
			}
			byte[] bits = BytesToBits(PacketToBytes(packet));
			stream.Write(bits, 0, bits.Length);

			// Discards the number of bytes sent to SPI from SPI
			int received = 0;
			while (received < bits.Length)
			{
				received += stream.Read(bits, received, bits.Length - received);
			}

			return ReceiveResponse(stream, 4, timeoutInSec);
		}

		public static byte[] WritePacket(Stream stream, UInt32 addr, byte data, int timeoutInSec = DefaultTimeoutInSec)
		{
			byte[] bytes = new byte[] { data };
			return WritePacket(stream, addr, bytes, false, timeoutInSec);
		}

		public static byte[] WritePacket(Stream stream, UInt32 addr, UInt16 data, int timeoutInSec = DefaultTimeoutInSec)
		{
			byte[] bytes = BitConverter.GetBytes(data);
			return WritePacket(stream, addr, bytes, false, timeoutInSec);
		}

		public static byte[] WritePacket(Stream stream, UInt32 addr, UInt32 data, int timeoutInSec = DefaultTimeoutInSec)
		{
			byte[] bytes = BitConverter.GetBytes(data);
			return WritePacket(stream, addr, bytes, false, timeoutInSec);
		}

		public static byte[] WritePacket(Stream stream, UInt32 addr, UInt16[] array, bool isIncremental = false, int timeoutInSec = DefaultTimeoutInSec)
		{
			int numBytes = 2;
			byte[] bytes = new byte[numBytes * array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] tempBytes = BitConverter.GetBytes(array[i]);
				tempBytes.CopyTo(bytes, numBytes * i);
			}
			return WritePacket(stream, addr, bytes, isIncremental, timeoutInSec);
		}

		public static byte[] WritePacket(Stream stream, UInt32 addr, UInt32[] array, bool isIncremental = false, int timeoutInSec = DefaultTimeoutInSec)
		{
			int numBytes = 4;
			byte[] bytes = new byte[numBytes * array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] tempBytes = BitConverter.GetBytes(array[i]);
				tempBytes.CopyTo(bytes, numBytes * i);
			}
			return WritePacket(stream, addr, bytes, isIncremental, timeoutInSec);
		}

		public static byte[] ReadBytePacket(Stream stream, UInt16 arraySize, UInt32 addr, bool isIncremental = false, int timeoutInSec = DefaultTimeoutInSec)
		{
			if (arraySize == 0)
				return new byte[0];

			byte[] packet = new byte[8];
			byte[] sizeBytes = BitConverter.GetBytes(arraySize);
			byte[] addrBytes = BitConverter.GetBytes(addr);
			packet[0] = (byte)(isIncremental ? 0x14 : 0x10);
			packet[1] = 0;
			packet[2] = sizeBytes[1];
			packet[3] = sizeBytes[0];
			packet[4] = addrBytes[3];
			packet[5] = addrBytes[2];
			packet[6] = addrBytes[1];
			packet[7] = addrBytes[0];
			byte[] bits = BytesToBits(PacketToBytes(packet));
			stream.Write(bits, 0, bits.Length);

			// Discards the number of bytes sent to SPI from SPI
			int received = 0;
			while (received < bits.Length)
			{
				received += stream.Read(bits, received, bits.Length - received);
			}

			return ReceiveResponse(stream, arraySize, timeoutInSec);
		}

		public static UInt16[] ReadUInt16Packet(Stream stream, UInt16 arraySize, UInt32 addr, bool isIncremental = false, int timeoutInSec = DefaultTimeoutInSec)
		{
			if (arraySize == 0)
				return new UInt16[0];

			int numBytes = 2;
			byte[] recvPacket = ReadBytePacket(stream, (UInt16)(numBytes * arraySize), addr, isIncremental, timeoutInSec);
			UInt16[] array = new UInt16[recvPacket.Length / numBytes];
			byte[] dataBytes = new byte[numBytes];
			for (int i = 0; i < array.Length; i++)
			{
				for (int b = 0; b < numBytes; b++)
				{
					dataBytes[b] = recvPacket[numBytes * i + b];
				}
				array[i] = BitConverter.ToUInt16(dataBytes, 0);
			}
			return array;
		}

		public static UInt32[] ReadUInt32Packet(Stream stream, UInt16 arraySize, UInt32 addr, bool isIncremental = false, int timeoutInSec = DefaultTimeoutInSec)
		{
			if (arraySize == 0)
				return new UInt32[0];

			int numBytes = 4;
			byte[] recvPacket = ReadBytePacket(stream, (UInt16)(numBytes * arraySize), addr, isIncremental, timeoutInSec);
			UInt32[] array = new UInt32[recvPacket.Length / numBytes];
			byte[] dataBytes = new byte[numBytes];
			for (int i = 0; i < array.Length; i++)
			{
				for (int b = 0; b < numBytes; b++)
				{
					dataBytes[b] = recvPacket[numBytes * i + b];
				}
				array[i] = BitConverter.ToUInt32(dataBytes, 0);
			}
			return array;
		}

		public static byte ReadBytePacket(Stream stream, UInt32 addr, int timeoutInSec = DefaultTimeoutInSec)
		{
			byte[] array = ReadBytePacket(stream, 1, addr, false, timeoutInSec);
			if (array.Length == 0)
				return 0;
			return array[0];
		}

		public static UInt16 ReadUInt16Packet(Stream stream, UInt32 addr, int timeoutInSec = DefaultTimeoutInSec)
		{
			UInt16[] array = ReadUInt16Packet(stream, 1, addr, false, timeoutInSec);
			if (array.Length == 0)
				return 0;
			return array[0];
		}

		public static UInt32 ReadUInt32Packet(Stream stream, UInt32 addr, int timeoutInSec = DefaultTimeoutInSec)
		{
			UInt32[] array = ReadUInt32Packet(stream, 1, addr, false, timeoutInSec);
			if (array.Length == 0)
				return 0;
			return array[0];
		}

		#endregion


		#region # private method

		private static byte[] PacketToBytes(byte[] packet)
		{
			List<byte> bytes = new List<byte>();
			bytes.Add(0x7c);
			bytes.Add(0);
			bytes.Add(0x7a);
			for (int i = 0; i < packet.Length; i++)
			{
				byte p = packet[i];
				if (0x7a <= p && p <= 0x7d)
				{
					if (i == packet.Length - 1)
					{
						bytes.Add(0x7b);
					}
					bytes.Add(0x7d);
					bytes.Add((byte)(p ^ 0x20));
				}
				else
				{
					if (i == packet.Length - 1)
					{
						bytes.Add(0x7b);
					}
					bytes.Add(p);
				}
			}
			return bytes.ToArray();
		}

		private static byte[] BytesToPacket(byte[] bytes)
		{
			List<byte> packet = new List<byte>();
			foreach (byte b in bytes)
			{
				if (b == 0x7a || b == 0x7b)
				{
					// Dropped
				}
				else if (b == 0x7c)
				{
					byteCh = true;
					// Dropped
				}
				else if (b == 0x7d)
				{
					bytesEscape = true;
					// Dropped
				}
				else
				{
					if (byteCh)
					{
						byteCh = false;
						// Dropped
					}
					else if (bytesEscape)
					{
						bytesEscape = false;
						packet.Add((byte)(b ^ 0x20));
					}
					else
					{
						packet.Add(b);
					}
				}
			}
			return packet.ToArray();
		}

		private static byte[] BytesToBits(byte[] bytes)
		{
			List<byte> bits = new List<byte>();
			foreach (byte b in bytes)
			{
				if (b == 0x4a || b == 0x4d)
				{
					bits.Add(0x4d);
					bits.Add((byte)(b ^ 0x20));
				}
				else
				{
					bits.Add(b);
				}
			}
			return bits.ToArray();
		}

		private static byte[] BitsToBytes(byte[] bits)
		{
			return BitsToBytes(bits, 0, bits.Length);
		}

		private static byte[] BitsToBytes(byte[] bits, int start, int length)
		{
			List<byte> bytes = new List<byte>();
			for (int i = start; i < start + length; i++)
			{
				byte b = bits[i];
				if (b == 0x4a)
				{
					// Dropped
				}
				else if (b == 0x4d)
				{
					bitsEscape = true;
					// Dropped
				}
				else
				{
					if (bitsEscape)
					{
						bitsEscape = false;
						bytes.Add((byte)(b ^ 0x20));
					}
					else
					{
						bytes.Add(b);
					}
				}
			}
			return bytes.ToArray();
		}

		private static byte[] ReceiveResponse(Stream stream, int requestSize, int timeoutInSec)
		{
			byteCh = false;
			bytesEscape = false;
			bitsEscape = false;

			int emptyCount = 0;

			byte[] response = new byte[0];
			while (response.Length < requestSize)
			{
				// Send dummy data to SPI to obtain response from FPGA
				byte[] bits = new byte[(int)((requestSize - response.Length) * 1.1) + 4];
				for (int i = 0; i < bits.Length; i++)
				{
					bits[i] = 0x4a;
				}
				stream.Write(bits, 0, bits.Length);
				int received = 0;
				while (received < bits.Length)
				{
					received += stream.Read(bits, received, bits.Length - received);
				}

				byte[] packet = BytesToPacket(BitsToBytes(bits));
				//Console.WriteLine("bits.Length: {0}, packet.Length: {1}", bits.Length, packet.Length);

				if (packet.Length > 0)
				{
					byte[] newResponse = new byte[response.Length + packet.Length];
					response.CopyTo(newResponse, 0);
					packet.CopyTo(newResponse, response.Length);
					response = newResponse;

					emptyCount = 0;
				}
				else
				{
					emptyCount += (emptyCount < 10) ? 1 : 10;
					if (emptyCount >= (timeoutInSec * 1000) / waitMs)
						break;
					System.Threading.Thread.Sleep(waitMs * ((emptyCount < 10) ? 1 : 10));
				}
			}
			return response;
		}

		#endregion
	}
}
