using Framework.Core.Data;
using System;

namespace Framework.Identity.Data.Dtos.ProfileInfo
{
    public class SkillsDto : EntityDto<Guid>
    {
        public Guid ApplicationUserId { get; set; }
        public string Text { get; set; }
    }
}