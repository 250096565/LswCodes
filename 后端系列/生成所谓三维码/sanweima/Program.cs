using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creat_1();
            //Creat_2();
            Creat_3();
        }
        private static void Creat_3()
        {
            Image img = GenerateQRCode("http://www.zhiyunxiaoku.com/ScanCodeDeal/ScanCodeDeal?type=1&pid=CF0011_D");//生成二维码
            Image img2 = Image.FromFile("E:\\pic\\葡萄籽油-.png");   //加载模板的背景图


            Bitmap bmp1 = new Bitmap(img);
            Bitmap bmp2 = new Bitmap(img2);

            Bitmap bmp3 = new Bitmap(492, 492);
            Color colortemp1;
            for (int h3 = 0; h3 < bmp3.Height; h3++)
            {
                for (int w3 = 0; w3 < bmp3.Height; w3++)
                {
                    colortemp1 = bmp1.GetPixel(h3, w3);
                    bmp3.SetPixel(h3, w3, colortemp1);//一个像素一个像素的设置新的图片颜色
                }
            }
            bmp1 = bmp3;



            Color color1;
            Color color2;
            Color color3;
            Color colortemp;

            int frame = (bmp2.Height - bmp1.Height) / 2;//沿用背景的边框长度
            int corner = 0;//左上、右上、左下二维码标识点的宽度
            int piece = 0;//二维码内容一个块级区间的宽度
            int dot;//一个块级区间中点的宽度

            //frame += 1;
            color1 = color2 = bmp1.GetPixel(0, 0);

            for (int h = 0; h < bmp1.Height; h++)
            {
                color1 = bmp1.GetPixel(0, h);//取背景图某个像素的颜色
                if (color1 != color2)
                {
                    corner = h;
                    break;
                }
            }

            for (int h = 0; h < bmp1.Height; h++)
            {
                color1 = bmp1.GetPixel((corner / 2), h);//取背景图某个像素的颜色
                if (color1 != color2)
                {
                    piece = h;
                    break;
                }
            }
            corner += piece;

            if (piece % 3 == 0)
            {
                dot = piece / 3;
            }
            else
            {
                dot = (piece / 3) + (piece % 3);
            }

            piece = 12;
            dot = 6;
            Dictionary<Point, string> listpoint = new Dictionary<Point, string>();

            using (Bitmap image = new Bitmap(bmp2.Height, bmp2.Height))
            {
                for (int w = 0; w < bmp2.Width; w++)
                {
                    for (int h = 0; h < bmp2.Height; h++)
                    {
                        if (h == 144 && w == 35)
                        {
                            
                        }
                        color2 = bmp2.GetPixel(w, h);//取背景图某个像素的颜色

                        if ((w > frame - piece && w < frame && h > frame - piece && h < frame + corner) || //竖着的左上白条
                            (h > frame - piece && h < frame && w > frame - piece && w < frame + corner) || //横着的左上白条
                            (h > frame - piece && h < frame && w > bmp2.Width - corner - frame && w < bmp2.Width - frame + piece) ||  //横着的右上白条
                            (w > frame - piece && w < frame && h > bmp2.Height - corner - frame && h < bmp2.Height - frame + piece) ||  //竖着的左下白条
                            (w >= bmp2.Width - frame && w < bmp2.Width - frame + piece && h > frame - piece && h < frame + corner) ||  //竖着的上、右白条
                            (h >= bmp2.Height - frame && h < bmp2.Height - frame + piece && w > frame - piece && w < frame + corner)  //横着的底、左白条
                            )
                        {
                            color1 = Color.FromArgb(255, 255, 255, 255);
                            color3 = MergeColor(color1, 7, color2, 3);
                            image.SetPixel(w, h, color3);//一个像素一个像素的设置新的图片颜色
                        }
                        else if (w < frame || h < frame || w >= bmp2.Width - frame || h >= bmp2.Height - frame)
                        {
                            color3 = color2;
                            image.SetPixel(w, h, color3);//一个像素一个像素的设置新的图片颜色
                        }
                        else
                        {
                            try
                            {
                                color1 = bmp1.GetPixel(w - frame, h - frame);//取二维码某个像素的颜色
                                if ((w - frame < corner && h - frame < corner) || (w - frame < corner && h - frame >= bmp1.Height - corner) || (h - frame < corner && w - frame >= bmp1.Width - corner))
                                {
                                    //三个角的标识  以二维码为主
                                    color3 = MergeColor(color1, 7, color2, 3);
                                    image.SetPixel(w, h, color3);//一个像素一个像素的设置新的图片颜色
                                }
                                else
                                {
                                    //二维码除三个角以外的地方，以背景色为主
                                    color3 = MergeColor(color1, 2, color2, 8);
                                    image.SetPixel(w, h, color3);//一个像素一个像素的设置新的图片颜色

                                    //判断二维码图片这个位置的像素前面是不是有一个同颜色的方块，有的话把这个方块中间加个纯色的点
                                    if (h >= piece + frame-1 && w >= piece + frame-1)
                                    {
                                        bool temp = true;
                                        int newpiece = piece;
                                        int i = 0; int n = 0;
                                        if (w - frame <= piece || h - frame <= piece)
                                        {
                                            newpiece--; 
                                            //color1 = bmp1.GetPixel(w - frame-1, h - frame-1);//取二维码某个像素的颜色
                                        }
                                        for (i = 0; i < newpiece; i++)
                                        {
                                            for (n = 0; n < newpiece; n++)
                                            {
                                                //如果这个字典里有数据，表示这个位置的方块被使用过，也就是算到别的方块里了，这个循环就不算了
                                                if (listpoint.ContainsKey(new Point(w - i - frame, h - n - frame)))
                                                {
                                                    temp = false;
                                                    break;
                                                }
                                                colortemp = bmp1.GetPixel(w - i - frame, h - n - frame);
                                                if (colortemp != color1)
                                                {
                                                    temp = false;
                                                    break;
                                                }
                                            }
                                            if (!temp)
                                            {
                                                break;
                                            }
                                        }
                                        if (temp)
                                        {
                                            newpiece = piece;
                                            i = 0; n = 0;
                                            if (w - frame <= piece || h - frame <= piece)
                                            {
                                                newpiece--; 
                                            }
                                            //把这个方块的坐标加到数组中，以后这些坐标就不算在其他方块中了
                                            for (i = 0; i < newpiece; i++)
                                            {
                                                for (n = 0; n < newpiece; n++)
                                                {
                                                    listpoint.Add(new Point(w - i - frame, h - n - frame), "");
                                                }
                                            }
                                            for (int ii = w - ((newpiece - dot) / 2) - dot; ii < w - ((newpiece - dot) / 2); ii++)
                                            {
                                                for (int nn = h - ((newpiece - dot) / 2) - dot; nn < h - ((newpiece - dot) / 2); nn++)
                                                {
                                                    color2 = bmp2.GetPixel(ii, nn);//取背景图某个像素的颜色
                                                    color1 = bmp1.GetPixel(ii - frame, nn - frame);//取二维码某个像素的颜色
                                                    color3 = MergeColor(color1, 7, color2, 3);
                                                    image.SetPixel(ii, nn, color3);//一个像素一个像素的设置新的图片颜色
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            catch {
                                color1 = Color.FromArgb(255, 255, 255, 255);
                                color3 = MergeColor(color1, 7, color2, 3);
                                image.SetPixel(w, h, color3);//一个像素一个像素的设置新的图片颜色
                            }
                            
                            
                            
                        }
                        
                    }
                }
                image.Save("E:\\pic\\TemplateNew.png");
            }
        }


        /// <summary>
        /// 合并颜色
        /// </summary>
        /// <param name="color1">第一个颜色</param>
        /// <param name="prop1">第一个颜色占得比重 最大10</param>
        /// <param name="color2">第二个颜色</param>
        /// <param name="prop2">第二个颜色 最大10</param>
        /// <returns></returns>
        private static Color MergeColor(Color color1, int prop1, Color color2, int prop2)
        {
            return Color.FromArgb(
                                ((color1.A * prop1) / 10 + (color2.A * prop2) / 10),
                                ((color1.R * prop1) / 10 + (color2.R * prop2) / 10),
                                ((color1.G * prop1) / 10 + (color2.G * prop2) / 10),
                                ((color1.B * prop1) / 10 + (color2.B * prop2) / 10)
                                ); ;
        }


        private static void Creat_2()
        {
            Image img = GenerateQRCode("http://www.baidu.com");//生成二维码
            Image img2 = Image.FromFile("E:\\Template.png");   //加载模板的背景图

            Bitmap bmp1 = new Bitmap(img);
            Bitmap bmp2 = new Bitmap(img2);

            Color color1;
            Color color2;
            Color color3;

            int frame = (img2.Height - img.Height) / 2;//沿用背景的边框长度
            using (Bitmap image = new Bitmap(img2.Height, img2.Height))
            {
                for (int j = 0; j < img2.Width; j++)
                {
                    for (int i = 0; i < img2.Height; i++)
                    {
                        color2 = bmp2.GetPixel(j, i);//取背景图某个像素的颜色

                        if (j < frame || i < frame || j >= img2.Width - frame || i >= img2.Height - frame)
                        {
                            color3 = color2;
                        }
                        else
                        {
                            color1 = bmp1.GetPixel(j - frame, i - frame);//取二维码某个像素的颜色
                            color3 = Color.FromArgb(
                                ((color1.A * 4) / 10 + (color2.A * 6) / 10),
                                ((color1.R * 4) / 10 + (color2.R * 6) / 10),
                                ((color1.G * 4) / 10 + (color2.G * 6) / 10),
                                ((color1.B * 4) / 10 + (color2.B * 6) / 10)
                                );
                        }
                        image.SetPixel(j, i, color3);//一个像素一个像素的设置新的图片颜色
                    }
                }
                image.Save("E:\\TemplateNew.png");
            }
        }
        private static void Creat_1()
        {
            Image img = GenerateQRCode("http://www.baidu.com");//生成二维码
            Image img2 = Image.FromFile("E:\\Template.png");   //加载模板的背景图

            Bitmap bmp1 = new Bitmap(img);
            Bitmap bmp2 = new Bitmap(img2);

            Color color1;
            Color color2;
            Color color3;
            using (Bitmap image = new Bitmap(img.Height, img.Height))
            {
                for (int j = 0; j < img.Width; j++)
                {
                    for (int i = 0; i < img.Height; i++)
                    {
                        color1 = bmp1.GetPixel(j, i);//取二维码某个像素的颜色
                        color2 = bmp2.GetPixel(j, i);//取背景图某个像素的颜色

                        color3 = Color.FromArgb(
                            ((color1.A * 4) / 10 + (color2.A * 6) / 10),
                            ((color1.R * 4) / 10 + (color2.R * 6) / 10),
                            ((color1.G * 4) / 10 + (color2.G * 6) / 10),
                            ((color1.B * 4) / 10 + (color2.B * 6) / 10)
                            );
                        image.SetPixel(j, i, color3);//一个像素一个像素的设置新的图片颜色
                    }
                }
                image.Save("E:\\TemplateNew.png");
            }
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="strData">要生成二维码的数据</param>
        /// <param name="logoPath">二维码中心logo图片的路径(选填)</param>
        /// <param name="qrEncoding">编码类型:Byte,AlphaNumeric,Numeric</param>
        /// <param name="level">容错率:L,M,Q,H四个等级，分别表示7%,15%,25%,30%</param>
        /// <param name="version">版本</param>
        /// <param name="scale">规模</param>
        /// <returns>二维码图片</returns>
        public static Image GenerateQRCode(string strData, string logoPath = null, string qrEncoding = "Byte", string level = "Q", int version = 6, int scale = 12)
        {
            if (string.IsNullOrEmpty(strData))
                return null;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            switch (qrEncoding)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }
            qrCodeEncoder.QRCodeScale = scale;
            qrCodeEncoder.QRCodeVersion = version;

            switch (level)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            if (string.IsNullOrEmpty(logoPath))
                return qrCodeEncoder.Encode(strData, System.Text.Encoding.UTF8);
            else
            {
                Image qrCodeImage = qrCodeEncoder.Encode(strData);
                return CombinImage(qrCodeImage, logoPath);
            }
        }

        /// <summary>    
        /// 调用此函数后使此两种图片合并，类似相册，有个    
        /// 背景图，中间贴自己的目标图片    
        /// </summary>    
        /// <param name="imgBack">粘贴的源图片</param>    
        /// <param name="destImg">粘贴的目标图片</param>    
        public static Image CombinImage(Image imgBack, string destImg)
        {
            Image img = Image.FromFile(destImg);        //照片图片      
            if (img.Height != 65 || img.Width != 65)
            {
                img = KiResizeImage(img, 65, 65, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);
            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        /// <summary>    
        /// Resize图片    
        /// </summary>    
        /// <param name="bmp">原始Bitmap</param>    
        /// <param name="newW">新的宽度</param>    
        /// <param name="newH">新的高度</param>    
        /// <param name="Mode">保留着，暂时未用</param>    
        /// <returns>处理以后的图片</returns>    
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
