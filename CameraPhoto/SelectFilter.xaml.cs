
using CameraPhoto.Helper;
using EDSDKLib;
using System;
using System.Collections.Generic;
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

namespace CameraPhoto
{
    /// <summary>
    /// SelectFilter.xaml 的交互逻辑
    /// </summary>
    public partial class SelectFilter : Window
    {
        private ZPhotoEngineDll zPhoto = null;

        public int currentFilter = 1;

        public int OrderID = 91;

        public ImageSource currentImage;

       public string IamgePath1 = "";

       

        public string IamgePath2= "";
        public SelectFilter()
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
           

            zPhoto = new ZPhotoEngineDll();



            string dic = @"D:\File\" + OrderID.ToString() + "\\Border";
            if (Directory.Exists(dic) == false)//如果不存
            {
                Directory.CreateDirectory(dic);
            }

            IamgePath1 = dic + "\\1.JPG";

            IamgePath2 = dic + "\\2.JPG";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;

            this.MainFirst.Source = new BitmapImage(new Uri(IamgePath1, UriKind.Absolute));
            this.MainSecond.Source = new BitmapImage(new Uri(IamgePath2, UriKind.Absolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pay = new MainWindow();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            string dic = @"D:\File\" + OrderID.ToString() + "\\Filter" +
                "'";
            if (Directory.Exists(dic) == false)//如果不存
            {
                Directory.CreateDirectory(dic);
            }
            string FileName1 = dic + "\\1.JPG";

            string FileName2 = dic + "\\2.JPG";

            SaveToImage(this.LastPanelFirst, FileName1);
            SaveToImage(this.LastPanelSecond, FileName2);


            PrintPhoto pay = new PrintPhoto();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < FilterStack1.Children.Count; i++)
            {
                Border b = FilterStack1.Children[i] as Border;//如果类型不一致返回null
                if (b != null)
                {
                    b.Background = null;
                }
            }
            for (int i = 0; i < FilterStack2.Children.Count; i++)
            {
                Border b = FilterStack2.Children[i] as Border;//如果类型不一致返回null
                if (b != null)
                {
                    b.Background = null;
                }
            }
            for (int i = 0; i < FilterStack3.Children.Count; i++)
            {
                Border b = FilterStack3.Children[i] as Border;//如果类型不一致返回null
                if (b != null)
                {
                    b.Background = null;
                }
            }


            this.MainFirst.Source = new BitmapImage(new Uri(IamgePath1, UriKind.Absolute));
            if (currentFilter == 2)
            {
                this.MainFirst.Source = new BitmapImage(new Uri(IamgePath2, UriKind.Absolute));
            }
            //释放图像引擎当前滤镜组,如果希望叠加多个滤镜的效果，则不需要清理，依次加入想要应用的滤镜特效即可
            //本引擎会自动按照添加的先后顺序对原始图片应用滤镜特效

            BitmapSource msource = (BitmapSource)MainFirst.Source;
            System.Drawing.Bitmap bt = ImageHelper.ToBitmap(msource);

            Border spanel = (object)sender as Border;

            int filterID = Convert.ToInt32(spanel.ToolTip);

            System.Drawing.Bitmap btnew = zPhoto.EffectFilterById(bt, filterID); ;
            BitmapSource sour = ImageHelper.ToBitmapSource(btnew);
            this.MainFirst.Source = sour;


            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/背景块-滤镜选中.png"));
            a.Stretch = Stretch.Fill;
            spanel.Background = a;

           
        }

        private void StackPanel_MouseLeftButtonDown_MainFirst(object sender, MouseButtonEventArgs e)
        {
            currentImage =MainSecond.Source;

            MainSecond.Source = MainFirst.Source;
            MainFirst.Source = currentImage;
            if (currentFilter == 2)
            {
                this.MainLabel.Content = "滤镜-001";
                currentFilter = 1;
            }

            LastPanelFirst.Source = MainFirst.Source;
            LastPanelSecond.Source = MainSecond.Source;
        }

        private void StackPanel_MouseLeftButtonDown_MainSecond(object sender, MouseButtonEventArgs e)
        {
            currentImage = MainFirst.Source;


            MainFirst.Source = MainSecond.Source;
            MainSecond.Source = currentImage;

            if (currentFilter == 1)
            {
                this.MainLabel.Content = "滤镜-002";
                currentFilter = 2;
            }

            LastPanelFirst.Source = MainFirst.Source;
            LastPanelSecond.Source = MainSecond.Source;
        }

        private void SaveToImage(Image frameworkElement, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)frameworkElement.Source));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
                encoder.Save(stream);

        }
    }
}
