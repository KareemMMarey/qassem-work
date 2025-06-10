using Framework.Core;
using Framework.Core.AutoMapper;
using Framework.Core.Extensions;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Repositories;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Application.Services.NewShema;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Request;
using QassimPrincipality.Web.ViewModels.Roles;
using System.Linq;

namespace QassimPrincipality.Web.Controllers
{
    public class RolesController : BaseController
    {
        private readonly UserAppService _userServices;
        private readonly UserRolesRepository _userRolesRepository;

        public RolesController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, UserAppService userServices, UserRolesRepository userRolesRepository)
            : base(userManager, null, roleManager)
        {
            _userServices = userServices;
            _userRolesRepository = userRolesRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            var roles = _roleManager.Roles.ToListAsync().Result;
            var roleViewModel = roles.MapTo<List<RoleDto>>();
            return View(roleViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var appRole = new ApplicationRole
                {
                    Name = model.Name,
                    DisplayNameAr = model.Name,
                    DisplayNameEn = model.Name,
                    CreatedBy = User.Identity.Name
                };

                IdentityResult result = await _roleManager.CreateAsync(appRole);

                if (!result.Succeeded)
                {
                    AddModelErrors(result);
                    return View(model);
                }

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Edit(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return RedirectToAction(nameof(List));

            var role = _roleManager.FindByIdAsync(roleId).Result;
            var roleViewModel = role.MapTo<RoleDto>();

            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var appRole = await _roleManager.FindByIdAsync(model.Id.ToString());
            appRole.Name = model.Name;

            IdentityResult result = await _roleManager.UpdateAsync(appRole);

            if (!result.Succeeded)
            {
                AddModelErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return RedirectToAction(nameof(List));

            var appRole = await _roleManager.FindByIdAsync(roleId);

            if (appRole == null)
                return RedirectToAction(nameof(List));

            await _roleManager.DeleteAsync(appRole);

            return RedirectToAction(nameof(List));
        }

        //[HttpGet]
        //public IActionResult Assign(string userId)
        //{
        //    var user = _userManager.FindByIdAsync(userId).Result;

        //    if (user == null)
        //        return RedirectToAction(nameof(List));

        //    IList<string> rolesForUser = _userManager.GetRolesAsync(user).Result;

        //    var assignedRoles = new List<RoleAssignViewModel>();

        //    foreach (var role in GetRoles) //GetRoles BaseController'dan geliyor.
        //    {
        //        var roleAssignViewModel = new RoleAssignViewModel
        //        {
        //            Id = role.Id.ToString(),
        //            Name = role.Name,
        //            Exist = rolesForUser.Any(p => p.Equals(role.Name)) //Bu rol, kullanıcıya atanmış roller arasında mı?
        //        };

        //        assignedRoles.Add(roleAssignViewModel);
        //    }

        //    ViewBag.Username = user.UserName;
        //    HttpContext.Session.SetString("userId", userId); //Post işlemi için aldım.

        //    return View(assignedRoles);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Assign(List<RoleAssignViewModel> rolesAssignViewModel)
        //{
        //    string userId = HttpContext.Session.GetString("userId");
        //    bool transactionStatus = true;

        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //        return RedirectToAction(nameof(List));

        //    var userRoles = await _userManager.GetRolesAsync(user); //Kullanıcının hazırdaki rolleri.

        //    IEnumerable<string> rolesToAssigned = null;
        //    IEnumerable<string> rolesToRemoved = null;

        //    if (userRoles.Any())
        //    {
        //        rolesToAssigned = rolesAssignViewModel.Where(p => p.Exist && userRoles.Any(name => name != p.Name)).
        //                                        Select(p => p.Name); //Eklenecek Roller

        //        rolesToRemoved = rolesAssignViewModel.Where(p => !p.Exist && userRoles.Any(name => name.Equals(p.Name)))
        //                                            .Select(p => p.Name); //Kaldırılacak Roller
        //    }
        //    else
        //    {
        //        rolesToAssigned = rolesAssignViewModel.Where(p => p.Exist).Select(p => p.Name);
        //    }

        //    if (rolesToAssigned != null && rolesToAssigned.Any())
        //    {
        //        await _userServices.AddRolesAsync(user.Id, rolesToAssigned.ToArray());
        //        //IdentityResult result = await _userManager.AddToRolesAsync(user, rolesToAssigned);

        //        //if (!result.Succeeded)
        //        //{
        //        //    AddModelErrors(result);
        //        //    transactionStatus = false;
        //        //}
        //    }

        //    if (rolesToRemoved != null && rolesToRemoved.Any())
        //    {
        //        //IdentityResult result = await _userManager.RemoveFromRolesAsync(user, rolesToRemoved);

        //        //if (!result.Succeeded)
        //        //{
        //        //    AddModelErrors(result);
        //        //    transactionStatus = false;
        //        //}
        //        foreach (var item in rolesToRemoved.ToArray())
        //        {
        //            await _userServices.RemoveUserFromRole(user.Id, item);
        //        }

        //    }

        //    if (!transactionStatus)
        //        return View(rolesAssignViewModel);


        //    return RedirectToAction(nameof(Assign), new { userId = user.Id });
        //}



        public async Task<IActionResult> Assign(string UserId)
        {
            var model = new AssignRolesViewModel();

            if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrWhiteSpace(UserId))
            {
                var user = await _userManager.FindByIdAsync(UserId);
                var Rols = await _userServices.GetUserRolesAsync(new Guid(UserId));

                model = new AssignRolesViewModel
                {
                    Users = await _userManager.Users
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.FullName })
                    .ToListAsync(),
                    AvailableRoles = await _roleManager.Roles
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                    .ToListAsync(),
                    SelectedUserId = user.Id,
                    SelectedRoleIds = Rols
                };
            }
            else
                model = new AssignRolesViewModel
                {
                    Users = await _userManager.Users
                       .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.FullName })
                       .ToListAsync(),
                    AvailableRoles = await _roleManager.Roles
                       .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                       .ToListAsync()
                };
            return View(model);
        }

        //public async Task<IActionResult> Assign()
        //{
        //    var model = new AssignRolesViewModel
        //    {
        //        Users = await _userManager.Users
        //            .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.FullName })
        //            .ToListAsync(),
        //        AvailableRoles = await _roleManager.Roles
        //            .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
        //            .ToListAsync()
        //    };
        //    return View(model);
        //}

        // POST: Roles/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUserRoles = (await _userRolesRepository.GetListAsync())
                                            .Where(ur => ur.UserId == model.SelectedUserId)
                                            .ToList();

                _userRolesRepository.DeleteRange(existingUserRoles, true);


                // Add the newly selected roles
                if (model.SelectedRoleIds != null && model.SelectedRoleIds.Any())
                {
                    foreach (var roleId in model.SelectedRoleIds)
                    {
                        await _userRolesRepository.InsertAsync(new ApplicationUserRoles
                        {
                            UserId = model.SelectedUserId,
                            RoleId = roleId
                        }, true);
                    }
                }

                TempData["SuccessMessage"] = "تم تعيين الصلاحيات بنجاح!"; // "Permissions assigned successfully!"
                return RedirectToAction("UserRoles", "Roles"); // Redirect to a list view of user roles
            }

            // If validation fails, re-populate DDLs and return to view
            model.Users = await _userManager.Users
                .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.FullName })
                .ToListAsync();
            model.AvailableRoles = await _roleManager.Roles
                .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles()
        {
            try
            {
                var results = await _userRolesRepository.GetListAsync();

                var data = new List<UserRolesViewDto>();
                foreach (var item in results.GroupBy(c => c.UserId))
                {
                    UserRolesViewDto userRolesVM = new UserRolesViewDto();
                    userRolesVM.Id = item.FirstOrDefault().Id;
                    userRolesVM.UserId = item.Key;
                    //userRolesVM.RoleId = item.RoleId;
                    var user = await _userManager.FindByIdAsync(item.Key.ToString());

                    userRolesVM.RoleName = string.Join(',', await _userManager.GetRolesAsync(user));
                    userRolesVM.UserName = user.FullName;

                    data.Add(userRolesVM);
                }


                return View(
                    new UserRolesVM
                    {
                        Results = data
                    }
                );

                //return View(results); // Pass list to the view
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUserRoles(string UserId)
        {
            try
            {

                var userRoles = await _userRolesRepository.GetListAsync();

                var toDeleteRoles = userRoles.Where(c => c.UserId == new Guid(UserId)).ToList();

                _userRolesRepository.DeleteRange(toDeleteRoles, true);

                return RedirectToAction("UserRoles", "Roles"); // Redirect to a list view of user roles

            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching requests: {ex.Message}");
            }
        }



    }
}

