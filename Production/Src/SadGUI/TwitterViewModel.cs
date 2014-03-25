using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGUI
{
    class TwitterViewModel : ViewModelBase
    {
        private string _twitterFeed;
        public TwitterViewModel()
        {

            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public string TwitterFeed
        {
            get { return _twitterFeed; }
            set
            {
                _twitterFeed = value;
                OnPropertyChanged("TwitterFeed");
            }
        }
        public void Ok()
        {
            
            TwitterFeedCheckBox.instance.TwitterFeed_CheckBox_IsChecked = false;
        }
        public void Cancel()
        {
            
            TwitterFeedCheckBox.instance.TwitterFeed_CheckBox_IsChecked = false;
        }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
