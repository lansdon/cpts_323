using SadGUI.mizaWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGUI
{
    
    class ServerViewModel : ViewModelBase
    {
        private string _serverIP;
        private string _serverPort;
        
        public ServerViewModel()
        {
            
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public string serverIP
        {
            get { return _serverIP; }
            set
            {
                _serverIP = value;
                OnPropertyChanged("serverIP");
            }
        }
        public string serverPort
        {
            get { return _serverPort; }
            set
            {
                _serverPort = value;
                OnPropertyChanged("serverPort");
            }
        }
        public void Ok()
        {
            //do something special.
            //ServerCheckBox.instance.ServerControl_CheckBox_IsChecked = false;
            ContentController.SetContentToController("RightCheckBoxPanel", new gameSelectionView());
        }
        public void Cancel()
        {
            //do something special.
            ServerCheckBox.instance.ServerControl_CheckBox_IsChecked = false;
        }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
