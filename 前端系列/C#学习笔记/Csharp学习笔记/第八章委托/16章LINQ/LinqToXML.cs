using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace 第八章委托._16章LINQ
{
    public class LinqToXml
    {
        public static string XmlString = "<person>" +
                                         "<person id='1'>" +
                                         "<name>张三</name>" +
                                         "</person>" +
                                         "<person>" +
                                         "<person id='2'>" +
                                         "<name>李四</name>" +
                                         "</person>";
        //模拟的Main方法
        public void Main()
        {

        }

        public void Query(string xmlString)
        {
            //导入XML
            XElement xmlDoc = XElement.Parse(xmlString);
            var queryResult = from temp in xmlDoc.Elements("person")
                              where xmlDoc.Element("name")?.Value == "李四"
                              select temp;

            foreach (var temp in queryResult)
            {
                Console.WriteLine("姓名为:" + temp.Element("name") + " id:" + temp.Element("id"));
            }
        }


    }
}
