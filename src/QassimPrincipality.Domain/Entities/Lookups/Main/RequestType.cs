using Framework.Core.Data;
using Framework.Core.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

namespace QassimPrincipality.Domain.Entities.Lookups.Main
{
    public class RequestType : LookupEntityBase
    {
        public string Code { get; set; }

        [NotMapped]
        public string RequestTypeName => CultureHelper.IsArabic ? NameAr : NameEn;
    }
}