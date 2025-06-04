using QassimPrincipality.Application.Dtos;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;

namespace QassimPrincipality.Web.ViewModels.Request
{
    public class MyRequestsViewModel
    {
        public RequestSearchFilterDto Filter { get; set; }
        public List<ServiceRequestDto> Results { get; set; }
        public List<SelectListDto> ServiceList { get; set; }
    }
}
