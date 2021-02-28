using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CameraPhoto.Helper
{
 public static   class ImageHelper
    {
        /// <summary>
        /// 通过http上传图片及传参数
        /// </summary>
        /// <param name="imgPath">图片地址(绝对路径：D:\demo\img\123.jpg)</param>
        public static void UploadImage(string imgPath)
        {
            var uploadUrl = "http://localhost:3020/upload/imgup";
            var dic = new Dictionary<string, string>() {
                    {"para1",1.ToString() },
                    {"para2",2.ToString() },
                    {"para3",3.ToString() },
                };
            var postData ="";//转换成：para1=1&para2=2&para3=3
            var postUrl = string.Format("{0}?{1}", uploadUrl, postData);//拼接url
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.AllowAutoRedirect = true;
            request.Method = "POST";

            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            int pos = imgPath.LastIndexOf("\\");
            string fileName = imgPath.Substring(pos + 1);

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
        }

        public static BitmapSource ToBitmapSource(System.Drawing.Bitmap bmp)
        {
            System.IntPtr hBitMap = bmp.GetHbitmap();
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitMap, System.IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        /// 将 BitmapSource 转化为 Bitmap
        /// </summary>
        /// <param name="source"/>要转换的 BitmapSource
        /// <returns>转化后的 Bitmap</returns>
        public static System.Drawing.Bitmap ToBitmap(this BitmapSource source)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(ms);
                return new System.Drawing.Bitmap(ms);
            }
        }

        
    }
}
