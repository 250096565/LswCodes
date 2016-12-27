using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 微信测试.Models
{
    public class OAuthAccessTokenResult:WxJsonResult
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int ExpiresIn { get; set; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string Refresh_Token { get; set; }
        /// <summary>
        /// 授权用户唯一标识
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string Scope { get; set; }
    }
}