using CustomTextbox.Contols;

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
            ErrorProvider = new ErrorProvider(components);
            VTextBox氏名漢字 = new ValidatingTextBox();
            VTextBox氏名カナ = new ValidatingTextBox();
            VTextBoxメールアドレス = new ValidatingTextBox();
            NVTextBox年収 = new NumericValidatingTextBox();
            label4 = new Label();
            label5 = new Label();
            VDateTimePicker生年月日 = new ValidatingDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)ErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 27);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 3;
            label1.Text = "氏名";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 57);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 4;
            label2.Text = "氏名（カナ）";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 86);
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
            button検証.Location = new Point(255, 295);
            button検証.Name = "button検証";
            button検証.Size = new Size(75, 31);
            button検証.TabIndex = 6;
            button検証.TabStop = false;
            button検証.Text = "実行";
            button検証.UseVisualStyleBackColor = false;
            // 
            // ErrorProvider
            // 
            ErrorProvider.ContainerControl = this;
            // 
            // VTextBox氏名漢字
            // 
            VTextBox氏名漢字.BorderStyle = BorderStyle.FixedSingle;
            VTextBox氏名漢字.ImeMode = ImeMode.On;
            VTextBox氏名漢字.Location = new Point(93, 25);
            VTextBox氏名漢字.Name = "VTextBox氏名漢字";
            VTextBox氏名漢字.Size = new Size(222, 23);
            VTextBox氏名漢字.TabIndex = 1;
            // 
            // VTextBox氏名カナ
            // 
            VTextBox氏名カナ.BorderStyle = BorderStyle.FixedSingle;
            VTextBox氏名カナ.ImeMode = ImeMode.On;
            VTextBox氏名カナ.Location = new Point(93, 55);
            VTextBox氏名カナ.Name = "VTextBox氏名カナ";
            VTextBox氏名カナ.Size = new Size(222, 23);
            VTextBox氏名カナ.TabIndex = 2;
            // 
            // VTextBoxメールアドレス
            // 
            VTextBoxメールアドレス.BorderStyle = BorderStyle.FixedSingle;
            VTextBoxメールアドレス.ImeMode = ImeMode.Off;
            VTextBoxメールアドレス.Location = new Point(93, 84);
            VTextBoxメールアドレス.Name = "VTextBoxメールアドレス";
            VTextBoxメールアドレス.Size = new Size(222, 23);
            VTextBoxメールアドレス.TabIndex = 3;
            // 
            // NVTextBox年収
            // 
            NVTextBox年収.BorderStyle = BorderStyle.FixedSingle;
            NVTextBox年収.ImeMode = ImeMode.Off;
            NVTextBox年収.Location = new Point(93, 159);
            NVTextBox年収.Name = "NVTextBox年収";
            NVTextBox年収.Size = new Size(100, 23);
            NVTextBox年収.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 161);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 12;
            label4.Text = "年収";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 132);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 13;
            label5.Text = "生年月日";
            // 
            // VDateTimePicker生年月日
            // 
            VDateTimePicker生年月日.ImeMode = ImeMode.Off;
            VDateTimePicker生年月日.Location = new Point(93, 126);
            VDateTimePicker生年月日.Name = "VDateTimePicker生年月日";
            VDateTimePicker生年月日.Size = new Size(138, 23);
            VDateTimePicker生年月日.TabIndex = 4;
            VDateTimePicker生年月日.Value = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 338);
            Controls.Add(VDateTimePicker生年月日);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(NVTextBox年収);
            Controls.Add(VTextBoxメールアドレス);
            Controls.Add(VTextBox氏名カナ);
            Controls.Add(VTextBox氏名漢字);
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
        private ValidatingTextBox VTextBox氏名漢字;
        private ValidatingTextBox VTextBox氏名カナ;
        private ValidatingTextBox VTextBoxメールアドレス;
        private NumericValidatingTextBox NVTextBox年収;
        private ErrorProvider ErrorProvider;
        private Label label5;
        private Label label4;
        private ValidatingDateTimePicker VDateTimePicker生年月日;
    }
}
