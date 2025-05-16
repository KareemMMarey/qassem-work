using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
    public class RequestSearchFilterDto
    {
        public string UserId { get; set; }
        public int? ServiceId { get; set; }
        public ServiceRequestStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
