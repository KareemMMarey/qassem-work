using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Account
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "البريد حقل مطلوب")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "تأكيد كلمة المرور حقل مطلوب")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "غير متطابق")]
        public string ConfirmPassword { get; set; }
    }
}
