namespace Exercise01 {
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
            btButton1 = new Button();
            btButton2 = new Button();
            btButton3 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            SuspendLayout();
            // 
            // btButton1
            // 
            btButton1.Location = new Point(72, 49);
            btButton1.Name = "btButton1";
            btButton1.Size = new Size(143, 60);
            btButton1.TabIndex = 0;
            btButton1.Text = "①";
            btButton1.UseVisualStyleBackColor = true;
            // 
            // btButton2
            // 
            btButton2.Location = new Point(72, 153);
            btButton2.Name = "btButton2";
            btButton2.Size = new Size(143, 60);
            btButton2.TabIndex = 0;
            btButton2.Text = "②";
            btButton2.UseVisualStyleBackColor = true;
            // 
            // btButton3
            // 
            btButton3.Location = new Point(72, 256);
            btButton3.Name = "btButton3";
            btButton3.Size = new Size(143, 60);
            btButton3.TabIndex = 0;
            btButton3.Text = "③";
            btButton3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(258, 49);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(516, 56);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(258, 153);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(516, 56);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(258, 256);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(516, 56);
            textBox3.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 367);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(btButton3);
            Controls.Add(btButton2);
            Controls.Add(btButton1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btButton1;
        private Button btButton2;
        private Button btButton3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
    }
}
