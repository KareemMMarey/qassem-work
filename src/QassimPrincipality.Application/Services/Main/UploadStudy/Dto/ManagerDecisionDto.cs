
namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class ManagerDecisionDto
    {
        public Guid? UploadRequestId { get; set; }
        public string NewRequestNameAr { get; set; }
        public string NewRequestNameEn { get; set; }
        public int? NewLevelOfSecrecyId { get; set; }
    }
}