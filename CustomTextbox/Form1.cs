namespace CustomTextbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetValidators();
        }

        // �e�L�X�g�{�b�N�X�̃`�F�b�N���e���`����B
        private void SetValidators()
        {
            // �����i�����j
            VTextBox����.ErrorProvider = this.ErrorProvider;
            VTextBox����.Required = true;     // �K�{
            VTextBox����.MaxLength = 10;

            // �����i�J�i�j
            VTextBox�����J�i.ErrorProvider = this.ErrorProvider;
            VTextBox�����J�i.Required = true;
            VTextBox�����J�i.MaxLength = 10;    // �K�{

            VTextBox�����J�i.Validator = s =>
            {
                foreach (char c in s)
                {
                    if (!((c >= '\u30A0' && c <= '\u30FF') || c == '\u3000'))
                        return (false, "�S�p�J�^�J�i�܂��͑S�p�X�y�[�X�݂̂���͂��Ă��������B");
                }
                return (true, string.Empty);
            };

            // ���[���A�h���X
            VTextBox���[���A�h���X.ErrorProvider = this.ErrorProvider;
            VTextBox���[���A�h���X.Required = true;    // �K�{
            VTextBox���[���A�h���X.MaxLength = 100;

            VTextBox���[���A�h���X.Validator = s =>
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(s);
                    return (addr.Address == s)
                        ? (true, string.Empty)
                        : (false, "���[���A�h���X�̌`��������������܂���B");
                }
                catch
                {
                    return (false, "���[���A�h���X�̌`��������������܂���B");
                }
            };

        }


    }
}
