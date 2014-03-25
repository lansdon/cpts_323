using SadLibrary.FileLoader;
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
                FileTargetLoader fLoader = FileLoaderFactory.GetFileLoader(SadLibrary.FileLoader.FileLoaderTypes.FILE_INI, filename);
                Target_Manager.Instance.Target_List = fLoader.Parse();

                Targets = new ObservableCollection<TargetViewModel>();
                foreach (var target in Target_Manager.Instance.Target_List)
                {
                    Targets.Add(new TargetViewModel(target));
                }
                TargetListBox.Items.Clear();

                AddNewTarget = new DelegateCommand(AddTarget);

                var t = new Target();
                t.Name = "this is a target";
 //               TargetsViewModel = new TargetViewModel(t);


            }
        }

        private void AddTarget()
        {
            var newTarget = new Target();
            newTarget.Name = "asdfasdf";

            Targets.Add(new TargetViewModel(newTarget));
        }


//        public TargetViewModel TargetsViewModel { get; set; }

        public ObservableCollection<TargetViewModel> Targets
        { get; private set; }

        public ICommand AddNewTarget { get; private set; }

    }
}
