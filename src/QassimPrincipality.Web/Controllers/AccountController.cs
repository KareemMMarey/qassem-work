using Framework.Core.AutoMapper;
using Framework.Core.Notifications;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Account;

namespace QassimPrincipality.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserAppService _userServices;
        private readonly LogAppService _logservice;
        private readonly ILogger<AccountController> _logger;
        private readonly IOptions<NafathConfiguration> _nafathConfiguartion;
        private readonly INotificationsManager _notificationsManager;
        private readonly IEmailService _emailservice;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<NafathConfiguration> nafathConfiguartion,
            UserAppService userAppService,
            ILogger<AccountController> logger,
            LogAppService logservice,
            INotificationsManager notificationsManager,
            IEmailService emailService
        )
            : base(userManager, signInManager, roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _nafathConfiguartion = nafathConfiguartion;
            _userServices = userAppService;
            _logger = logger;
            _logservice = logservice;
            _notificationsManager = notificationsManager;
            _emailservice = emailService;
        }

        //[AllowAnonymous]
        //public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Profile()
        {
            var userViewModel = CurrentUser.MapTo<UserDto>();

            return View(userViewModel);
        }

        [AllowAnonymous]
        public IActionResult Login() => View(new LoginVM());

        [AllowAnonymous]
        public IActionResult NafathLogin() => View(new NafathLoginVM());

        [AllowAnonymous]
        public async Task<IActionResult> ProcessLogin(ApplicationUser user, LoginVM loginVM)
        {
            await _signInManager.SignInAsync(
                user,
                false
            );
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> VerifyOtp(LoginVM loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", loginVM.OTP);

            if (result)
            {
                return await ProcessLogin(user, loginVM);

            }

            TempData["Error"] = " رمز التحقق غير صحيح";
            return View(loginVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RsendOtpToUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            return View("VerifyOtp", new LoginVM
            {
                EmailAddress = email
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
         {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    try
                    {
                        var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");


                        // Currently use this instead of email 

                        // Save token to file
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "otp_tokens", $"{user.Id}.txt");
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure directory exists
                        await System.IO.File.WriteAllTextAsync(filePath, token);
                        //_emailservice.SendEmail(new EmailMessage
                        //{
                        //    From="",
                        //    Body = token,
                        //    To = new List<string> { user.Email, "mqassem.c@nec.gov.sa" },
                        //    TemplateName = "LoginOTP"
                        //});



                        //await _notificationsManager.EnqueueEmailAsync(
                        //    new EmailMessage
                        //    {
                        //        Body = token,
                        //        To = new List<string> { user.Email, "mqassem.c@nec.gov.sa" },
                        //        TemplateName = "LoginOTP"
                        //    }
                        //);
                        return View("VerifyOtp", loginVM);
                    }
                    catch (Exception ex)
                    {

                        TempData["Error"] = "بيانات المستخدم غير صحيحة";
                        _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                        return View(loginVM);
                    }
                }
                TempData["Error"] = "بيانات المستخدم غير صحيحة";
                return View(loginVM);
            }

            TempData["Error"] = "بيانات المستخدم غير صحيحة!";
            return View(loginVM);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NafathLogin(NafathLoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            await _logservice.InsertAsync(
                new Framework.Core.SharedServices.Dto.LogDto
                {
                    CallSite = "",
                    Date = DateTime.Now,
                    Exception = "After validate model",
                    Host = "myhost",
                    Logger = "my logger",
                    LogLevel = "Info",
                    MachineName = "manual",
                    Message = "my message",
                    Thread = "No thread",
                    Url = "api",
                    UserAgent = "agent",
                    UserName = "my name",
                    Id = Guid.NewGuid(),
                }
            );
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
            if (!ModelState.IsValid)
                return View(registerVM);

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

            if (newUserResponse.Succeeded)
            {
                //var addedUser = await _roleManager.FindByNameAsync(UserRoles.User);
                //await _userManager.AddToRoleAsync(newUser, addedUser.Name);
                await _userServices.AddRoleAsync(newUser.Id, UserRoles.User);
            }

            return RedirectToAction(
                "Index",
                "Common",
                new { SuccessMessage = "تم تسجيل المستخدم بنجاح" }
            );
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
            //await _logservice.InsertAsync(new Framework.Core.SharedServices.Dto.LogDto
            //{
            //    CallSite = "",
            //    Date = DateTime.Now,
            //    Exception = "Inside Call",
            //    Host = "myhost",
            //    Logger = "my logger",
            //    LogLevel = "Info",
            //    MachineName = "manual",
            //    Message = "my message",
            //    Thread = "No thread",
            //    Url = "api",
            //    UserAgent = "agent",
            //    UserName = "my name",
            //    Id = Guid.NewGuid(),
            //});
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
            await _logservice.InsertAsync(
                new Framework.Core.SharedServices.Dto.LogDto
                {
                    CallSite = "",
                    Date = DateTime.Now,
                    Exception =
                        "Nafath Body data "
                        + JsonConvert.SerializeObject(
                            _nafathConfiguartion.Value.NafathBody,
                            Formatting.Indented
                        ),
                    Host = "myhost",
                    Logger = "my logger",
                    LogLevel = "Info",
                    MachineName = "manual",
                    Message = "my message",
                    Thread = "No thread",
                    Url = "api",
                    UserAgent = "agent",
                    UserName = "my name",
                    Id = Guid.NewGuid(),
                }
            );
            try
            {
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception =
                            "Header data "
                            + JsonConvert.SerializeObject(headers, Formatting.Indented),
                        Host = "myhost",
                        Logger = "my logger",
                        LogLevel = "Info",
                        MachineName = "manual",
                        Message = "my message",
                        Thread = "No thread",
                        Url = "api",
                        UserAgent = "agent",
                        UserName = "my name",
                        Id = Guid.NewGuid(),
                    }
                );
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception = "url data " + _nafathConfiguartion.Value.ApiUrl,
                        Host = "myhost",
                        Logger = "my logger",
                        LogLevel = "Info",
                        MachineName = "manual",
                        Message = "my message",
                        Thread = "No thread",
                        Url = "api",
                        UserAgent = "agent",
                        UserName = "my name",
                        Id = Guid.NewGuid(),
                    }
                );

                _logger.LogInformation("before call" + "Hello Hell");
                var token = await ApiConsumer.ServicePostConsumerAsync<dynamic>(
                    _logger,
                    _logservice,
                    _nafathConfiguartion.Value.ApiUrl,
                    _nafathConfiguartion.Value.NafathBody,
                    headers.ToArray()
                );

                try
                {
                    var random = token["random"].ToString();
                    var transId = token["transId"].ToString();
                    ViewBag.Message = string.Format(
                        "الرجاء اختيار الرقم الظاهر على تطبيق نفاذ <h1>{0}</h1>",
                        random
                    );
                    ViewBag.UserName = model.IdentityNumber;
                    ViewBag.TransId = transId;
                    ViewBag.Random = random;
                }
                catch (Exception exce)
                {
                    if (token == null)
                        ModelState.AddModelError("token", "null");
                    else
                        ModelState.AddModelError("token", token.ToString());
                    ModelState.AddModelError(
                        "_nafathConfiguartion.Value.ApiUrl",
                        _nafathConfiguartion.Value.ApiUrl
                    );
                    ModelState.AddModelError(
                        "_nafathConfiguartion.Value.ApiKey",
                        _nafathConfiguartion.Value.ApiKey
                    );
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception = "in catch" + ex.ToString(),
                        Host = "myhost",
                        Logger = "my logger",
                        LogLevel = "Info",
                        MachineName = "manual",
                        Message = "my message",
                        Thread = "No thread",
                        Url = "api",
                        UserAgent = "agent",
                        UserName = "my name",
                        Id = Guid.NewGuid(),
                    }
                );
                _logger.LogError(ex, "An error occurred while processing your request.");
            }

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> CompleteLogin(
            string username,
            bool rememberLogin,
            string random,
            string transId
        )
        {
            if (!string.IsNullOrEmpty(transId))
            {
                try
                {
                    var user = await _userManager.FindByNameAsync($"{username}@Nafath");

                    if (TempData["arTwoNames"] != null)
                    {
                        var accesstoken = JsonConvert
                            .DeserializeObject(TempData["accessToken"].ToString())
                            .ToString();

                        ViewBag.accesstoken = accesstoken;
                        string userFullName = "";
                        if (user is null)
                        {
                            userFullName = JsonConvert
                                .DeserializeObject(TempData["arTwoNames"].ToString())
                                .ToString();
                            string phone = "05xxxxxxxx";
                            user = new ApplicationUser(username, userFullName)
                            {
                                Email = $"{username}@Nafath",
                                UserName = $"{username}@Nafath",
                                FullName = userFullName,
                                PhoneNumber = phone,
                                CreatedBy = "Admin",
                                CreatedOn = DateTime.Now,
                                EmailConfirmed = true
                            };
                            await _userManager.CreateAsync(user, "P@ssw0rd");
                            await _userServices.AddRoleAsync(user.Id, UserRoles.User);
                        }
                        await _signInManager.SignInAsync(user, rememberLogin);

                        return RedirectToAction(
                            "Index",
                            "Home",
                            new { nafathFullName = userFullName }
                        );
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception exc)
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
                        PhoneNumber = phone,
                        CreatedBy = "Admin",
                        CreatedOn = DateTime.Now,
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(user, "P@ssw0rd");
                    await _userServices.AddRoleAsync(user.Id, UserRoles.User);
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CheckNafathRequest(
            string userName,
            string random,
            string transId
        )
        {
            try
            {
                List<ApiHeaders> headers = new List<ApiHeaders>
                {
                    new ApiHeaders
                    {
                        Name = "Authorization",
                        Value = _nafathConfiguartion.Value.ApiKey
                    },
                    new ApiHeaders
                    {
                        Name = "AUD",
                        Value = _nafathConfiguartion.Value.ApplicationKey
                    }
                };

                _nafathConfiguartion.Value.NafathCheckRequstBody.Parameters.id = long.Parse(
                    userName
                );
                if (!string.IsNullOrEmpty(random))
                    _nafathConfiguartion.Value.NafathCheckRequstBody.Parameters.random = long.Parse(
                        random
                    );
                if (!string.IsNullOrEmpty(transId))
                    _nafathConfiguartion.Value.NafathCheckRequstBody.Parameters.transId = transId;

                var result = await ApiConsumer.ServicePostConsumerAsync<dynamic>(
                    _logger,
                    _logservice,
                    _nafathConfiguartion.Value.ApiUrl,
                    _nafathConfiguartion.Value.NafathCheckRequstBody,
                    headers.ToArray()
                );
                var status = result["status"].ToString();

                for (int i = 0; i <= 3; i++)
                {
                    Thread.Sleep(5000);
                    result = await ApiConsumer.ServicePostConsumerAsync<dynamic>(
                        _logger,
                        _logservice,
                        _nafathConfiguartion.Value.ApiUrl,
                        _nafathConfiguartion.Value.NafathCheckRequstBody,
                        headers.ToArray()
                    );
                    status = result["status"].ToString();
                    if (status == NafathStatus.COMPLETED.ToString())
                    {
                        var person = result["person"];
                        TempData["arTwoNames"] = JsonConvert.SerializeObject(person["arTwoNames"]);
                        TempData["accessToken"] = JsonConvert.SerializeObject(
                            result["accessToken"]
                        );
                        break;
                    }
                }

                return Ok(new { status });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Exception" });
            }
        }
    }
}
