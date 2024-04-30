using QassimPrincipality.Application.Dtos;

namespace QassimPrincipality.Application.Services.Main.UploadRequest.Dto
{
    public class SanitizedDocumentDto
    {
        public Guid? UploadRequestId { get; set; }
        public Guid? SanitizedDocumentId { get; set; }
        public string SanitizedDocumentFileName { get; set; }
        public AttachmentDto[] SanitizedDocuments { get; set; }
        public int? LevelOfSecrecy { get; set; }
    }
}