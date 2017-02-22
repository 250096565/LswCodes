using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dm.AiMa.Domain.Notifications
{
    /// <summary>
    /// 系统通知
    /// </summary>
    [Table("System", Schema = SchemaConsts.Notification)]
    public class Notification_System : BaseEntityOnlyWithCreationAudited
    {
        ///// <summary>
        ///// 操作
        ///// </summary>
        //[Required]
        //[StringLength(StringLengthConsts.Notification.Action)] 
        //public virtual string Action { get; set; }

        /// <summary>
        /// 内容
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

        public class TypeConsts
        {
            public const string Bank = "Bank";
            public const string Assignment = "Assignment";
            public const string Authorization = "Authorization";
        }

        public class ContentTemplates
        {
            public const string Add = "{0} 添加密码库：{1}";
            public const string Delete = "{0} 删除密码库：{1}";
            public const string Assignment = "{0} 转让密码库：{1} 给 {2}";
            public const string Authorization = "{0} 授权密码库：{1} 给 {2}";
        }

        //public class ActionConsts
        //{
        //    public const string Add = "添加密码库";
        //    public const string Delete = "删除密码库"; 
        //}
    }

}
