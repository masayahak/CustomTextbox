using CustomTextbox.Models;

namespace CustomTextbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetValidators();
        }

        // テキストボックスのチェック内容を定義する。
        private void SetValidators()
        {
            // 氏名（漢字）
            VTextBox氏名漢字.ErrorProvider = this.ErrorProvider;
            VTextBox氏名漢字.BindValidationToModelProperty(typeof(顧客Model), nameof(顧客Model.氏名漢字));

            // 氏名（カナ）
            VTextBox氏名カナ.ErrorProvider = this.ErrorProvider;
            VTextBox氏名カナ.BindValidationToModelProperty(typeof(顧客Model), nameof(顧客Model.氏名カナ));

            VTextBox氏名カナ.InputValidator = s =>
            {
                foreach (char c in s)
                {
                    if (!((c >= '\u30A0' && c <= '\u30FF') || c == '\u3000'))
                        return (false, "全角カタカナまたは全角スペースのみを入力してください。");
                }
                return (true, string.Empty);
            };

            // メールアドレス
            VTextBoxメールアドレス.ErrorProvider = this.ErrorProvider;
            VTextBoxメールアドレス.BindValidationToModelProperty(typeof(顧客Model), nameof(顧客Model.メールアドレス));

            // 生年月日
            VDateTimePicker生年月日.ErrorProvider = this.ErrorProvider;
            VDateTimePicker生年月日.Format = DateTimePickerFormat.Custom;
            VDateTimePicker生年月日.CustomFormat = "yyyy/MM/dd";
            VDateTimePicker生年月日.BindValidationToModelProperty(typeof(顧客Model), nameof(顧客Model.生年月日));

            // 年収
            NVTextBox年収.ErrorProvider = this.ErrorProvider;
            NVTextBox年収.BindValidationToModelProperty(typeof(顧客Model), nameof(顧客Model.年収));
            NVTextBox年収.NumberFormat = "#,##0";
        }
    }
}
