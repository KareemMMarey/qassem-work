using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Services.Lookups.Main.CommonEService
{
    public class CommonEServiceDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string DescriptionAr { get; set; }
        public int CategoryId { get; set; }
		public string DurationDays { get; set; }
		public string DescriptionEn { get; set; }
        public bool HasSubCategory { get; set; }
        public string ServiceRequierment { get; set; }
        public decimal ServiceFees { get; set; }
        public string Audience { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
