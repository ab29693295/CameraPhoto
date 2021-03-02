using log4net;
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
        }
    }
}
