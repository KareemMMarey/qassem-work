using Framework.Core.Data;
using Framework.Core.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

namespace QassimPrincipality.Domain.Entities.Lookups.Main
{
    public class Classification : LookupEntityBase
    {
        public string Code { get; set; }

        

        [NotMapped]
        public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
    }
}