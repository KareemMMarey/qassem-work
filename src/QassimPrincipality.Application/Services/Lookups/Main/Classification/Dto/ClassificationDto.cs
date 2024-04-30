using Framework.Core.Globalization;

namespace QassimPrincipality.Application.Services.Lookups.Main.RequestClassification.Dto
{
    public class RequestClassificationDto
    {
        public int Id { get; set; }
        public string Name => CultureHelper.IsArabic ? NameAr : NameEn;
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public int SubClassificationsCount { get; set; }
        public string Code { get; set; }
    }
}