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
            VTextBox氏名.ErrorProvider = this.ErrorProvider;
            VTextBox氏名.Required = true;     // 必須
            VTextBox氏名.MaxLength = 10;

            // 氏名（カナ）
            VTextBox氏名カナ.ErrorProvider = this.ErrorProvider;
            VTextBox氏名カナ.Required = true;
            VTextBox氏名カナ.MaxLength = 10;    // 必須

            VTextBox氏名カナ.Validator = s =>
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
            VTextBoxメールアドレス.Required = true;    // 必須
            VTextBoxメールアドレス.MaxLength = 100;

            VTextBoxメールアドレス.Validator = s =>
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(s);
                    return (addr.Address == s)
                        ? (true, string.Empty)
                        : (false, "メールアドレスの形式が正しくありません。");
                }
                catch
                {
                    return (false, "メールアドレスの形式が正しくありません。");
                }
            };

        }


    }
}
