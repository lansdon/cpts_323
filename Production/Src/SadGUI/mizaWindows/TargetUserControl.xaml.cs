using SadLibrary.FileLoader;
using SadLibrary.Launcher;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SadGUI.mizaWindows
{
    /// <summary>
    /// Interaction logic for TargetUserControl.xaml
    /// </summary>
    public partial class TargetUserControl : UserControl
    {

        public TargetUserControl()
        {
            InitializeComponent();
            LoadTargetsFromFile("targets.ini");
            LoadTargetsFromServerButton.IsEnabled = false;
            Mediator.Instance.Register("Target List", populateTargets);
            Mediator.Instance.Register("Clear Targets", clearTargets);
        }
        void clearTargets(object p)
        {
            Targets = new ObservableCollection<TargetViewModel>();
            TargetListBox.ItemsSource = Targets;
        }
        void populateTargets(object param)
        {
            IEnumerable<TargetServerCommunicator.Data.Target> temps = param as IEnumerable<TargetServerCommunicator.Data.Target>;
            Targets = new ObservableCollection<TargetViewModel>();
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
            TargetListBox.ItemsSource = Targets;
        }
        private void OpenFileCommand(object sender, RoutedEventArgs e)
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

            Targets = new ObservableCollection<TargetViewModel>();
            foreach (var target in Target_Manager.Instance.Target_List)
            {
                target.Alive = true;
                Targets.Add(new TargetViewModel(target));
            }
            TargetListBox.ItemsSource = Targets;
        }

        private void ClearTargets(object sender, RoutedEventArgs e)
        {
            Targets.Clear();
        }

         private void LoadTargetsFromServer(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void MoveToTarget(object sender, RoutedEventArgs e)
        {
            // cast the sender to a button
            Button button = sender as Button;

            // find the item that is the datacontext for this button
            TargetViewModel targetVM = button.DataContext as TargetViewModel;

            ITarget target = targetVM.Target();
            LauncherViewModel.Instance.MoveToCoords(target.x, target.y, target.z);
        }
        private void KillTarget(object sender, RoutedEventArgs e)
        {
            // cast the sender to a button
            Button button = sender as Button;

            // find the item that is the datacontext for this button
            TargetViewModel targetVM = button.DataContext as TargetViewModel;

            ITarget target = targetVM.Target();
           

            LauncherViewModel.Instance.FireAt(target.x, target.y, target.z);
        }

    }
}
