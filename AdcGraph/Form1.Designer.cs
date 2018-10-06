namespace AdcGraph
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkBoxServo2 = new System.Windows.Forms.CheckBox();
			this.checkBoxServo1 = new System.Windows.Forms.CheckBox();
			this.buttonFastStop = new System.Windows.Forms.Button();
			this.buttonFastStart = new System.Windows.Forms.Button();
			this.trackBarServo2 = new System.Windows.Forms.TrackBar();
			this.trackBarServo1 = new System.Windows.Forms.TrackBar();
			this.buttonLedStop = new System.Windows.Forms.Button();
			this.buttonLedStart = new System.Windows.Forms.Button();
			this.buttonRollStop = new System.Windows.Forms.Button();
			this.buttonRollStart = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.loggingTextBox1 = new Rapidnack.Common.LoggingTextBox();
			this.tabPageGraph = new System.Windows.Forms.TabPage();
			this.plotView1 = new OxyPlot.WindowsForms.PlotView();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPageLog.SuspendLayout();
			this.tabPageGraph.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Location = new System.Drawing.Point(12, 28);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(136, 23);
			this.textBoxAddress.TabIndex = 1;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(83, 60);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(65, 27);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonOpen
			// 
			this.buttonOpen.Location = new System.Drawing.Point(12, 60);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(65, 27);
			this.buttonOpen.TabIndex = 2;
			this.buttonOpen.Text = "Open";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "IP address";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.checkBoxServo2);
			this.panel1.Controls.Add(this.checkBoxServo1);
			this.panel1.Controls.Add(this.buttonFastStop);
			this.panel1.Controls.Add(this.buttonFastStart);
			this.panel1.Controls.Add(this.trackBarServo2);
			this.panel1.Controls.Add(this.trackBarServo1);
			this.panel1.Controls.Add(this.buttonLedStop);
			this.panel1.Controls.Add(this.buttonLedStart);
			this.panel1.Controls.Add(this.buttonRollStop);
			this.panel1.Controls.Add(this.buttonRollStart);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.buttonOpen);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.textBoxAddress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(160, 320);
			this.panel1.TabIndex = 0;
			// 
			// checkBoxServo2
			// 
			this.checkBoxServo2.AutoSize = true;
			this.checkBoxServo2.Location = new System.Drawing.Point(12, 177);
			this.checkBoxServo2.Name = "checkBoxServo2";
			this.checkBoxServo2.Size = new System.Drawing.Size(15, 14);
			this.checkBoxServo2.TabIndex = 8;
			this.checkBoxServo2.UseVisualStyleBackColor = true;
			this.checkBoxServo2.CheckedChanged += new System.EventHandler(this.checkBoxServo2_CheckedChanged);
			// 
			// checkBoxServo1
			// 
			this.checkBoxServo1.AutoSize = true;
			this.checkBoxServo1.Location = new System.Drawing.Point(12, 143);
			this.checkBoxServo1.Name = "checkBoxServo1";
			this.checkBoxServo1.Size = new System.Drawing.Size(15, 14);
			this.checkBoxServo1.TabIndex = 6;
			this.checkBoxServo1.UseVisualStyleBackColor = true;
			this.checkBoxServo1.CheckedChanged += new System.EventHandler(this.checkBoxServo1_CheckedChanged);
			// 
			// buttonFastStop
			// 
			this.buttonFastStop.Location = new System.Drawing.Point(83, 255);
			this.buttonFastStop.Name = "buttonFastStop";
			this.buttonFastStop.Size = new System.Drawing.Size(65, 41);
			this.buttonFastStop.TabIndex = 13;
			this.buttonFastStop.Text = "Fast Stop";
			this.buttonFastStop.UseVisualStyleBackColor = true;
			this.buttonFastStop.Click += new System.EventHandler(this.buttonFastStop_Click);
			// 
			// buttonFastStart
			// 
			this.buttonFastStart.Location = new System.Drawing.Point(12, 255);
			this.buttonFastStart.Name = "buttonFastStart";
			this.buttonFastStart.Size = new System.Drawing.Size(65, 41);
			this.buttonFastStart.TabIndex = 12;
			this.buttonFastStart.Text = "Fast Start";
			this.buttonFastStart.UseVisualStyleBackColor = true;
			this.buttonFastStart.Click += new System.EventHandler(this.buttonFastStart_Click);
			// 
			// trackBarServo2
			// 
			this.trackBarServo2.AutoSize = false;
			this.trackBarServo2.LargeChange = 200;
			this.trackBarServo2.Location = new System.Drawing.Point(36, 174);
			this.trackBarServo2.Maximum = 2500;
			this.trackBarServo2.Minimum = 500;
			this.trackBarServo2.Name = "trackBarServo2";
			this.trackBarServo2.Size = new System.Drawing.Size(112, 28);
			this.trackBarServo2.SmallChange = 50;
			this.trackBarServo2.TabIndex = 9;
			this.trackBarServo2.TickFrequency = 500;
			this.trackBarServo2.Value = 1500;
			this.trackBarServo2.Scroll += new System.EventHandler(this.trackBarServo2_Scroll);
			// 
			// trackBarServo1
			// 
			this.trackBarServo1.AutoSize = false;
			this.trackBarServo1.LargeChange = 200;
			this.trackBarServo1.Location = new System.Drawing.Point(36, 140);
			this.trackBarServo1.Maximum = 2500;
			this.trackBarServo1.Minimum = 500;
			this.trackBarServo1.Name = "trackBarServo1";
			this.trackBarServo1.Size = new System.Drawing.Size(112, 28);
			this.trackBarServo1.SmallChange = 50;
			this.trackBarServo1.TabIndex = 7;
			this.trackBarServo1.TickFrequency = 500;
			this.trackBarServo1.Value = 1500;
			this.trackBarServo1.Scroll += new System.EventHandler(this.trackBarServo1_Scroll);
			// 
			// buttonLedStop
			// 
			this.buttonLedStop.Location = new System.Drawing.Point(83, 93);
			this.buttonLedStop.Name = "buttonLedStop";
			this.buttonLedStop.Size = new System.Drawing.Size(65, 41);
			this.buttonLedStop.TabIndex = 5;
			this.buttonLedStop.Text = "LED Stop";
			this.buttonLedStop.UseVisualStyleBackColor = true;
			this.buttonLedStop.Click += new System.EventHandler(this.buttonLedStop_Click);
			// 
			// buttonLedStart
			// 
			this.buttonLedStart.Location = new System.Drawing.Point(12, 93);
			this.buttonLedStart.Name = "buttonLedStart";
			this.buttonLedStart.Size = new System.Drawing.Size(65, 41);
			this.buttonLedStart.TabIndex = 4;
			this.buttonLedStart.Text = "LED Start";
			this.buttonLedStart.UseVisualStyleBackColor = true;
			this.buttonLedStart.Click += new System.EventHandler(this.buttonLedStart_Click);
			// 
			// buttonRollStop
			// 
			this.buttonRollStop.Location = new System.Drawing.Point(83, 208);
			this.buttonRollStop.Name = "buttonRollStop";
			this.buttonRollStop.Size = new System.Drawing.Size(65, 41);
			this.buttonRollStop.TabIndex = 11;
			this.buttonRollStop.Text = "Roll Stop";
			this.buttonRollStop.UseVisualStyleBackColor = true;
			this.buttonRollStop.Click += new System.EventHandler(this.buttonRollStop_Click);
			// 
			// buttonRollStart
			// 
			this.buttonRollStart.Location = new System.Drawing.Point(12, 208);
			this.buttonRollStart.Name = "buttonRollStart";
			this.buttonRollStart.Size = new System.Drawing.Size(65, 41);
			this.buttonRollStart.TabIndex = 10;
			this.buttonRollStart.Text = "Roll Start";
			this.buttonRollStart.UseVisualStyleBackColor = true;
			this.buttonRollStart.Click += new System.EventHandler(this.buttonRollStart_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPageLog);
			this.tabControl1.Controls.Add(this.tabPageGraph);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(160, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(320, 320);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.loggingTextBox1);
			this.tabPageLog.Location = new System.Drawing.Point(4, 26);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Size = new System.Drawing.Size(312, 290);
			this.tabPageLog.TabIndex = 0;
			this.tabPageLog.Text = "Log";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// loggingTextBox1
			// 
			this.loggingTextBox1.BackColor = System.Drawing.SystemColors.Window;
			this.loggingTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loggingTextBox1.Location = new System.Drawing.Point(0, 0);
			this.loggingTextBox1.Multiline = true;
			this.loggingTextBox1.Name = "loggingTextBox1";
			this.loggingTextBox1.ReadOnly = true;
			this.loggingTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.loggingTextBox1.Size = new System.Drawing.Size(312, 290);
			this.loggingTextBox1.TabIndex = 0;
			// 
			// tabPageGraph
			// 
			this.tabPageGraph.Controls.Add(this.plotView1);
			this.tabPageGraph.Location = new System.Drawing.Point(4, 26);
			this.tabPageGraph.Name = "tabPageGraph";
			this.tabPageGraph.Size = new System.Drawing.Size(312, 290);
			this.tabPageGraph.TabIndex = 1;
			this.tabPageGraph.Text = "Graph";
			this.tabPageGraph.UseVisualStyleBackColor = true;
			// 
			// plotView1
			// 
			this.plotView1.BackColor = System.Drawing.SystemColors.Window;
			this.plotView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plotView1.Location = new System.Drawing.Point(0, 0);
			this.plotView1.Name = "plotView1";
			this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.plotView1.Size = new System.Drawing.Size(312, 290);
			this.plotView1.TabIndex = 0;
			this.plotView1.Text = "plotView1";
			this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(480, 320);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "ADC Graph";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPageLog.ResumeLayout(false);
			this.tabPageLog.PerformLayout();
			this.tabPageGraph.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonRollStop;
		private System.Windows.Forms.Button buttonRollStart;
		private System.Windows.Forms.Button buttonLedStop;
		private System.Windows.Forms.Button buttonLedStart;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageLog;
		private System.Windows.Forms.TabPage tabPageGraph;
		private OxyPlot.WindowsForms.PlotView plotView1;
		private System.Windows.Forms.TrackBar trackBarServo2;
		private System.Windows.Forms.TrackBar trackBarServo1;
		private System.Windows.Forms.Button buttonFastStop;
		private System.Windows.Forms.Button buttonFastStart;
		private System.Windows.Forms.CheckBox checkBoxServo2;
		private System.Windows.Forms.CheckBox checkBoxServo1;
		private Rapidnack.Common.LoggingTextBox loggingTextBox1;
	}
}

