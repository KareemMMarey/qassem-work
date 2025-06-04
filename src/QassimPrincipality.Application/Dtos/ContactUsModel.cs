using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Application.Dtos
{
    public class ContactUsModel
    {
        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "الاسم الأخير مطلوب")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نوع الرسالة مطلوب")]
        public string MessageType { get; set; }

        [Required(ErrorMessage = "الموضوع مطلوب")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "الرسالة مطلوبة")]
        public string Message { get; set; }

    }


}
