using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.User
{
    /// <summary>
    /// 用户证件实体
    /// </summary>
    [Table("T_User_Card")]
    public class UserCard
    {
        public UserCard()
        {
            UserId = new Guid(Guid.NewGuid().ToString("D"));
        }
        [Key]
        public Guid UserId { get; set; }

        [MaxLength(18)]
        public string IdCard { get; set; }


        //一个证件只能属于一个人..一对一 
        public User User { get; set; }

    }

    public class UserCardMap : EntityTypeConfiguration<UserCard>
    {
        public UserCardMap()
        {

            //一对一的关系, User表中可以没有UserCard;但是UserCard表中必须有User -- 开启级联删除
            HasRequired(u => u.User).WithOptional(u => u.Card).WillCascadeOnDelete(true);
            //不推荐的写法 
            //HasRequired(u => u.User).WithMany().HasForeignKey(u => u.UserId);
        }
    }
}
