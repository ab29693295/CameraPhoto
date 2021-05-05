using EDSDKLib;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CameraPhoto
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int times = 0;

        List<Camera> CamList;

        SDKHandler CameraHandler;

        bool processing;

        public MainWindow()
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
            string backMp3 = System.AppDomain.CurrentDomain.BaseDirectory+ "camera.wav";
            System.Media.SoundPlayer Audio = new System.Media.SoundPlayer(backMp3);
            Audio.Load();
            Audio.PlayLooping();//播放
            

        }

        private void Step1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Step2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Step3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Step4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// 开始按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (processing == true) return;
            try
            {
                processing = true;
                CameraHandler = new SDKHandler();
                CamList = CameraHandler.GetCameraList();
                if (CamList.Count() > 0)
                {
                    //执行需要2秒以上

                    SelectMeal _meal = new SelectMeal();
                    _meal.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    _meal.Show();

                    CameraHandler.Dispose();

                    this.Close();
                }
                else
                {
                    MessageTip message = new MessageTip("相机未连接请联系工作人员");
                    message.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    message.Show();

                    CameraHandler.Dispose();
                    return;
                }
            }
            catch (Exception ex)
            {


                MessageBox.Show(ex.ToString());
            }
            finally
            {
                processing = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
            //机器注册码
            string EqCode = ConfigHelper.GetConfigString("EquipCode");
            if (string.IsNullOrEmpty(EqCode))
            {

                Login _login = new Login();
                _login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _login.Show();

                this.Close();

            }

            this.WindowState = WindowState.Maximized;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            times += 1;

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 800);

            timer.Tick += (s, e1) => { timer.IsEnabled = false; times = 0; };

            timer.IsEnabled = true;

            if (times % 2 == 0)
            {


                timer.IsEnabled = false;
                times = 0;

                Application.Current.Shutdown();

            }

        }
    }
}
