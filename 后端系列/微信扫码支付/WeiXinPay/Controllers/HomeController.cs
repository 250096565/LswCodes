using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;
using WxPayAPI;

namespace WeiXinPay.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 检查支付状态
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckPay(string outTradeNo)
        {
            WxPayData data = OrderQuery.Run("", outTradeNo);
            string payCode = data.GetValue("trade_state").ToString();
            if (!payCode.Equals("success", StringComparison.CurrentCultureIgnoreCase))
            {
                //支付失败或还未支付
                return Json(new OutPut() { Code = -1, Msg = "未支付" });
            }
            string transactionId = data.GetValue("appid").ToString();

            return Json(new OutPut() {Code = 1, Msg = transactionId},JsonRequestBehavior.AllowGet);

        }

        //转到支付页面
        public ActionResult Payment(string orderId)
        {

            if (string.IsNullOrEmpty(orderId))
                throw new ArgumentException("guide");

            NativePay nativePay = new NativePay();
            string outTradeNo;
            string url2 = nativePay.GetPayUrl(orderId, out outTradeNo);
            ViewBag.QRCode = "/Home/MakeQrCode?data=" + HttpUtility.UrlEncode(url2);
            //ViewBag.Order = order;
            ViewBag.outTradeNo = outTradeNo;
            return View();
        }

        public FileResult MakeQrCode(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentException("data");

            //初始化二维码生成工具
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M,
                QRCodeVersion = 0,
                QRCodeScale = 4
            };

            //将字符串生成二维码图片
            Bitmap image = qrCodeEncoder.Encode(data, Encoding.Default);

            //保存为PNG到内存流  
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);

            return File(ms.ToArray(), "image/jpeg");
        }

        /// <summary>
        /// 后台回调
        /// </summary>
        /// <returns></returns>
        public void ResultNotify()
        {
            //接收从微信后台POST过来的数据
            Stream s = Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();
            //转换数据格式并验证签名
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch (WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void ProcessNotify(WxPayData data)
        {
            WxPayData notifyData = data;
            WxPayData res = new WxPayData();
            //检查支付结果
            string payCode = notifyData.GetValue("result_code").ToString();
            if (!payCode.Equals("success", StringComparison.CurrentCultureIgnoreCase))
            {
                //支付失败
                string msg = notifyData.GetValue("return_msg").ToString();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", msg);
                Response.Write(res.ToXml());
                Response.End();
            }


            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台

                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().Name, "The Pay result is error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }

            //string transactionId = notifyData.GetValue("transaction_id").ToString();


            //查询订单成功
            //这里是告诉微信后台成功

            res.SetValue("return_code", "SUCCESS");
            res.SetValue("return_msg", "OK");
            Log.Info(this.GetType().Name, "order query success : " + res.ToXml());
            //支付成功
            //SetPaymentResult(data.GetValue("out_trade_no").ToString(), PaymentStatus.Paid);
            Response.Write(res.ToXml());

        }



    }

    public class OutPut
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }
}