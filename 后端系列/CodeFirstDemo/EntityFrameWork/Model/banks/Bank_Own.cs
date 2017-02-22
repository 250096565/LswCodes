using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Dm.AiMa.Domain.Tags;
using Dm.AiMa.Domain.Activities;
using Dm.AiMa.Domain.Singles;

namespace Dm.AiMa.Domain.Banks
{
    /// <summary>
    /// 密码库（自己的）
    /// </summary>
    [Table("Own", Schema = SchemaConsts.Bank)]
    public class Bank_Own : BaseEntityWithoutSoftDeleteAudited
    {
        public Bank_Own()
        {
            IsEnabledUrl = true;
            Tags = new HashSet<Tag_Own>();
        }


        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(StringLengthConsts.Bank.Name)]
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(StringLengthConsts.Bank.Image)]
        public virtual string Image { get; set; }

        /// <summary>
        /// 是否开启Url
        /// </summary>
        public bool IsEnabledUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(StringLengthConsts.Bank.Description)]
        public string Description { get; set; }

        /// <summary>
        /// 是否常用
        /// </summary>
        public virtual bool Fix { get; set; }


        [ForeignKey("BankId")]
        public virtual ICollection<Tag_Own> Tags { get; set; }

        [ForeignKey("BankId")]
        public virtual ICollection<Single_Data> Datas { get; set; }

        [ForeignKey("BankId")]
        public virtual ICollection<Bank_Authorization> BankAuthorizations { get; set; }

        [ForeignKey("BankId")]
        public virtual ICollection<Activity_System> SystemActivities { get; set; }

    }
}
