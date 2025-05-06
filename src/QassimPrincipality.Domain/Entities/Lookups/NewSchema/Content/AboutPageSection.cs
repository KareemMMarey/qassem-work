using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content
{
    public class AboutPageSection : LookupEntityBase
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

    }
}
