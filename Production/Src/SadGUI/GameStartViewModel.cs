using SadGUI.mizaWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TargetServerCommunicator;

namespace SadGUI
{
    class GameStartViewModel : ViewModelBase
    {
        private double _points;
        private double _time;
        IGameServer gameServer;
        string _gameName;
        public GameStartViewModel()
        {
            _points = 0;
            _time = 0;
            Mediator.Instance.Register("to games", togame);
            Mediator.Instance.Register("Game Name", gameName);
            StartCommand = new DelegateCommand(Start);
            StopCommand = new DelegateCommand(Stop);
        }
        public double points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged("points");
            }
        }
        public double time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("time");
            }
        }
        void togame(object param)
        {
             gameServer = param as IGameServer;
        }
        void gameName(object param)
        {
            _gameName = param as string;
        }
        public void Start()
        {
            gameServer.StartGame(_gameName);

        }
        public void Stop()
        {
            gameServer.StopRunningGame();
            Mediator.Instance.SendMessage("Clear Targets", null);
            ContentController.SetContentToController("RightCheckBoxPanel", new gameSelectionView());
        }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

    }
}
