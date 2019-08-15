namespace SoundVisualizer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sel_device = new System.Windows.Forms.ComboBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.enable_serial = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.num_cols = new System.Windows.Forms.NumericUpDown();
            this.num_rows = new System.Windows.Forms.NumericUpDown();
            this.sel_fft = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sel_serial = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_cols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_rows)).BeginInit();
            this.SuspendLayout();
            // 
            // sel_device
            // 
            this.sel_device.FormattingEnabled = true;
            this.sel_device.Location = new System.Drawing.Point(24, 80);
            this.sel_device.Name = "sel_device";
            this.sel_device.Size = new System.Drawing.Size(352, 21);
            this.sel_device.TabIndex = 4;
            this.sel_device.SelectedIndexChanged += new System.EventHandler(this.Sel_device_SelectedIndexChanged);
            // 
            // btn_start
            // 
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_start.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_start.Location = new System.Drawing.Point(24, 112);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(352, 32);
            this.btn_start.TabIndex = 5;
            this.btn_start.Text = "Visualize";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(24, 152);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 144);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // enable_serial
            // 
            this.enable_serial.AutoSize = true;
            this.enable_serial.Location = new System.Drawing.Point(288, 248);
            this.enable_serial.Name = "enable_serial";
            this.enable_serial.Size = new System.Drawing.Size(85, 17);
            this.enable_serial.TabIndex = 7;
            this.enable_serial.Text = "enable serial";
            this.enable_serial.UseVisualStyleBackColor = true;
            this.enable_serial.CheckedChanged += new System.EventHandler(this.Enable_serial_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sound Visualizer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // num_cols
            // 
            this.num_cols.Location = new System.Drawing.Point(152, 40);
            this.num_cols.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.num_cols.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_cols.Name = "num_cols";
            this.num_cols.Size = new System.Drawing.Size(96, 20);
            this.num_cols.TabIndex = 2;
            this.num_cols.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_rows
            // 
            this.num_rows.Location = new System.Drawing.Point(272, 40);
            this.num_rows.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.num_rows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_rows.Name = "num_rows";
            this.num_rows.Size = new System.Drawing.Size(104, 20);
            this.num_rows.TabIndex = 3;
            this.num_rows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // sel_fft
            // 
            this.sel_fft.FormattingEnabled = true;
            this.sel_fft.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048"});
            this.sel_fft.Location = new System.Drawing.Point(24, 40);
            this.sel_fft.Name = "sel_fft";
            this.sel_fft.Size = new System.Drawing.Size(104, 21);
            this.sel_fft.TabIndex = 1;
            this.sel_fft.SelectedIndexChanged += new System.EventHandler(this.Sel_fft_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "FFT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Столбцы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(304, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Строки";
            // 
            // sel_serial
            // 
            this.sel_serial.FormattingEnabled = true;
            this.sel_serial.Items.AddRange(new object[] {
            "COM0",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.sel_serial.Location = new System.Drawing.Point(288, 272);
            this.sel_serial.Name = "sel_serial";
            this.sel_serial.Size = new System.Drawing.Size(88, 21);
            this.sel_serial.TabIndex = 8;
            this.sel_serial.SelectedIndexChanged += new System.EventHandler(this.Sel_serial_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Устройство воспроизведения";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 318);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sel_serial);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sel_fft);
            this.Controls.Add(this.num_rows);
            this.Controls.Add(this.num_cols);
            this.Controls.Add(this.enable_serial);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.sel_device);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisibleSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_cols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_rows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox enable_serial;
        public System.Windows.Forms.ComboBox sel_device;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.NumericUpDown num_cols;
        private System.Windows.Forms.NumericUpDown num_rows;
        private System.Windows.Forms.ComboBox sel_fft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox sel_serial;
        private System.Windows.Forms.Label label4;
    }
}

