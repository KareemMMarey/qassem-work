using Framework.Core.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Lookups.Dto
{
    public class RequestSubcategoryDto
    {
        public int Id { get; set; }
        public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}
