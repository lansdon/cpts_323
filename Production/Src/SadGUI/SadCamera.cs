using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SadGUI
{
    public class SadCamera : Image
    {
        static private SadCamera _cameraInstance;

        private Capture _capture = null;
        private Image _image = null;
        private bool _cameraOn = false;
//        DispatcherTimer timer;

        public SadCamera()
        {
        }

        public bool isOn()
        {
            return _cameraOn;
        }

        static public SadCamera Instance
        {
            get
            {
                if (_cameraInstance == null)
                {
                    _cameraInstance = new SadCamera();
                }
                return _cameraInstance;
            }
        }

        public void StartCamera(Image image) 
        {
            if(_cameraOn == true) return;

            //Dispose of Capture if it was created before
            if (_capture == null)
                _capture = new Capture();
            if (_capture == null) return;
            _cameraOn = true;

            if (image != null)
                _image = image;

            Image<Bgr, Byte> currentFrame = _capture.QueryFrame();
            Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();
            _image.Source = ToBitmapSource(currentFrame);

            BackgroundWorker bw = new BackgroundWorker();

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                // do some simple processing for 10 seconds
                while (_cameraOn)
                {
                   currentFrame = _capture.QueryFrame();

                    if (currentFrame != null)
                    {
                        grayFrame = currentFrame.Convert<Gray, Byte>();
                        Dispatcher.Invoke((Action<Image<Bgr, Byte>>)(obj => _image.Source = ToBitmapSource(obj)), currentFrame as Image<Bgr, Byte>);
                    }
                    Thread.Sleep(1000 / 30);
                }
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                Dispatcher.Invoke((Action<Image<Bgr, Byte>>)(obj => _image.Source = null), null as Image<Bgr, Byte>);
            });

            bw.RunWorkerAsync();
        }

        public void StopCamera()
        {
            _cameraOn = false;
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
                bs.Freeze();
                return bs;
            }
        }
    }
}
