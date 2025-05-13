using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
    public class ServiceStep : LookupEntityBase
    {
        [Required]
        [MaxLength(500)]
        public string StepNameAr { get; set; }

        [Required]
        [MaxLength(500)]
        public string StepNameEn { get; set; }

        [MaxLength(2000)]
        public string DescriptionAr { get; set; }

        [MaxLength(2000)]
        public string DescriptionEn { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        [Required]
        public int Order { get; set; }

        // علاقات
        public virtual EService EService { get; set; }
        public virtual ICollection<ServiceDataSource> DataSources { get; set; }

        public ServiceStep()
        {
            DataSources = new List<ServiceDataSource>();
        }
    }
}
