using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
    public class GetEServiceDetailsDto
    {

        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string ServiceCode { get; set; }
        public string IconUrl { get; set; }

        public string ServiceController { get; set; }
        public string ServiceActionMethos { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        // Details
        public string AudienceTypeAr { get; set; }
        public string AudienceTypeEn { get; set; }
        public string ExecutionTimeAr { get; set; }
        public string ExecutionTimeEn { get; set; }
        public string CostAr { get; set; }
        public string CostEn { get; set; }
        public List<string> ServiceRequirement { get; set; }
        public List<string> ServiceFlow { get; set; }
        public List<ServiceFAQDto> ServiceFAQs { get; set; }

        // Rating
        public string RateValue { get; set; }
    }
    public class ServiceFAQDto {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AnswerAr { get; set; }
        public string AnswerEn { get; set; }
    }
}
