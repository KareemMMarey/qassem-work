using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Web.ViewModels.Request
{
    public class MyRequestsViewModel
    {
        public RequestSearchFilterDto Filter { get; set; }
        public List<ServiceRequestDto> Results { get; set; }
    }
}
