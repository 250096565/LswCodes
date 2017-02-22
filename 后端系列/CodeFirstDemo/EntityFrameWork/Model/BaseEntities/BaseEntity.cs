using System;


namespace Dm.AiMa.Domain
{
    public class BaseEntity
    {


        public virtual long? CreatorUserId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual long UserId { get; set; }


    }
}
