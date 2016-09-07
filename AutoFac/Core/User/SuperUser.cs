using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.User
{
    /// <summary>
    /// 超级用户
    /// </summary>
    public class SuperUser : User
    {
        /// <summary>
        /// 超级用户卡号
        /// </summary>
        public string UserNum { get; set; }
    }

   
}
