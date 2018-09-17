namespace PigpiodIfTest
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
			this.SuspendLayout();
			// 
			// buttonTest
			// 
			this.buttonTest.Location = new System.Drawing.Point(199, 53);
			this.buttonTest.Name = "buttonTest";
			this.buttonTest.Size = new System.Drawing.Size(75, 27);
			this.buttonTest.TabIndex = 5;
			this.buttonTest.Text = "Test";
			this.buttonTest.UseVisualStyleBackColor = true;
			this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
			// 
			// buttonOff
			// 
			this.buttonOff.Location = new System.Drawing.Point(12, 86);
			this.buttonOff.Name = "buttonOff";
			this.buttonOff.Size = new System.Drawing.Size(75, 27);
			this.buttonOff.TabIndex = 4;
			this.buttonOff.Text = "Off";
			this.buttonOff.UseVisualStyleBackColor = true;
			this.buttonOff.Click += new System.EventHandler(this.buttonOff_Click);
			// 
			// buttonOn
			// 
			this.buttonOn.Location = new System.Drawing.Point(12, 53);
			this.buttonOn.Name = "buttonOn";
			this.buttonOn.Size = new System.Drawing.Size(75, 27);
			this.buttonOn.TabIndex = 3;
			this.buttonOn.Text = "On";
			this.buttonOn.UseVisualStyleBackColor = true;
			this.buttonOn.Click += new System.EventHandler(this.buttonOn_Click);
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Location = new System.Drawing.Point(12, 12);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(100, 23);
			this.textBoxAddress.TabIndex = 0;
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(199, 10);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 27);
			this.buttonClose.TabIndex = 2;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonOpen
			// 
			this.buttonOpen.Location = new System.Drawing.Point(118, 10);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(75, 27);
			this.buttonOpen.TabIndex = 1;
			this.buttonOpen.Text = "Open";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(464, 281);
			this.Controls.Add(this.buttonTest);
			this.Controls.Add(this.buttonOff);
			this.Controls.Add(this.buttonOn);
			this.Controls.Add(this.textBoxAddress);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonOpen);
			this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "Pigpiod C# I/F Test";
			this.Load += new System.EventHandler(this.MainForm_Load);
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
	}
}

