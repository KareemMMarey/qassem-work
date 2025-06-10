using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Web.ViewModels.Request
{
    public class UserRolesVM
    {
        public List<UserRolesViewDto> Results { get; set; }

    }

    public class UserRolesViewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        //public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
