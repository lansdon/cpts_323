using SadLibrary.FileLoader;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TargetServerCommunicator;

namespace SadGUI
{
    class TargetsViewModel : ViewModelBase
    {
        private int _listBoxSelection;
        private int _score;
        public TargetsViewModel()
        {
            Targets = new ObservableCollection<Target>();
            //LoadTargetsFromFile("targets.ini");
            //LoadTargetsFromServerButton.IsEnabled = false;
            MoveToTargetCommand = new DelegateCommand(MoveToTarget);
            KillTargetCommand = new DelegateCommand(KillTarget);
            ClearTargetsCommand = new DelegateCommand(ClearTargets);
            OpenFileCommand = new DelegateCommand(OpenFile);
            KillAllTargetsCommand = new DelegateCommand(killAllTargets);
            KillAllFriendlyTargetsCommand = new DelegateCommand(killAllFriendlyTargets); 
            KillAllEnemyTargetsCommand = new DelegateCommand(killAllEnemyTargets);
            score = 0;

            Mediator.Instance.Register("to games", gameServer);
            Mediator.Instance.Register("Game Name", populateTargets);
            Mediator.Instance.Register("Clear Targets", clearTargets);
        }
        public int score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("score");
            }
        }
        public int listBoxSelection
        {
            get { return _listBoxSelection; }
            set
            {
                _listBoxSelection = value;
                OnPropertyChanged("listBoxSelection");
            }
        }
        void clearTargets(object p)
        {
            Targets.Clear();
            
        }
        private string gameName { get; set; }
        private IGameServer gameserver { get; set; }
        void gameServer(object param)
        {
            gameserver = param as IGameServer;
        }

        void populateTargets(object param)
        {
            gameName = param as string;
            IEnumerable<TargetServerCommunicator.Data.Target> temps = gameserver.RetrieveTargetList(gameName);//param as IEnumerable<TargetServerCommunicator.Data.Target>;
            
            foreach (var temp in temps)
            {
                Target mytemp = new Target();
                mytemp.dutyCycle = temp.dutyCycle;
                mytemp.hit = temp.hit;
                mytemp.id = temp.id;
                mytemp.input = temp.input;
                mytemp.isMoving = temp.isMoving;
                mytemp.led = temp.led;
                mytemp.movingState = temp.movingState;
                mytemp.name = temp.name;
                mytemp.points = temp.points;
                mytemp.spawnRate = temp.spawnRate;
               // mytemp.startTime = temp.startTime;
                mytemp.status = temp.status;
                mytemp.x = temp.x;
                mytemp.y = temp.y;
                mytemp.z = temp.z;
                
                mytemp.Alive = true;
                Targets.Add((mytemp));
            }
            //TargetListBox.ItemsSource = Targets;
            Mediator.Instance.SendMessage("TargetsList", Targets);
        }
        private void OpenFile()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "targets"; // Default file name
            dlg.DefaultExt = ".ini"; // Default file extension
            dlg.Filter = "Target Ini (.ini)|*.ini"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {

                // Open document 
                string filename = dlg.FileName;
                LoadTargetsFromFile(filename);
            }
        }

        public ObservableCollection<Target> Targets
        { get; private set; }

        private void LoadTargetsFromFile(string filename) 
        {
            FileTargetLoader fLoader = FileLoaderFactory.GetFileLoader(SadLibrary.FileLoader.FileLoaderTypes.FILE_INI, filename);
            Target_Manager.Instance.Target_List = fLoader.Parse();

            Targets.Clear();
            foreach (var target in Target_Manager.Instance.Target_List)
            {
                target.Alive = true;
                Targets.Add((target));
            }
            
        }
        private void killAllTargets()
        {
            foreach (var target in Targets)
            {
                LauncherViewModel.Instance.FireAt(target.x, 4 + target.y, target.z);
                score += 50;
            }
        }
        private void killAllFriendlyTargets()
        {
            foreach (var target in Targets)
            {
                if(target.status == 1)
                    LauncherViewModel.Instance.FireAt(target.x, 4 + target.y, target.z);
            }
        }
        private void killAllEnemyTargets()
        {
            foreach (var target in Targets)
            {
                if (target.status==0)
                    LauncherViewModel.Instance.FireAt(target.x, 4 + target.y, target.z);
            }
        }
        private void ClearTargets()
        {
            Targets.Clear();
        }

        

        private void MoveToTarget()
        {
            // cast the sender to a button
            //Button button = sender as Button;

            // find the item that is the datacontext for this button
            //TargetViewModel targetVM = button.DataContext as TargetViewModel;

            //ITarget target = targetVM.Target();
            if(Targets.Count!=0)
            LauncherViewModel.Instance.MoveToCoords(Targets[_listBoxSelection].x,2+ Targets[_listBoxSelection].y, Targets[_listBoxSelection].z);
        }
        private void KillTarget()
        {
            // cast the sender to a button
           // Button button = sender as Button;

            // find the item that is the datacontext for this button
            //TargetViewModel targetVM = button.DataContext as TargetViewModel;

           // foreach(var target in Targets)
            {
                if(Targets.Count!=0)
                LauncherViewModel.Instance.FireAt(Targets[_listBoxSelection].x,2+ Targets[_listBoxSelection].y, Targets[_listBoxSelection].z);

            }
           

           // 
        }
        
        public ICommand ClearTargetsCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand MoveToTargetCommand { get; set; }
        public ICommand KillTargetCommand { get; set; }
        public ICommand KillAllTargetsCommand { get; set; }
        public ICommand KillAllFriendlyTargetsCommand { get; set; }
        public ICommand KillAllEnemyTargetsCommand { get; set; }
    }
}
