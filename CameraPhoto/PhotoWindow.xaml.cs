using CameraPhoto.Helper;
using CameraPhoto.Model;
using EDSDKLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// PhotoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoWindow : Window
    {

        #region Variables
    
      
        SDKHandler CameraHandler;
        //List<int> AvList;
        //List<int> TvList;
        //List<int> ISOList;
        List<Camera> CamList;
        bool IsInit = false;
        int BulbTime = 30;
        ImageBrush bgbrush = new ImageBrush();
        Action<BitmapImage> SetImageAction;
        System.Windows.Forms.FolderBrowserDialog SaveFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();

        int ErrCount;
        object ErrLock = new object();

        #endregion

        public int CurrentCount = 1;

        public string CurrentIamgePath = "";

        public int OrderID = 0;
        public int MealType = 1;

        public int _downCOunt = 9;

        public int _nextDownCount = 15;


        public int rePhotoCount = 1;
        //倒计时9秒
        DispatcherTimer timer;

        DispatcherTimer Nextimer;
        //美颜工具
        private ZPhotoEngineDll zPhoto = null;
        private ZBeautyEngineDll zSoftSkin = null;
        private int[] landMark;
        private int faceNum = 0;
        private int baseLMLen = 101;

        public PhotoWindow(int orderID, int mealType)
        {
            InitializeComponent();

            zPhoto = new ZPhotoEngineDll();
            zSoftSkin = new ZBeautyEngineDll();
            #region
            try
            {
                CameraHandler = new SDKHandler();
                // CameraHandler.CameraAdded += new SDKHandler.CameraAddedHandler(SDK_CameraAdded);
                CameraHandler.LiveViewUpdated += new SDKHandler.StreamUpdate(SDK_LiveViewUpdated);
                CameraHandler.ProgressChanged += new SDKHandler.ProgressHandler(SDK_ProgressChanged);
                CameraHandler.ImageHostDownloaded += new SDKHandler.ImageUpdate(SDK_ImageDownloaed);
                CameraHandler.CameraHasShutdown += SDK_CameraHasShutdown;
                IsInit = true;
                SetImageAction = (BitmapImage img) => { bgbrush.ImageSource = img; };
               
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show("相机未连接请检查设备!");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


            #endregion

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
            this.TipPanelDownCount.Background = _tippanel;


            OrderID = orderID;
            MealType = mealType;


        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;

            if (MealType == 2)
            {
                this.mealLabel.Content = "现在开始超值套餐中 1/2 的拍摄";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pay = new MainWindow();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }
        /// <summary>
        /// 确认图片操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SureBtn_Click(object sender, RoutedEventArgs e)
        {

           
            _nextDownCount = 15;
            Nextimer.Stop();
            rePhotoCount = 1;
            //判断4张照片
            if (CurrentCount < 5)
            {
                //将图片保存到数据库中
                int imageID = new OrderPhotoHelper().AddOrdePhoto(CurrentIamgePath, 1);
               

                switch (CurrentCount)
                {
                    case 1:
                        this.ImageBc1.Visibility = Visibility.Hidden;
                        this.ImageBc2.Visibility = Visibility.Visible;
                        this.IamgeFirst.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                    case 2:
                        this.ImageBc2.Visibility = Visibility.Hidden;
                        this.ImageBc3.Visibility = Visibility.Visible;
                        this.IamgeSecond.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                    case 3:
                        this.ImageBc3.Visibility = Visibility.Hidden;
                        this.ImageBc4.Visibility = Visibility.Visible;
                        this.IamgeThird.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                    case 4:
                        this.ImageBc4.Visibility = Visibility.Hidden;

                        this.ImageForth.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                }
                //控制相机倒计时
                if (CurrentCount < 4)
                {


                    CurrentCount = CurrentCount + 1;
                    CurrentIamgePath = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\" + CurrentCount.ToString() + ".JPG";

                    //样式控制
                    this.SurePanel.Visibility = Visibility.Collapsed;
                    TipPanel.Visibility = Visibility.Collapsed;
                    TipPanelDownCount.Visibility = Visibility.Visible;

                    CameraCanvas.Background = bgbrush;
                    CameraHandler.StartLiveView();


                    CameraHandler.ImageSaveDirectory = CurrentIamgePath;


                    //启动倒计时
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer1_Tick;
                    timer.Start();

                }
                else
                {
                    this.SurePanel.Visibility = Visibility.Collapsed;
                    this.Next_Btn.Visibility = Visibility.Visible;
                }

            }
            else
            {
               
            }

        }
        /// <summary>
        /// 下一步点击按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {
            SelectBorder seBorder = new SelectBorder(OrderID);
            seBorder.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            seBorder.Show();

            this.Close();
        }

        private void ReButton_Click(object sender, RoutedEventArgs e)
        {
            _nextDownCount = 15;
            Nextimer.Stop();
            rePhotoCount = rePhotoCount + 1;
            if (rePhotoCount > 2)
            {
                MessageBox.Show("最多可以重拍一次！");
            }
            else
            {
                //样式控制
                this.SurePanel.Visibility = Visibility.Collapsed;
                TipPanel.Visibility = Visibility.Collapsed;
                TipPanelDownCount.Visibility = Visibility.Visible;

                CameraCanvas.Background = bgbrush;
                CameraHandler.StartLiveView();


                CameraHandler.ImageSaveDirectory = CurrentIamgePath;


                //启动倒计时
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer1_Tick;
                timer.Start();
            }
          

        }
        /// <summary>
        /// 开始拍摄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCamera_Click(object sender, RoutedEventArgs e)
        {
            openSession();

            //样式控制
            TipPanel.Visibility = Visibility.Collapsed;
            TipPanelDownCount.Visibility = Visibility.Visible;
            LookPanel.Visibility = Visibility.Visible;
            string dicPth = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString();
            if (Directory.Exists(dicPth) == false)//如果不存
            {
                Directory.CreateDirectory(dicPth);
            }
            CurrentIamgePath = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\" + CurrentCount.ToString() + ".JPG";
           
            try
            {
                if (!CameraHandler.IsLiveViewOn)
                {
                    CameraCanvas.Visibility = Visibility.Visible;
                    MainPhotoPanel.Visibility = Visibility.Collapsed;
                    CameraCanvas.Background = bgbrush;
                    CameraHandler.StartLiveView();
                  
                    //CameraHandler.ImageSaveDirectory = CurrentIamgePath;
                    //设置第一个照片背景显示
                    this.ImageBc1.Visibility = Visibility.Visible;

                    //启动倒计时
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer1_Tick;
                    timer.Start();

                    

                }
                else
                {
                    CameraHandler.StopLiveView();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 定时器执行的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
         
            //定时执行的内容
            _downCOunt = _downCOunt - 1;
            DownCountLabel.Content = _downCOunt.ToString();
            if (_downCOunt < 4 && _downCOunt > 0)
            {
                this.downPhoto.Content = _downCOunt.ToString();
                this.downPhoto.Visibility = Visibility.Visible;
               

            }
            if (_downCOunt <= 0)
            {
                //CameraHandler.TakePhoto();
                //CameraHandler.DownloadImage()
                CameraHandler.StopLiveView();
                //美颜照片
                MBphoto();

                SaveToImage(CameraCanvas, CurrentIamgePath);

                this.TipPanelDownCount.Visibility = Visibility.Collapsed;

              

                this.TipPanel.Visibility = Visibility.Visible;

                this.downPhoto.Visibility = Visibility.Hidden;

                //显示确认按钮
                this.SurePanel.Visibility = Visibility.Visible;



                timer.Stop();

                _downCOunt = 10;
                _nextDownCount = 15;
                //启动另一个倒计时
                Nextimer = new DispatcherTimer();
                Nextimer.Interval = TimeSpan.FromSeconds(1);
                Nextimer.Tick += Nextimer_Tick;
                Nextimer.Start();

            }
            

        }

        public void Nextimer_Tick(object sender, EventArgs e)
        {
            if (CurrentCount == 4)
            {
                this.TipLabel.Text = "点击确认进入照片边框选择";
            }
            else
            {
                this.TipLabel.Text = "确认后，开始下一张拍摄 或 " + _nextDownCount.ToString() + "秒后自动开始下一张拍摄";
            }
          
            _nextDownCount = _nextDownCount - 1;
            if (_nextDownCount < 0)
            {
                rePhotoCount = 1;
                Nextimer.Stop();

                int imageID = new OrderPhotoHelper().AddOrdePhoto(CurrentIamgePath, 1);
                if (imageID > 0)
                {
                    //CurrentCount = CurrentCount + 1;
                }

                switch (CurrentCount)
                {
                    case 1:
                        this.ImageBc1.Visibility = Visibility.Hidden;
                        this.ImageBc2.Visibility = Visibility.Visible;
                        this.IamgeFirst.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                    case 2:
                        this.ImageBc2.Visibility = Visibility.Hidden;
                        this.ImageBc3.Visibility = Visibility.Visible;
                        this.IamgeSecond.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                    case 3:
                        this.ImageBc3.Visibility = Visibility.Hidden;
                        this.ImageBc4.Visibility = Visibility.Visible;
                        this.IamgeThird.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                    case 4:
                        this.ImageBc4.Visibility = Visibility.Hidden;
                        this.TipLabel.Text = "点击确认进入边框选择";
                        this.ImageForth.Source = new BitmapImage(new Uri(CurrentIamgePath, UriKind.Absolute));
                        break;
                }
                _downCOunt = 10;
                CurrentCount = CurrentCount + 1;
                CurrentIamgePath = ConfigHelper.GetConfigString("ImageFile") + "\\" + OrderID.ToString() + "\\" + CurrentCount.ToString() + ".JPG";

                //样式控制
                this.SurePanel.Visibility = Visibility.Collapsed;
                TipPanel.Visibility = Visibility.Collapsed;
                TipPanelDownCount.Visibility = Visibility.Visible;

                CameraCanvas.Background = bgbrush;
                CameraHandler.StartLiveView();


                CameraHandler.ImageSaveDirectory = CurrentIamgePath;


                //启动倒计时
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer1_Tick;
                timer.Start();

            }
        }
        /// <summary>
        /// 使用美颜处理图片
        /// </summary>

        public void MBphoto()
        {
          
            EquipMB _Equip = EquipHelper.GetEquipMB();
   

            BitmapSource msource = CreateElementScreenshot(CameraCanvas);
            System.Drawing.Bitmap curBitmap = ImageHelper.ToBitmap(msource);
            ImageBrush b = new ImageBrush();
            System.Drawing.Bitmap btnew = curBitmap;


            landMark = new int[baseLMLen * 2];
            using (YNFaceDetector detector = new YNFaceDetector())
            {
                String startup = System.Windows.Forms.Application.StartupPath;
                YNFaceDetector.YNRESULT res = detector.loadModels(startup + "\\models\\yn_model_detect.tar");
                if (res != YNFaceDetector.YNRESULT.YN_OK)
                {
                    faceNum = 0;
                 
                    
                }

                YNFaceDetector.YNFaces[] result = detector.Detect(curBitmap);
                if (result != null && result.Count() > 0)
                {
                    faceNum = 1;
                    for (int i = 0; i < baseLMLen; i++)
                    {
                        landMark[i * 2 + 0] = (int)result[0].shape.pts[i].x;
                        landMark[i * 2 + 1] = (int)result[0].shape.pts[i].y;
                    }
                }
                else
                {
                    faceNum = 0;
                    //MessageBox.Show("获取人脸点位失败！！！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (_Equip != null&& faceNum>0)
            {
                //美白
                if (_Equip.BeaMB > 0)
                {
                    btnew = zSoftSkin.DoSkinWhitening(curBitmap, Convert.ToInt32( _Equip.BeaMB));
                }
                //磨皮
                if (_Equip.BeaMP > 0)
                {
                    btnew = zSoftSkin.DoSoftSkin(curBitmap, landMark, Convert.ToInt32(_Equip.BeaMP));
                }
                //大眼
                if (_Equip.BeaDY > 0)
                {
                    btnew = zSoftSkin.DoEyeWarp(curBitmap, landMark, Convert.ToInt32(_Equip.BeaDY));
                }
                //瘦脸
                if (_Equip.BeaSL > 0)
                {
                    btnew = zSoftSkin.DoFaceLift(curBitmap, landMark, Convert.ToInt32(_Equip.BeaSL)/2);
                }
                //眼袋
                if (_Equip.BeaYD > 0)
                {
                    btnew = zSoftSkin.DoEyeBagRemoval(curBitmap, landMark, Convert.ToInt32(_Equip.BeaYD));
                }
                //鼻梁
                if (_Equip.BeaBL > 0)
                {
                    btnew = zSoftSkin.DoHighNose(curBitmap, landMark, Convert.ToInt32(_Equip.BeaBL));
                }
          
                //亮眼
                if (_Equip.BeaLY > 0)
                {
                    btnew = zSoftSkin.DoLightEye(curBitmap, landMark, Convert.ToInt32(_Equip.BeaLY));
                }
                //自动雀斑
                if (_Equip.BeaMB==1)
                {
                    btnew = zSoftSkin.DoDefreckleAuto(curBitmap, landMark, true);
                }

                b.ImageSource = BitmapToBitmapImage(btnew);
            }



            //System.Drawing.Bitmap btnew = zPhoto.EffectFilterById(bt, 1);
            //b.ImageSource = BitmapToBitmapImage(btnew);


            this.Background = b;
            
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="frameworkElement"></param>
        /// <returns></returns>
        private BitmapSource CreateElementScreenshot(FrameworkElement frameworkElement)
        {

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)frameworkElement.Width, (int)frameworkElement.Height, 96, 96, PixelFormats.Default);
            bmp.Render(frameworkElement);

            return bmp;
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



        //保存图片
        private void SaveToImage(FrameworkElement frameworkElement, string fileName)
        {

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)frameworkElement.ActualWidth, (int)frameworkElement.ActualHeight, 96, 96, PixelFormats.Default);
            bmp.Render(frameworkElement);
            BitmapEncoder encoder = new TiffBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();
        }

        #region 相机操作

        /// <summary>
        /// 打开相机
        /// </summary>
        public void openSession()
        {
            CamList = CameraHandler.GetCameraList();
            if (CamList.Count() > 0)
            {
                CameraHandler.OpenSession(CamList[0]);

                string cameraname = CameraHandler.MainCamera.Info.szDeviceDescription;

                if (CameraHandler.GetSetting(EDSDK.PropID_AEMode) != EDSDK.AEMode_Manual) MessageBox.Show("Camera is not in manual mode. Some features might not work!");

                CameraHandler.SetSetting(EDSDK.PropID_SaveTo, (uint)EDSDK.EdsSaveTo.Host);
                CameraHandler.SetCapacity();
            }

        }

        private void SDK_LiveViewUpdated(Stream img)
        {
            try
            {
                if (CameraHandler.IsLiveViewOn)
                {
                    using (WrappingStream s = new WrappingStream(img))
                    {
                        img.Position = 0;
                        BitmapImage EvfImage = new BitmapImage();
                        EvfImage.BeginInit();
                        EvfImage.StreamSource = s;
                        EvfImage.CacheOption = BitmapCacheOption.OnLoad;
                        EvfImage.EndInit();
                        EvfImage.Freeze();
                        Application.Current.Dispatcher.Invoke(SetImageAction, EvfImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SDK_ImageDownloaed(string filePath)
        {
          
            //设置图片背景
            //ImageBrush _tipcanvas = new ImageBrush();
            //_tipcanvas.ImageSource = new BitmapImage(new Uri(filePath));
            //_tipcanvas.Stretch = Stretch.Fill;
           // this.CameraCanvas.Background = _tipcanvas;

            //this.SurePanel.Visibility = Visibility.Visible;

        }
        private void SDK_ProgressChanged(int Progress)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SDK_CameraHasShutdown(object sender, EventArgs e)
        {
            try { CloseSession(); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SDK_Event(object sender, EventArgs e)
        {
           
        }

        private void CloseSession()
        {
            CameraHandler.CloseSession();

        }
        /// <summary>
        /// 刷新设备
        /// </summary>
        private void RefreshCamera()
        {

            CamList = CameraHandler.GetCameraList();

        }


        #endregion

      
    }
}
