using System.ComponentModel;

namespace CustomTextbox
{
    /// <summary>
    /// 基本仕様：
    /// １．フォーカスを受けたときボーダーラインを強調し青い太枠へ変える
    /// ２．フォーカスを失った時にボーダーラインを元に戻す
    /// ３．フォーカスを受けたとき、文字が入力されていたら全選択する
    /// ４．エンターキーで次のタブインデックスへ遷移する
    /// ５．そのテキストボックスの入力値のチェックメソッドを受け取れる。また、文字列の最大長をプロパティに持つ。
    /// ６．チェックメソッドや文字列長に違反した場合、フォーカスを失った時にボーダーラインを太い赤色にする
    /// </summary>
    public class ValidatingTextBox : TextBox
    {
        [Category("データ")]
        [Description("必須項目はTrueにしてください")]
        [DefaultValue(false)]
        public bool Required { get; set; } = false;
        
        [Category("外観")]
        [Description("外枠の通常色です")]
        [DefaultValue(typeof(Color), "Gray")]
        public Color 通常色 { get; set; } = Color.Gray;

        [Category("外観")]
        [Description("フォーカスを受けた時の外枠の色です")]
        [DefaultValue(typeof(Color), "DeepSkyBlue")]
        public Color フォーカス色 { get; set; } = Color.DeepSkyBlue;

        [Category("外観")]
        [Description("エラー時の外枠の色です")]
        [DefaultValue(typeof(Color), "Red")]
        public Color エラー色 { get; set; } = Color.Red;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ErrorProvider? ErrorProvider { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<string, (bool isValid, string errorMessage)>? Validator { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid => _isValid;

        private bool _hasFocus = false;
        private bool _isValid = true;

        public ValidatingTextBox()
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnEnter(EventArgs e)
        {
            _hasFocus = true;
            Invalidate();

            if (!string.IsNullOrEmpty(this.Text))
            {
                // ３．フォーカスを受けたとき、文字が入力されていたら全選択する
                this.SelectAll(); // 全選択
            }

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            _hasFocus = false;
            _isValid = ValidateInput();
            Invalidate();
            base.OnLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // ４．エンターキーで次のタブインデックスへ遷移する
                this.Parent?.SelectNextControl(this, true, true, true, true);
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0x000F;
            if (m.Msg == WM_PAINT)
            {
                using Graphics g = this.CreateGraphics();
                // １．フォーカスを受けたときボーダーラインを強調し青い太枠へ変える
                Color borderColor = 通常色;

                // ２．フォーカスを失った時にボーダーラインを元に戻す
                if (_hasFocus)
                    borderColor = フォーカス色;
                // ６．チェックメソッドや文字列長に違反した場合、フォーカスを失った時にボーダーラインを太い赤色にする
                else if (!_isValid)
                    borderColor = エラー色;

                using Pen pen = new Pen(borderColor, 2);
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                g.DrawRectangle(pen, rect);
            }
        }

        private bool ValidateInput()
        {
            // ５．そのテキストボックスの入力値のチェックメソッドを受け取れる。また、文字列の最大長をプロパティに持つ。
            var errorMessage = string.Empty;

            if (Required && string.IsNullOrWhiteSpace(this.Text))
            {
                errorMessage = "必須入力です。";
                ErrorProvider?.SetError(this, errorMessage);
                return false;
            }

            if (this.Text.Length > MaxLength)
            {
                errorMessage = $"最大文字数({MaxLength})を超えています。";
                ErrorProvider?.SetError(this, errorMessage);
                return false;
            }

            if (Validator != null)
            {
                var result = Validator(this.Text);
                if (!result.isValid)
                {
                    errorMessage = result.errorMessage;
                    ErrorProvider?.SetError(this, errorMessage);
                    return false;
                }
            }

            ErrorProvider?.SetError(this, string.Empty);
            return true;
        }
    }
}
