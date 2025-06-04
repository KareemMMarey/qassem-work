using QassimPrincipality.Domain.Enums;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
    public class WorkFlowItem
    {
        public ServiceRequestStatus Id { get; set; }
        public ServiceRequestStatus[] AllowedStatusses { get; set; }

    }
}
