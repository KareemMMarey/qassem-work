using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
    [Serializable]
    public class RequestSearchFilterDto
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("requestNumber")]
        public string RequestNumber { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("serviceId")]
        public int? ServiceId { get; set; }
        [JsonPropertyName("status")]
        public ServiceRequestStatus? Status { get; set; }
        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
    }
}
