using System;


namespace Dm.AiMa.Domain
{
    /// <summary>
    /// 只实现了添加和修改的审计
    /// </summary>
    public class BaseEntityWithoutSoftDeleteAudited 
    {

        public virtual DateTime CreationTime { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual long UserId { get; set; }

      

        public long? LastModifierUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
