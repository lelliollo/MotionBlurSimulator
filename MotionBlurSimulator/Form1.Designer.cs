namespace MotionBlurSimulator
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.SaveImMatrixButton = new System.Windows.Forms.Button();
            this.ComputeMBButton = new System.Windows.Forms.Button();
            this.SaveBlrdImgButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.MB_W_Input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MB_alpha_input = new System.Windows.Forms.TextBox();
            this.ParallelSelect = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SelectParThNumb = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.OrigImPicBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BlurdImgPicBox = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.NotificationTextBox = new System.Windows.Forms.Label();
            this.LoadImageDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectParThNumb)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrigImPicBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlurdImgPicBox)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 455);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.LoadImageButton);
            this.flowLayoutPanel1.Controls.Add(this.SaveImMatrixButton);
            this.flowLayoutPanel1.Controls.Add(this.ComputeMBButton);
            this.flowLayoutPanel1.Controls.Add(this.SaveBlrdImgButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(188, 202);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(3, 3);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(182, 23);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load Image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // SaveImMatrixButton
            // 
            this.SaveImMatrixButton.Location = new System.Drawing.Point(3, 32);
            this.SaveImMatrixButton.Name = "SaveImMatrixButton";
            this.SaveImMatrixButton.Size = new System.Drawing.Size(182, 23);
            this.SaveImMatrixButton.TabIndex = 1;
            this.SaveImMatrixButton.Text = "Save image matrix to NetCDF";
            this.SaveImMatrixButton.UseVisualStyleBackColor = true;
            this.SaveImMatrixButton.Click += new System.EventHandler(this.SaveImMatrixButton_Click);
            // 
            // ComputeMBButton
            // 
            this.ComputeMBButton.Location = new System.Drawing.Point(3, 61);
            this.ComputeMBButton.Name = "ComputeMBButton";
            this.ComputeMBButton.Size = new System.Drawing.Size(182, 23);
            this.ComputeMBButton.TabIndex = 2;
            this.ComputeMBButton.Text = "Compute motion blur simulation";
            this.ComputeMBButton.UseVisualStyleBackColor = true;
            this.ComputeMBButton.Click += new System.EventHandler(this.ComputeMBButton_Click);
            // 
            // SaveBlrdImgButton
            // 
            this.SaveBlrdImgButton.Location = new System.Drawing.Point(3, 90);
            this.SaveBlrdImgButton.Name = "SaveBlrdImgButton";
            this.SaveBlrdImgButton.Size = new System.Drawing.Size(182, 23);
            this.SaveBlrdImgButton.TabIndex = 3;
            this.SaveBlrdImgButton.Text = "Save blurred image";
            this.SaveBlrdImgButton.UseVisualStyleBackColor = true;
            this.SaveBlrdImgButton.Click += new System.EventHandler(this.SaveBlrdImgButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 222);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motion Blur Simulation Parameters";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.MB_W_Input);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.MB_alpha_input);
            this.flowLayoutPanel2.Controls.Add(this.ParallelSelect);
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.SelectParThNumb);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(188, 203);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Motion blur width [px]";
            // 
            // MB_W_Input
            // 
            this.MB_W_Input.Location = new System.Drawing.Point(3, 16);
            this.MB_W_Input.Name = "MB_W_Input";
            this.MB_W_Input.Size = new System.Drawing.Size(182, 20);
            this.MB_W_Input.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Motion blur direction angle [deg]";
            // 
            // MB_alpha_input
            // 
            this.MB_alpha_input.Location = new System.Drawing.Point(3, 55);
            this.MB_alpha_input.Name = "MB_alpha_input";
            this.MB_alpha_input.Size = new System.Drawing.Size(182, 20);
            this.MB_alpha_input.TabIndex = 3;
            // 
            // ParallelSelect
            // 
            this.ParallelSelect.AutoSize = true;
            this.ParallelSelect.Location = new System.Drawing.Point(3, 81);
            this.ParallelSelect.Name = "ParallelSelect";
            this.ParallelSelect.Size = new System.Drawing.Size(132, 17);
            this.ParallelSelect.TabIndex = 6;
            this.ParallelSelect.Text = "Parallelize Convolution";
            this.ParallelSelect.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parallel Threads";
            // 
            // SelectParThNumb
            // 
            this.SelectParThNumb.Location = new System.Drawing.Point(3, 117);
            this.SelectParThNumb.Name = "SelectParThNumb";
            this.SelectParThNumb.Size = new System.Drawing.Size(182, 20);
            this.SelectParThNumb.TabIndex = 7;
            this.SelectParThNumb.ValueChanged += new System.EventHandler(this.SelectParThNumb_ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(200, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(827, 455);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.OrigImPicBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(819, 429);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Original Image";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // OrigImPicBox
            // 
            this.OrigImPicBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OrigImPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrigImPicBox.Location = new System.Drawing.Point(3, 3);
            this.OrigImPicBox.Name = "OrigImPicBox";
            this.OrigImPicBox.Size = new System.Drawing.Size(813, 423);
            this.OrigImPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OrigImPicBox.TabIndex = 0;
            this.OrigImPicBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.BlurdImgPicBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(819, 429);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Blurred Image";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BlurdImgPicBox
            // 
            this.BlurdImgPicBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BlurdImgPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlurdImgPicBox.Location = new System.Drawing.Point(3, 3);
            this.BlurdImgPicBox.Name = "BlurdImgPicBox";
            this.BlurdImgPicBox.Size = new System.Drawing.Size(813, 423);
            this.BlurdImgPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BlurdImgPicBox.TabIndex = 1;
            this.BlurdImgPicBox.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.NotificationTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(819, 429);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Program Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // NotificationTextBox
            // 
            this.NotificationTextBox.AutoSize = true;
            this.NotificationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotificationTextBox.Location = new System.Drawing.Point(0, 0);
            this.NotificationTextBox.Name = "NotificationTextBox";
            this.NotificationTextBox.Size = new System.Drawing.Size(35, 13);
            this.NotificationTextBox.TabIndex = 0;
            this.NotificationTextBox.Text = "label1";
            // 
            // LoadImageDlg
            // 
            this.LoadImageDlg.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 455);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Motion Blur Simulator - A.Lavatelli - KU Leuven 2017";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectParThNumb)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OrigImPicBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BlurdImgPicBox)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox OrigImPicBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox BlurdImgPicBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.OpenFileDialog LoadImageDlg;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label NotificationTextBox;
        private System.Windows.Forms.Button SaveImMatrixButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MB_W_Input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MB_alpha_input;
        private System.Windows.Forms.Button ComputeMBButton;
        private System.Windows.Forms.Button SaveBlrdImgButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox ParallelSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown SelectParThNumb;
    }
}

