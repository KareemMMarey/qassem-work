using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Application.Services.Main.Contact
{
    public class ContactFormDto
    {
        public Guid Id { get; set; }

        [Display(Name = "اسم المستخدم")]
        public string UserFullName { get; set; }
        [Display(Name = "رقم الطلب")]
        public string ReferralNumber { get; set; }

        [Display(Name = "البريد الالكتروني ")]
        public string UserEmail { get; set; }

        [Display(Name = "رقم الجوال")]
        [MaxLength(14, ErrorMessage = "يجب ادخال 14 رقم كحد اقصى")]
        public string UserMobile { get; set; }

        [Display(Name = "عنوان الشكوى")]
        public string ContactTitle { get; set; }

        [Display(Name = "التفاصيل")]
        public string Description { get; set; }

        [Display(Name = "نوع الطلب")]
        public int ContactTypeId { get; set; }

        [Display(Name = "نوع الطلب")]
        public string ContactTypeName { get; set; }

        [Display(Name = "رقم الهوية")]
        [MaxLength(10, ErrorMessage = "يجب ادخال 10 ارقام كحد اقصى")]
        public string IdentityNumber { get; set; }
        public bool? IsApproved { get; set; }

        [Display(Name = "وقت الطلب")]
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        //[Display(Name = "رقم الطلب")]
        //public string ReferralNumber { get; set; }
    }
}
