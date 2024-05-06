using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static QassimPrincipality.Web.Helpers.IdentityConstants;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly RoleManager<ApplicationRole> _roleManager;

        protected ApplicationUser CurrentUser => _userManager.FindByNameAsync(User.Identity.Name).Result;
        protected List<ApplicationRole> GetRoles => _roleManager.Roles.ToListAsync().Result;

        public BaseController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager = null, RoleManager<ApplicationRole> roleManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        protected void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public async void SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        [NonAction]
        protected string ReturnHomePageUrl()
        {
            var user = _signInManager.GetTwoFactorAuthenticationUserAsync().Result;

            if (user == null)
            {
                if (User.Identity.Name == null)
                {
                    return "/";
                }

                if (CurrentUser == null)
                {
                    return "/";
                }
            }

            string returnUrl = TempData[Namer.RETURN_URL] as string;

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return returnUrl;
            }

            var rolesLoggedUser = _userManager.GetRolesAsync(user).Result; //Giriş yapan kullanıcının rolleri

            if (rolesLoggedUser.Contains(Role.ADMIN))
            {
                return Url.Action(Namer.INDEX, Role.ADMIN);
            }
            else if (rolesLoggedUser.Contains(Role.MANAGER))
            {
                return Url.Action(Namer.INDEX, Role.MANAGER);
            }
            else
            {
                return Url.Action(Namer.INDEX, Role.MEMBER);
            }
        }
    }
}
