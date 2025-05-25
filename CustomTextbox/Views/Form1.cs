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

        // �e�L�X�g�{�b�N�X�̃`�F�b�N���e���`����B
        private void SetValidators()
        {
            // �����i�����j
            VTextBox��������.ErrorProvider = this.ErrorProvider;
            VTextBox��������.BindValidationToModelProperty(typeof(�ڋqModel), nameof(�ڋqModel.��������));

            // �����i�J�i�j
            VTextBox�����J�i.ErrorProvider = this.ErrorProvider;
            VTextBox�����J�i.BindValidationToModelProperty(typeof(�ڋqModel), nameof(�ڋqModel.�����J�i));

            VTextBox�����J�i.InputValidator = s =>
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
            VTextBox���[���A�h���X.BindValidationToModelProperty(typeof(�ڋqModel), nameof(�ڋqModel.���[���A�h���X));

            // ���N����
            VDateTimePicker���N����.ErrorProvider = this.ErrorProvider;
            VDateTimePicker���N����.Format = DateTimePickerFormat.Custom;
            VDateTimePicker���N����.CustomFormat = "yyyy/MM/dd";
            VDateTimePicker���N����.BindValidationToModelProperty(typeof(�ڋqModel), nameof(�ڋqModel.���N����));

            // �N��
            NVTextBox�N��.ErrorProvider = this.ErrorProvider;
            NVTextBox�N��.BindValidationToModelProperty(typeof(�ڋqModel), nameof(�ڋqModel.�N��));
            NVTextBox�N��.NumberFormat = "#,##0";
        }
    }
}
