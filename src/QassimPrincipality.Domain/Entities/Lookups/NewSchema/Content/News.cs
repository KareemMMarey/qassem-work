using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema.Content
{

    public class News : LookupEntityBase
    {

        public string ShortDescriptionAr { get; set; } = string.Empty;
        public string ShortDescriptionEn { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

    }


}
