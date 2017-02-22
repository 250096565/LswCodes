using System.ComponentModel.DataAnnotations.Schema;

using Dm.AiMa.Domain.Banks;
using System.ComponentModel.DataAnnotations;

namespace Dm.AiMa.Domain.Tags
{
    /// <summary>
    /// 授权
    /// </summary>
    [Table("Authorization", Schema = SchemaConsts.Tag)]
    public class Tag_Authorization : BaseEntityOnlyWithCreationAudited
    {

        [Key]
        public int Id { get; set; }

        public virtual int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag_Own Tag { get; set; }

        public virtual long AuthorizationToUserId { get; set; }


        public virtual int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank_Own Bank { get; set; }

    }
}
