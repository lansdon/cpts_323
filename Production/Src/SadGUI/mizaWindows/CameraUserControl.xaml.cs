using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SadGUI.mizaWindows
{
    /// <summary>
    /// Interaction logic for CameraUserControl.xaml
    /// </summary>
    public partial class CameraUserControl : UserControl
    {
        Capture capture;
//        private HaarCascade haarCascade;
        DispatcherTimer timer;

        bool m_isProcessing;
//        private Emgu.CV.UI.ImageBox captureImageBox;
         
        public CameraUserControl()
        {
            InitializeComponent();
         }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            capture = new Capture();
 //           haarCascade = new HaarCascade(@"haarcascade_frontalface_alt_tree.xml");
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }


        void timer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame();

            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();

 //               var detectedFaces = grayFrame.DetectHaarCascade(haarCascade)[0];

 //               foreach (var face in detectedFaces)
 //                   currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);

                image1.Source = ToBitmapSource(currentFrame);
            }

        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
    }
}
