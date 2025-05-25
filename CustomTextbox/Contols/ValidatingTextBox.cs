using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CustomTextbox.Contols
{
    /// <summary>
    /// 基本仕様：
    /// １．フォーカスを受けたときボーダーラインを強調し青い太枠へ変える
    /// ２．フォーカスを失った時にボーダーラインを元に戻す
    /// ３．フォーカスを受けたとき、文字が入力されていたら全選択する
    /// ４．エンターキーで次のタブインデックスへ遷移する
    /// ５．Modelのプロパティのデータアノテーションから、チェック内容を受け取れる。
    /// ６．そのテキストボックスの入力値のチェックメソッドを受け取れる。また、文字列の最大長をプロパティに持つ。
    /// ７．チェックメソッドや文字列長に違反した場合、フォーカスを失った時にボーダーラインを太い赤色にする
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

        //バインドするModleのプロパティから自動生成するValidator
        private Func<string, (bool isValid, string errorMessage)>? _modelValidator;
        // 個別に追加指定するValidator
        private Func<string, (bool isValid, string errorMessage)>? _customValidator;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<string, (bool isValid, string errorMessage)>? InputValidator
        {
            get => _combinedValidator;
            set
            {
                _customValidator = value;
                UpdateCombinedValidator();
            }
        }

        // モデルからのValidatorと、個別追加Validator ２つを合成した最終的なValidator
        private Func<string, (bool isValid, string errorMessage)>? _combinedValidator;
        private void UpdateCombinedValidator()
        {
            _combinedValidator = CombineValidators();
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid => _isValid;

        internal bool _hasFocus = false;
        private bool _isValid = true;


        public ValidatingTextBox()
        {
            BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnEnter(EventArgs e)
        {
            _hasFocus = true;
            Invalidate();

            if (!string.IsNullOrEmpty(Text))
            {
                // ３．フォーカスを受けたとき、文字が入力されていたら全選択する
                SelectAll(); // 全選択
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
                Parent?.SelectNextControl(this, true, true, true, true);
                e.Handled = true;
                e.SuppressKeyPress = true; // ← ★これでビープ音防止！
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
                using Graphics g = CreateGraphics();
                // １．フォーカスを受けたときボーダーラインを強調し青い太枠へ変える
                Color borderColor = 通常色;

                // ２．フォーカスを失った時にボーダーラインを元に戻す
                if (_hasFocus)
                    borderColor = フォーカス色;
                // ７．チェックメソッドや文字列長に違反した場合、フォーカスを失った時にボーダーラインを太い赤色にする
                else if (!_isValid)
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
            var requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
            Required = requiredAttr != null;

            // 最大長
            var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttr != null)
            {
                MaxLength = maxLengthAttr.Length;
            }

            // その他のValidation
            _modelValidator = s =>
            {
                // 空文字は Validator に流さず、Required で判定
                if (string.IsNullOrWhiteSpace(s)) return (true, string.Empty);

                // 型変換チェック
                var prop = modelType.GetProperty(propertyName);
                if (prop == null) return (true, string.Empty);

                var targetType = prop.PropertyType;

                // TryParse 相当の安全な型変換
                object? typedValue;
                try
                {
                    typedValue = TypeDescriptor.GetConverter(targetType).ConvertFromString(s);
                }
                catch (Exception ex)
                {
                    return (false, $"形式が正しくありません: {ex.Message}");
                }


                var dummyInstance = Activator.CreateInstance(modelType) ?? 
                    throw new InvalidOperationException("モデルのインスタンス生成に失敗しました");
                var validationContext = new ValidationContext(dummyInstance) 
                { 
                    MemberName = propertyName 
                };

                var results = new List<ValidationResult>();
                bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateProperty(typedValue, validationContext, results);

                if (isValid)
                    return (true, string.Empty);
                else
                    return (false, results[0].ErrorMessage ?? "無効な入力です");
            };
        }

        // モデルからの属性チェックと個別チェックを組み合わせる
        // （両方のチェックを有効にする）
        private Func<string, (bool isValid, string errorMessage)>? CombineValidators()
        {
            if (_customValidator == null && _modelValidator == null)
                return null;

            return s =>
            {
                if (_modelValidator != null)
                {
                    var result = _modelValidator(s);
                    if (!result.isValid) return result;
                }

                if (_customValidator != null)
                {
                    var result = _customValidator(s);
                    if (!result.isValid) return result;
                }

                return (true, string.Empty);
            };
        }

        // Validation実行
        private bool ValidateInput()
        {
            // ６．そのテキストボックスの入力値のチェックメソッドを受け取れる。また、文字列の最大長をプロパティに持つ。
            var errorMessage = string.Empty;

            if (Required && string.IsNullOrWhiteSpace(Text))
            {
                errorMessage = "必須入力です。";
                ErrorProvider?.SetError(this, errorMessage);
                return false;
            }

            if (InputValidator != null)
            {
                var result = InputValidator(Text);
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
