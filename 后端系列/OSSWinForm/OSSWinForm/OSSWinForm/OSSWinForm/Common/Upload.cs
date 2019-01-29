using Aliyun.OpenServices.OpenStorageService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSSWinForm
{
    public class Upload
    {
        const string accessId = "***************";
        const string accessKey = "***************";
        const string endpoint = "****************";
        const string bucketName = "shianyun-oss";

        public static string UploadFile(Stream stream, string name, string ext)
        {
            //BinaryReader br = new BinaryReader(stream);
            //byte[] fileByte = br.ReadBytes((int)stream.Length);
            //string baseFileString = Convert.ToBase64String(fileByte);

            //OperationalFilesService.OperationalFiles OperationalFiles = new OperationalFilesService.OperationalFiles();
            //return OperationalFiles.UploadFile("SHIANDIANPINGCOM!@#HGHGHG", name, baseFileString);

            OssClient client = new OssClient(endpoint, accessId, accessKey);
            string key = name;
            try
            {
                var metadata = new ObjectMetadata();
                //metadata.UserMetadata.Add("mykey1", "myval1");
                //metadata.UserMetadata.Add("mykey2", "myval2");
                metadata.CacheControl = "No-Cache";
                metadata.ContentLength = stream.Length;
                if (ext.Equals(".css", StringComparison.OrdinalIgnoreCase))
                {
                    metadata.ContentType = "text/css";
                }

                client.PutObject(bucketName, key, stream, metadata);
                return "1";
            }
            catch (OssException ex)
            {
                return string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                return string.Format("Failed with error info: {0}", ex.Message);
            }
        }

        public static string DelFile(string name)
        {
            //OperationalFilesService.OperationalFiles OperationalFiles = new OperationalFilesService.OperationalFiles();
            //return OperationalFiles.DelFile("SHIANDIANPINGCOM!@#HGHGHG", name);

            OssClient client = new OssClient(endpoint, accessId, accessKey);
            try
            {
                var keys = new List<string>();
                keys.Add(name);
                //var listResult = client.ListObjects(bucketName);
                //foreach (var summary in listResult.ObjectSummaries)
                //{
                //    Console.WriteLine(summary.Key);
                //    keys.Add(summary.Key);
                //}
                var request = new DeleteObjectsRequest(bucketName, keys, false);
                client.DeleteObjects(request);
                return "1";
            }
            catch (OssException ex)
            {
                return string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                return string.Format("Failed with error info: {0}", ex.Message);
            }
        }

        public static string UploadFileMakeThumbnail(Stream stream, string name, int width, int height, string mode, string imageType)
        {
            //OperationalFilesService.OperationalFiles OperationalFiles = new OperationalFilesService.OperationalFiles();
            //return OperationalFiles.UploadFileMakeThumbnail("SHIANDIANPINGCOM!@#HGHGHG", originalImagePath, thumbnailPath, width, height, mode, imageType);

            OssClient client = new OssClient(endpoint, accessId, accessKey);
            string key = name;
            try
            {
                stream.Position = 0;
                Stream streamThumbnail = MakeThumbnail(stream, 200, 150, "HW");
                streamThumbnail.Position = 0;
                var metadata = new ObjectMetadata();
                //metadata.UserMetadata.Add("mykey1", "myval1");
                //metadata.UserMetadata.Add("mykey2", "myval2");
                metadata.CacheControl = "No-Cache";
                metadata.ContentLength = streamThumbnail.Length;
                client.PutObject(bucketName, key, streamThumbnail, metadata);
                return "1";
            }
            catch (OssException ex)
            {
                return string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                return string.Format("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径</param> 
        /// <param name="thumbnailPath">缩略图路径</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式:HW指定高宽缩放(可能变形);W指定宽，高按比例 H指定高，宽按比例 Cut指定高宽裁减(不变形)</param>　　 
        /// <param name="imageType">要缩略图保存的格式(gif,jpg,bmp,png) 为空或未知类型都视为jpg</param>　　 
        public static Stream MakeThumbnail(Stream originalStream, int width, int height, string mode)
        {
            Image originalImage = Image.FromStream(originalStream);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode.ToUpper())
            {
                case "HW"://指定高宽缩放（可能变形）　　　　　　　　 
                    break;
                case "W"://指定宽，高按比例　　　　　　　　　　 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "CUT"://指定高宽裁减（不变形）　　　　　　　　 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
              new Rectangle(x, y, ow, oh),
             GraphicsUnit.Pixel);

            MemoryStream stream = ImageToStream(bitmap);

            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();


            return stream;
        }

        public static MemoryStream ImageToStream(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms;
        }
    }
}
