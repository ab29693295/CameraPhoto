using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraPhoto
{
    public sealed class ConfigHelper
    {
        public static bool GetConfigBool(string key)
        {
            bool flag = false;
            string configString = GetConfigString(key);
            if (!string.IsNullOrEmpty(configString))
            {
                bool.TryParse(configString, out flag);
            }
            return flag;
        }

        public static decimal GetConfigDecimal(string key)
        {
            decimal num = 0M;
            string configString = GetConfigString(key);
            if (!string.IsNullOrEmpty(configString))
            {
                decimal.TryParse(configString, out num);
            }
            return num;
        }

        public static int GetConfigInt(string key)
        {
            int num = 0;
            string configString = GetConfigString(key);
            if (!string.IsNullOrEmpty(configString))
            {
                int.TryParse(configString, out num);
            }
            return num;
        }

        public static string GetConfigString(string key)
        {

            try
            {
                string configString = ConfigurationManager.AppSettings[key].ToString();
                return configString;
            }
            catch
            {
                return "";
            }
        }
    }
}
