using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
namespace 第八章委托.操作Word
{
    public class Office
    {
        public void Main()
        {
            //启动Word并使Word可见
            Application wordApp = new Application() { Visible = true };

            //新建word文档
            wordApp.Documents.Add();
            Document wordDoc = wordApp.ActiveDocument;
            Paragraph para = wordDoc.Paragraphs.Add();
            para.Range.Text = "这里是Word文档";

            //保存文档
            object fileName = @"D:\wordTest.doc";

            //使用命名参数使得代码更加易懂
            wordDoc.SaveAs(FileName:fileName);

            //关闭word
            wordDoc.Close();
            wordApp.Application.Quit();
        }

    }
}
