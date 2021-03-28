using CameraPhoto.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CameraPhoto
{
  public  class OrderPhotoHelper
    {
        public int AddOrdePhoto(string imgPath,int OrderID)
        {
            try
            {


                var uploadUrl = ConfigHelper.GetConfigString("HttpUlr") + "/FileUpload/AddOrderPhoto";

                string postData = "OrderID=" + OrderID.ToString() + "&PhotoPath=" + imgPath + "&EqupID=" + ConfigHelper.GetConfigString("EquipmentID");//转换成：para1=1&para2=2&para3=3
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

                JObject orderObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                int PhtoID = Convert.ToInt32(orderObject["ID"].ToString());
                if (PhtoID > 0)
                {
                    return PhtoID;
                }
                else
                {
                    return 0;
                }

            }
            catch(Exception ex)
            {
                return 0;
            }
           
        }

        /// <summary>
        /// 上传filter
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public int AddOrdeFilter(string imgPath, int OrderID)
        {
            try
            {


                var uploadUrl = ConfigHelper.GetConfigString("HttpUlr") + "/FileUpload/AddOrderFilter";

                string postData = "OrderID=" + OrderID.ToString();//转换成：para1=1&para2=2&para3=3
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

                JObject orderObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                int PhtoID = Convert.ToInt32(orderObject["ID"].ToString());
                if (PhtoID > 0)
                {
                    return PhtoID;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
