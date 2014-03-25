using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadGUI.mizaWindows;
using SadGUI;


namespace SadGUI
{
    public class ServerCheckBox: ViewModelBase
    {   
        static private ServerCheckBox _instance;
        private bool IsChecked;
        public ServerCheckBox()
        {
            IsChecked = false;
        }
        static public ServerCheckBox instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServerCheckBox();


                return _instance;
            }

        }
        public bool ServerControl_CheckBox_IsChecked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                if (TwitterFeedCheckBox.instance.TwitterFeed_CheckBox_IsChecked == true && value == true)
                    TwitterFeedCheckBox.instance.TwitterFeed_CheckBox_IsChecked = false;
                IsChecked = value;
                Process_CheckBox();
                OnPropertyChanged("ServerControl_CheckBox_IsChecked");
            }
        }

        private void Process_CheckBox()
        {
            if (IsChecked == true)
            {
                ContentController.SetContentToController("RightCheckBoxPanel", new ServerUserControl());
            }
            else
            {
                ContentController.SetContentToController("RightCheckBoxPanel", new DefaultUserControl());
            }

        }
    }
}
