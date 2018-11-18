namespace Max10Graph
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelOperation = new System.Windows.Forms.Panel();
			this.trackBarLevel = new System.Windows.Forms.TrackBar();
			this.comboBoxPol = new System.Windows.Forms.ComboBox();
			this.comboBoxSource = new System.Windows.Forms.ComboBox();
			this.trackBarHor = new System.Windows.Forms.TrackBar();
			this.trackBarPos = new System.Windows.Forms.TrackBar();
			this.trackBarServo1 = new System.Windows.Forms.TrackBar();
			this.trackBarServo2 = new System.Windows.Forms.TrackBar();
			this.checkBoxServo1 = new System.Windows.Forms.CheckBox();
			this.checkBoxServo2 = new System.Windows.Forms.CheckBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.loggingTextBox1 = new Rapidnack.Common.LoggingTextBox();
			this.tabPageGraph = new System.Windows.Forms.TabPage();
			this.plotView1 = new OxyPlot.WindowsForms.PlotView();
			this.panel1.SuspendLayout();
			this.panelOperation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo2)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPageLog.SuspendLayout();
			this.tabPageGraph.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panelOperation);
			this.panel1.Controls.Add(this.buttonOpen);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.textBoxAddress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(160, 320);
			this.panel1.TabIndex = 1;
			// 
			// panelOperation
			// 
			this.panelOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelOperation.Controls.Add(this.trackBarLevel);
			this.panelOperation.Controls.Add(this.comboBoxPol);
			this.panelOperation.Controls.Add(this.comboBoxSource);
			this.panelOperation.Controls.Add(this.trackBarHor);
			this.panelOperation.Controls.Add(this.trackBarPos);
			this.panelOperation.Controls.Add(this.trackBarServo1);
			this.panelOperation.Controls.Add(this.trackBarServo2);
			this.panelOperation.Controls.Add(this.checkBoxServo1);
			this.panelOperation.Controls.Add(this.checkBoxServo2);
			this.panelOperation.Controls.Add(this.buttonStart);
			this.panelOperation.Controls.Add(this.buttonStop);
			this.panelOperation.Controls.Add(this.label4);
			this.panelOperation.Controls.Add(this.label1);
			this.panelOperation.Controls.Add(this.label3);
			this.panelOperation.Controls.Add(this.label2);
			this.panelOperation.Location = new System.Drawing.Point(0, 74);
			this.panelOperation.Margin = new System.Windows.Forms.Padding(0);
			this.panelOperation.Name = "panelOperation";
			this.panelOperation.Size = new System.Drawing.Size(160, 246);
			this.panelOperation.TabIndex = 3;
			// 
			// trackBarLevel
			// 
			this.trackBarLevel.AutoSize = false;
			this.trackBarLevel.LargeChange = 10;
			this.trackBarLevel.Location = new System.Drawing.Point(118, 89);
			this.trackBarLevel.Maximum = 100;
			this.trackBarLevel.Name = "trackBarLevel";
			this.trackBarLevel.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBarLevel.Size = new System.Drawing.Size(28, 82);
			this.trackBarLevel.SmallChange = 2;
			this.trackBarLevel.TabIndex = 10;
			this.trackBarLevel.TickFrequency = 500;
			this.trackBarLevel.Scroll += new System.EventHandler(this.trackBarLevel_Scroll);
			// 
			// comboBoxPol
			// 
			this.comboBoxPol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPol.FormattingEnabled = true;
			this.comboBoxPol.Items.AddRange(new object[] {
            "Rise",
            "Fall"});
			this.comboBoxPol.Location = new System.Drawing.Point(47, 133);
			this.comboBoxPol.Name = "comboBoxPol";
			this.comboBoxPol.Size = new System.Drawing.Size(65, 24);
			this.comboBoxPol.TabIndex = 9;
			this.comboBoxPol.SelectedIndexChanged += new System.EventHandler(this.comboBoxPol_SelectedIndexChanged);
			// 
			// comboBoxSource
			// 
			this.comboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSource.FormattingEnabled = true;
			this.comboBoxSource.Location = new System.Drawing.Point(47, 103);
			this.comboBoxSource.Name = "comboBoxSource";
			this.comboBoxSource.Size = new System.Drawing.Size(65, 24);
			this.comboBoxSource.TabIndex = 7;
			this.comboBoxSource.SelectedIndexChanged += new System.EventHandler(this.comboBoxSource_SelectedIndexChanged);
			// 
			// trackBarHor
			// 
			this.trackBarHor.AutoSize = false;
			this.trackBarHor.LargeChange = 1;
			this.trackBarHor.Location = new System.Drawing.Point(36, 36);
			this.trackBarHor.Maximum = 9;
			this.trackBarHor.Name = "trackBarHor";
			this.trackBarHor.Size = new System.Drawing.Size(112, 28);
			this.trackBarHor.TabIndex = 3;
			this.trackBarHor.TickFrequency = 500;
			this.trackBarHor.Scroll += new System.EventHandler(this.trackBarHor_Scroll);
			// 
			// trackBarPos
			// 
			this.trackBarPos.AutoSize = false;
			this.trackBarPos.LargeChange = 10;
			this.trackBarPos.Location = new System.Drawing.Point(36, 67);
			this.trackBarPos.Maximum = 100;
			this.trackBarPos.Name = "trackBarPos";
			this.trackBarPos.Size = new System.Drawing.Size(112, 28);
			this.trackBarPos.SmallChange = 2;
			this.trackBarPos.TabIndex = 5;
			this.trackBarPos.TickFrequency = 500;
			this.trackBarPos.Scroll += new System.EventHandler(this.trackBarPos_Scroll);
			// 
			// trackBarServo1
			// 
			this.trackBarServo1.AutoSize = false;
			this.trackBarServo1.LargeChange = 200;
			this.trackBarServo1.Location = new System.Drawing.Point(36, 172);
			this.trackBarServo1.Maximum = 2500;
			this.trackBarServo1.Minimum = 500;
			this.trackBarServo1.Name = "trackBarServo1";
			this.trackBarServo1.Size = new System.Drawing.Size(112, 28);
			this.trackBarServo1.SmallChange = 50;
			this.trackBarServo1.TabIndex = 12;
			this.trackBarServo1.TickFrequency = 500;
			this.trackBarServo1.Value = 1500;
			this.trackBarServo1.Scroll += new System.EventHandler(this.trackBarServo1_Scroll);
			// 
			// trackBarServo2
			// 
			this.trackBarServo2.AutoSize = false;
			this.trackBarServo2.LargeChange = 200;
			this.trackBarServo2.Location = new System.Drawing.Point(36, 206);
			this.trackBarServo2.Maximum = 2500;
			this.trackBarServo2.Minimum = 500;
			this.trackBarServo2.Name = "trackBarServo2";
			this.trackBarServo2.Size = new System.Drawing.Size(112, 28);
			this.trackBarServo2.SmallChange = 50;
			this.trackBarServo2.TabIndex = 14;
			this.trackBarServo2.TickFrequency = 500;
			this.trackBarServo2.Value = 1500;
			this.trackBarServo2.Scroll += new System.EventHandler(this.trackBarServo2_Scroll);
			// 
			// checkBoxServo1
			// 
			this.checkBoxServo1.AutoSize = true;
			this.checkBoxServo1.Location = new System.Drawing.Point(12, 176);
			this.checkBoxServo1.Name = "checkBoxServo1";
			this.checkBoxServo1.Size = new System.Drawing.Size(15, 14);
			this.checkBoxServo1.TabIndex = 11;
			this.checkBoxServo1.UseVisualStyleBackColor = true;
			this.checkBoxServo1.CheckedChanged += new System.EventHandler(this.checkBoxServo1_CheckedChanged);
			// 
			// checkBoxServo2
			// 
			this.checkBoxServo2.AutoSize = true;
			this.checkBoxServo2.Location = new System.Drawing.Point(12, 210);
			this.checkBoxServo2.Name = "checkBoxServo2";
			this.checkBoxServo2.Size = new System.Drawing.Size(15, 14);
			this.checkBoxServo2.TabIndex = 13;
			this.checkBoxServo2.UseVisualStyleBackColor = true;
			this.checkBoxServo2.CheckedChanged += new System.EventHandler(this.checkBoxServo2_CheckedChanged);
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(12, 3);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(65, 27);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(83, 3);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(65, 27);
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Pol.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 106);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Src";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Pos.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Hor.";
			// 
			// buttonOpen
			// 
			this.buttonOpen.Location = new System.Drawing.Point(12, 44);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(65, 27);
			this.buttonOpen.TabIndex = 1;
			this.buttonOpen.Text = "Open";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(83, 44);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(65, 27);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Location = new System.Drawing.Point(12, 12);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(136, 23);
			this.textBoxAddress.TabIndex = 0;
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
			this.tabControl1.TabIndex = 2;
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.loggingTextBox1);
			this.tabPageLog.Location = new System.Drawing.Point(4, 26);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLog.Size = new System.Drawing.Size(312, 290);
			this.tabPageLog.TabIndex = 0;
			this.tabPageLog.Text = "Log";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// loggingTextBox1
			// 
			this.loggingTextBox1.BackColor = System.Drawing.SystemColors.Window;
			this.loggingTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loggingTextBox1.Location = new System.Drawing.Point(3, 3);
			this.loggingTextBox1.Multiline = true;
			this.loggingTextBox1.Name = "loggingTextBox1";
			this.loggingTextBox1.ReadOnly = true;
			this.loggingTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.loggingTextBox1.Size = new System.Drawing.Size(306, 284);
			this.loggingTextBox1.TabIndex = 0;
			// 
			// tabPageGraph
			// 
			this.tabPageGraph.Controls.Add(this.plotView1);
			this.tabPageGraph.Location = new System.Drawing.Point(4, 22);
			this.tabPageGraph.Name = "tabPageGraph";
			this.tabPageGraph.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageGraph.Size = new System.Drawing.Size(312, 294);
			this.tabPageGraph.TabIndex = 1;
			this.tabPageGraph.Text = "Graph";
			this.tabPageGraph.UseVisualStyleBackColor = true;
			// 
			// plotView1
			// 
			this.plotView1.BackColor = System.Drawing.SystemColors.Window;
			this.plotView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plotView1.Location = new System.Drawing.Point(3, 3);
			this.plotView1.Name = "plotView1";
			this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
			this.plotView1.Size = new System.Drawing.Size(306, 288);
			this.plotView1.TabIndex = 0;
			this.plotView1.Text = "plotView1";
			this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
			this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(480, 320);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "MAX 10 Graph";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panelOperation.ResumeLayout(false);
			this.panelOperation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarServo2)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPageLog.ResumeLayout(false);
			this.tabPageLog.PerformLayout();
			this.tabPageGraph.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelOperation;
		private System.Windows.Forms.TrackBar trackBarLevel;
		private System.Windows.Forms.ComboBox comboBoxPol;
		private System.Windows.Forms.ComboBox comboBoxSource;
		private System.Windows.Forms.TrackBar trackBarHor;
		private System.Windows.Forms.TrackBar trackBarPos;
		private System.Windows.Forms.TrackBar trackBarServo1;
		private System.Windows.Forms.TrackBar trackBarServo2;
		private System.Windows.Forms.CheckBox checkBoxServo1;
		private System.Windows.Forms.CheckBox checkBoxServo2;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageLog;
		private Rapidnack.Common.LoggingTextBox loggingTextBox1;
		private System.Windows.Forms.TabPage tabPageGraph;
		private OxyPlot.WindowsForms.PlotView plotView1;
	}
}

