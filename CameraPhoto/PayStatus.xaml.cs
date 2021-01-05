using System;
using System.Collections.Generic;
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

        DispatcherTimer timer;
        public PayStatus(int orderID)
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

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            //支付倒计时定时器
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
            //定时执行的内容
            int Seconds = Convert.ToInt32(this.TimeLabel.Content);

            this.TimeLabel.Content = Seconds - 1;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PhotoWindow pay = new PhotoWindow();
            //pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //pay.Show();

            this.Close();
        }
    }
}
