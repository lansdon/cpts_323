﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGUI
{
    
    class ServerViewModel : ViewModelBase
    {
        private string _serverIP;
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