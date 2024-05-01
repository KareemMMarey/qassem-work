using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class UploadRequestDtoEdit : UploadRequestDto
    {
        public string SerialNumber { get; set; }
        public int RequestSubClassificationId { get; set; }
        public int requestClassificationId { get; set; }
        public int RequestTypeId { get; set; }
        
        public Guid? RequestOwnerId { get; set; }
        public AttachmentDto[] OpenSourceArFiles { get; set; }
        public AttachmentDto[] OpenSourceEnFiles { get; set; }
        public AttachmentDto[] CloseSourceArFiles { get; set; }
        public AttachmentDto[] CloseSourceEnFiles { get; set; }
        public AttachmentDto[] DataFiles { get; set; }
        public AttachmentDto[] SupportingFiles { get; set; }
        public List<Guid> DeletedAttachmentsIds { get; set; }
        public List<AttachmentDto> ExistAttachments { get; set; }
    }
}