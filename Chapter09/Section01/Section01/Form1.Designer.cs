namespace Section01 {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            ofdOpen = new OpenFileDialog();
            btGet = new Button();
            dtpDate = new DateTimePicker();
            tbOut = new TextBox();
            nudDay = new NumericUpDown();
            dtpBirth = new DateTimePicker();
            btBirthCalc = new Button();
            label1 = new Label();
            label2 = new Label();
            tbOut2 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudDay).BeginInit();
            SuspendLayout();
            // 
            // ofdOpen
            // 
            ofdOpen.FileName = "openFileDialog1";
            // 
            // btGet
            // 
            btGet.Location = new Point(342, 39);
            btGet.Name = "btGet";
            btGet.Size = new Size(156, 91);
            btGet.TabIndex = 0;
            btGet.Text = "計算";
            btGet.UseVisualStyleBackColor = true;
            btGet.Click += btGet_Click;
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            dtpDate.Location = new Point(89, 58);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 39);
            dtpDate.TabIndex = 1;
            // 
            // tbOut
            // 
            tbOut.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbOut.Location = new Point(144, 353);
            tbOut.Name = "tbOut";
            tbOut.Size = new Size(213, 39);
            tbOut.TabIndex = 2;
            // 
            // nudDay
            // 
            nudDay.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            nudDay.Location = new Point(117, 135);
            nudDay.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudDay.Name = "nudDay";
            nudDay.Size = new Size(120, 39);
            nudDay.TabIndex = 3;
            // 
            // dtpBirth
            // 
            dtpBirth.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            dtpBirth.Location = new Point(91, 284);
            dtpBirth.Name = "dtpBirth";
            dtpBirth.Size = new Size(200, 39);
            dtpBirth.TabIndex = 4;
            // 
            // btBirthCalc
            // 
            btBirthCalc.Location = new Point(358, 285);
            btBirthCalc.Name = "btBirthCalc";
            btBirthCalc.Size = new Size(116, 38);
            btBirthCalc.TabIndex = 5;
            btBirthCalc.Text = "計算";
            btBirthCalc.UseVisualStyleBackColor = true;
            btBirthCalc.Click += btBirthCalc_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(89, 256);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 6;
            label1.Text = "生年月日";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(260, 143);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 7;
            label2.Text = "日後";
            // 
            // tbOut2
            // 
            tbOut2.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbOut2.Location = new Point(144, 417);
            tbOut2.Name = "tbOut2";
            tbOut2.Size = new Size(206, 39);
            tbOut2.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(107, 370);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 9;
            label3.Text = "年齢";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(83, 434);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 10;
            label4.Text = "経過日数";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1069, 633);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(tbOut2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btBirthCalc);
            Controls.Add(dtpBirth);
            Controls.Add(nudDay);
            Controls.Add(tbOut);
            Controls.Add(dtpDate);
            Controls.Add(btGet);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)nudDay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog ofdOpen;
        private Button btGet;
        private DateTimePicker dtpDate;
        private TextBox tbOut;
        private NumericUpDown nudDay;
        private DateTimePicker dtpBirth;
        private Button btBirthCalc;
        private Label label1;
        private Label label2;
        private TextBox tbOut2;
        private Label label3;
        private Label label4;
    }
}
