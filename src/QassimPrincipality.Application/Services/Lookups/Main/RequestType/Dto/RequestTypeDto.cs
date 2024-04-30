using Framework.Core.Globalization;

namespace QassimPrincipality.Application.Services.Lookups.Main.RequestType.Dto
{
    public class RequestTypeDto
    {
        public int Id { get; set; }
        public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
    }
}