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
    public  class OrderHelper
    {
        public  int AddOrder(int _MealType,string _MealName, double price)
        {
            Hashtable packageParameter = new Hashtable();
            packageParameter.Add("OrderID", CombHelper.GenerateOrderNumber());//直播名称
            packageParameter.Add("MealType", _MealType.ToString());//配置文件中的AppKey
            packageParameter.Add("MealName", _MealName);//配置文件中的AppName
            packageParameter.Add("Price", price.ToString());//开始时间 string格式
            packageParameter.Add("PayStatus",((int)OrderEnumPayStatus.Wait).ToString());//未支付
            packageParameter.Add("Status", ((int)OrderEnumphotoStatus.Wait).ToString());//订单状态
            packageParameter.Add("EqID", ConfigHelper.GetConfigString("EquipCode"));//随机生成的字符串

            packageParameter.Add("EqUserID", ConfigHelper.GetConfigString("EqUserID"));//默认推流 0为否
           
        

            var xmlStr = HttpHelper.getXmlStr(packageParameter);

            string Url = ConfigHelper.GetConfigString("HttpUlr")+ "/api/Order/AddOrder";

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


            JObject orderObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(str);

            int  orderID =Convert.ToInt32( orderObject["OrderID"].ToString());
            if (orderID>0)
            {
                return orderID;
            }
            else
            {
                return 0;
            }
        }
    }
}
