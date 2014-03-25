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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SadGUI.mizaWindows
{
    /// <summary>
    /// Interaction logic for MainWindowControl.xaml
    /// </summary>
    public partial class MainWindowControl : UserControl
    {
        public MainWindowControl()
        {
            InitializeComponent();

            ContentControllerManager.Instance().SetContentToController(TopLeft_CC, new CameraUserControl());
            ContentControllerManager.Instance().SetContentToController(BottomLeft_CC, new launcherUserControl());
            ContentControllerManager.Instance().SetContentToController(TopRight_CC, new TargetUserControl());
            ContentControllerManager.Instance().SetContentToController(BottomRight_CC, new CheckBoxUserControls());
        }
    }
}
