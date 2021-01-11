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
    /// SelectBorder.xaml 的交互逻辑
    /// </summary>
    public partial class SelectBorder : Window
    {

        public int OrderID = 91;

        public int CurrentBorder = 1;

        public SolidColorBrush _BorderFirst;
        public SolidColorBrush _BorderSecond;

        public SolidColorBrush _CurrentColor;

        public string BorderFirstColor = "";
        public string BorderSecondColor = "";

        public SelectBorder()
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;

            string _IamgePath1 = @"D:\File\"+OrderID.ToString()+"\\1.JPG";
            string _IamgePath2 = @"D:\File\" + OrderID.ToString() + "\\2.JPG";
            string _IamgePath3 = @"D:\File\" + OrderID.ToString() + "\\3.JPG";
            string _IamgePath4 = @"D:\File\" + OrderID.ToString() + "\\4.JPG";

            //主界面1
            this.MainFirst1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.MainFirst2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.MainFirst3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.MainFirst4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //主界面2
            this.MainSecond1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.MainSecond2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.MainSecond3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.MainSecond4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //边框一 
            this.BorderFirst1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderFirst2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderFirst3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderFirst4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));       
            this.BorderFirst_1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderFirst_2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderFirst_3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderFirst_4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //边框三 
            this.BorderThird1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderThird2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderThird3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderThird4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            this.BorderThird_1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderThird_2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderThird_3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderThird_4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //边框二
            this.BorderSecond1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderSecond2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderSecond3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderSecond4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            this.BorderSecond_1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderSecond_2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderSecond_3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderSecond_4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));


            //边框四 
            this.BorderForth1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderForth2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderForth3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderForth4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            this.BorderForth_1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderForth_2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderForth_3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderForth_4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));


            //边框五
            this.BorderFive1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderFive2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderFive3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderFive4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            this.BorderFive_1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderFive_2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderFive_3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderFive_4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //边框六 
            this.BorderSix1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderSix2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderSix3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderSix4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            this.BorderSix_1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.BorderSix_2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.BorderSix_3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.BorderSix_4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));


            //最终图像 
            this.LastFirst1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.LastFirst2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.LastFirst3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.LastFirst4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            this.LastSecond1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.LastSecond2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.LastSecond3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.LastSecond4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));


            Color color = (Color)ColorConverter.ConvertFromString("White");
            _BorderSecond = new SolidColorBrush(color);

            _BorderFirst = new SolidColorBrush(color);
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
        /// <summary>
        /// bao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {
          
          
           

           
            string dic = @"D:\File\"+ OrderID.ToString() + "\\Border";
            if (Directory.Exists(dic) == false)//如果不存
            {
                Directory.CreateDirectory(dic);
            }
            string FileName1 = dic + "\\1.JPG";
          
            string FileName2 = dic + "\\2.JPG";
           
            SaveToImage(this.LastPanelFirst, FileName1);
            SaveToImage(this.LastPanelSecond, FileName2);

            SelectFilter pay = new SelectFilter();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void SaveToImage(FrameworkElement frameworkElement, string fileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)frameworkElement.ActualWidth, (int)frameworkElement.ActualHeight,  96,96, PixelFormats.Default);
            bmp.Render(frameworkElement);
            BitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _CurrentColor =(SolidColorBrush)MainPanelSecond.Background;

            MainPanelSecond.Background = MainPanelFirst.Background;
            MainPanelFirst.Background = _CurrentColor;
            if (CurrentBorder == 2)
            {
                this.MainLabel.Content = "边框-001";
                CurrentBorder = 1;
            }
        }

        private void StackPanel_MouseLeftButtonDown_MainSecond(object sender, MouseButtonEventArgs e)
        {
            _CurrentColor = (SolidColorBrush)MainPanelFirst.Background;


            MainPanelFirst.Background = MainPanelSecond.Background;
            MainPanelSecond.Background = _CurrentColor;

            if (CurrentBorder == 1)
            {
                this.MainLabel.Content = "边框-002";
                CurrentBorder = 2;
            }

            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }
        /// <summary>
        /// 边框一选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_First(object sender, MouseButtonEventArgs e)
        { 
            Color color = (Color)ColorConverter.ConvertFromString("LightPink");
            SolidColorBrush borderColor = new SolidColorBrush(color);
            if (CurrentBorder == 1)
            {
                _BorderFirst = new SolidColorBrush(color);
            }
            else
            {
                _BorderSecond = new SolidColorBrush(color);
            }
           
            MainPanelFirst.Background = borderColor;
            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }
        /// <summary>
        /// 边框二选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_Second(object sender, MouseButtonEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("Violet");
            SolidColorBrush borderColor = new SolidColorBrush(color);
            if (CurrentBorder == 1)
            {
                _BorderFirst = new SolidColorBrush(color);
            }
            else
            {
                _BorderSecond = new SolidColorBrush(color);
            }
          
            MainPanelFirst.Background = borderColor;
            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }
        /// <summary>
        /// 边框三选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_Third(object sender, MouseButtonEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("RoyalBlue");
            SolidColorBrush borderColor = new SolidColorBrush(color);
            if (CurrentBorder == 1)
            {
                _BorderFirst = new SolidColorBrush(color);
            }
            else
            {
                _BorderSecond = new SolidColorBrush(color);
            }
          
            MainPanelFirst.Background = borderColor;
            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }
        /// <summary>
        /// 边框四选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_Forth(object sender, MouseButtonEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("SeaGreen");
            SolidColorBrush borderColor = new SolidColorBrush(color);
            if (CurrentBorder == 1)
            {
                _BorderFirst = new SolidColorBrush(color);
            }
            else
            {
                _BorderSecond = new SolidColorBrush(color);
            }
         
            MainPanelFirst.Background = borderColor;

            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }
        /// <summary>
        /// 边框五选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_Five(object sender, MouseButtonEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("Khaki");
            SolidColorBrush borderColor = new SolidColorBrush(color);
            if (CurrentBorder == 1)
            {
                _BorderFirst = new SolidColorBrush(color);
            }
            else
            {
                _BorderSecond = new SolidColorBrush(color);
            }
          
            MainPanelFirst.Background = borderColor;

            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }
        /// <summary>
        /// 边框六选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_Six(object sender, MouseButtonEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("DarkOrange");
            SolidColorBrush borderColor = new SolidColorBrush(color);
            if (CurrentBorder == 1)
            {
                _BorderFirst = new SolidColorBrush(color);
            }
            else
            {
                _BorderSecond = new SolidColorBrush(color);
            }
         
            MainPanelFirst.Background = borderColor;

            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }


    }
}
