using CameraPhoto.Helper;
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
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            // 在此点之下插入创建对象所需的代码。
            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/CameraPhoto;component/Resources/bacbkground.png"));
            b.Stretch = Stretch.Fill;
            this.Background = b;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {
            string EqCode = EquipText.Text;
            if (string.IsNullOrEmpty(EqCode))
            {

                MessageBox.Show("注册秘钥不能为空！");
                return;
            }
            else
            {
                int EqID = EquipHelper.LoginEquip(EqCode);
                if (EqID > 0)
                {
                    if (ConfigHelper.ChangeConfig("EquipCode", EqCode))
                    {

                        ConfigHelper.ChangeConfig("EquipmentID", EqID.ToString());

                        MainWindow _main = new MainWindow();
                        _main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        _main.Show();

                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("注册失败！");
                        return;
                    }

                   
                }
                else
                {
                    MessageBox.Show("注册失败！");
                    return;
                }
            }
        }
    }
}
