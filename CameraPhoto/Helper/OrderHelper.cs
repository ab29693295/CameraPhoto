using CameraPhoto.Model;
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
    public  class OrderHelper
    {
        public static bool AddOrder(int _MealType,string _MealName, double price)
        {
            Hashtable packageParameter = new Hashtable();
            packageParameter.Add("OrderID", CombHelper.GenerateOrderNumber());//直播名称
            packageParameter.Add("MealType", _MealType);//配置文件中的AppKey
            packageParameter.Add("MealName", _MealName);//配置文件中的AppName
            packageParameter.Add("Price", price);//开始时间 string格式
            packageParameter.Add("PayStatus", OrderEnumPayStatus.Wait);//未支付
            packageParameter.Add("Status", OrderEnumphotoStatus.Wait);//订单状态
            packageParameter.Add("EqID", ConfigHelper.GetConfigString("EquipmentID"));//随机生成的字符串

            packageParameter.Add("EqUserID", ConfigHelper.GetConfigString("EqUserID"));//默认推流 0为否
           
        

            var xmlStr = HttpHelper.getXmlStr(packageParameter);

            string Url = ConfigHelper.GetConfigString("HttpUlr")+"/api/Order/AddMeetingLive";

            var data = Encoding.UTF8.GetBytes(xmlStr);
            Stream responseStream;
            HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            responseStream = request.GetResponse().GetResponseStream();

            string str = string.Empty;
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                str = reader.ReadToEnd();
            }
            responseStream.Close();
            if (str.Contains("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
