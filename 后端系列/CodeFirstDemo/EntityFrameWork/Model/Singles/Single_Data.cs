using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Dm.AiMa.Domain.Tags;
using Dm.AiMa.Domain.Banks;

namespace Dm.AiMa.Domain.Singles
{
    /// <summary>
    /// 单个密码库的数据项
    /// </summary>
    [Table("Data", Schema = SchemaConsts.Single)]
    public class Single_Data : BaseEntityWithoutSoftDeleteAudited
    {
        public Single_Data()
        {
        }


        [Key]
        public virtual int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank_Own Bank { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(StringLengthConsts.Bank.Item.Name)]
        public virtual string Name { get; set; }

   

        /// <summary>
        /// 网址
        /// </summary>
        [StringLength(StringLengthConsts.Bank.Item.Url)]
        public virtual string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(StringLengthConsts.Bank.Item.Description)]
        public virtual string Description { get; set; }

        public virtual ICollection<Tag_Own> Tags { get; set; }


        public virtual ICollection<Single_Authorization> Single_Authorizations { get; set; }
    }
}
