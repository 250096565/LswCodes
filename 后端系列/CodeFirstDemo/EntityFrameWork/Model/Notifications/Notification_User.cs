using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dm.AiMa.Domain.Notifications
{
    /// <summary>
    /// 用户通知
    /// </summary>
    [Table("User", Schema = SchemaConsts.Notification)]
    public class Notification_User : BaseEntityOnlyWithCreationAudited
    {
        ///// <summary>
        ///// 操作
        ///// </summary>
        //[Required]
        //[StringLength(StringLengthConsts.Notification.Action)]
        //public virtual string Action { get; set; }

        ///// <summary>
        ///// 其他用户名称
        ///// </summary>
        //[Required]
        //[StringLength(AbpUserBase.MaxUserNameLength)]
        //public virtual string OtherUserName { get; set; }
         
        /// <summary>
        /// 操作内容
        /// </summary>
        [Required]
        [StringLength(StringLengthConsts.Notification.Content)]
        public virtual string Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary> 
        [StringLength(StringLengthConsts.Notification.Type)]
        public virtual string Type { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public virtual bool IsRead { get; set; }

        public class ContentTemplates
        {
            public const string Get = "获得 {0} 授权，{1}：{2}";
            public const string Drop = "失去 {0} 授权，{1}：{2}";
            public const string DeleteAuthorization = "{0} 删除了来自你的授权，{1}：{2}";
        }

        public class TypeConsts
        {
            public const string Get = "Get";
            public const string Drop = "Drop";
            public const string DeleteAuthorization = "DeleteAuthorization";
        }
    }

}
