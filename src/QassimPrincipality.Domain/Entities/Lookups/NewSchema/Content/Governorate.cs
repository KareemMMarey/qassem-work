using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content
{
    public class Governorate : LookupEntityBase
    {
        

        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public int Order { get; set; }
    }
}
