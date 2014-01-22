using Ionic.Zip;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZipManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  隐藏 ToolBar 左侧的移动虚线和右侧的小箭头
        /// </summary>
        private void toolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        #region Ionic.Zip压缩文件
        //压缩方法一  
        public void ExeCompOne()
        {
            string FileName = DateTime.Now.ToString("yyMMddHHmmssff");
            //ZipFile实例化一个压缩文件保存路径的一个对象zip  
            using (ZipFile zip = new ZipFile(@"E:\\yangfeizai\\" + FileName + ".zip", Encoding.Default))
            {
                //加密压缩  
                zip.Password = "123456";
                //将要压缩的文件夹添加到zip对象中去(要压缩的文件夹路径和名称)  
                zip.AddDirectory(@"E:\\yangfeizai\\" + "12051214544443");
                //将要压缩的文件添加到zip对象中去,如果文件不存在抛错FileNotFoundExcept  
                //zip.AddFile(@"E:\\yangfeizai\\12051214544443\\"+"Jayzai.xml");  
                zip.Save();
            }
        }
        //压缩方法二  
        public void ExeCompTwo()
        {
            string FileName = DateTime.Now.ToString("yyMMddHHmmssff");
            //ZipFile实例化一个对象zip  
            using (ZipFile zip = new ZipFile())
            {
                //加密压缩  
                zip.Password = "123456";
                //将要压缩的文件夹添加到zip对象中去(要压缩的文件夹路径和名称)  
                zip.AddDirectory(@"E:\\yangfeizai\\" + "12051214544443");
                //将要压缩的文件添加到zip对象中去,如果文件不存在抛错FileNotFoundExcept  
                //zip.AddFile(@"E:\\yangfeizai\\12051214544443\\"+"Jayzai.xml");  
                //用zip对象中Save重载方法保存压缩的文件，参数为保存压缩文件的路径  
                zip.Save(@"E:\\yangfeizai\\" + FileName + ".zip");
            }
        }
        #endregion

        #region //删除压缩包中的文件
        //3.从zip文件中删除一个文件,注意无法直接删除一个文件夹  
        public void ExeDelete(string FileName)
        {
            using (ZipFile zip = ZipFile.Read(@"E:\\yangfeizai\\" + FileName + ".zip"))
            {
                //zip["Jayzai.xml"] = null;  
                //删除zip对象中的一个文件  
                zip.RemoveEntry("Jayzai.xml");
                zip.Save();
            }
        }
        #endregion

        //从zip文件中解压出一个文件  
        public void ExeSingleDeComp(string FileName)
        {
            using (ZipFile zip = ZipFile.Read(@"E:\\yangfeizai\\" + FileName + ".zip"))
            {
                zip.Password = "123456";//密码解压  
                //Extract解压zip文件包的方法，参数是保存解压后文件的路基  
                zip["Jayzai.xml"].Extract(@"E:\\yangfeizai\\Test");
            }
        }

        //从zip文件中解压全部文件  
        public void ExeAllDeComp(string FileName)
        {
            using (ZipFile zip = ZipFile.Read(@"E:\\yangfeizai\\" + FileName + ".zip"))
            {
                zip.Password = "123456";//密码解压  
                foreach (ZipEntry entry in zip)
                {
                    //Extract解压zip文件包的方法，参数是保存解压后文件的路基  
                    entry.Extract(@"E:\\yangfeizai\\Test");
                }
            }
        }

        private void btnCaptureOK_Click(object sender, RoutedEventArgs e)
        {
            PhotoCapture win = new PhotoCapture();
            if (win.ShowDialog() == true)
            {
                byte[] data = win.CaptureData;
                ShowImg(data);
            }
        }

        private void ShowImg(byte[] imgBytes)
        {
            MemoryStream stream = new MemoryStream(imgBytes);
            BitmapImage bmpImg = new BitmapImage();
            bmpImg.BeginInit();
            bmpImg.StreamSource = stream;
            bmpImg.EndInit();
            imgPhoto.Source = bmpImg;
        } 
    }
}
