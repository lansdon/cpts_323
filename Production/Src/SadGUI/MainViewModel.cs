using SadLibrary.Launcher;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGUI
{
    class MainViewModel: ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private ILauncher m_launcher;
        private string m_name;
        private double m_xPosition;
        
        public MainViewModel(ILauncher launcher)
        {
            LCVM = new LauncherConnectViewModel();
            var t = new Target();
            t.Name = "this is a target";
            TargetsViewModel = new TargetViewModel(t);
            LauncherViewModel = LauncherViewModel.Instance;
            VideoCheckBox = new VideoCheckBox();
            DefaultControlCheckBox = new DefaultControlCheckBox();
            TwitterFeedCheckBox = new TwitterFeedCheckBox();
        }

        public TargetViewModel TargetsViewModel { get; set; }

        //public ObservableCollection<TargetViewModel> Targets
        //{ get; private set; }

        public LauncherViewModel LauncherViewModel { get; set; }
        //public ICommand AddNewTarget { get; private set; }

        public VideoCheckBox VideoCheckBox { get; set; }
        public LauncherConnectViewModel LCVM { get; set; }
        public DefaultControlCheckBox DefaultControlCheckBox { get; set; }

        public TwitterFeedCheckBox TwitterFeedCheckBox { get; set; }
    }
}
