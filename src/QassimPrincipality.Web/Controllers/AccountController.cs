using Framework.Core.AutoMapper;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QassimPrincipality.Infrastructure.Data;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.Identity.Configuration;
using QassimPrincipality.Web.Identity.Helpers.Localization;
using QassimPrincipality.Web.ViewModels.Account;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace QassimPrincipality.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserAppService _userServices;
        private readonly IOptions<NafathConfiguration> _nafathConfiguartion;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IOptions<NafathConfiguration> nafathConfiguartion, UserAppService userAppService)
            : base(userManager, signInManager, roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _nafathConfiguartion = nafathConfiguartion;
            _userServices = userAppService;
        }
        [AllowAnonymous]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Profile()
        {
            
            var userViewModel = CurrentUser.MapTo<UserDto>();

            return View(userViewModel);
        }
        [AllowAnonymous]
        public IActionResult Login() => View(new LoginVM());
        public IActionResult NafathLogin() => View(new NafathLoginVM());
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "بيانات المستخدم غير صحيحة";
                return View(loginVM);
            }

            TempData["Error"] = "بيانات المستخدم غير صحيحة!";
            return View(loginVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NafathLogin(NafathLoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            return await ProcessNafathUser(loginVM);
            //var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            //if (user != null)
            //{
            //    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            //    if (passwordCheck)
            //    {
            //        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            //        if (result.Succeeded)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }
            //    TempData["Error"] = "Wrong credentials. Please, try again!";
            //    return View(loginVM);
            //}

            //TempData["Error"] = "Wrong credentials. Please, try again!";
            //return View(loginVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "هذا البريد مسجل مسبقا";
                return View(registerVM);
            }

            var newUser = new ApplicationUser(registerVM.EmailAddress, registerVM.EmailAddress)
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                EmailConfirmed = true
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded) {

                //var addedUser = await _roleManager.FindByNameAsync(UserRoles.User);
                //await _userManager.AddToRoleAsync(newUser, addedUser.Name);
                await _userServices.AddRoleAsync(newUser.Id, UserRoles.User);
            }
               

            return RedirectToAction("Index", "Common", new { SuccessMessage = "تم تسجيل المستخدم بنجاح" });
        }


        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

        public async Task<IActionResult> ProcessNafathUser(NafathLoginVM model)
        {

            List<ApiHeaders> headers = new List<ApiHeaders>
                    {
                        new ApiHeaders
                        {
                            Name = "Authorization",
                            Value = _nafathConfiguartion.Value.ApiKey
                        },
                        new ApiHeaders { Name = "AUD", Value = _nafathConfiguartion.Value.ApplicationKey }
                    };

            _nafathConfiguartion.Value.NafathBody.Parameters.id = long.Parse(model.IdentityNumber);

            var token = await ApiConsumer.ServicePostConsumerAsync<dynamic>(
                _nafathConfiguartion.Value.ApiUrl,
                _nafathConfiguartion.Value.NafathBody,
                headers.ToArray()
            );

            try
            {
                var random = token["random"].ToString();
                var transId = token["transId"].ToString();
                ViewBag.Message = string.Format("الرجاء اختيار الرقم الظاهر على تطبيق نفاذ {0}", random);
                ViewBag.TransId = transId;
                ViewBag.Random = random;
            }
            catch (Exception exce)
            {

                if (token == null)
                    ModelState.AddModelError("token", "null");
                else ModelState.AddModelError("token", token.ToString());
                ModelState.AddModelError("_nafathConfiguartion.Value.ApiUrl", _nafathConfiguartion.Value.ApiUrl);
                ModelState.AddModelError("_nafathConfiguartion.Value.ApiKey", _nafathConfiguartion.Value.ApiKey);
                return View(model);
            }

            return View();
        }
        public async Task<IActionResult> CompleteLogin(string username,
            bool rememberLogin,
            string random,
            string transId) {


            if (!string.IsNullOrEmpty(transId))
            {
                try
                {
                    var user = await _userManager.FindByNameAsync($"{username}@Nafath");

                    if (TempData["arTwoNames"] != null)
                    {

                        var accesstoken = JsonConvert.DeserializeObject(TempData["accessToken"].ToString()).ToString();

                        ViewBag.accesstoken = accesstoken;
                        string userFullName = "";
                        if (user is null)
                        {
                            userFullName = JsonConvert.DeserializeObject(TempData["arTwoNames"].ToString()).ToString();
                            string phone = "05xxxxxxxx";
                            user = new ApplicationUser(username, userFullName)
                            {
                                Email = $"{username}@Nafath",
                                UserName = $"{username}@Nafath",
                                PhoneNumber = phone
                            };
                            await _userManager.CreateAsync(user);
                        }
                        await _signInManager.SignInAsync(user, rememberLogin);

                        return RedirectToAction("Index", "Home", new { nafathFullName = userFullName });

                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }

                }
                catch (Exception)
                {

                    ModelState.AddModelError("خطأ", "حدث خطأ");
                    return RedirectToAction("Index", "Account");
                }

            }

            else
            {
                var user = await _userManager.FindByNameAsync($"{username}@Nafath");
                if (user is null)
                {
                    string phone = "05xxxxxxxx";

                    user = new ApplicationUser(username, username)
                    {
                        Email = $"{username}@Nafath",
                        UserName = $"{username}@Nafath",
                        PhoneNumber = phone
                    };
                    await _userManager.CreateAsync(user);
                }

                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Members()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModel = users.MapTo<List<UserDto>>();
            return View(userViewModel);
        }


    }
}
