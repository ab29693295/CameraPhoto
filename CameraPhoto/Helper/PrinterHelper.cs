using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraPhoto.Helper
{
   public class PrinterHelper
    {

        //调用win api将指定名称的打印机设置为默认打印机
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name);

        private static PrintDocument fPrintDocument = new PrintDocument();
        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
            return fPrintDocument.PrinterSettings.PrinterName;
        }
        //获得系统中的打印机列表
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<String>();
            fPrinters.Add(DefaultPrinter()); //默认打印机始终出现在列表的第一项
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }

        private static string PrintImgPath = string.Empty;//打印图片的路径
        /// <summary>
        /// 调用打印机打印图片 landscape横向打印为true
        /// </summary>
        public static bool PrintImage(string path, bool landscape)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            try
            {
                PrintImgPath = path;
                PrintDialog PD = new PrintDialog();
                System.Drawing.Printing.PrinterSettings Ps = new System.Drawing.Printing.PrinterSettings();
                PD.PrinterSettings = Ps;
                var document = new PrintDocument();
                System.Drawing.Printing.PageSettings pageSettings = new PageSettings();
                pageSettings.Landscape = landscape;
                document.DefaultPageSettings = pageSettings;
                document.PrintPage += Document_PrintPage;
                document.DocumentName = "打印测试";
                document.Print();
                PD.Document = document;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(PrintImgPath);
            e.Graphics.DrawString("打印输出", new System.Drawing.Font("微软雅黑", 28), System.Drawing.Brushes.Black, e.MarginBounds.Width / 2, e.MarginBounds.Top);
            e.Graphics.DrawString("打印日期" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new System.Drawing.Font("微软雅黑", 12), System.Drawing.Brushes.Black, e.MarginBounds.Right - 250, e.MarginBounds.Top + 60);
            e.Graphics.DrawImage(img, new System.Drawing.Rectangle(e.MarginBounds.Left, e.MarginBounds.Top + 100, e.MarginBounds.Width, e.MarginBounds.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            e.HasMorePages = false;
        }

    }
}
