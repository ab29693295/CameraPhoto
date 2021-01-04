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
    /// SelectMeal.xaml 的交互逻辑
    /// </summary>
    public partial class SelectMeal : Window
    {
        public SelectMeal()
        {
            InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;

            //设置选择套餐的背景
            ImageBrush selectback = new ImageBrush();
            selectback.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/mealbackgrond.png"));
            selectback.Stretch = Stretch.Fill;

            this.select_Panel1.Background = selectback;
            this.select_Panel2.Background = selectback;


        

            

        }
        /// <summary>
        /// 套餐一点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PayStatus pay = new PayStatus();
            pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pay.Show();

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
    }
}
