using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace SadGUI
{
    public class VideoCheckBox: ViewModelBase
    {
        private bool IsChecked;

        public VideoCheckBox()
        {
            IsChecked = true;
        }

        public bool Video_CheckBox_Ischecked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                IsChecked = value;
                Process_CheckedBox();
            }
        }

        public void Process_CheckedBox()
        {
            if (IsChecked == true) 
            {
                SadCamera.Instance.StartCamera(null);
            }
            else
            {
                SadCamera.Instance.StopCamera();
            }
        }
    }
}
