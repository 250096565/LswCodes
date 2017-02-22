
using Entity.Model;
using EntityFrameWork;
using System;
using System.Linq;

namespace CodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FirstDbContext db = new FirstDbContext())
            {

                //var u1 = db.User.AsNoTracking().FirstOrDefault();


                //u1.Name = "小李";

                //using (var db2 = new FirstDbContext())
                //{
                //    var u2 = db2.User.AsNoTracking().FirstOrDefault();

                //    u2.Name = "小四";
                //    db2.Entry(u2).State = System.Data.Entity.EntityState.Modified;

                //    var reuslt = db2.SaveChanges();
                //}
                //db.Entry(u1).State = System.Data.Entity.EntityState.Modified;
                //var aa = db.SaveChanges();

                #region 准备数据 
                //db.User.Add(new User() { Id = Guid.NewGuid().ToString(), Name = "小明", Age = "23" });
                //db.User.Add(new User() { Id = Guid.NewGuid().ToString(), Name = "小李", Age = "27" });


                //db.SaveChanges();
                //db.UserRole.Add(new UserRole() { Name = "小明角色", Users = db.User.ToList() });
                ////db.UserRole.Add(new UserRole() { Name = "小李" });
                //db.SaveChanges();

                var userrole = db.UserRole.FirstOrDefault();
                db.UserRole.Remove(userrole);

                //var user = db.User.FirstOrDefault();
                //db.User.Remove(user);



                db.SaveChanges();
                #endregion
            }
        }
    }
}
