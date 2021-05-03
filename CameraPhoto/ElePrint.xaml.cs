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
    /// ElePrint.xaml 的交互逻辑
    /// </summary>
    public partial class ElePrint : Window
    {
        DispatcherTimer timer;
        int TimeCount = 20;

        public ElePrint(string ImagePath)
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


            EleCode.Source = new BitmapImage(new Uri(ImagePath, UriKind.Absolute));

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
            timer.Stop();
            MainWindow pay = new MainWindow();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }
    }
}
