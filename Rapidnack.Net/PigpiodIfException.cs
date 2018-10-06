using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapidnack.Net
{
	public class PigpiodIfException : Exception
	{
		public int ErrorNum { get; }

		public PigpiodIfException(int errorNum, string message)
			: base(message)
		{
			ErrorNum = errorNum;
		}
	}
}
