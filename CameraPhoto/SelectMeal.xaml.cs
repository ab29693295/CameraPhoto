using CameraPhoto.Helper;
using CameraPhoto.Model;
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
    /// SelectMeal.xaml 的交互逻辑
    /// </summary>
    public partial class SelectMeal : Window
    {
        public SelectMeal()
        {
            InitializeComponent();

            try
            {
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


                LoadMeal();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

         
        }


        public void LoadMeal()
        {
            List<EquipMeal> MealData = EquipHelper.GetEquipMeal();
            if (MealData != null&&MealData.Count()>1)
            {
                this.FisrtMealName.Content = MealData[0].MealName;
                this.FisrtMealPrice.Content = MealData[0].MealPrice+"元";
                this.SecondMealName.Content = MealData[1].MealName;
                this.SecondMealPrice.Content = MealData[1].MealPrice + "元";
            }
        }



        /// <summary>         
        /// 套餐一点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FirstName = this.FisrtMealName.Content.ToString();
                int EqID = Convert.ToInt32(ConfigHelper.GetConfigString("EquipmentID"));
                double _firstMeal = Convert.ToDouble(EquipHelper.GetMealPrice(EqID,1));
                OrderResult _orderResult = new OrderHelper().GetPayUrl(2, FirstName, _firstMeal);
                if (_orderResult.OrderID != 0)
                {

                    if (Directory.Exists(ConfigHelper.GetConfigString("ImageFile")) == false)//如果不存
                    {
                        Directory.CreateDirectory(ConfigHelper.GetConfigString("ImageFile"));
                    }

                    string dicPth = ConfigHelper.GetConfigString("ImageFile") + "\\" + _orderResult.OrderID.ToString() + "\\Pay";
                    if (Directory.Exists(dicPth) == false)//如果不存
                    {
                        Directory.CreateDirectory(dicPth);
                    }
                    string ImagePath = dicPth + "\\" + _orderResult.OrderID + ".PNG";
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    qrCodeEncoder.QRCodeVersion = 0;
                    qrCodeEncoder.QRCodeScale = 4;
                    //将字符串生成二维码图片
                    //Bitmap image = qrCodeEncoder.Encode(HttpUtility.UrlEncode(url), Encoding.Default);
                    Bitmap imageBit = qrCodeEncoder.Encode(_orderResult.Url, Encoding.Default);
                    imageBit.Save(ImagePath, System.Drawing.Imaging.ImageFormat.Png);

                    PayStatus pay = new PayStatus(_orderResult.OrderID, ImagePath, 1);
                    pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    pay.Show();

                    //PhotoWindow pay = new PhotoWindow(_orderResult.OrderID, 2);
                    //pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    //pay.Show();

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                App.CameraLog.Info("错误信息："+ex.ToString());
            }
          
        }
        /// <summary>
        /// 第二个套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondMeal_Click(object sender, RoutedEventArgs e)
        {
            string SecondName = this.SecondMealName.Content.ToString();

            int EqID = Convert.ToInt32(ConfigHelper.GetConfigString("EquipmentID"));
            double _SecondMeal = Convert.ToDouble(EquipHelper.GetMealPrice(EqID, 2));
    
            OrderResult _orderResult = new OrderHelper().GetPayUrl(2, SecondName, _SecondMeal);
            if (_orderResult.OrderID != 0)
            {
                string dicPth = ConfigHelper.GetConfigString("ImageFile") + "\\" + _orderResult.OrderID.ToString() + "\\Pay";


                if (Directory.Exists(ConfigHelper.GetConfigString("ImageFile")) == false)//如果不存
                {
                    Directory.CreateDirectory(ConfigHelper.GetConfigString("ImageFile"));
                }
                if (Directory.Exists(dicPth) == false)//如果不存
                {
                    Directory.CreateDirectory(dicPth);
                }
                string ImagePath = dicPth + "\\" + _orderResult.OrderID + ".PNG";
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeScale = 4;
                //将字符串生成二维码图片
                //Bitmap image = qrCodeEncoder.Encode(HttpUtility.UrlEncode(url), Encoding.Default);
                Bitmap imageBit = qrCodeEncoder.Encode(_orderResult.Url, Encoding.Default);
                imageBit.Save(ImagePath, System.Drawing.Imaging.ImageFormat.Png);


                //PhotoWindow pp = new PhotoWindow(452, 2,2);
                //pp.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //pp.Show();
                PayStatus pay = new PayStatus(_orderResult.OrderID, ImagePath, 2);
                pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pay.Show();

                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

      
    }
}
