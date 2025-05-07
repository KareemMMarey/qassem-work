using Framework.Core.Data;
using QassimPrincipality.Domain.Enums;
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
        
        public AboutSectionType AboutSectionType { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

    }
}
