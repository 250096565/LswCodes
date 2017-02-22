
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dm.AiMa.Domain.Banks
{
    /// <summary>
    /// 已转让 
    /// </summary>
    [Table("Assignment", Schema = SchemaConsts.Bank)]
    public class Bank_Assignment : BaseEntityOnlyWithCreationTime
    {
        [Required]
        [StringLength(StringLengthConsts.Bank.Name)]
        public virtual string Name { get; set; }
         
        [StringLength(StringLengthConsts.Bank.Image)]
        public virtual string Image { get; set; }

        public virtual long AssignmentToUserId { get; set; }

    }
}
