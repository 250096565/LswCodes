using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.User
{
    /// <summary>
    /// 用户地址类  复杂类型
    /// </summary>
    [Table("T_User_Address")]
    [ComplexType]
    public class UserAddress
    {
        /// <summary>
        /// 动态地址
        /// </summary>
        public string DynamicAddress { get; set; }

        public string City { get; set; }


    }
}
