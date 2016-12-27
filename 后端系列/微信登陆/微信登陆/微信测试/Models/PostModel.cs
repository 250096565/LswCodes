using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 微信测试.Models
{
    public class PostModel
    {
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Token { get; set; }
        public string AppId { get; set; }
    }
}