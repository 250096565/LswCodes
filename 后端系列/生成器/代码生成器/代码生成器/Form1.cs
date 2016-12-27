using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 代码生成器.Sql;

namespace 代码生成器
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        public static string BasePath = "/BuildCodes/";

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString = "Data Source=.;User Id=test;Password=test;DataBase=test";

        /// <summary>
        /// 显示表名列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnViewList_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtConnection.Text))
            {
                ConnectionString = TxtConnection.Text;
            }
            ViewList.Items.Clear();
            SqlHelper helper = new SqlHelper(ConnectionString);

            var TableList = helper.GetTables();
            ViewList.View = View.List;
            ViewList.BeginUpdate();
            foreach (var table in TableList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = table;
                ViewList.Items.Add(item);
            }
            ViewList.EndUpdate();
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtConnection.Text))
            {
                ConnectionString= TxtConnection.Text;
            }
            if (string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                MessageBox.Show("请输入要搜索的表名");
                return;
            }
            ViewList.Items.Clear();
            SqlHelper helper = new SqlHelper(ConnectionString);
            var list = await helper.GetTablesByName(txtTableName.Text);
            var resultList = new List<ListViewItem>();
            foreach (var item in list)
            {
                if (item.Contains(txtTableName.Text))
                {
                    resultList.Add(new ListViewItem() { Text = item });
                }
            }
            if (resultList.Count <= 0)
            {
                MessageBox.Show("没有搜索到相应的表,请更换搜索关键词");
                return;
            }
            ViewList.View = View.List;
            ViewList.Items.AddRange(resultList.ToArray());

        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnBuild_Click(object sender, EventArgs e)
        {
            var list = ViewList.SelectedItems;
            if (list.Count <= 0)
            {
                MessageBox.Show("请选择要生成的表");
                return;
            }

            if (!string.IsNullOrWhiteSpace(TxtConnection.Text))
            {
                ConnectionString = TxtConnection.Text;
            }
              SqlHelper helper = new SqlHelper(ConnectionString);
                foreach (ListViewItem item in list)
                {
                    var tableName = item.Text;
                    var resultList = await helper.GetTableInformation(tableName);
                    Task.Factory.StartNew(() => BuildDto(resultList));
                    Task.Factory.StartNew(() => BuildSerivces(tableName));
                }
           

            Task.WaitAll();
            MessageBox.Show("生成成功");
        }


        /// <summary>
        /// 生成Services
        /// </summary>
        /// <param name="tableName"></param>
        public void BuildSerivces(string tableName)
        {
            string serviceName = tableName + "Services";
            string servicePath = BasePath + tableName + "/Services";
            StringBuilder serviceCs = new StringBuilder();
            if (!Directory.Exists(servicePath))
                Directory.CreateDirectory(servicePath);
            //生成Services
            Stream outStream = new FileStream($"{servicePath}/{serviceName}.cs", FileMode.Create);
            StreamWriter write = new StreamWriter(outStream, Encoding.Default);
            write.WriteLine($"using DTO;{Environment.NewLine}using EFDataAccess;{Environment.NewLine}using System.Data.Entity;{Environment.NewLine}using System.Collections.Generic;{Environment.NewLine}using System.Data.SqlClient;{Environment.NewLine}using System.Linq;{ Environment.NewLine}using AutoMapperExt.AutoMapper;{Environment.NewLine}");

            serviceCs.Append($"{Environment.NewLine}{Environment.NewLine}" +
               $"namespace Service{Environment.NewLine}{{{Environment.NewLine}" +
               $"    public class {serviceName} : BaseService<{tableName}>" +
               $"    {Environment.NewLine}   {{{Environment.NewLine}{Environment.NewLine}");

            //同步和异步版本增删改查
            for (int i = 0; i < 2; i++)
            {
                //查询
                serviceCs.Append($"        /// <summary>{Environment.NewLine}        /// 根据Id获取记录{Environment.NewLine}        /// </summary>{Environment.NewLine}        /// <param name=\"id\">ID</param>{Environment.NewLine}        /// <returns>一条记录</returns>{Environment.NewLine}");
                serviceCs.Append(i == 0 ? $"        public {tableName}Dto GetById(int id){Environment.NewLine}        {{" : $"        public async Task<{tableName}Dto> GetByIdAsync(int id){Environment.NewLine}        {{");
                serviceCs.Append(i == 0 ? $"{Environment.NewLine}           return Select(id).FirstOrDefault().MapTo<{tableName}Dto>(); {Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}" : $"{Environment.NewLine}           var result = await Select(id).FirstOrDefaultAsync();{Environment.NewLine}           return result.MapTo<{tableName}Dto>();{Environment.NewLine}       }}{Environment.NewLine}{Environment.NewLine}");

                //修改
                serviceCs.Append($"        /// <summary>{Environment.NewLine}        /// 修改记录{Environment.NewLine}        /// </summary>{Environment.NewLine}        /// <param name=\"dto\">dto</param>{Environment.NewLine}        /// <returns>是否成功</returns>{Environment.NewLine}");
                serviceCs.Append(i == 0 ? $"        public bool Update({tableName}Dto dto){Environment.NewLine}        {{" : $"        public async Task<bool> UpdateAsync({tableName}Dto dto){Environment.NewLine}        {{");
                serviceCs.Append(i == 0 ? $"{Environment.NewLine}           Update(dto.MapTo<{tableName}>());{Environment.NewLine}           return Save() > 0;{Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}" : $"{Environment.NewLine}           Update(dto.MapTo<{tableName}>());{Environment.NewLine}           return await SaveAsync();{Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}");

                //添加
                serviceCs.Append($"        /// <summary>{Environment.NewLine}        /// 新增{Environment.NewLine}        /// </summary>{Environment.NewLine}        /// <param name=\"dto\">dto</param>{Environment.NewLine}        /// <returns>是否成功</returns>{Environment.NewLine}");
                serviceCs.Append(i == 0 ? $"        public bool Add({tableName}Dto dto){Environment.NewLine}        {{" : $"        public async Task<bool> AddAsync({tableName}Dto dto){Environment.NewLine}        {{");
                serviceCs.Append(i == 0 ? $"{Environment.NewLine}           Add(dto.MapTo<{tableName}>());{Environment.NewLine}           return Save() > 0;{Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}" : $"{Environment.NewLine}           Add(dto.MapTo<{tableName}>());{Environment.NewLine}           return await SaveAsync();{Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}");

                //删除
                serviceCs.Append($"        /// <summary>{Environment.NewLine}        /// 删除一条记录{Environment.NewLine}        /// </summary>{Environment.NewLine}        /// <param name=\"Id\">Id</param>{Environment.NewLine}        /// <returns>是否成功</returns>{Environment.NewLine}");
                serviceCs.Append(i == 0 ? $"        public bool Del(int id){Environment.NewLine}        {{" : $"        public async Task<bool> Del(int id){Environment.NewLine}        {{");
                serviceCs.Append(i == 0 ? $"{Environment.NewLine}           Delete(id);{Environment.NewLine}           return Save() > 0;{Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}" : $"{Environment.NewLine}           Delete(id);{Environment.NewLine}           return await SaveAsync();{Environment.NewLine}        }}{Environment.NewLine}{Environment.NewLine}");
            }

            serviceCs.Append($"{Environment.NewLine}{Environment.NewLine}   }}{Environment.NewLine}}}");

            write.WriteLine(serviceCs.ToString());
            write.Flush();

        }

        /// <summary>
        /// 生成dto
        /// </summary>
        /// <param name="list"></param>
        public void BuildDto(List<TableInFormation> list)
        {
            string dtoName = list[0].TableName + "Dto";

            string dtoPath = BasePath + list[0].TableName + "/Dto";
            if (!Directory.Exists(dtoPath))
                Directory.CreateDirectory(dtoPath);
            //生成Dto
            Stream outStream = new FileStream($"{dtoPath}/{dtoName}.cs", FileMode.Create);
            StreamWriter write = new StreamWriter(outStream, Encoding.Default);
            write.WriteLine($"using AutoMapperExt.AutoMapper;{ Environment.NewLine}using EFDataAccess;{ Environment.NewLine}using Enums;{ Environment.NewLine}using Newtonsoft.Json;{ Environment.NewLine}using System;{ Environment.NewLine}using System.ComponentModel.DataAnnotations;");

            StringBuilder dtoCs = new StringBuilder();
            dtoCs.Append($"{Environment.NewLine}{Environment.NewLine}" +
                $"namespace DTO{Environment.NewLine}{{{Environment.NewLine}" +
                $"    [AutoMap(typeof({list[0].TableName}))]{Environment.NewLine}" +
                $"    public class {dtoName}" +
                $"    {Environment.NewLine}   {{{Environment.NewLine}{Environment.NewLine}");

            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item.Description))
                    dtoCs.Append($"        /// <summary>{Environment.NewLine}        /// {item.Description}{Environment.NewLine}        /// </summary>{Environment.NewLine}");

                if (!item.IsNull)
                    dtoCs.Append($"        [Required(ErrorMessage=\"{item.Filed}不可以为空\")]{Environment.NewLine}");
                dtoCs.Append($"        public {item.FiledType} {item.Filed}  {{get;set;}}{Environment.NewLine}{Environment.NewLine}");
            }

            dtoCs.Append($"{Environment.NewLine}{Environment.NewLine}   }}{Environment.NewLine}}}");

            write.WriteLine(dtoCs.ToString());
            write.Flush();
        }
    }
}
