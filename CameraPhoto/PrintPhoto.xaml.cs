using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ThoughtWorks.QRCode.Codec;

namespace CameraPhoto
{

 
    /// <summary>
    /// PrintPhoto.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPhoto : Window
    {

        private int times = 0;
        public int OrderID = 91;
        public int MealTime = 1;
        DispatcherTimer timer;
        int TimeCount = 20;

        public PrintPhoto(int _orderID,int _MealType,int _MealTime)//
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;

            OrderID = _orderID;
            MealTime = _MealTime;

            if (_MealType != 2)
            {
                this.Btn_second.Visibility = Visibility.Collapsed;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;

            //启动倒计时
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();

        } 
        /// <summary>
          /// 定时器执行的方法
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeCount--;
            if (TimeCount < 1)
            {
                MainWindow pay = new MainWindow();
                pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pay.Show();
                timer.Stop();
                this.Close();
            }
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Url = ConfigHelper.GetConfigString("HttpUlr") + "/OrderFilter/Index?oID=" + OrderID.ToString();

            string dicPth = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\ElePrint";
            if (Directory.Exists(dicPth) == false)//如果不存
            {
                Directory.CreateDirectory(dicPth);
            }
            string ImagePath = dicPth + "\\" + OrderID + ".PNG";
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeScale = 4;
            //将字符串生成二维码图片
            //Bitmap image = qrCodeEncoder.Encode(HttpUtility.UrlEncode(url), Encoding.Default);
            Bitmap imageBit = qrCodeEncoder.Encode(Url, Encoding.Default);
            imageBit.Save(ImagePath, System.Drawing.Imaging.ImageFormat.Png);
            ElePrint pay = new ElePrint(ImagePath);
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void Button_Click_Agin(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            PhotoWindow pay = new PhotoWindow(OrderID, 2, 2);
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            MainWindow pay = new MainWindow();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            times += 1;

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 800);

            timer.Tick += (s, e1) => { timer.IsEnabled = false; times = 0; };

            timer.IsEnabled = true;

            if (times % 2 == 0)
            {
                timer.IsEnabled = false;
                times = 0;

                Application.Current.Shutdown();

            }

        }
        /// <summary>
        /// 开始第二次拍摄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_second_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            PhotoWindow pay = new PhotoWindow(OrderID, 1, 2);
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }
    }
}
