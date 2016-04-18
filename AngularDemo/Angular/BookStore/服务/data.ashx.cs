using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComomHelper;

namespace BookStore.服务
{
    /// <summary>
    /// Data 的摘要说明
    /// </summary>
    public class Data : BaseHandler
    {
        public void GetUser(HttpContext context)
        {
            context.Response.Write(OutputJson.Response("ok", "成功", new List<object>() { new { Name = "小明" }, new { Name = "运数注定难通全" }, new { Name = "人谋怎似所天休" } }));
        }
    }
}