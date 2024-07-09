using static QassimPrincipality.Web.Helpers.IdentityConstants;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Category
{
    public class ServiceCategoryVM
    {
        

        [Required(ErrorMessage = "أدخل اسم الخدمة")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "أدخل وصف الخدمة")]
        [MaxLength(400)]
        public string DescriptionAr { get; set; }
    }
}
