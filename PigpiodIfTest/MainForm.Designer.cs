﻿namespace PigpiodIfTest
{
	partial class MainForm
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
			this.buttonTest = new System.Windows.Forms.Button();
			this.buttonOff = new System.Windows.Forms.Button();
			this.buttonOn = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonTest
			// 
			this.buttonTest.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonTest.Location = new System.Drawing.Point(12, 206);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(65, 27);
			this.buttonTest.TabIndex = 6;
			this.buttonTest.Text = "Test";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// buttonOff
			// 
			this.buttonOff.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOff.Location = new System.Drawing.Point(83, 130);
			this.buttonOff.Name = "buttonOff";
			this.buttonOff.Size = new System.Drawing.Size(65, 27);
			this.buttonOff.TabIndex = 5;
			this.buttonOff.Text = "Off";
			this.buttonOff.UseVisualStyleBackColor = true;
			this.buttonOff.Click += new System.EventHandler(this.buttonOff_Click);
			// 
			// buttonOn
			// 
			this.buttonOn.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOn.Location = new System.Drawing.Point(12, 130);
			this.buttonOn.Name = "buttonOn";
			this.buttonOn.Size = new System.Drawing.Size(65, 27);
			this.buttonOn.TabIndex = 4;
			this.buttonOn.Text = "On";
			this.buttonOn.UseVisualStyleBackColor = true;
			this.buttonOn.Click += new System.EventHandler(this.buttonOn_Click);
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
			this.buttonClose.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonClose.Location = new System.Drawing.Point(83, 57);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(65, 27);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonOpen
			// 
			this.buttonOpen.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonOpen.Location = new System.Drawing.Point(12, 57);
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
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "IP address";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.buttonOpen);
			this.panel1.Controls.Add(this.buttonTest);
			this.panel1.Controls.Add(this.buttonClose);
			this.panel1.Controls.Add(this.buttonOff);
			this.panel1.Controls.Add(this.textBoxAddress);
			this.panel1.Controls.Add(this.buttonOn);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(160, 281);
			this.panel1.TabIndex = 0;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buttonCancel.Location = new System.Drawing.Point(83, 206);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(65, 27);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// textBoxLog
			// 
			this.textBoxLog.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxLog.Location = new System.Drawing.Point(160, 0);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ReadOnly = true;
			this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLog.Size = new System.Drawing.Size(304, 281);
			this.textBoxLog.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(464, 281);
			this.Controls.Add(this.textBoxLog);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "Pigpiod C# I/F Test";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonTest;
		private System.Windows.Forms.Button buttonOff;
		private System.Windows.Forms.Button buttonOn;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.Button buttonCancel;
	}
}

