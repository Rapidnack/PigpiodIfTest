using System;
using System.Linq;

namespace PigpiodIfTest
{
	public class LogWriter : System.IO.TextWriter
	{
		#region # event

		public event EventHandler TextChanged;

		#endregion


		#region # private field

		private const int LINE_NUMS = 300;

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

			string[] lines = Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			if (lines.Length > LINE_NUMS)
			{
				lines = lines.Skip(lines.Length - LINE_NUMS).ToArray();
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
