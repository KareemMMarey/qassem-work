using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.OpenData
{
    public class AddOpenDataViewModel
    {


        public string UserFullName { get; set; }

        [Required(ErrorMessage = "أدخل البريد الإلكتروني")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "البريد غير صحيح")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "أدخل  الجوال")]
        [RegularExpression(@"^(009665|9665|\+9665|05|5)(5|0|2|3|6|4|9|1|8|7)([0-9]{7})$",
                   ErrorMessage = "أدخل رقم جوال صحيح")]
        public string UserMobile { get; set; }
        [Required(ErrorMessage = "أدخل عنوان")]
        public string Title { get; set; }
        [Required(ErrorMessage = "أدخل تفاصيل الطلب ")]
        [MaxLength(400)]
        public string Description { get; set; }
        public string IdentityNumber { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "أدخل وصف مقدم الطلب")]
        public int RequesterTypeId { get; set; }
        public bool IsApproved { get; set; }


    }
}
