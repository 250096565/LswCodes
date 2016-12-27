using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 代码生成器.Sql
{
    public class TableInFormation
    {
        public string TableName { get; set; }

        public string Filed { get; set; }

        public string FiledType { get; set; }

        public bool IsNull { get; set; }

        public string Description { get; set; }
    }
}
