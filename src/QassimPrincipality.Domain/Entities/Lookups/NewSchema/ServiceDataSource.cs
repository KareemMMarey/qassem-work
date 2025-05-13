using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Data;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
    public class ServiceDataSource : LookupEntityBase
    {

        [Required]
        public int StepId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DataSourceType { get; set; } // مثل "نفاذ" أو "سبل"

        [MaxLength(1000)]
        public string EndpointUrl { get; set; } // رابط التكامل مع المصدر

        
        public virtual ServiceStep ServiceStep { get; set; }
    }
}
