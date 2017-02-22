using System;

using System.ComponentModel.DataAnnotations.Schema;

namespace Dm.AiMa.Domain
{
    /// <summary>
    /// 只实现了添加时间的审计
    /// </summary>
    public class BaseEntityOnlyWithCreationTime 
    {

        public virtual DateTime CreationTime { get; set; }
          
        public virtual long UserId { get; set; }

     
    }
}
