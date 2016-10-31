using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComomHelper
{
    public static class OutputJson
    {
        public static string Response(string status, string message, object data)
        {
            return JsonConvert.SerializeObject(new { Status = status, Message = message, Data = data });
        }
    }
}
