using SadGUI.mizaWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TargetServerCommunicator;
using TargetServerCommunicator.Servers;



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
            GameServerType serverType;
            IGameServer gameServer = null;
           
            if(_serverIP == "mock" || string.IsNullOrEmpty(_serverIP))
                serverType = GameServerType.Mock;
            else
                serverType = GameServerType.WebClient;

            if ((gameServer = GameServerFactory.Create(serverType, "Team Mizu!!", _serverIP, Convert.ToInt32(_serverPort))) != null)
            {
                Mediator.Instance.SendMessage("to games", gameServer);
                ContentController.SetContentToController("RightCheckBoxPanel", new gameSelectionView());
            }
            else
            {
                serverIP = null;
                serverPort = null;
                // change label to reflect that server didn't connect
            }
            

        }
        public void Cancel()
        {
            //do something special.
            serverIP = null;
            serverPort = null;
            ServerCheckBox.instance.ServerControl_CheckBox_IsChecked = false;
        }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
