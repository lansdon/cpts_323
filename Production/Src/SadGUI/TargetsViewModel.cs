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

namespace SadGUI
{
    class TargetsViewModel : ViewModelBase
    {
        private int _listBoxSelection;
        public TargetsViewModel()
        {
            Targets = new ObservableCollection<TargetViewModel>();
            //LoadTargetsFromFile("targets.ini");
            //LoadTargetsFromServerButton.IsEnabled = false;
            MoveToTargetCommand = new DelegateCommand(MoveToTarget);
            KillTargetCommand = new DelegateCommand(KillTarget);
            ClearTargetsCommand = new DelegateCommand(ClearTargets);
            OpenFileCommand = new DelegateCommand(OpenFile);
            
            Mediator.Instance.Register("Target List", populateTargets);
            Mediator.Instance.Register("Clear Targets", clearTargets);
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

        void populateTargets(object param)
        {
            IEnumerable<TargetServerCommunicator.Data.Target> temps = param as IEnumerable<TargetServerCommunicator.Data.Target>;
            
            foreach (var temp in temps)
            {
                ITarget mytemp = new Target();
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
                
                //target.Alive = true;
                Targets.Add(new TargetViewModel(mytemp));
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

        public ObservableCollection<TargetViewModel> Targets
        { get; private set; }

        private void LoadTargetsFromFile(string filename) 
        {
            FileTargetLoader fLoader = FileLoaderFactory.GetFileLoader(SadLibrary.FileLoader.FileLoaderTypes.FILE_INI, filename);
            Target_Manager.Instance.Target_List = fLoader.Parse();

            Targets.Clear();
            foreach (var target in Target_Manager.Instance.Target_List)
            {
                target.Alive = true;
                Targets.Add(new TargetViewModel(target));
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
            LauncherViewModel.Instance.MoveToCoords(Targets[_listBoxSelection].x, Targets[_listBoxSelection].y, Targets[_listBoxSelection].z);
        }
        private void KillTarget()
        {
            // cast the sender to a button
           // Button button = sender as Button;

            // find the item that is the datacontext for this button
            //TargetViewModel targetVM = button.DataContext as TargetViewModel;

           // foreach(var target in Targets)
            {
                LauncherViewModel.Instance.FireAt(Targets[_listBoxSelection].x, Targets[_listBoxSelection].y, Targets[_listBoxSelection].z);

            }
           

           // 
        }
        
        public ICommand ClearTargetsCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand MoveToTargetCommand { get; set; }
        public ICommand KillTargetCommand { get; set; }
    }
}
