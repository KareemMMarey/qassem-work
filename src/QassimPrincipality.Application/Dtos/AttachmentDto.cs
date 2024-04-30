namespace QassimPrincipality.Application.Dtos
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public int AttachmentTypeId { get; set; }
        public byte[] Thumbnail { get; set; }
        public string FileContent { get; set; }
        public byte[] FileContentData { get; set; }
        public double Size { get; set; }

        //new
        public bool? IsOpenSourceArabic { get; set; } = false;

        public bool? IsOpenSourceEnglish { get; set; } = false;
        public bool? IsCloseSourceArabic { get; set; } = false;
        public bool? IsCloseSourceEnglish { get; set; } = false;
        public bool? IsData { get; set; } = false;
        public bool? IsSupporting { get; set; } = false;
        public bool? IsSanitizedDocument { get; set; } = false;
        public Guid? UploadRequestId { get; set; }
    }
}