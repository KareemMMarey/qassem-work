using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Inquery
{
    public class InqueryVM
    {
        [MaxLength(10, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "يجب ادخال ارقام فقط")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string NafathNumber { get; set; } = null!;
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public string RequestNumber { get; set; } = null!;
    }
}
