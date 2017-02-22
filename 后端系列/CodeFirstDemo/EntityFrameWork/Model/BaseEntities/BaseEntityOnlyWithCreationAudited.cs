using System;


namespace Dm.AiMa.Domain
{
    /// <summary>
    /// 只实现了添加的审计
    /// </summary>
    public class BaseEntityOnlyWithCreationAudited 
    {

     

        public virtual DateTime CreationTime { get; set; }

        public virtual long? CreatorUserId { get; set; }

        public virtual long UserId { get; set; }

     
    }
}
