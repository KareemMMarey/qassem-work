using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Inquery
{
    public class InqueryVM
    {
        [MaxLength(10, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NationalNo { get; set; } = null!;
        [MaxLength(14, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string RecordNo { get; set; } = null!;

        //[MaxLength(50, ErrorMessage = "يجب ادخال 50 حرف كحد اقصى")]
        //[Required]
        //public string UserFullName { get; set; }

        //[MaxLength(20, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        //public string IdentityNumber { get; set; }
    }
}
