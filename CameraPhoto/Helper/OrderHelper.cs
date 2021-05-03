using CameraPhoto.Model;
using CameraPhoto.Pay;
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
        public static string Website = ConfigHelper.GetConfigString("HttpUrl").ToString();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EquipCode"></param>
        /// <returns></returns>
        public static int GetOrderPayStatus(int OrderID)
        {
            string Url = Website + "/api/Order/GetOrderPayStatus?ID=" + OrderID;

            string response = HttpHelper.SendGet(Url);

            JObject result = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            //isTruncated nextMarker  streams


            return Convert.ToInt32(result["PayStatus"]);
        }
        /// <summary>
        /// 更改打印状态
        /// </summary>
        /// <param name="EquipCode"></param>
        /// <returns></returns>
        public static bool UpdatePrintStatus(int OrderID)
        {
            string Url = Website + "/api/Order/UpdateOrderPrint?ID=" + OrderID;

            string response = HttpHelper.SendGet(Url);

            JObject result = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            //isTruncated nextMarker  streams


            return Convert.ToBoolean(result["R"]);
        }
        /// <summary>
        /// 获取跳转链接
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="prodName"></param>
        /// <param name="money"></param>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        public OrderResult GetPayUrl(int _MealType, string _MealName, double price)
        {
            OrderResult _orderResult = new OrderResult();
            string out_trade_no = CombHelper.GenerateOrderNumber();
            string proID = CombHelper.GenerateLong().ToString();

            WxPayData data = new WxPayData();
            data.SetValue("body", _MealName);//商品描述
            data.SetValue("attach", "精诚博源");//附加数据
            data.SetValue("out_trade_no", out_trade_no);//随机字符串
            data.SetValue("total_fee",Convert.ToInt32( price*100));//总金额
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            data.SetValue("goods_tag", ConfigHelper.GetConfigString("EquipCode"));//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", proID);//商品ID
            string url = "";

            //订单信息写入数据库
            //OrderInfoBLL bll = new OrderInfoBLL();
            int orderID = AddOrder(_MealType, _MealName, price, proID, out_trade_no);
            _orderResult.OrderID = orderID;
            try
            {
                WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口
                _orderResult.Url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            }
            catch (Exception ex)
            {
             
            }
            return _orderResult;
        }

        public  int AddOrder(int _MealType,string _MealName, double price,string OrderID,string out_trade_no)
        {
            try
            {
                Hashtable packageParameter = new Hashtable();
                packageParameter.Add("OrderID", OrderID);//直播名称
                packageParameter.Add("MealType", _MealType.ToString());//配置文件中的AppKey
                packageParameter.Add("MealName", _MealName);//配置文件中的AppName
                packageParameter.Add("Price", price.ToString());//开始时间 string格式
                packageParameter.Add("PayStatus", ((int)OrderEnumPayStatus.Wait).ToString());//未支付
                packageParameter.Add("Status", ((int)OrderEnumphotoStatus.Wait).ToString());//订单状态
                packageParameter.Add("EqID", ConfigHelper.GetConfigString("EquipmentID"));
                packageParameter.Add("out_trade_no", out_trade_no);//订单交易ID

                packageParameter.Add("EqUserID", ConfigHelper.GetConfigString("EqUserID"));



                var xmlStr = HttpHelper.getXmlStr(packageParameter);

                string Url = ConfigHelper.GetConfigString("HttpUrl") + "/api/Order/AddOrder";

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

                int orderID = Convert.ToInt32(orderObject["OrderID"].ToString());
                if (orderID > 0)
                {
                    return orderID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                App.CameraLog.Info(ex.ToString());
                return 0;
            }
        }
    }
}
