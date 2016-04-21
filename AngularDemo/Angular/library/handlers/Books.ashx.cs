using ComomHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace library
{
    /// <summary>
    /// Books 的摘要说明
    /// </summary>
    public class Books : BaseHandler
    {
        public List<Book> list = new List<Book>()
        {
            new Book() {Name = "C#学习笔记",BookId = "1",Index = "1",Author = "Learnging hard",Explain = "初学者和实习生必备的一本书", Type = "1",Time = DateTime.Now},
            new Book() {Name = "大话设计模式 ",BookId = "2",Index = "2",Author = "Learnging hard",Explain = "初学者和实习生必备的一本书",Type = "1",Time = DateTime.Now},
            new Book() {Name = "Swifte ",BookId = "3",Index = "1",Author = "王巍",Explain = "100 个 Swift 必备 tips，ios开发大神王巍写的swift开发必备教程",Type = "2",Time = DateTime.Now},
            new Book() {Name = "iOS 开发进阶 ",BookId = "4",Index = "2",Author = "王巍",Explain = "该书作者唐巧是国内 iOS 开发界的名人, 曾参与多个知名软件的开发, 目前该书尚在预售中, 书本内容由浅入深, 将读者一步一步引入到 iOS 中去, 同样适合初级跳到中级的 iOS 开发者阅读",Type = "2",Time = DateTime.Now},
            new Book() {Name = "百年孤独",BookId = "5",Index = "1",Author = "加西亚·马尔克斯",Explain = "作品描写了布恩迪亚家族七代人的传奇故事，以及加勒比海沿岸小镇马孔多的百年兴衰，反映了拉丁美洲一个世纪以来风云变幻的历史。作品融入神话传说、民间故事、宗教典故等神秘因素，巧妙地糅合了现实与虚幻，展现出一个瑰丽的想象世界，成为20世纪最重要的经典文学巨著之一", Type = "3",Time = DateTime.Now},
            new Book() {Name = "白夜行",BookId = "6",Index = "1",Author = "东野圭吾",Explain = "推理界大佬", Type = "3",Time = DateTime.Now}
        };

        public void GetBooks(HttpContext context)
        {
            string type = context.Request["type"];
            if (type == "0")
            {
                int index = 1;
                foreach (Book book in list)
                {
                    book.Index = index.ToString();
                    index++;
                }
                context.Response.Write(OutputJson.Response("1", "成功", list));
            }
            else if (type == "1")
            {
                context.Response.Write(OutputJson.Response("1", "成功", list.Where(o => o.Type == "1").ToList()));
            }
            else if (type == "2")
            {
                context.Response.Write(OutputJson.Response("1", "成功", list.Where(o => o.Type == "2").ToList()));
            }
            else if (type == "3")
            {
                context.Response.Write(OutputJson.Response("1", "成功", list.Where(o => o.Type == "3").ToList()));
            }


        }
    }

    public class Book
    {
        public string BookId { get; set; }
        public string Index { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Explain { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}