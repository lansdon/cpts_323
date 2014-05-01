using SadGUI.mizaWindows;
using SadLibrary;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TargetServerCommunicator;
using System.Timers;


namespace SadGUI
{
    class GameStartViewModel : ViewModelBase
    {
        private double _points;
        private bool _running;
        private double _time;
        IGameServer gameServer;
        string _gameName;

        private System.Timers.Timer timer;
        private System.Timers.Timer timer2;
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
            //Targets = new IEnumerable<ITarget>();

            Mediator.Instance.Register("start game", StartGameTimer);
            Mediator.Instance.Register("End Game", gameEnd);

        }
        private void gameEnd(object p)
        {
            Stop();
        }
        void StartGameTimer(object param)
        {
            startTimers();
            Twitterizer.SendTweet(string.Format("\"{0}\" has begun!  Ready, SHOOT!", _gameName));
        }
        private void displayTime(object source, ElapsedEventArgs e)
        {

            time += 1;
          
        }

        void startTimers()
        {
            if (timer2 != null)
                timer2.Stop();
            if (timer != null)
                timer.Stop();

            timer2 = new System.Timers.Timer();
            timer2.Interval = 1000;
            timer2.Elapsed += new ElapsedEventHandler(displayTime);
            timer2.Start();

            timer = new System.Timers.Timer();
            timer.Interval = 60000;
            timer.Elapsed += new ElapsedEventHandler(GameTimerEnd);
            timer.Start();
        }
        void GameTimerEnd(object source, ElapsedEventArgs e)
        {
            
            Mediator.Instance.SendMessage("End Game", 0);
           

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
        public IEnumerable<ITarget> Targets { get; private set; } 
        private void populateList(object param)
        {
            Targets = param as IEnumerable<ITarget>;
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
            points = 0;
            Mediator.Instance.SendMessage("Reset Score", points);
            time = 0;
            string temp = string.Format("Starting game {0}", _gameName);
            Console.WriteLine(temp);
            Twitterizer.SendTweet(temp);
            gameServer.StartGame(_gameName);
            _running = true;
            //send target list to strategy!!
            //var sortedTargets = Targets.OrderBy(c => c.x);
            Mediator.Instance.SendMessage("start game", 0);

        }
        public void Stop()
        {
            //if timer running stop it
            if(gameServer != null && _running)
            {
                gameServer.StopRunningGame();
                Twitterizer.SendTweet("Time is up!  The current game has ended!");
                _running = false;
                points += 60 - time;
                Mediator.Instance.SendMessage("Adjust Score", points);
            }
            if (timer != null)
            {
                timer.Stop();
                timer.Close();
            }
            if (timer2 != null)
            {
                timer2.Stop();
                timer2.Close();
            }
            LauncherViewModel.Instance.ClearQueue();
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
