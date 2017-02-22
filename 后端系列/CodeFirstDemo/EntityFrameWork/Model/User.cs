using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{

    [Table("T_User")]
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string Age { get; set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
