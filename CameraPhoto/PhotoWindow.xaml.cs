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

            ImageBrush panel1 = new ImageBrush();
            panel1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/照片序列占位图-01.png"));
            panel1.Stretch = Stretch.Fill;
            this.FirstImagePanel.Background = panel1;
            ImageBrush panel2 = new ImageBrush();
            panel2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/照片序列占位图-02.png"));
            panel2.Stretch = Stretch.Fill;
            this.SecondImagePanel.Background = panel2;
            ImageBrush panel3 = new ImageBrush();
            panel3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/照片序列占位图-03.png"));
            panel3.Stretch = Stretch.Fill;
            this.ThirdImagePanel.Background = panel3;
            ImageBrush panel4 = new ImageBrush();
            panel4.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/照片序列占位图-04.png"));
            panel4.Stretch = Stretch.Fill;
            this.ForthImagePanel.Background = panel4;


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
            SelectBorder pay = new SelectBorder();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }
    }
}
