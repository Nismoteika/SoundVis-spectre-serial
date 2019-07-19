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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sel_device = new System.Windows.Forms.ComboBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_min = new System.Windows.Forms.Label();
            this.txt_max = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.enable_serial = new System.Windows.Forms.CheckBox();
            this.sel_serial = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sel_device
            // 
            this.sel_device.FormattingEnabled = true;
            this.sel_device.Location = new System.Drawing.Point(40, 24);
            this.sel_device.Name = "sel_device";
            this.sel_device.Size = new System.Drawing.Size(328, 21);
            this.sel_device.TabIndex = 0;
            this.sel_device.SelectedIndexChanged += new System.EventHandler(this.Sel_device_SelectedIndexChanged);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(40, 56);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(240, 23);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(40, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 144);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // txt_min
            // 
            this.txt_min.AutoSize = true;
            this.txt_min.Location = new System.Drawing.Point(320, 104);
            this.txt_min.Name = "txt_min";
            this.txt_min.Size = new System.Drawing.Size(23, 13);
            this.txt_min.TabIndex = 7;
            this.txt_min.Text = "min";
            // 
            // txt_max
            // 
            this.txt_max.AutoSize = true;
            this.txt_max.Location = new System.Drawing.Point(320, 128);
            this.txt_max.Name = "txt_max";
            this.txt_max.Size = new System.Drawing.Size(26, 13);
            this.txt_max.TabIndex = 8;
            this.txt_max.Text = "max";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(296, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "print arr";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // enable_serial
            // 
            this.enable_serial.AutoSize = true;
            this.enable_serial.Location = new System.Drawing.Point(304, 152);
            this.enable_serial.Name = "enable_serial";
            this.enable_serial.Size = new System.Drawing.Size(85, 17);
            this.enable_serial.TabIndex = 10;
            this.enable_serial.Text = "enable serial";
            this.enable_serial.UseVisualStyleBackColor = true;
            this.enable_serial.CheckedChanged += new System.EventHandler(this.Enable_serial_CheckedChanged);
            // 
            // sel_serial
            // 
            this.sel_serial.FormattingEnabled = true;
            this.sel_serial.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.sel_serial.Location = new System.Drawing.Point(304, 176);
            this.sel_serial.Name = "sel_serial";
            this.sel_serial.Size = new System.Drawing.Size(88, 21);
            this.sel_serial.TabIndex = 11;
            this.sel_serial.SelectedIndexChanged += new System.EventHandler(this.Sel_serial_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 261);
            this.Controls.Add(this.sel_serial);
            this.Controls.Add(this.enable_serial);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_max);
            this.Controls.Add(this.txt_min);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.sel_device);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisibleSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox sel_device;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txt_min;
        private System.Windows.Forms.Label txt_max;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox enable_serial;
        private System.Windows.Forms.ComboBox sel_serial;
    }
}

