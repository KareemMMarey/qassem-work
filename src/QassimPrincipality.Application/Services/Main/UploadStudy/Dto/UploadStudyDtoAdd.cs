using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestDtoAdd : UploadRequestDto
    {
        public string SerialNumber { get; set; }
        public string RequestTitle { get; set; }
        public int RequestSubClassificationId { get; set; }
        public int requestClassificationId { get; set; }
        public int RequestTypeId { get; set; }
        public int? ConsultantId { get; set; }
        public int LevelOfSecrecyId { get; set; }
        public string RequestSource { get; set; }
        public Guid? RequestOwnerId { get; set; }
        public AttachmentDto[] OpenSourceArFiles { get; set; }
        public AttachmentDto[] OpenSourceEnFiles { get; set; }
        public AttachmentDto[] CloseSourceArFiles { get; set; }
        public AttachmentDto[] CloseSourceEnFiles { get; set; }
        public AttachmentDto[] DataFiles { get; set; }
        public AttachmentDto[] SupportingFiles { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}