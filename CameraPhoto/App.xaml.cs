using log4net;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace CameraPhoto
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public static NetworkCredential OSSNetworkCredential;
        public static ILog CameraLog;

        static App()
        {
            log4net.Config.XmlConfigurator.Configure();
            CameraLog = LogManager.GetLogger(typeof(App));

           
            #region 设置开机自启
            try
            {
                string strName = AppDomain.CurrentDomain.BaseDirectory + "CameraPhoto.exe";//获取要自动运行的应用程序名，也就是本程序的名称
                if (!System.IO.File.Exists(strName))//判断要自动运行的应用程序文件是否存在
                    return;
                string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);//获取应用程序文件名，不包括路径
                RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//检索指定的子项
                if (registry == null)//若指定的子项不存在
                    registry = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");//则创建指定的子项
                registry.SetValue(strnewName, strName);//设置该子项的新的“键值对”
                registry.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            #endregion
        }
    }
}
