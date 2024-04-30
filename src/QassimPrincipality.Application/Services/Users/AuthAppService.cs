using Framework.Identity.Data;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace QassimPrincipality.Application.Users
{
    public class AuthAppService
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthAppService(IUserAppService userAppService, UserManager<ApplicationUser> userManager)
        {
            _userAppService = userAppService;
            _userManager = userManager;
        }

        public ApplicationUser CurrentUser => _userAppService.CurrentUser;
        public Guid CurrentUserId => _userAppService.CurrentUser.Id;
        public string CurrentUserName => _userAppService.CurrentUserName;
        public string CurrentUserEmail => _userAppService.CurrentUserEmail;
        public string CurrentUserFullName => _userAppService.FindCurrentUserFullName();
        public string CurrentUserRoleName => _userAppService.CurrentUserRoleName;
        public Guid CurrentUserRoleId => _userAppService.CurrentUserRoleId.Value;
        public string CurrentUserRoleDisplayName => _userAppService.GetRoleDisplayName(CurrentUserRoleName).Result;

        public bool IsAdmin => _userAppService.IsCurrentUserInRole(Roles.Admin.ToString());// _userAppService.CurrentUserRoleName == SystemUserRole.Admin.ToString();

        public List<string> CurrentUserRoles => _userAppService.CurrentUserRoles;

        public string GetClaimValue(string key)
        {
            return _userAppService.GetClaimValueByKey(key);
        }

        public List<Guid> CurrentPortId => _userAppService.CurrentPortId != null && _userAppService.CurrentPortId.Count > 0 ? _userAppService.CurrentPortId.Select(q => new Guid(q)).ToList() : new List<Guid>();

        public List<string> CurrentPortName => _userAppService.CurrentPortName != null && _userAppService.CurrentPortName.Count > 0 ? _userAppService.CurrentPortName.ToList() : new List<string>();
    }
}