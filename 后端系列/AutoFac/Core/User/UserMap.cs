using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace Core.User
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            //最大长度20
            Property(u => u.Name).HasMaxLength(20);
            //类型为varchar
            Property(u => u.Name).HasColumnType("varchar");
            //不可以为空
            Property(u => u.Name).IsRequired();



            //TPH玩法 
            //Map<Core.User.SuperUser>(u => u.Requires("From").HasValue("From-Super"));
            //Map<Core.User.User>(u => u.Requires("From").HasValue("From-User"));


            //TPT  
            Map<Core.User.SuperUser>(u => u.ToTable("T_SuperUser"));

            //TPC
            //Map<Core.User.SuperUser>(u =>
            //{
            //    u.ToTable("T_SuperUser");
            //    u.MapInheritedProperties();
            //});
        }
    }
}
