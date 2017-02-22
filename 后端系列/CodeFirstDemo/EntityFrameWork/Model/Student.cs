using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    [Table("T_Student")]
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Tearcher Tearcher { get; set; }
    }


    [Table("T_Tearcher")]
    public class Tearcher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
