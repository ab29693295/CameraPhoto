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
    /// PhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoWindow : Window
    {
        public PhotoWindow()
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;

            ImageBrush mainpanel = new ImageBrush();
            mainpanel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/准备拍照背景块.png"));
            mainpanel.Stretch = Stretch.Fill;
            this.MainPhotoPanel.Background = mainpanel;
            ImageBrush _tippanel = new ImageBrush();
            _tippanel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/黄色提示框背景-A.png"));
            _tippanel.Stretch = Stretch.Fill;
            this.TipPanel.Background = _tippanel;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pay = new MainWindow();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void SureBtn_Click(object sender, RoutedEventArgs e)
        {
           
            string imagePath =ConfigHelper.GetConfigString("ImageFile") + "\\1\\test.png";

            new OrderPhotoHelper().AddOrdePhoto(imagePath,1);

            SelectBorder pay = new SelectBorder();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void ReButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
