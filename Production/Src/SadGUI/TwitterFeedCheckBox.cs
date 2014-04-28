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
        static private TwitterFeedCheckBox _instance;

        public TwitterFeedCheckBox()
        {
            IsChecked = false;
        }
        static public TwitterFeedCheckBox instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TwitterFeedCheckBox();
                
                
                return _instance; }
            
        }
        public bool TwitterFeed_CheckBox_IsChecked
        {
            get
            {

                return IsChecked;
            }
            set
            {
               
                IsChecked = value;
                Process_CheckBox();
                OnPropertyChanged("TwitterFeed_CheckBox_IsChecked");
            }
        }
        //not needed if everything is static
        public void helper(bool value)
        {
            TwitterFeed_CheckBox_IsChecked = value;
        }
        private void Process_CheckBox()
        {
            if (IsChecked == true)
            {
                Twitterizer.Active = true;
                ContentController.SetContentToController("RightCheckBoxPanel", new TwitterExperiment());
            }
            else
            {
                Twitterizer.Active = false;
                ContentController.SetContentToController("RightCheckBoxPanel", new DefaultUserControl());
            }
        }
    }
}
