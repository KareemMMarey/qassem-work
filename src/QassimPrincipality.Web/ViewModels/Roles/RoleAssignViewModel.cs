using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace QassimPrincipality.Web.ViewModels.Roles
{
    public class RoleAssignViewModel
    {
        public string Id { get; set; } //Role Id
        public string Name { get; set; } //Role Name
        public bool Exist { get; set; }
    }


    //public class SelectListItem
    //{
    //    public string Value { get; set; }
    //    public string Text { get; set; }

    //}

    public class AssignRolesViewModel
    {
        [Display(Name = "اسم المستخدم")] // "User Name"
        [Required(ErrorMessage = "الرجاء اختيار مستخدم.")] // "Please select a user."
        public Guid SelectedUserId { get; set; }

        public IEnumerable<SelectListItem>? Users { get; set; }

        [Display(Name = "الصلاحيات")] // "Permissions"
        [Required(ErrorMessage = "الرجاء اختيار صلاحية واحدة على الأقل.")] // "Please select at least one permission."
        public List<Guid> SelectedRoleIds { get; set; } // This will hold multiple selected role IDs

        public IEnumerable<SelectListItem>? AvailableRoles { get; set; }
    }
}
