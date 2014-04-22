using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    public class SadCamera : System.Windows.Controls.Image
    {
        static private SadCamera _cameraInstance;

        private Capture _capture = null;
        private System.Windows.Controls.Image _image = null;
        private bool _cameraOn = false;
        private bool _cameraDisabled = false;
        private ImageProcessor imgProcessor;

        public SadCamera()
        {
            imgProcessor = new ImageProcessor();
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

        public void StartCamera(System.Windows.Controls.Image image) 
        {
            if (image != null)
                _image = image;

            if (_cameraOn == true) return;

            if (_capture == null)
            {
                try
                {
                    _capture = new Capture();
                    _cameraDisabled = false;
                }
                catch
                {
                    SetDisabled();
                    return;
                }
            }

            // Invalid Camera
            if (_capture == null)
            {
                SetDisabled();
                return;
            }
            _cameraOn = true;

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
                        imgProcessor.ProcessImage(ref currentFrame);
 //                       grayFrame = currentFrame.Convert<Gray, Byte>();
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

        private void SetDisabled()
        {
            _cameraDisabled = true;
            _cameraOn = false;
            Font arialFont = new Font("Arial", 24);
            _image.Source = loadBitmap((Bitmap)DrawText("Camera Unavailable", arialFont, Color.Red, Color.Black));
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

        private System.Drawing.Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            System.Drawing.Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }
    }
}
