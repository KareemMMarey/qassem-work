using Framework.Core.AutoMapper;
using Framework.Core.Notifications;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Account;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using ZXing;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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

        [HttpPost]
        public async Task<IActionResult> Profile(ContactInfoDto model)
        {
            if (!ModelState.IsValid)
            {
                // Optionally return the same view with validation errors
                return View(CurrentUser.MapTo<UserDto>());
            }

            var user = await _userServices.GetUserAsync(Guid.Parse(model.Id));

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.NormalizedEmail = model.Email.ToUpper();

           await  _userManager.UpdateAsync(user);


            // Save the data here (e.g., update the DB or call a service)
            // _userService.UpdateContactInfo(User.Identity.Name, model);

            TempData["SuccessMessage"] = "Contact info updated successfully.";
            return RedirectToAction("Profile"); // Or wherever your profile page is
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

            //await _logservice.InsertAsync(
            //    new Framework.Core.SharedServices.Dto.LogDto
            //    {
            //        CallSite = "",
            //        Date = DateTime.Now,
            //        Exception = "After validate model",
            //        Host = "myhost",
            //        Logger = "my logger",
            //        LogLevel = "Info",
            //        MachineName = "manual",
            //        Message = "my message",
            //        Thread = "No thread",
            //        Url = "api",
            //        UserAgent = "agent",
            //        UserName = "my name",
            //        Id = Guid.NewGuid(),
            //    }
            //);
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
            // Simulate nafath login

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "otp_tokens", $"{model.IdentityNumber}.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure directory exists
            Random simulate_random = new Random();
            int fourDigitNumber = simulate_random.Next(1000, 10000);
            await System.IO.File.WriteAllTextAsync(filePath, fourDigitNumber.ToString());

            ViewBag.Message = string.Format(
                       "الرجاء اختيار الرقم الظاهر على تطبيق نفاذ <h1>{0}</h1>",
                       fourDigitNumber.ToString()
                   );
            var simulate_transId= Guid.NewGuid();
            ViewBag.UserName = model.IdentityNumber;
            ViewBag.TransId = simulate_transId.ToString();
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "otp_tokens", $"simulateid.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure directory exists
            await System.IO.File.WriteAllTextAsync(filePath, simulate_transId.ToString());
            ViewBag.Random = fourDigitNumber.ToString();
            return View();

            // End of simulation
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

            //await _logservice.InsertAsync(
            //    new Framework.Core.SharedServices.Dto.LogDto
            //    {
            //        CallSite = "",
            //        Date = DateTime.Now,
            //        Exception =
            //            "Nafath Body data "
            //            + JsonConvert.SerializeObject(
            //                _nafathConfiguartion.Value.NafathBody,
            //                Formatting.Indented
            //            ),
            //        Host = "myhost",
            //        Logger = "my logger",
            //        LogLevel = "Info",
            //        MachineName = "manual",
            //        Message = "my message",
            //        Thread = "No thread",
            //        Url = "api",
            //        UserAgent = "agent",
            //        UserName = "my name",
            //        Id = Guid.NewGuid(),
            //    }
            //);
            try
            {
                
                await _logservice.InsertAsync(
                    new Framework.Core.SharedServices.Dto.LogDto
                    {
                        CallSite = "",
                        Date = DateTime.Now,
                        Exception = "Header data and url data " + JsonConvert.SerializeObject(headers, Formatting.Indented)+ " "+ _nafathConfiguartion.Value.ApiUrl,
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
            // Simulate Nafath
             return await SimulateNafath(username, rememberLogin, random, transId);
            // End Nafath Simulate

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
                            
                            var dOB = JsonConvert
                                .DeserializeObject(TempData["dateOfBirthGregorian"].ToString())
                                .ToString();

                            DateTime? DOBDate = null;
                            if (!string.IsNullOrEmpty(dOB) && !string.IsNullOrWhiteSpace(dOB))
                            {
                                DOBDate = ConvertHijriToGregorian(dOB);
                            }
                            string phone = "05xxxxxxxx";
                            user = new ApplicationUser(username, userFullName)
                            {
                                Email = $"{username}@Nafath",
                                UserName = $"{username}@Nafath",
                                FullName = userFullName,
                                PhoneNumber = phone,
                                CreatedBy = "Admin",
                                CreatedOn = DateTime.Now,
                                EmailConfirmed = true,
                                DateOfBirth = DOBDate
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

        private async Task<IActionResult> SimulateNafath(
            string username,
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
                        var accesstoken = TempData["accessToken"].ToString();

                        ViewBag.accesstoken = accesstoken;
                        string userFullName = "";
                        if (user is null)
                        {
                            userFullName = TempData["arTwoNames"].ToString();

                            var dOB = TempData["dateOfBirthGregorian"].ToString();
                            var nationality = TempData["nationality"].ToString();
                            var IdentityNumber = TempData["nationalId"].ToString();

                            DateTime? DOBDate = null;
                            if (!string.IsNullOrEmpty(dOB) && !string.IsNullOrWhiteSpace(dOB))
                            {
                                DOBDate = ConvertHijriToGregorian(dOB);
                            }
                            string phone = "0500020015";
                            user = new ApplicationUser(username, userFullName)
                            {
                                Email = $"{username}@Nafath",
                                UserName = $"{username}@Nafath",
                                FullName = userFullName,
                                PhoneNumber = phone,
                                CreatedBy = "Admin",
                                CreatedOn = DateTime.Now,
                                EmailConfirmed = true,
                                DateOfBirth = DOBDate,
                                Nationality = nationality,
                                IdentityNumber = IdentityNumber
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
            // Simulate Check nafath

            for (int i = 0; i <= 2; i++)
            {
                Thread.Sleep(1000);
            }
            var person_simulate = new
            {
                id = Guid.NewGuid(),
                nationalId = userName,
                firstNameAr = "كريم",
                secondNameAr = "محمد",
                thirdNameAr = "حسن",
                lastNameAr = "المرى",
                fullNameAr = "كريم محمد حسن المرى",
                firstNameEn = "Kareem",
                secondNameEn = "Mohamed",
                thirdNameEn = "Hassan",
                lastNameEn = "El Marey",
                fullNameEn = "Kareem Mohamed Hassan El Marey",
                dateOfBirthHijri = "1405-01-01",
                dateOfBirthGregorian = "1985-10-15",
                gender = "Male",
                mobileNumber = "966500000000",
                issuePlace = "Riyadh",
                nationality = "Saudi",
                identityType = "NationalID",
                identityExpiryDate = "1447-01-01",
                arTwoNames = "كريم محمد"
            };
            TempData["arTwoNames"] = person_simulate.arTwoNames;
            TempData["accessToken"] = "mocked-token-123";
            TempData["dateOfBirthGregorian"] = person_simulate.dateOfBirthGregorian;
            TempData["nationality"] = person_simulate.nationality;
            TempData["nationalId"] = person_simulate.nationalId;

            return Ok(new { status= NafathStatus.COMPLETED.ToString() });

            // End Simulate Check nafath
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
                        TempData["dateOfBirthGregorian"] = person["dateOfBirthGregorian"].ToString();
                        TempData["nationality"] = person["nationality"].ToString();
                        TempData["nationalId"] = person["nationalId"].ToString();
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

        public static DateTime ConvertHijriToGregorian(string hijriDate)
        {
            // Step 1: Clean unwanted Arabic text and invisible RTL characters
            hijriDate = Regex.Replace(hijriDate, @"[\u200F\u202E\u202A-\u202C]", "");
            hijriDate = hijriDate.Replace("بعد الهجرة", "").Trim();

            // Step 2: Convert Arabic digits to Western
            hijriDate = ConvertArabicToWesternDigits(hijriDate);

            // Step 3: Replace Arabic AM/PM
            hijriDate = hijriDate.Replace("ص", "AM").Replace("م", "PM");

            // Step 4: Parse using HijriCalendar
            var hijriCalendar = new HijriCalendar();
            var culture = new CultureInfo("ar-SA");
            culture.DateTimeFormat.Calendar = hijriCalendar;

            // Extract date parts
            var parts = hijriDate.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int month = int.Parse(parts[0]);
            int day = int.Parse(parts[1]);
            int persianYear = int.Parse(parts[2]);

            // Convert Persian to Gregorian
            //PersianCalendar pc = new PersianCalendar();
            // DateTime gregorianDate = hijriCalendar.ToDateTime(persianYear, month, day, 0, 0, 0, 0);
            string date = day.ToString() + "/" + month.ToString() + "/" + persianYear.ToString();
            var dateConverted = DateTime.ParseExact(date, "d/M/yyyy", culture);
            return dateConverted;
        }

        private static string ConvertArabicToWesternDigits(string input)
        {
            string[] arabicDigits = { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
            for (int i = 0; i < arabicDigits.Length; i++)
            {
                input = input.Replace(arabicDigits[i], i.ToString());
            }
            return input;
        }
    }
}
