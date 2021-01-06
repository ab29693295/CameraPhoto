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
        //倒计时9秒
        DispatcherTimer timer;


        public PhotoWindow(int orderID, int mealType)
        {
            InitializeComponent();

            #region
            try
            {
                CameraHandler = new SDKHandler();
                // CameraHandler.CameraAdded += new SDKHandler.CameraAddedHandler(SDK_CameraAdded);
                CameraHandler.LiveViewUpdated += new SDKHandler.StreamUpdate(SDK_LiveViewUpdated);
                CameraHandler.ProgressChanged += new SDKHandler.ProgressHandler(SDK_ProgressChanged);
                CameraHandler.CameraHasShutdown += SDK_CameraHasShutdown;
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

        private void SureBtn_Click(object sender, RoutedEventArgs e)
        {

            CurrentIamgePath = ConfigHelper.GetConfigString("ImageFile") + "\\1\\test.png";

            int imageID = new OrderPhotoHelper().AddOrdePhoto(CurrentIamgePath, 1);
            if (imageID > 0)
            {
                CurrentCount = CurrentCount + 1;
            }


        }

        private void ReButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 开始拍摄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCamera_Click(object sender, RoutedEventArgs e)
        {
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
            CameraHandler.ImageSaveDirectory = CurrentIamgePath;
            openSession();
            try
            {
                if (!CameraHandler.IsLiveViewOn)
                {
                    CameraCanvas.Visibility = Visibility.Visible;
                    MainPhotoPanel.Visibility = Visibility.Collapsed;
                    CameraCanvas.Background = bgbrush;
                    CameraHandler.StartLiveView();
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
            if (_downCOunt <= 0)
            {
                CameraHandler.TakePhoto();
                timer.Stop();
             

                this.SurePanel.Visibility = Visibility.Visible;



            }
            

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
