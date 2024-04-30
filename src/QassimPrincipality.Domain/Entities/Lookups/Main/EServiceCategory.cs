using Framework.Core.Data;
using Framework.Core.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZXing.QrCode.Internal.Mode;

namespace QassimPrincipality.Domain.Entities.Lookups.Main
{
    public class EServiceCategory : LookupEntityBase
    {
        public string Icon { get; set; }
        public string Url { get; set; }

        public string DescriptionAr { get; set; }

        public string DescriptionEn { get; set; }

        public List<EServiceSubCategory> EServiceSubCategories { get; set; }


        [NotMapped]
        public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
    }
}
