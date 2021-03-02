using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace CameraPhoto
{
    /// <summary>
    /// PayStatus.xaml 的交互逻辑
    /// </summary>
    public partial class PayStatus : Window
    {
        public static int _OrderID = 0;

        public int _MealType = 1;

        DispatcherTimer timer;

        DispatcherTimer paytimer;
        public PayStatus(int orderID,string  imagePath,int MealType)
        {
            InitializeComponent();
            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/PayStatusbackground.png"));
            a.Stretch = Stretch.Fill;
            MainStack.Background = a;

            _OrderID = orderID;

            PayCode.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));

            _MealType = MealType;
        }

        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            //支付倒计时定时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();

            paytimer=new DispatcherTimer();
            paytimer.Interval = TimeSpan.FromSeconds(1);
            paytimer.Tick += PayTimer_Tick;
            paytimer.Start();

        }
        /// <summary>
        /// 定时器执行的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时执行的内容
            int Seconds = Convert.ToInt32(this.TimeLabel.Content);
            if (Seconds >= 1)
            {
                this.TimeLabel.Content = Seconds - 1;
            }
            else
            {
                this.RePayStack.Visibility = Visibility.Visible;
                this.MainStack.Visibility = Visibility.Collapsed;
            }
            

        }
        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PayTimer_Tick(object sender, EventArgs e)
        {
            int PayStatus = OrderHelper.GetOrderPayStatus(_OrderID);
            if (PayStatus == 1)
            {
               
                PhotoWindow pay = new PhotoWindow(_OrderID, _MealType);//
                pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pay.Show();

                this.Close();
            }
        }
        /// <summary>
        /// 支付返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pay = new MainWindow();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }
        /// <summary>
        /// 重新支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RePayBtn_Click(object sender, RoutedEventArgs e)
        {
            this.RePayStack.Visibility = Visibility.Collapsed;
            this.MainStack.Visibility = Visibility.Visible;
            this.TimeLabel.Content = "90";
        }
    }
}
