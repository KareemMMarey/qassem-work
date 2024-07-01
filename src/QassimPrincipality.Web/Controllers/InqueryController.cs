using Framework.Core.Extensions;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Web.Helpers;
using QassimPrincipality.Web.ViewModels.Inquery;
using QassimPrincipality.Web.ViewModels.OpenData;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    public class InqueryController : Controller
    {
        private readonly LogAppService _logservice;
        private readonly UserAppService _userAppService;
        public InqueryController(LogAppService logservice, UserAppService userAppService)
        {
            _logservice = logservice;
            _userAppService = userAppService;
        }
        public async Task<ActionResult> Index(InqueryVM? vM)
        {
            try
            {
                vM = vM?? new InqueryVM();
                var user = await _userAppService.GetUserAsync(Guid.Parse(HttpContext.User.GetId()));
                //vM.UserFullName = user.FullNameAr ?? user.FullName;
                vM.NationalNo = user.UserName.Replace("@nafath", "");
                return View(vM);
            }
            catch (Exception ex)
            {

                await _logservice.InsertAsync(new Framework.Core.SharedServices.Dto.LogDto
                {
                    CallSite = "",
                    Date = DateTime.Now,
                    Exception = "Inquery Index Catch" + ex.ToString(),
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
                });
                return View(new InqueryVM());
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InqueryRequest(InqueryVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "حدث خطأ أثناء عملية الاستعلام";
                return View("Index",model);
            }
            try
            {

                var result = await ApiConsumer.ServiceConsumerAsync<dynamic>(
            "https://dms.alqassim.gov.sa/INTEGSERV_DEMO/api/Receiver/SearchForRecord/?RecordNo="+long.Parse(model.RecordNo)+"&NationalNo="+long.Parse(model.NationalNo)
            );
                await _logservice.InsertAsync(new Framework.Core.SharedServices.Dto.LogDto
                {
                    CallSite = "",
                    Date = DateTime.Now,
                    Exception = "Header data " + JsonConvert.SerializeObject(result, Formatting.Indented),
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
                });

                if (result["isSuccess"].ToString() == "True")
                {
                    var RequestStatus = result["RequestStatus"].ToString();
                    ViewBag.RequestStatus = RequestStatus;
                }
                else { ViewBag.RequestStatus = "لا توجد بيانات "; }
            }
            catch (Exception ex)
            {

                await _logservice.InsertAsync(new Framework.Core.SharedServices.Dto.LogDto
                {
                    CallSite = "",
                    Date = DateTime.Now,
                    Exception = "InqueryRequest" + ex.ToString(),
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
                });
                ViewBag.ErrorMessage = "حدث خطأ أثناء عملية الاستعلام";
            }

            return View("Index");
        }
    }
}
