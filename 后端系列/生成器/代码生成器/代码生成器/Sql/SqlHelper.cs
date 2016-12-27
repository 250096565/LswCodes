using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace 代码生成器.Sql
{
    public class SqlHelper
    {
        public string ConnectionString { get; set; }

        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }


        /// <summary>
        /// 拿到所有的表名列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetTables()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "select name from dbo.sysobjects where xtype='u' and (not name LIKE 'dtproperties')";
                    var reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    List<string> list = new List<string>();

                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(row["name"] as string);
                    }

                    return list;
                }
            }

        }


        /// <summary>
        /// 根据表名搜索列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<string>> GetTablesByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select name from dbo.sysobjects where xtype='u' and (not name LIKE 'dtproperties') and name like @name";
                    cmd.Parameters.Add(new SqlParameter("@name", "%" + name + "%"));
                    var reader = await cmd.ExecuteReaderAsync();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    List<string> list = new List<string>();

                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(row["name"] as string);
                    }

                    return list;
                }
            }
        }


        /// <summary>
        /// 根据表名搜索表信息
        /// </summary>
        /// <param name="name"></param>
        public async Task<List<TableInFormation>> GetTableInformation(string name)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT" +
                        " TableName =case  when a.colorder = 1   then d.name   else   ''   end," +
                        " Field = a.name," +
                        " FieldType= b.name," +
                        " [IsNull]=case   when a.isnullable=1   then   1 else   0   end," +
                        " Description= isnull(g.[value], '') FROM syscolumns   a " +
                        " left   join systypes   b on   a.xusertype= b.xusertype" +
                        " inner join   sysobjects d   on a.id= d.id     and d.xtype= 'U'   and d.name<>'dtproperties'" +
                        " left join   syscomments e   on a.cdefault= e.id left join   sys.extended_properties g   on a.id= g.major_id   and a.colid= g.minor_id" +
                        " left join   sys.extended_properties f   on d.id= f.major_id   and f.minor_id= 0 where d.name= @name order   by a.id, a.colorder";

                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    var reader = await cmd.ExecuteReaderAsync();

                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    var list = new List<TableInFormation>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new TableInFormation()
                        {
                            TableName = row["TableName"]?.ToString(),
                            Filed = row["Field"]?.ToString(),
                            FiledType = CSharp_DB_Mapping[row["FieldType"]?.ToString()] as string,
                            IsNull = Convert.ToBoolean(row["IsNull"]),
                            Description = row["Description"]?.ToString()
                        });

                    }
                    return list;
                }
            }
        }

        /// <summary>
        /// 数据库类型与.NET类型映射
        /// </summary>
        public static Hashtable CSharp_DB_Mapping
        {
            get
            {
                return
                    new Hashtable()
            {
                {"int", "int"},
                {"text", "string"},
                {"bigint", "long"},
                {"binary", "Byte[]"},
                {"bit", "bool"},
                {"char", "string"},
                {"datetime", "DateTime"},
                {"datetime2", "DateTime"},
                {"date", "DateTime"},
                {"decimal", "Decimal"},
                {"float", "Double"},
                {"image", "Byte[]"},
                {"money", "Decimal"},
                {"nchar", "string"},
                {"ntext", "string"},
                {"numeric", "Decimal"},
                {"nvarchar", "string"},
                {"real", "Single"},
                {"smalldatetime", "DateTime"},
                {"smallint", "Int16"},
                {"smallmoney", "Decimal"},
                {"timestamp", "DateTime"},
                {"tinyint", "int"},
                {"uniqueidentifier", "Guid"},
                {"varbinary", "Byte[]"},
                {"varchar", "string"},
                {"variant", "Object"},
                {"NUMBER", "Decimal"},
                {"NVARCHAR2", "string"},
                {"VARCHAR2", "string"},
                {"DATE", "DateTime"},
                {"CHAR", "string"},
                {"CLOB", "string"},
            };
            }
        }


    }
}


