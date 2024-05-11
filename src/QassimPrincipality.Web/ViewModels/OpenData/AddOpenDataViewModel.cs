using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.OpenData
{
    public class AddOpenDataViewModel
    {
        [MaxLength(50, ErrorMessage = "يجب ادخال 50 حرف كحد اقصى")]
        [Required]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = "أدخل البريد الإلكتروني")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "البريد غير صحيح")]
        [MaxLength(50, ErrorMessage = "يجب ادخال 50 حرف كحد اقصى")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "أدخل  الجوال")]
        [MaxLength(14, ErrorMessage = "يجب ادخال 14 رقم كحد اقصى")]
        [RegularExpression(
            @"^(009665|9665|\+9665|05|5)(5|0|2|3|6|4|9|1|8|7)([0-9]{7})$",
            ErrorMessage = "أدخل رقم جوال صحيح"
        )]
        public string UserMobile { get; set; }

        [Required(ErrorMessage = "أدخل عنوان")]
        [MaxLength(50, ErrorMessage = "يجب ادخال 50 حرف كحد اقصى")]
        public string Title { get; set; }

        [Required(ErrorMessage = "أدخل تفاصيل الطلب ")]
        [MaxLength(400, ErrorMessage = "يجب ادخال 400 حرف كحد اقصى")]

        public string Description { get; set; }

        [MaxLength(10, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        public string IdentityNumber { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "أدخل وصف مقدم الطلب")]
        public int RequesterTypeId { get; set; }
        public bool IsApproved { get; set; }
    }
}
