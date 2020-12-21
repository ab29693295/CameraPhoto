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
    /// PayStatus.xaml 的交互逻辑
    /// </summary>
    public partial class PayStatus : Window
    {
        public PayStatus()
        {
            InitializeComponent();
            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Paybackground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/PayStatusbackground.png"));
            a.Stretch = Stretch.Fill;
            MainStack.Background = a;
         


        }
    }
}
