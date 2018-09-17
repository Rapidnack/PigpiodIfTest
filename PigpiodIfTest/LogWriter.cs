using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigpiodIfTest
{
	public class LogWriter : System.IO.TextWriter
	{
		#region # event

		public event EventHandler TextChanged;

		#endregion


		#region # public property

		public override System.Text.Encoding Encoding
		{
			get { return System.Text.Encoding.UTF8; }
		}

		public string Text { get; set; }

		#endregion


		#region # constructor

		public LogWriter()
			: base()
		{
			Text = string.Empty;
		}

		#endregion


		#region # public method

		public override void WriteLine(string value)
		{
			Write(value + "\r\n");
		}

		public override void Write(string value)
		{
			base.Write(value);

			Text += value;
			if (TextChanged != null)
			{
				TextChanged.Invoke(this, new EventArgs());
			}
		}

		#endregion
	}
}
