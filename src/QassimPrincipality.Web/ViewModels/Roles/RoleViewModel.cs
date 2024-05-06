using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Roles
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [DisplayName("اسم الصلاحية")]
        [Required(ErrorMessage = "اسم الصلاحية مطلوب")]
        public string Name { get; set; }
    }
}
