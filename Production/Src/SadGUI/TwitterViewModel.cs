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
            //do something special with the _serverIP.
        }
        public void Cancel()
        {
            //do something special.
        }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
