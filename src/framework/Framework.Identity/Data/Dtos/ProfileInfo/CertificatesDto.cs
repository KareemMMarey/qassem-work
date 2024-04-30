using Framework.Core.Data;
using System;

namespace Framework.Identity.Data.Dtos.ProfileInfo
{
    public class CertificatesDto : EntityDto<Guid>
    {
        public Guid ApplicationUserId { get; set; }
        public Guid CertificateId { get; set; }
        public string Text { get; set; }
    }
}