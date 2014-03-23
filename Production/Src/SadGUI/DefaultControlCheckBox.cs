using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadGUI
{
    public class DefaultControlCheckBox: ViewModelBase
    {
        private bool IsChecked;
        public DefaultControlCheckBox()
        {
            IsChecked = false;
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
                IsChecked = !IsChecked;
        }
    }
}
