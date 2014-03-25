using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadGUI;
using SadGUI.mizaWindows;

namespace SadGUI
{
    public class DefaultControlCheckBox: ViewModelBase
    {
        private bool IsChecked;
        public DefaultControlCheckBox()
        {
            IsChecked = true;
        }

        public bool DefaultControl_CheckBox_IsChecked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                IsChecked = value;
                Process_CheckBox();
            }
        }

        private void Process_CheckBox()
        {
            if (IsChecked == true)
            {
                //ContentControllerManager.Instance().SetContentToController(RightCheckBoxPanel, new launcherUserControl());
            }
            else
            {
                
            }

        }
    }
}
