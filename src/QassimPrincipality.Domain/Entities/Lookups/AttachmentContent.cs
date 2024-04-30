using Framework.Core.Data;

namespace QassimPrincipality.Domain.Entities.Lookups
{
    public partial class AttachmentContent : EntityBase<Guid>
    {
        public byte[] Thumbnail { get; set; }
        public byte[] FileContent { get; set; }
        public Guid AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}