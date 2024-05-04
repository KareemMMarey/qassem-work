using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Web.ViewModels.Request
{
    public class ListVM
    {
        public ListVM()
        {
            Requests = new List<AddRequestViewModel>();
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<AddRequestViewModel> Requests { get; set; }
    }
    
}
