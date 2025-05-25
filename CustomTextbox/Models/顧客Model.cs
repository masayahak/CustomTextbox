using System.ComponentModel.DataAnnotations;

namespace CustomTextbox.Models
{
    public class 顧客Model
    {
        [Required(ErrorMessage = "氏名漢字は必須です")]
        [MaxLength(10)]
        public string 氏名漢字 { get; set; } = string.Empty;

        [Required(ErrorMessage = "氏名カナは必須です")]
        [MaxLength(10)]
        public string 氏名カナ { get; set; } = string.Empty;

        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "メールアドレスの形式が正しくありません")]
        [MaxLength(100)]
        public string メールアドレス { get; set; } = string.Empty;

        [Required(ErrorMessage = "生年月日は必須です")]
        [Range(typeof(DateTime), "1900-01-01", "2025-12-31", ErrorMessage = "生年月日は1900年～2025年の間で指定してください")]
        public DateTime 生年月日 { get; set; }

        [Range(0, 999999999999, ErrorMessage = "0〜999999999999 の範囲で入力してください。")]
        public decimal 年収 { get; set; }

    }
}
