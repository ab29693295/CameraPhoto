﻿using CameraPhoto.Model;
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
            double _firstMeal =Convert.ToDouble( ConfigHelper.GetConfigString("FirstMeal"));
            OrderResult _orderResult = new OrderHelper().GetPayUrl(2, "套餐一 39.9元", _firstMeal);
            if (_orderResult.OrderID != 0)
            {

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

                PayStatus pay = new PayStatus(_orderResult.OrderID, ImagePath);
                pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pay.Show();

                //PhotoWindow pay = new PhotoWindow(_orderResult.OrderID, 2);
                //pay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //pay.Show();

                this.Close();
            }
           
        }
        /// <summary>
        /// 第二个套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondMeal_Click(object sender, RoutedEventArgs e)
        {
            double _SecondMeal = Convert.ToDouble(ConfigHelper.GetConfigString("SecondMeal"));
            OrderResult _orderResult = new OrderHelper().GetPayUrl(2, "套餐一 39.9元", _SecondMeal);
            if (_orderResult.OrderID != 0)
            {
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



                PayStatus pay = new PayStatus(_orderResult.OrderID, ImagePath);
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
