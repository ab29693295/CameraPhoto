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
using ThoughtWorks.QRCode.Codec;

namespace CameraPhoto
{
    /// <summary>
    /// PrintPhoto.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPhoto : Window
    {
        public int OrderID = 91;
        public int MealTime = 1;

        public PrintPhoto(int _orderID,int _MealTime)//
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;

            OrderID = _orderID;
            _MealTime = MealTime;

            if (MealTime != 2)
            {
                this.Btn_second.Visibility = Visibility.Collapsed;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
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

            PhotoWindow pay = new PhotoWindow(OrderID, 2, 2);
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }
    }
}
