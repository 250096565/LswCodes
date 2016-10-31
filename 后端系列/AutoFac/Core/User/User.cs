using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.User
{
    [Table("T_User")]
    public class User
    {
        public User()
        {
            UserId = new Guid(Guid.NewGuid().ToString("D")); ;
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }

        public List<UserRole.UserRole> UserRoles { get; set; }

        /// <summary>
        /// 扩展
        /// </summary>
        public UserAddress UserAddress { get; set; }

        /// <summary>
        /// 一个用户拥有一个身份证
        /// </summary>
        public UserCard UserCard { get; set; }

    }

}
