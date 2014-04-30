using SadGUI.mizaWindows;
using SadLibrary;
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
        private string _serverIPLabel;
        private string _serverPortLabel;
        public ServerViewModel()
        {
            _serverIPLabel = "Input Server IP:";
            _serverPortLabel = "Input Server Port:";
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public string serverPortLabel
        {
            get { return _serverPortLabel; }
            set
            {
                _serverPortLabel = value;
                OnPropertyChanged("serverPortLabel");
            }
        }
        public string serverIPLabel
        {
            get { return _serverIPLabel; }
            set
            {
                _serverIPLabel = value;
                OnPropertyChanged("serverIPLabel");
            }
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
            try
            {  
                gameServer = GameServerFactory.Create(serverType, "Team Mizu!!", _serverIP, Convert.ToInt32(_serverPort));
                
                Mediator.Instance.SendMessage("to games", gameServer);
               
                ContentController.SetContentToController("RightCheckBoxPanel", new gameSelectionView());
            }
            catch
            {
                serverIP = null;
                serverPort = null;
                // change label to reflect that server didn't connect
                serverIPLabel = "Error please try again, server IP:";
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
