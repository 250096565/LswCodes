using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Dm.AiMa.Domain.Banks;
using Dm.AiMa.Domain.Singles;

namespace Dm.AiMa.Domain.Tags
{
    /// <summary>
    /// 标签（自己的）
    /// </summary>
    [Table("Own", Schema = SchemaConsts.Tag)]
    public class Tag_Own : BaseEntityWithoutSoftDeleteAudited
    {
        public Tag_Own()
        {
           
        }
         
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(StringLengthConsts.Tag.Name)]
        public virtual string Name { get; set; }

        public virtual int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank_Own Bank { get; set; }

        [ForeignKey("TagId")]
        public virtual ICollection<Tag_Authorization> TagAuthorizations { get; set; }
         
        public virtual ICollection<Single_Data> SingleDatas { get; set; }
    }
}
