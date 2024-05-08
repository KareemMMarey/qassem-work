using Framework.Core.AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using QassimPrincipality.Application.Services.Lookups.Main.RequestType;
using QassimPrincipality.Application.Services.Main.UploadRequest;
using QassimPrincipality.Application.Services.Main.UploadRequest.Dto;
using static OfficeOpenXml.ExcelErrorValue;

namespace QassimPrincipality.Web.Controllers
{
    [Authorize]
    public class RequestsAdminController : Controller
    {
        private readonly UploadRequestAppService _uploadRequestService;
        private readonly RequestTypeAppService _requestTypeAppService;

        public RequestsAdminController(
            UploadRequestAppService uploadRequestAppService,
            RequestTypeAppService requestTypeAppService
        )
        {
            _uploadRequestService = uploadRequestAppService;
            _requestTypeAppService = requestTypeAppService;
        }

        public async Task<IActionResult> RequestList(string type,int page = 1)
        {
            bool? status = null;
            bool? isPending = null;
            switch (type)
            {
                case "1":
                    status = true;
                    break;
                case "0":
                    status = false;
                    break;
                case "2":
                    status = null;
                    isPending = true;
                    break;
                default:
                    status = null;
                    type = "20";
                    break;
            }

            var lst = new List<object>
            {
                new {Id = "0",Name="طلبات منتهية بالرفض"},
                new {Id = "1",Name="طلبات منتهية بالموافقة"},
                new {Id = "2",Name="طلبات قيد الإجراء"},
                new {Id = "20",Name="كل الطلبات"},
            };
            ViewBag.items = new SelectList(lst, "Id", "Name", type);

            ViewBag.status = type;
            var result = await _uploadRequestService.SearchAsync(
                new UploadRequestSearchDto()
                {
                    isPending = isPending,
                    IsApproved = status,
                    PageNumber = page,
                    PageSize = 10
                }
            );
            return View(result);
        }

        public async Task<IActionResult> Details(string requestId)
        {
            var result = await _uploadRequestService.GetById(Guid.Parse(requestId));

            return View(result.MapTo<UploadRequestDto>());
        }
        public async Task<IActionResult> Accept(string requestId)
        {
            await _uploadRequestService.AcceptOrReject(Guid.Parse(requestId), true);
            return RedirectToAction("Details",new { requestId });
        }
        public async Task<IActionResult> Reject(string requestId,string notes)
        {
            await _uploadRequestService.AcceptOrReject(Guid.Parse(requestId), false, notes);
            return RedirectToAction("Details",new { requestId });
        }
        // [HttpGet]
        //public async Task<IActionResult> Users(int? page, string search)
        //{
        //    ViewBag.Search = search;
        //    List<UserIdentity> resultList = new List<UserIdentity>();
        //    var extenalEntities = await _externalEntityService.GetExternalEntityAsync();
        //    System.Security.Claims.Claim result = GetCurrentUserClaimRelatedToExternalEntity(
        //        extenalEntities
        //    );
        //    ExternalEntityDto currentEntity = null;

        //    if (result != null)
        //    {
        //        resultList = (List<UserIdentity>)await _userManager.GetUsersForClaimAsync(result);
        //        currentEntity = extenalEntities.FirstOrDefault(c => c.ClaimValue == result.Value);
        //    }
        //    ExternalEntityUsersDto usersEntities = new ExternalEntityUsersDto
        //    {
        //        TotalCount = 0,
        //        PageSize = 10,
        //        Users = new List<UserExternalEntityDto>()
        //    };
        //    var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        //    resultList = resultList
        //        .Where(c => c.Id != email)
        //        .Where(c => string.IsNullOrEmpty(search) || c.Email.Contains(search))
        //        ?.ToList();

        //    if (resultList?.Any() ?? false)
        //    {
        //        var admins = await _userManager.GetUsersInRoleAsync("OwningEntityAdmin");
        //        var pms = await _userManager.GetUsersInRoleAsync("OwnerProjectManager");
        //        var entry = await _userManager.GetUsersInRoleAsync("OwningEntity");
        //        var users = entry
        //            .Where(c => !pms.Any(p => p.UserName == c.UserName))
        //            .Where(c => !admins.Any(p => p.UserName == c.UserName))
        //            //  .Select(c => c.Id.ToString())
        //            .ToArray();

        //        string[] userIds = users.Select(c => c.Id.ToString()).ToArray();
        //        resultList = resultList.Where(c => userIds.Contains(c.Id)).ToList();

        //        usersEntities = await _userExternalEntityService.GetUsersExternalEntityAsync(
        //            page.HasValue ? page.Value - 1 : 0,
        //            resultList.Select(c => c.Id).ToArray()
        //        );
        //        foreach (var item in usersEntities.Users)
        //        {
        //            var u = resultList.FirstOrDefault(c => c.Id == item.UserId);
        //            var user = users.FirstOrDefault(c => c.Id.ToString() == u.Id);
        //            var claims = await _userManager.GetClaimsAsync(user);

        //            item.ExternalEntity = currentEntity;
        //            item.Email = user?.Email;
        //            item.PhoneNumber = user?.PhoneNumber;
        //            item.UserName =
        //                claims?.FirstOrDefault(c => c.Type == "given_name")?.Value
        //                ?? user?.UserName;
        //        }

        //        if (usersEntities.Users.Any(c => c.SubExternalEntityId != null))
        //        {
        //            var entities = await _subExternalEntityService.GetSubExternalEntityByIdsAsync(
        //                usersEntities.Users.Select(c => c.SubExternalEntityId).ToArray()
        //            );

        //            foreach (var item in usersEntities.Users)
        //            {
        //                item.SubExternalEntity =
        //                    entities.FirstOrDefault(c => c.Id == item.SubExternalEntityId) ?? null;
        //            }
        //        }
        //    }

        //    return View(usersEntities);
        //}
    }
}
