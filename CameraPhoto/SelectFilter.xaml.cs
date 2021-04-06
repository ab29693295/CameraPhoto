
using CameraPhoto.Helper;
using EDSDKLib;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
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

        public int MealTime = 1;

        public int OrderID = 91;

        public ImageSource currentImage;

        public string IamgePath1 = "";
        public string BorderPath1 = "";
        public string BorderPath2= "";

        public string FileName1 = "";
        public string FileName2 = "";

        public Brush _CurrentBackGroud;

        public ImageSource currentImage1;
        public ImageSource currentImage2;
        public ImageSource currentImage3;
        public ImageSource currentImage4;


        public string IamgePath2 = "";
        public SelectFilter(int _orderID, int _MealTime = 1,string borderPath1="",string borderPath2="")
        {
            InitializeComponent();


            OrderID = _orderID;

            MealTime = _MealTime;

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;


            zPhoto = new ZPhotoEngineDll();



            string dic = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\Border";
            if (Directory.Exists(dic) == false)//如果不存
            {
                Directory.CreateDirectory(dic);
            }

            IamgePath1 = dic + "\\1.PNG";

            IamgePath2 = dic + "\\2.PNG";

            if (MealTime == 2)
            {
                IamgePath1 = dic + "\\3.PNG";

                IamgePath2 = dic + "\\4.PNG";
            }


            string _IamgePath1 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\1.JPG";
            string _IamgePath2 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\2.JPG";
            string _IamgePath3 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\3.JPG";
            string _IamgePath4 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\4.JPG";
            if (MealTime == 2)
            {
                _IamgePath1 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "_2" + "\\1.JPG";
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


            //主界面1
            this.LastFirst1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.LastFirst2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.LastFirst3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.LastFirst4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //主界面2
            this.LastSecond1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.LastSecond2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.LastSecond3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.LastSecond4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));

            //边框路径
            BorderPath1 = borderPath1;
            BorderPath2 = borderPath2;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;

            //记载边框背景图
            LoadBackGround();

            //this.MainFirst.Source = new BitmapImage(new Uri(IamgePath1, UriKind.Absolute));
            //this.MainSecond.Source = new BitmapImage(new Uri(IamgePath2, UriKind.Absolute));
        }

        public void LoadBackGround()
        {

            ImageBrush mainback = new ImageBrush();
            mainback.ImageSource = new BitmapImage(new Uri(BorderPath1));
            mainback.Stretch = Stretch.Fill;

            MainPanelFirst.Background = mainback;

            LastPanelFirst.Background = mainback;




            ImageBrush mainback_2 = new ImageBrush();
            mainback_2.ImageSource = new BitmapImage(new Uri(BorderPath2));
            mainback_2.Stretch = Stretch.Fill;

            MainPanelSecond.Background = mainback_2;

            LastPanelSecond.Background = mainback_2;

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
            string dic = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\Filter" +
                "";
            if (Directory.Exists(dic) == false)//如果不存
            {
                Directory.CreateDirectory(dic);
            }
             FileName1 = dic + "\\1.PNG";

             FileName2 = dic + "\\2.PNG";
            if (MealTime == 2)
            {
                IamgePath1 = dic + "\\3.PNG";

                IamgePath2 = dic + "\\4.PNG";
            }

            SaveToImage(this.FinalPhoto, FileName1);
            //SaveToImage(this.LastPanelSecond, FileName2);


            PrintPhoto pay = new PrintPhoto(OrderID,MealTime);//_orderID
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();

            //PrintMainFirst(FileName1);

            //PrintMainSecond(FileName2);

            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.Font DrawFont = new System.Drawing.Font("Arial", 22);

            TestPrint();

            //PrintDocument pd = new PrintDocument();
            //pd.PrintPage += PicturePrintDocument_PrintPage; //注册打印事件
            ////pd.PrinterSettings.PrinterName = "HP LaserJet Professional M1213nf MFP";        //打印机选择,使用默认打印机不需要设置
            //pd.Print();

        }

        private void PicturePrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //打开文件
            FileStream fs = File.OpenRead(ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\1.JPG"); //OpenRead
                                                       //转换为BYTE
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] imageByte = new Byte[filelength]; //建立一个字节数组 
            fs.Read(imageByte, 0, filelength); //按字节流读取 
            //转换为 IMAGE
            System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
            fs.Close();

            e.Graphics.DrawImage(image, 0, 0);  //img大小
          // e.Graphics.DrawString(TicCode, DrawFont, brush, 600, 600); //绘制字符串
            e.HasMorePages = false;
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
            string _IamgePath1 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\1.JPG";
            string _IamgePath2 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\2.JPG";
            string _IamgePath3 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\3.JPG";
            string _IamgePath4 = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\4.JPG";

            this.MainFirst1.Source = new BitmapImage(new Uri(_IamgePath1, UriKind.Absolute));
            this.MainFirst2.Source = new BitmapImage(new Uri(_IamgePath2, UriKind.Absolute));
            this.MainFirst3.Source = new BitmapImage(new Uri(_IamgePath3, UriKind.Absolute));
            this.MainFirst4.Source = new BitmapImage(new Uri(_IamgePath4, UriKind.Absolute));
            //if (currentFilter == 2)
            //{
            //    this.MainFirst1.Source = new BitmapImage(new Uri(IamgePath2, UriKind.Absolute));
            //}
            //释放图像引擎当前滤镜组,如果希望叠加多个滤镜的效果，则不需要清理，依次加入想要应用的滤镜特效即可
            //本引擎会自动按照添加的先后顺序对原始图片应用滤镜特效

            //BitmapSource msource = (BitmapSource)MainFirst1.Source;
            //System.Drawing.Bitmap bt = ImageHelper.ToBitmap(msource);

            //Border spanel = (object)sender as Border;

            //int filterID = Convert.ToInt32(spanel.ToolTip);

            //System.Drawing.Bitmap btnew = zPhoto.EffectFilterById(bt, filterID); ;
            //BitmapSource sour = ImageHelper.ToBitmapSource(btnew);
            this.MainFirst1.Source = GetSource(MainFirst1, sender);
            this.MainFirst2.Source = GetSource(MainFirst2, sender);
            this.MainFirst3.Source = GetSource(MainFirst3, sender);
            this.MainFirst4.Source = GetSource(MainFirst4, sender);

            if (currentFilter == 1)
            {
                this.LastFirst1.Source = this.MainFirst1.Source;
                this.LastFirst2.Source = this.MainFirst2.Source;
                this.LastFirst3.Source = this.MainFirst3.Source;
                this.LastFirst4.Source = this.MainFirst4.Source;
            }
            else
            {
                this.LastSecond1.Source = this.MainFirst1.Source;
                this.LastSecond2.Source = this.MainFirst2.Source;
                this.LastSecond3.Source = this.MainFirst3.Source;
                this.LastSecond4.Source = this.MainFirst4.Source;
            }




            //LastPanelFirst.Source = MainFirst.Source;
            //LastPanelSecond.Source = MainSecond.Source;

        }

        //打印
        private void TestPrint()
        {
            PrintDocument printDocument1 = new PrintDocument();
            //获取或设置一个值，该值指示是否发送到文件或端口
            printDocument1.PrinterSettings.PrintToFile = true;
            //设置打印时横向还是纵向
            printDocument1.DefaultPageSettings.Landscape = true;
            //打印预览
            PrintPreviewDialog ppd = new PrintPreviewDialog();
          
            //设置边距
            Margins margin = new Margins(0, 0, 0, 0);
            printDocument1.DefaultPageSettings.Margins = margin;
       
            PaperSize pageSize = new PaperSize("First custom size", 800, 600);
            printDocument1.DefaultPageSettings.PaperSize = pageSize;
            //打印事件设置
            printDocument1.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            //ppd.Document = printDocument1;
            // ppd.ShowDialog();
            try
            {
                printDocument1.Print();
            }
            catch (Exception ex)
            {
             
            }

        }

        //打印事件处理
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            FileStream fs = File.OpenRead(FileName1); //OpenRead
                                                                                                                              //转换为BYTE
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] imageByte = new Byte[filelength]; //建立一个字节数组 
            fs.Read(imageByte, 0, filelength); //按字节流读取 
            //转换为 IMAGE
            System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
            fs.Close();

            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
        }


        public BitmapSource GetSource(Image frameworkElement, object sender)
        {
            BitmapSource msource = (BitmapSource)frameworkElement.Source;
            System.Drawing.Bitmap bt = ImageHelper.ToBitmap(msource);

            Border spanel = (object)sender as Border;

            int filterID = Convert.ToInt32(spanel.ToolTip);

            System.Drawing.Bitmap btnew = zPhoto.EffectFilterById(bt, filterID); ;
            BitmapSource sour = ImageHelper.ToBitmapSource(btnew);


            ImageBrush a = new ImageBrush();
            a.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/背景块-滤镜选中.png"));
            a.Stretch = Stretch.Fill;
            spanel.Background = a;

            return sour;
        }

        private void StackPanel_MouseLeftButtonDown_MainFirst(object sender, MouseButtonEventArgs e)
        {
            _CurrentBackGroud = MainPanelSecond.Background;
            //currentImage =MainSecond.Source;
            currentImage1 = this.MainSecond1.Source;
            currentImage2 = this.MainSecond2.Source;
            currentImage3 = this.MainSecond3.Source;
            currentImage4 = this.MainSecond4.Source;

            this.MainSecond1.Source = this.MainFirst1.Source;
            this.MainSecond2.Source = this.MainFirst2.Source;
            this.MainSecond3.Source = this.MainFirst3.Source;
            this.MainSecond4.Source = this.MainFirst4.Source;

            this.MainFirst1.Source = currentImage1;
            this.MainFirst2.Source = currentImage2;
            this.MainFirst3.Source = currentImage3;
            this.MainFirst4.Source = currentImage4;


            //MainSecond.Source = MainFirst.Source;
            //MainFirst.Source = currentImage;
            if (currentFilter == 2)
            {
                this.MainLabel.Content = "滤镜-001";
                currentFilter = 1;
            }

            this.LastFirst1.Source = this.MainFirst1.Source;
            this.LastFirst2.Source = this.MainFirst2.Source;
            this.LastFirst3.Source = this.MainFirst3.Source;
            this.LastFirst4.Source = this.MainFirst4.Source;


            this.LastSecond1.Source = this.MainSecond1.Source;
            this.LastSecond2.Source = this.MainSecond2.Source;
            this.LastSecond3.Source = this.MainSecond3.Source;
            this.LastSecond4.Source = this.MainSecond4.Source;
        }

        private void StackPanel_MouseLeftButtonDown_MainSecond(object sender, MouseButtonEventArgs e)
        {
            _CurrentBackGroud = MainPanelFirst.Background;
            currentImage1 = this.MainFirst1.Source;
            currentImage2 = this.MainFirst2.Source;
            currentImage3 = this.MainFirst3.Source;
            currentImage4 = this.MainFirst4.Source;
            this.MainPanelSecond.Background = this.MainPanelFirst.Background;

            this.MainFirst1.Source = this.MainSecond1.Source;
            this.MainFirst2.Source = this.MainSecond2.Source;
            this.MainFirst3.Source = this.MainSecond3.Source;
            this.MainFirst4.Source = this.MainSecond4.Source;

            this.MainSecond1.Source =currentImage1;
            this.MainSecond2.Source = currentImage2;
            this.MainSecond3.Source = currentImage3;
            this.MainSecond4.Source = currentImage4;

            //MainFirst.Source = MainSecond.Source;
            //MainSecond.Source = currentImage;

            if (currentFilter == 1)
            {
                this.MainLabel.Content = "滤镜-002";
                currentFilter = 2;
            }

            this.LastFirst1.Source = this.MainFirst1.Source;
            this.LastFirst2.Source = this.MainFirst2.Source;
            this.LastFirst3.Source = this.MainFirst3.Source;
            this.LastFirst4.Source = this.MainFirst4.Source;


            this.LastSecond1.Source = this.MainSecond1.Source;
            this.LastSecond2.Source = this.MainSecond2.Source;
            this.LastSecond3.Source = this.MainSecond3.Source;
            this.LastSecond4.Source = this.MainSecond4.Source;

            //LastPanelFirst.Source = MainFirst.Source;
            //LastPanelSecond.Source = MainSecond.Source;
        }


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


        /// <summary>
        /// 从Bitmap转换成BitmapSource
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public BitmapSource ChangeBitmapToBitmapSource(System.Drawing.Bitmap bmp)
        {
            BitmapSource returnSource;
            try
            {
                returnSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                returnSource = null;
            }
            return returnSource;
        }

    }
}
