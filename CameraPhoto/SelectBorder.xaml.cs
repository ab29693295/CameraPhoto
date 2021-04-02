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

        public int MealTime = 1;

        public int CurrentBorder = 1;

        public Brush _BorderFirst;
        public Brush _BorderSecond;

        public Brush _CurrentColor;

        public string BorderFirstColor = "";
        public string BorderSecondColor = "";

        public SelectBorder()//int _orderID,int _MealTime
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;

            //记载边框背景图
            LoadBackGround();

            MealTime =1;//_MealTime;

            OrderID =205;//_orderID;


            string _IamgePath1 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString()+"\\1.JPG";
            string _IamgePath2 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\2.JPG";
            string _IamgePath3 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\3.JPG";
            string _IamgePath4 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\4.JPG";
            if (MealTime == 2)
            {
                _IamgePath1= ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "_2"+"\\1.JPG";
                _IamgePath2 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "_2" + "\\2.JPG";
                _IamgePath3 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "_2" + "\\3.JPG";
                _IamgePath4 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "_2" + "\\4.JPG";
            }

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
        /// <summary>
        /// 加载背景图
        /// </summary>

        public void LoadBackGround()
        {
            ImageBrush mainback = new ImageBrush();
            mainback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-1.png"));
            mainback.Stretch = Stretch.Fill;

            MainPanelFirst.Background = mainback;


            ImageBrush mainback_2 = new ImageBrush();
            mainback_2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-2.png"));
            mainback_2.Stretch = Stretch.Fill;

            MainPanelSecond.Background = mainback_2;

            ImageBrush Firstback = new ImageBrush();
            Firstback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-1.png"));
            Firstback.Stretch = Stretch.Fill;

            PanelFirst.Background = Firstback;
            PanelFirst2.Background = Firstback;

            ImageBrush Secondback = new ImageBrush();
            Secondback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-2.png"));
            Secondback.Stretch = Stretch.Fill;

            PanelSecond.Background = Secondback;
            PanelSecond2.Background = Secondback;

            ImageBrush Thirdback = new ImageBrush();
            Thirdback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-3.png"));
            Thirdback.Stretch = Stretch.Fill;

            PanelThird.Background = Thirdback;
            PanelThird2.Background = Thirdback;

            ImageBrush Forthback = new ImageBrush();
            Forthback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-4.png"));
            Forthback.Stretch = Stretch.Fill;


            PanelForth.Background = Forthback;
            PanelForth2.Background = Forthback;

            ImageBrush Fivetback = new ImageBrush();
            Fivetback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-5.png"));
            Fivetback.Stretch = Stretch.Fill;


            PanelFive.Background = Fivetback;
            PanelFive2.Background = Fivetback;

            ImageBrush Sixback = new ImageBrush();
            Sixback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/Border/Border-6.png"));
            Sixback.Stretch = Stretch.Fill;

            PanelSix.Background = Sixback;
            PanelSix2.Background = Sixback;


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
          
          
           

           
            string dic = ConfigHelper.GetConfigString("ImageFile") +"\\"+ OrderID.ToString() + "\\Border";
            if (Directory.Exists(dic) == false)//如果不存
            {
                Directory.CreateDirectory(dic);
            }
            string FileName1 = dic + "\\1.PNG";

            string FileName2 = dic + "\\2.PNG";
            if (MealTime == 2)
            {
                FileName1 = dic + "\\3.PNG";
                FileName2 = dic + "\\4.PNG";
            }

            //UISaveToImage(this.LastPanelFirst, FileName1);
            //UISaveToImage(this.LastPanelSecond, FileName2);


            SaveToImage(this.LastPanelFirst, FileName1);
            SaveToImage(this.LastPanelSecond, FileName2);

            SelectFilter pay = new SelectFilter(OrderID);
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        //public static void UISaveToImage(FrameworkElement ui, string fileName)
        //{
        //    int bigX = (int)ui.ActualWidth;
        //    int BigY = (int)ui.ActualHeight;
        //    int width = (int)ui.ActualWidth;
        //    int height = (int)ui.ActualHeight;
        //    RenderTargetBitmap bmp = new RenderTargetBitmap(bigX, BigY, 96d, 96d, PixelFormats.Pbgra32);
        //    bmp.Render(ui);
        //    int offsetX = Math.Max((int)(0.5 * (bigX - width)), 0);
        //    int offsetY = Math.Max((int)(0.5 * (BigY - height)), 0);
        //    BitmapSource bmpSource = new CroppedBitmap(BitmapFrame.Create(bmp), new Int32Rect(offsetX, offsetY, width, height));
        //    ImageSourceToBitmap(bmpSource).Save(fileName);
        //}
        //public static System.Drawing.Bitmap ImageSourceToBitmap(ImageSource imageSource)
        //{
        //    BitmapSource bitmapSource = (BitmapSource)imageSource;
        //    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        //    System.Drawing.Imaging.BitmapData data = bmp.LockBits(
        //    new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        //    bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
        //    bmp.UnlockBits(data);
        //    return bmp;
        //}




        private void SaveToImage(FrameworkElement frameworkElement, string fileName)
        {

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)frameworkElement.ActualWidth, (int)frameworkElement.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(frameworkElement);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();
        }


   
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _CurrentColor =(Brush)MainPanelSecond.Background;

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
            _CurrentColor = (Brush)MainPanelFirst.Background;


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
           
            Brush borderColor = this.PanelFirst.Background;
            if (CurrentBorder == 1)
            {
                _BorderFirst = borderColor;
            }
            else
            {
                _BorderSecond = borderColor;
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
            Brush borderColor = this.PanelSecond.Background;
            if (CurrentBorder == 1)
            {
                _BorderFirst = borderColor;
            }
            else
            {
                _BorderSecond = borderColor;
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
            Brush borderColor = this.PanelThird.Background;
            if (CurrentBorder == 1)
            {
                _BorderFirst = borderColor;
            }
            else
            {
                _BorderSecond = borderColor;
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
            Brush borderColor = this.PanelForth.Background;
            if (CurrentBorder == 1)
            {
                _BorderFirst = borderColor;
            }
            else
            {
                _BorderSecond = borderColor;
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
            Brush borderColor = this.PanelFive.Background;
            if (CurrentBorder == 1)
            {
                _BorderFirst = borderColor;
            }
            else
            {
                _BorderSecond = borderColor;
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
            Brush borderColor = this.PanelSix.Background;
            if (CurrentBorder == 1)
            {
                _BorderFirst = borderColor;
            }
            else
            {
                _BorderSecond = borderColor;
            }

            MainPanelFirst.Background = borderColor;

            LastPanelFirst.Background = MainPanelFirst.Background;
            LastPanelSecond.Background = MainPanelSecond.Background;
        }


    }
}
