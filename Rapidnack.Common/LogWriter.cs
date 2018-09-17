using System;
using System.Linq;

namespace Rapidnack.Common
{
	public class LogWriter : System.IO.TextWriter
	{
		#region # event

		public event EventHandler TextChanged;

		#endregion


		#region # private field

		private int lineNums = 300;

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

		public LogWriter(int lineNums)
			: this()
		{
			this.lineNums = lineNums;
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

			string[] lines = Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			if (lines.Length > lineNums)
			{
				lines = lines.Skip(lines.Length - lineNums).ToArray();
			}
			Text = string.Join("\r\n", lines);

			if (TextChanged != null)
			{
				TextChanged.Invoke(this, new EventArgs());
			}
		}

		#endregion
	}
}
