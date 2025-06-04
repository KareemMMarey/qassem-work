using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
    public class ChangeStatusDto
    {
        public Guid RequestId { get; set; }
        public ServiceRequestStatus NewStatus { get; set; }
        public string ActionNotes { get; set; }
        public string UserId { get; set; }
    }


}
