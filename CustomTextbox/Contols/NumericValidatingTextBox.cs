using System.ComponentModel;
using System.Globalization;

namespace CustomTextbox.Contols
{
    public class NumericValidatingTextBox : ValidatingTextBox
    {
        [Category("数値設定")]
        [Description("入力可能な最小値")]
        [DefaultValue(null)]
        public decimal? MinValue { get; set; }

        [Category("数値設定")]
        [Description("入力可能な最大値")]
        [DefaultValue(null)]
        public decimal? MaxValue { get; set; }

        [Category("数値設定")]
        [Description("整数部＋小数部の最大桁数")]
        [DefaultValue(null)]
        public int? MaxDigits { get; set; }

        [Category("数値設定")]
        [Description("小数点以下の最大桁数")]
        [DefaultValue(null)]
        public int? MaxDecimalPlaces { get; set; }

        [Category("数値設定")]
        [Description("マイナス値を許可するか")]
        [DefaultValue(true)]
        public bool AllowNegative { get; set; } = true;

        [Category("数値設定")]
        [Description("0 を許可するか")]
        [DefaultValue(true)]
        public bool AllowZero { get; set; } = true;

        [Category("数値設定")]
        [Description("表示フォーマット（例: N0, N2, 0.000）")]
        [DefaultValue(null)]
        public string? NumberFormat { get; set; }

        public NumericValidatingTextBox()
        {
            InputValidator = ValidateNumber;
        }

        private (bool isValid, string errorMessage) ValidateNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return (true, string.Empty);

            if (!decimal.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal value))
                return (false, "数値として認識できません。");

            if (!AllowNegative && value < 0)
                return (false, "負の数は入力できません。");

            if (!AllowZero && value == 0)
                return (false, "0 は入力できません。");

            if (MinValue.HasValue && value < MinValue.Value)
                return (false, $"最小値 {MinValue.Value} 以上を入力してください。");

            if (MaxValue.HasValue && value > MaxValue.Value)
                return (false, $"最大値 {MaxValue.Value} 以下を入力してください。");

            string[] parts = value.ToString(CultureInfo.InvariantCulture).Split('.');
            int digits = parts[0].TrimStart('-').Length + (parts.Length > 1 ? parts[1].Length : 0);

            if (MaxDigits.HasValue && digits > MaxDigits.Value)
                return (false, $"全体の桁数は最大 {MaxDigits.Value} 桁までです。");

            if (MaxDecimalPlaces.HasValue && parts.Length > 1 && parts[1].Length > MaxDecimalPlaces.Value)
                return (false, $"小数点以下は最大 {MaxDecimalPlaces.Value} 桁までです。");

            return (true, string.Empty);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            _hasFocus = true;
            Invalidate();

            if (!string.IsNullOrWhiteSpace(this.Text) && NumberFormat != null)
            {
                if (decimal.TryParse(this.Text, out decimal value))
                {
                    // 書式を解除して純粋な数値に戻す
                    this.Text = value.ToString("0.################"); // 桁落ち防止
                }

                this.SelectAll();
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (IsValid && !string.IsNullOrWhiteSpace(Text) && NumberFormat != null)
            {
                if (decimal.TryParse(Text, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal value))
                {
                    Text = value.ToString(NumberFormat);
                }
            }
        }
    }
}
