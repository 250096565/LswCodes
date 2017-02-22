using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dm.AiMa.Domain.Tags;
using Dm.AiMa.Domain.Banks;
using Dm.AiMa.Domain.Singles;

namespace Dm.AiMa.Domain.Activities
{
    /// <summary>
    /// 活动记录（一些基本操作）
    /// </summary>
    [Table("System", Schema = SchemaConsts.Activity)]
    public class Activity_System : BaseEntityOnlyWithCreationTime
    {
        public Activity_System()
        {
            Target_Type = "System";
        }

        [Key]
        public virtual int BankId { get; set; }

     
        public virtual int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag_Own Tag { get; set; }

        public virtual int SingleId { get; set; }

        [ForeignKey("SingleId")]
        public virtual Single_Data Single { get; set; }

        /// <summary>
        /// 操作关键字
        /// </summary>
        [Required]
        [StringLength(StringLengthConsts.Activity.Action)]
        public virtual string Action { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [Required]
        [StringLength(StringLengthConsts.Activity.Action_Msg)]
        public virtual string Action_Msg { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary> 
        public virtual ActionData Action_Data { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [StringLength(StringLengthConsts.Activity.Type)]
        public virtual string Type { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        [Required]
        [StringLength(StringLengthConsts.Activity.Target_Type)]
        public virtual string Target_Type { get; set; }

        public class ActionConsts
        {
            public const string Add_Bank = "Add_Bank";
            public const string Update_Bank = "Update_Bank";
            public const string Delete_Bank = "Delete_Bank";

            public const string Add_Tag = "Add_Tag";
            public const string Update_Tag = "Update_Tag";
            public const string Delete_Tag = "Delete_Tag";

            public const string Add_Single = "Add_Single";
            public const string Update_Single = "Update_Single";
            public const string Delete_Single = "Delete_Single";
        }

        public class TypeConsts
        {
            public const string Bank = "Bank";
            public const string Tag = "Tag";
            public const string Single = "Single";
        }

        [ComplexType]
        public class ActionData
        {
            [Required]
            [StringLength(StringLengthConsts.Activity.ActionData_Name)]
            public virtual string Name { get; set; }

        }
    }
}
