using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
    public class GetEServiceListHome
    {

        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public bool HasApplicantStatus { get; set; } // صفة مقدم الطلب
        public bool HasTypeOfSummons { get; set; }
        public string ServiceCode { get; set; }
        public string IconUrl { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        // Details
        public string AudienceTypeAr { get; set; }
        public string AudienceTypeEn { get; set; }
        public string ExecutionTimeAr { get; set; }
        public string ExecutionTimeEn { get; set; }
        public string CostAr { get; set; }
        public string CostEn { get; set; }

        // Rating
        public string RateValue { get; set; }
    }
}
