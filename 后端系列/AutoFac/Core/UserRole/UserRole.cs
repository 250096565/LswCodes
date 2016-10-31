using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Core.UserRole
{
    [Table("T_UserRole")]
    public class UserRole
    {
        [Key]
        public string UserRoleId { get; set; }

        public string UserName { get; set; }


        #region 一对多版的User与UserRole关系
        //public User.User User { get; set; }

        //public Guid UserId { get; set; } 
        #endregion


        public List<User.User> Users { get; set; }


    }


    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            //一对一的配置
            //HasRequired(o => o.User).WithMany(u => u.UserRoles).Map(o => o.MapKey("UserId"));

            //多对多的配置
            HasMany(o => o.Users).WithMany(o => o.UserRoles).Map(m =>
            {
                m.ToTable("T_User_UserRole_Config");
                m.MapLeftKey("UserId");
                //这里为了区分,把id改成了UserRoleId
                m.MapRightKey("UserRoleId");
            });

        }
    }
}
