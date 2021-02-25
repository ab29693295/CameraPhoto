using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraPhoto.Helper
{
   public class EquipHelper
    {
        public static string Website = ConfigHelper.GetConfigString("HttpUrl").ToString();
        public static bool LoginEquip(string EquipCode)
        {
            string Url = Website + "/api/EquipAPI/LoginEquip?EqCode=" + EquipCode;

            string response = HttpHelper.SendGet(Url);

            JObject result = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            //isTruncated nextMarker  streams


            return Convert.ToBoolean(result["R"]);
        }
    }
}
