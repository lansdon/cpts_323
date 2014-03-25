using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadGUI;
using SadGUI.mizaWindows;

namespace SadGUI
{
    public class TwitterFeedCheckBox: ViewModelBase
    {
        private bool IsChecked;

        public TwitterFeedCheckBox()
        {
            IsChecked = false;
        }

        public bool TwitterFeed_CheckBox_Checked
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
                ContentController.SetContentToController("RightCheckBoxPanel", new TwitterUserControl());
            }
            else
            {
                ContentController.SetContentToController("RightCheckBoxPanel", new DefaultUserControl());
            }
        }
    }
}
