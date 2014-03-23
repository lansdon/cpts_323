using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SadGUI;

namespace SadGUI.mizaWindows
{
    /// <summary>
    /// Interaction logic for CameraUserControl.xaml
    /// </summary>
    public partial class CameraUserControl : UserControl
    {
         
        public CameraUserControl()
        {
            InitializeComponent();
            SadCamera.Instance.StartCamera(image1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

   }
}
