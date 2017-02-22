using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dm.AiMa.Domain.Singles
{
    /// <summary>
    /// 授权的
    /// </summary>
    [Table("Authorization", Schema = SchemaConsts.Single)]
    public class Single_Authorization : BaseEntityOnlyWithCreationAudited
    {

        [Key]
        public virtual long AuthorizationToUserId { get; set; }

        public virtual int SingleDataId { get; set; }

        [ForeignKey("SingleDataId")]
        public virtual Single_Data SingleData { get; set; }

    }
}
