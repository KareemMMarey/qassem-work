namespace QassimPrincipality.Application.Dtos
{
    public class FileDto
    {
        public byte[] Data { get; set; }
        public string ContentType { get; set; } // e.g. "application/pdf", "image/png"
        public string FileName { get; set; }
    }

}
