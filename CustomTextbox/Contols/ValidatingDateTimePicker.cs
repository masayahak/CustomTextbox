using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CustomTextbox.Contols
{
    /// <summary>
    /// 基本仕様：
    /// １．フォーカスを受けたときボーダーラインを強調し青い太枠へ変える
    /// ２．フォーカスを失った時にボーダーラインを元に戻す
    /// ３．×××××××挙動不安定により禁止（フォーカスを受けたとき、文字が入力されていたら全選択する）
    /// ４．エンターキーで次のタブインデックスへ遷移する
    /// ５．Modelのプロパティのデータアノテーションから、チェック内容を受け取れる。
    /// ６．チェックメソッドや文字列長に違反した場合、フォーカスを失った時にボーダーラインを太い赤色にする
    /// </summary>
    public class ValidatingDateTimePicker : DateTimePicker
    {
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
        public bool IsValid { get; private set; } = true;

        private bool _hasFocus = false;
        private Func<DateTime, (bool isValid, string errorMessage)>? _validator;

        public ValidatingDateTimePicker()
        {
        }

        protected override void OnEnter(EventArgs e)
        {
            _hasFocus = true;
            Invalidate();

            // ３．フォーカスを受けたとき、文字が入力されていたら全選択する
            // 全選択（DateTimePickerはキーボードフォーカスで文字選択不可なため SendKeys を使う）
            //BeginInvoke((System.Windows.Forms.MethodInvoker)(() => SendKeys.SendWait("{HOME}+{END}")));

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            _hasFocus = false;
            IsValid = ValidateInput();
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

        // フォーカスあり／なし／エラー状態で外枠の色を変える
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
                else if (!IsValid)
                    borderColor = エラー色;

                using Pen pen = new Pen(borderColor, borderColor != 通常色 ? 2 : 1);
                Rectangle rect = new Rectangle(1, 1, this.Width - 3, this.Height - 3);
                g.DrawRectangle(pen, rect);
            }
        }

        // ５．Modelのプロパティのデータアノテーションから、チェック内容を受け取れる。
        public void BindValidationToModelProperty(Type modelType, string propertyName)
        {
            var prop = modelType.GetProperty(propertyName);
            if (prop == null) return;

            // Requiredの有無
            var required = prop.GetCustomAttribute<RequiredAttribute>();

            // 値の範囲
            var range = prop.GetCustomAttribute<RangeAttribute>();

            _validator = value =>
            {
                if (required != null && value == DateTime.MinValue)
                    return (false, required.ErrorMessage ?? "日付の入力は必須です");

                if (range != null)
                {
                    if (DateTime.TryParse(range.Minimum?.ToString(), out DateTime min) &&
                        DateTime.TryParse(range.Maximum?.ToString(), out DateTime max))
                    {
                        if (value < min || value > max)
                            return (false, range.ErrorMessage ?? $"日付は {min.ToShortDateString()} ～ {max.ToShortDateString()} の範囲で入力してください。");
                    }
                }

                return (true, string.Empty);
            };
        }

        private bool ValidateInput()
        {
            if (_validator != null)
            {
                var result = _validator(this.Value);
                if (!result.isValid)
                {
                    ErrorProvider?.SetError(this, result.errorMessage);
                    return false;
                }
            }

            ErrorProvider?.SetError(this, string.Empty);
            return true;
        }
    }
}
