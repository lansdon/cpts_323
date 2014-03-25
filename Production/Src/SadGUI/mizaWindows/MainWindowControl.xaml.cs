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

            ContentController.AddContentControl("TopLeft_CC", TopLeft_CC);
            ContentController.AddContentControl("BottomLeft_CC", BottomLeft_CC);
            ContentController.AddContentControl("TopRight_CC", TopRight_CC);
            ContentController.AddContentControl("BottomRight_CC", BottomRight_CC);

            ContentController.SetContentToController(TopLeft_CC, new CameraUserControl());
            ContentController.SetContentToController(BottomLeft_CC, new launcherUserControl());
            ContentController.SetContentToController(TopRight_CC, new TargetUserControl());
            ContentController.SetContentToController(BottomRight_CC, new CheckBoxUserControls());
        }
    }
}
