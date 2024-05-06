using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Account
{
   
    public class NafathLoginVM
    {
        [Required(ErrorMessage = "أدخل  رقم الهوية")]
        [RegularExpression(@"^(1|2)([0-9]{9})$",
                   ErrorMessage = "أدخل رقم هوية صحيح")]
        public string IdentityNumber { get; set; }
    }
}
