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

namespace CameraPhoto
{
    /// <summary>
    /// PrintPhoto.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPhoto : Window
    {
        public PrintPhoto()
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ElePrint pay = new ElePrint();
            //pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //pay.Show();

            this.Close();
        }

        private void Button_Click_Agin(object sender, RoutedEventArgs e)
        {

            //PhotoWindow pay = new PhotoWindow();
            //pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //pay.Show();

            this.Close();
        }
    }
}
