using SadGUI.mizaWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private bool _running;
        private double _time;
        IGameServer gameServer;
        string _gameName;
        public GameStartViewModel()
        {
            _running = false;
            _points = 0;
            _time = 0;
            Mediator.Instance.Register("to games", togame);
            Mediator.Instance.Register("Game Name", gameName);
            Mediator.Instance.Register("TargetsList", populateList);
            StartCommand = new DelegateCommand(Start);
            StopCommand = new DelegateCommand(Stop);
            resetCommand = new DelegateCommand(reset);
            Targets = new ObservableCollection<TargetViewModel>();
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
        public ObservableCollection<TargetViewModel> Targets { get; private set; } 
        private void populateList(object param)
        {
            Targets = param as ObservableCollection<TargetViewModel>;
        }

        public void CheckTargets()
        {
            foreach(var Target in Targets)
            {
                
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
            string temp = string.Format("Starting game {0}", _gameName);
            Console.WriteLine(temp);
            Twitterizer.SendTweet(temp);
            gameServer.StartGame(_gameName);
            _running = true;
            //send target list to strategy!!

            //after 60 sec stop game
            foreach(var target in Targets)
            {
                if (target.status)
                    LauncherViewModel.Instance.FireAt(target.x, target.y, target.z);
            }
            
        }
        public void Stop()
        {
            //if timer running stop it
            gameServer.StopRunningGame();
            _running = false;
            
        }
        public void reset()
        {
            if (!_running)
            {
                ContentController.SetContentToController("RightCheckBoxPanel", new gameSelectionView());
                Mediator.Instance.SendMessage("Clear Targets", null);
                //reset score and timer
            }
        }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand resetCommand { get; set; }

    }
}
