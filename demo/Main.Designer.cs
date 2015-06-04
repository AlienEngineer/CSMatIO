namespace CSMatIOTest
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.label1 = new System.Windows.Forms.Label();
			this.btnRead = new System.Windows.Forms.Button();
			this.btnCreate = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkImagMatrix = new System.Windows.Forms.CheckBox();
			this.chkSparse = new System.Windows.Forms.CheckBox();
			this.chkCell = new System.Windows.Forms.CheckBox();
			this.chkStruct = new System.Windows.Forms.CheckBox();
			this.chkChar = new System.Windows.Forms.CheckBox();
			this.chkDouble = new System.Windows.Forms.CheckBox();
			this.chkInt32 = new System.Windows.Forms.CheckBox();
			this.chkUInt64 = new System.Windows.Forms.CheckBox();
			this.chkSingle = new System.Windows.Forms.CheckBox();
			this.chkInt16 = new System.Windows.Forms.CheckBox();
			this.chkUInt8 = new System.Windows.Forms.CheckBox();
			this.chkInt64 = new System.Windows.Forms.CheckBox();
			this.chkInt8 = new System.Windows.Forms.CheckBox();
			this.chkUInt16 = new System.Windows.Forms.CheckBox();
			this.chkUInt32 = new System.Windows.Forms.CheckBox();
			this.chkCompress = new System.Windows.Forms.CheckBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.btnCheckEmAll = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Output:";
			// 
			// btnRead
			// 
			this.btnRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.btnRead.Location = new System.Drawing.Point(12, 29);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new System.Drawing.Size(101, 23);
			this.btnRead.TabIndex = 2;
			this.btnRead.Text = "Read MAT-File";
			this.btnRead.UseVisualStyleBackColor = true;
			this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(12, 77);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(101, 23);
			this.btnCreate.TabIndex = 3;
			this.btnCreate.Text = "Create MAT-File";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkImagMatrix);
			this.groupBox1.Controls.Add(this.chkSparse);
			this.groupBox1.Controls.Add(this.btnCheckEmAll);
			this.groupBox1.Controls.Add(this.chkCell);
			this.groupBox1.Controls.Add(this.chkStruct);
			this.groupBox1.Controls.Add(this.chkChar);
			this.groupBox1.Controls.Add(this.chkDouble);
			this.groupBox1.Controls.Add(this.chkInt32);
			this.groupBox1.Controls.Add(this.chkUInt64);
			this.groupBox1.Controls.Add(this.chkSingle);
			this.groupBox1.Controls.Add(this.chkInt16);
			this.groupBox1.Controls.Add(this.chkUInt8);
			this.groupBox1.Controls.Add(this.chkInt64);
			this.groupBox1.Controls.Add(this.chkInt8);
			this.groupBox1.Controls.Add(this.chkUInt16);
			this.groupBox1.Controls.Add(this.chkUInt32);
			this.groupBox1.Location = new System.Drawing.Point(125, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(338, 158);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "MAT-File Creation Parameters";
			// 
			// chkImagMatrix
			// 
			this.chkImagMatrix.AutoSize = true;
			this.chkImagMatrix.Location = new System.Drawing.Point(203, 65);
			this.chkImagMatrix.Name = "chkImagMatrix";
			this.chkImagMatrix.Size = new System.Drawing.Size(132, 17);
			this.chkImagMatrix.TabIndex = 15;
			this.chkImagMatrix.Text = "Large Imaginary Matrix";
			this.chkImagMatrix.UseVisualStyleBackColor = true;
			// 
			// chkSparse
			// 
			this.chkSparse.AutoSize = true;
			this.chkSparse.Location = new System.Drawing.Point(6, 88);
			this.chkSparse.Name = "chkSparse";
			this.chkSparse.Size = new System.Drawing.Size(86, 17);
			this.chkSparse.TabIndex = 14;
			this.chkSparse.Text = "Sparse Array";
			this.chkSparse.UseVisualStyleBackColor = true;
			// 
			// chkCell
			// 
			this.chkCell.AutoSize = true;
			this.chkCell.Location = new System.Drawing.Point(6, 19);
			this.chkCell.Name = "chkCell";
			this.chkCell.Size = new System.Drawing.Size(70, 17);
			this.chkCell.TabIndex = 13;
			this.chkCell.Text = "Cell Array";
			this.chkCell.UseVisualStyleBackColor = true;
			// 
			// chkStruct
			// 
			this.chkStruct.AutoSize = true;
			this.chkStruct.Location = new System.Drawing.Point(6, 42);
			this.chkStruct.Name = "chkStruct";
			this.chkStruct.Size = new System.Drawing.Size(69, 17);
			this.chkStruct.TabIndex = 12;
			this.chkStruct.Text = "Structure";
			this.chkStruct.UseVisualStyleBackColor = true;
			// 
			// chkChar
			// 
			this.chkChar.AutoSize = true;
			this.chkChar.Location = new System.Drawing.Point(6, 65);
			this.chkChar.Name = "chkChar";
			this.chkChar.Size = new System.Drawing.Size(75, 17);
			this.chkChar.TabIndex = 11;
			this.chkChar.Text = "Char Array";
			this.chkChar.UseVisualStyleBackColor = true;
			// 
			// chkDouble
			// 
			this.chkDouble.AutoSize = true;
			this.chkDouble.Location = new System.Drawing.Point(6, 111);
			this.chkDouble.Name = "chkDouble";
			this.chkDouble.Size = new System.Drawing.Size(87, 17);
			this.chkDouble.TabIndex = 10;
			this.chkDouble.Text = "Double Array";
			this.chkDouble.UseVisualStyleBackColor = true;
			// 
			// chkInt32
			// 
			this.chkInt32.AutoSize = true;
			this.chkInt32.Location = new System.Drawing.Point(103, 111);
			this.chkInt32.Name = "chkInt32";
			this.chkInt32.Size = new System.Drawing.Size(91, 17);
			this.chkInt32.TabIndex = 5;
			this.chkInt32.Text = "Int32 Element";
			this.chkInt32.UseVisualStyleBackColor = true;
			// 
			// chkUInt64
			// 
			this.chkUInt64.AutoSize = true;
			this.chkUInt64.Location = new System.Drawing.Point(203, 42);
			this.chkUInt64.Name = "chkUInt64";
			this.chkUInt64.Size = new System.Drawing.Size(99, 17);
			this.chkUInt64.TabIndex = 8;
			this.chkUInt64.Text = "UInt64 Element";
			this.chkUInt64.UseVisualStyleBackColor = true;
			// 
			// chkSingle
			// 
			this.chkSingle.AutoSize = true;
			this.chkSingle.Location = new System.Drawing.Point(6, 134);
			this.chkSingle.Name = "chkSingle";
			this.chkSingle.Size = new System.Drawing.Size(82, 17);
			this.chkSingle.TabIndex = 9;
			this.chkSingle.Text = "Single Array";
			this.chkSingle.UseVisualStyleBackColor = true;
			// 
			// chkInt16
			// 
			this.chkInt16.AutoSize = true;
			this.chkInt16.Location = new System.Drawing.Point(103, 65);
			this.chkInt16.Name = "chkInt16";
			this.chkInt16.Size = new System.Drawing.Size(91, 17);
			this.chkInt16.TabIndex = 3;
			this.chkInt16.Text = "Int16 Element";
			this.chkInt16.UseVisualStyleBackColor = true;
			// 
			// chkUInt8
			// 
			this.chkUInt8.AutoSize = true;
			this.chkUInt8.Location = new System.Drawing.Point(103, 42);
			this.chkUInt8.Name = "chkUInt8";
			this.chkUInt8.Size = new System.Drawing.Size(93, 17);
			this.chkUInt8.TabIndex = 2;
			this.chkUInt8.Text = "UInt8 Element";
			this.chkUInt8.UseVisualStyleBackColor = true;
			// 
			// chkInt64
			// 
			this.chkInt64.AutoSize = true;
			this.chkInt64.Location = new System.Drawing.Point(203, 17);
			this.chkInt64.Name = "chkInt64";
			this.chkInt64.Size = new System.Drawing.Size(91, 17);
			this.chkInt64.TabIndex = 7;
			this.chkInt64.Text = "Int64 Element";
			this.chkInt64.UseVisualStyleBackColor = true;
			// 
			// chkInt8
			// 
			this.chkInt8.AutoSize = true;
			this.chkInt8.Location = new System.Drawing.Point(103, 19);
			this.chkInt8.Name = "chkInt8";
			this.chkInt8.Size = new System.Drawing.Size(85, 17);
			this.chkInt8.TabIndex = 1;
			this.chkInt8.Text = "Int8 Element";
			this.chkInt8.UseVisualStyleBackColor = true;
			// 
			// chkUInt16
			// 
			this.chkUInt16.AutoSize = true;
			this.chkUInt16.Location = new System.Drawing.Point(103, 88);
			this.chkUInt16.Name = "chkUInt16";
			this.chkUInt16.Size = new System.Drawing.Size(99, 17);
			this.chkUInt16.TabIndex = 4;
			this.chkUInt16.Text = "UInt16 Element";
			this.chkUInt16.UseVisualStyleBackColor = true;
			// 
			// chkUInt32
			// 
			this.chkUInt32.AutoSize = true;
			this.chkUInt32.Location = new System.Drawing.Point(103, 134);
			this.chkUInt32.Name = "chkUInt32";
			this.chkUInt32.Size = new System.Drawing.Size(99, 17);
			this.chkUInt32.TabIndex = 6;
			this.chkUInt32.Text = "UInt32 Element";
			this.chkUInt32.UseVisualStyleBackColor = true;
			// 
			// chkCompress
			// 
			this.chkCompress.AutoSize = true;
			this.chkCompress.Checked = true;
			this.chkCompress.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCompress.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkCompress.Location = new System.Drawing.Point(12, 106);
			this.chkCompress.Name = "chkCompress";
			this.chkCompress.Size = new System.Drawing.Size(108, 17);
			this.chkCompress.TabIndex = 0;
			this.chkCompress.Text = "Use Compression";
			this.chkCompress.UseVisualStyleBackColor = true;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			this.openFileDialog.Filter = "MAT-files|*.mat|All files|*.*";
			this.openFileDialog.Title = "Select a MAT-file";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "MAT-Files|*.mat|All files|*.*";
			// 
			// txtOutput
			// 
			this.txtOutput.AcceptsTab = true;
			this.txtOutput.Location = new System.Drawing.Point(12, 176);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(451, 247);
			this.txtOutput.TabIndex = 5;
			this.txtOutput.Text = "";
			// 
			// btnCheckEmAll
			// 
			this.btnCheckEmAll.Location = new System.Drawing.Point(231, 128);
			this.btnCheckEmAll.Name = "btnCheckEmAll";
			this.btnCheckEmAll.Size = new System.Drawing.Size(101, 23);
			this.btnCheckEmAll.TabIndex = 3;
			this.btnCheckEmAll.Text = "(Un)Check All";
			this.btnCheckEmAll.UseVisualStyleBackColor = true;
			this.btnCheckEmAll.Click += new System.EventHandler(this.btnCheckEmAll_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(475, 435);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.btnRead);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkCompress);
			this.Name = "Main";
			this.Text = "CSMatIOTest";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkCompress;
        private System.Windows.Forms.CheckBox chkUInt16;
        private System.Windows.Forms.CheckBox chkInt16;
        private System.Windows.Forms.CheckBox chkUInt8;
        private System.Windows.Forms.CheckBox chkInt8;
        private System.Windows.Forms.CheckBox chkSingle;
        private System.Windows.Forms.CheckBox chkUInt64;
        private System.Windows.Forms.CheckBox chkInt64;
        private System.Windows.Forms.CheckBox chkUInt32;
        private System.Windows.Forms.CheckBox chkInt32;
        private System.Windows.Forms.CheckBox chkCell;
        private System.Windows.Forms.CheckBox chkStruct;
        private System.Windows.Forms.CheckBox chkChar;
        private System.Windows.Forms.CheckBox chkDouble;
        private System.Windows.Forms.CheckBox chkSparse;
        private System.Windows.Forms.CheckBox chkImagMatrix;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.RichTextBox txtOutput;
		private System.Windows.Forms.Button btnCheckEmAll;
    }
}

