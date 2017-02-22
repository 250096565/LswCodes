using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dm.AiMa.Domain.Banks
{
    /// <summary>
    /// 授权的
    /// </summary>
    [Table("Authorization", Schema = SchemaConsts.Bank)]
    public class Bank_Authorization : BaseEntityOnlyWithCreationAudited
    {
        [Key]
        public virtual long AuthorizationToUserId { get; set; }


        public virtual int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank_Own Bank { get; set; }
    }
}
