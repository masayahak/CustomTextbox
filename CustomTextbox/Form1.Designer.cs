namespace CustomTextbox
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button検証 = new Button();
            VTextBox氏名 = new ValidatingTextBox();
            VTextBox氏名カナ = new ValidatingTextBox();
            VTextBoxメールアドレス = new ValidatingTextBox();
            ErrorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(104, 108);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 3;
            label1.Text = "氏名";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(104, 158);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 4;
            label2.Text = "氏名（カナ）";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(104, 215);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 5;
            label3.Text = "メールアドレス";
            // 
            // button検証
            // 
            button検証.BackColor = Color.FromArgb(192, 192, 255);
            button検証.FlatAppearance.BorderSize = 0;
            button検証.FlatStyle = FlatStyle.Flat;
            button検証.Location = new Point(196, 276);
            button検証.Name = "button検証";
            button検証.Size = new Size(113, 51);
            button検証.TabIndex = 6;
            button検証.TabStop = false;
            button検証.Text = "検証";
            button検証.UseVisualStyleBackColor = false;
            // 
            // VTextBox氏名
            // 
            VTextBox氏名.BorderStyle = BorderStyle.FixedSingle;
            VTextBox氏名.Location = new Point(196, 100);
            VTextBox氏名.Name = "VTextBox氏名";
            VTextBox氏名.PlaceholderText = "10文字以内";
            VTextBox氏名.Required = true;
            VTextBox氏名.Size = new Size(194, 23);
            VTextBox氏名.TabIndex = 7;
            VTextBox氏名.MaxLength = 10;
            // 
            // VTextBox氏名カナ
            // 
            VTextBox氏名カナ.BorderStyle = BorderStyle.FixedSingle;
            VTextBox氏名カナ.Location = new Point(196, 156);
            VTextBox氏名カナ.Name = "VTextBox氏名カナ";
            VTextBox氏名カナ.PlaceholderText = "10文字以内";
            VTextBox氏名カナ.Required = true;
            VTextBox氏名カナ.Size = new Size(194, 23);
            VTextBox氏名カナ.TabIndex = 8;
            VTextBox氏名カナ.MaxLength = 10;
            // 
            // VTextBoxメールアドレス
            // 
            VTextBoxメールアドレス.BorderStyle = BorderStyle.FixedSingle;
            VTextBoxメールアドレス.Location = new Point(196, 213);
            VTextBoxメールアドレス.Name = "VTextBoxメールアドレス";
            VTextBoxメールアドレス.Required = true;
            VTextBoxメールアドレス.Size = new Size(194, 23);
            VTextBoxメールアドレス.TabIndex = 9;
            // 
            // ErrorProvider
            // 
            ErrorProvider.ContainerControl = this;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(VTextBoxメールアドレス);
            Controls.Add(VTextBox氏名カナ);
            Controls.Add(VTextBox氏名);
            Controls.Add(button検証);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button button検証;
        private ValidatingTextBox VTextBox氏名;
        private ValidatingTextBox VTextBox氏名カナ;
        private ValidatingTextBox VTextBoxメールアドレス;
        private ErrorProvider ErrorProvider;
    }
}
