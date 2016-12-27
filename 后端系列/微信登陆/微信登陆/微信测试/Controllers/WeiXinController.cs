using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;
using 微信测试.eums;
using 微信测试.ExtFunction;
using 微信测试.Models;

namespace 微信测试.Controllers
{
    public class WeiXinController : Controller
    {

        public static readonly string AppId = "wxa00c3ce82c65c0d4";
        public static readonly string AppSecret = "7a74513a6b0a2d4f6b5c3f5a6bb7dc34";
        public static readonly string GrantType = "authorization_code";
        public static readonly string Token = "weixin";//与微信公众账号后台的Token设置保持一致，区分大小写。
        [HttpGet]
        [ActionName("Index")]
        public string Get(string signature, string timestamp, string nonce, string echostr)
        {

            using (Stream sm = new FileStream(Server.MapPath("~/log.txt"), FileMode.Append))
            {
                byte[] aa = Encoding.Default.GetBytes("进来了");
                sm.Write(aa, 0, aa.Length);

                try
                {

                    string[] arr = { Token, timestamp, nonce };
                    Array.Sort(arr);
                    string pwd = string.Join("", arr);
                    string sss = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");

                    if (sss.ToUpper().Equals(signature.ToUpper()))
                    {
                        byte[] bb = Encoding.Default.GetBytes("\r\n成功了,返回" + echostr);
                        sm.Write(bb, 0, bb.Length);

                        return echostr;
                    }
                    else
                    {
                        byte[] bb = Encoding.Default.GetBytes("\r\n失败了");
                        sm.Write(bb, 0, bb.Length);
                        return "";
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }

        }


        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// PS：此方法为简化方法，效果与OldPost一致。         /// v0.8之后的版本可以结合Senparc.Weixin.MP.MvcExtension扩展包，使用WeixinResult，见MiniPost方法。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public void Post()
        {
            using (Stream sm = new FileStream(Server.MapPath("~/log.txt"), FileMode.Append))
            {
                byte[] aa = Encoding.Default.GetBytes("\r\n用户发消息了");
                sm.Write(aa, 0, aa.Length);

                try
                {
                    Stream xmlStream = Request.InputStream;
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlStream);
                    XmlElement rootElement = doc.DocumentElement;//根节点

                    string toUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                    string fromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                    string createTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                    string msgType = rootElement.SelectSingleNode("MsgType").InnerText;
                    string content = rootElement.SelectSingleNode("Content").InnerText;
                    string msgId = rootElement.SelectSingleNode("MsgId").InnerText;
                    DateTime dt = new DateTime(1970, 1, 1, 8, 0, 0);
                    int i = (int)(DateTime.Now - dt).TotalSeconds;
                    string msg = "";
                    byte[] cc = Encoding.Default.GetBytes("\r\n用户" + fromUserName + "发送了" + msgType + "类型的消息,内容是" + content);
                    sm.Write(cc, 0, cc.Length);
                    if (content.IndexOf("授权", StringComparison.Ordinal) >= 0)
                    {

                        msg = "网页授权演示 " +
                              "<a href=\"https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxa00c3ce82c65c0d4&redirect_uri=http://114.246.124.181:880/WeiXin/Callback&response_type=code&Scope=snsapi_userinfo&state=ok#wechat_redirect\">点击这里体验</a>" +
                              "技术支持—食安端口";
                        string str = "\r\n给用户返回了。\r\n toUserName:" + toUserName + "\r\n fromUserName:" + fromUserName +
                                     "\r\n createTime:" + createTime + "\r\n msgType=" + msgType + "\r\n content:" +
                                     content + "\r\n msgid=" + msgId + "\r\n msg=" + msg;
                        byte[] bb = Encoding.Default.GetBytes(str);
                        sm.Write(bb, 0, bb.Length);

                        Reply(fromUserName, toUserName, i, msgType, msg, msgId, System.Web.HttpContext.Current);
                        HttpContext.Response.End();
                    }


                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }

        }



        public ActionResult Callback(string code, string state)
        {
            using (Stream sm = new FileStream(Server.MapPath("~/log.txt"), FileMode.Append))
            {

                try
                {
                    byte[] aa = Encoding.Default.GetBytes("\r\n授权回调 code:" + code);
                    sm.Write(aa, 0, aa.Length);
                    if (string.IsNullOrEmpty(code))
                    {
                        return Content("拒绝了授权");
                    }
                    if (state != "ok")
                    {
                        return Content("验证失败,请从正规途径进入");
                    }
                    OAuthAccessTokenResult result = new OAuthAccessTokenResult();

                    var url =
                        string.Format(
                            "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}",
                            AppId.AsUrlData(), AppSecret.AsUrlData(), code.AsUrlData(), GrantType.AsUrlData());

                    byte[] gettokenlog = Encoding.Default.GetBytes("\r\n下面开始请求token url:" + url);
                    sm.Write(gettokenlog, 0, gettokenlog.Length);

                    result = GetJson<OAuthAccessTokenResult>(sm, url);

                    if (result.errcode != ReturnCode.请求成功)
                    {
                        return Content("错误：" + result.errmsg);
                    }
                    //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
                    //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
                    Session["OAuthAccessTokenStartTime"] = DateTime.Now;


                    byte[] tokenlog =
                        Encoding.Default.GetBytes("\r\n 拿到了access_token等信息,token:" + result.Access_Token + "，opid" +
                                                  result.Openid);
                    sm.Write(tokenlog, 0, tokenlog.Length);


                    //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
                    OAuthUserInfo userInfo = null;
                    try
                    {
                        //已关注，可以得到详细信息
                        var userUrl =
                            string.Format(
                                "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}",
                                result.Access_Token.AsUrlData(), result.Openid.AsUrlData(), Language.zh_CN);
                        userInfo = GetJson<OAuthUserInfo>(sm, userUrl);

                        byte[] userLog = Encoding.Default.GetBytes("\r\n 拿到了用户信息,name:" + userInfo.nickname + "，country" + userInfo.country);
                        sm.Write(userLog, 0, userLog.Length);

                        return View(userInfo);
                    }
                    catch (Exception ex)
                    {
                        //未关注，只能授权，无法得到详细信息
                        //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
                        return Content("\r\n用户已授权，授权Token：" + result);
                    }
                }
                catch (Exception ex)
                {

                    byte[] error = Encoding.Default.GetBytes("\r\n出现错误" + ex.Message);
                    sm.Write(error, 0, error.Length);
                }
            }

            return Content("不知道出了什么错");

        }


        /// <summary>
        /// GET方式请求URL，并返回T类型
        /// </summary>
        /// <typeparam name="T">接收JSON的数据类型</typeparam>
        /// <param name="sm"></param>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <returns></returns>
        public T GetJson<T>(Stream sm, string url, Encoding encoding = null, int? maxJsonLength = null)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string returnText = client.DownloadString(url);

                JavaScriptSerializer js = new JavaScriptSerializer();
                if (maxJsonLength.HasValue)
                {
                    js.MaxJsonLength = maxJsonLength.Value;
                }

                if (returnText.Contains("errcode"))
                {
                    //可能发生错误
                    WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                    if (errorResult.errcode != ReturnCode.请求成功)
                    {
                        var str =
                           string.Format("\r\n微信请求发生错误！错误代码：{0}，说明：{1}",
                                           (int)errorResult.errcode, errorResult.errmsg, null, errorResult, url);
                        //发生错误
                        byte[] aa = Encoding.Default.GetBytes(str);
                        sm.Write(aa, 0, aa.Length);

                    }
                }

                T result = js.Deserialize<T>(returnText);

                return result;
            }

        }


        private void Reply(string fromUserName, string toUserName, int i, string msgType, string msg, string msgId, HttpContext context)
        {
            string xmlmsg = "<xml>" +
                    "<ToUserName><![CDATA[" + fromUserName + "]]></ToUserName>" +
                    "<FromUserName><![CDATA[" + toUserName + "]]></FromUserName>" +
                    "<CreateTime>" + i + "</CreateTime>" +
                    "<MsgType><![CDATA[" + msgType + "]]></MsgType>" +
                    "<Content><![CDATA[" + msg + "]]></Content>" +
                    "<MsgId>" + msgId + "</MsgId>" +
                    "</xml>";

            context.Response.Write(xmlmsg);
        }

    }



}