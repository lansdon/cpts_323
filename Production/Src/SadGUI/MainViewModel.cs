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

        private ILauncher m_launcher;
        private string m_name;
        private double m_xPosition;

        public MainViewModel(ILauncher launcher, IEnumerable<Target> targets)
        {
            m_launcher = launcher;
            FireCommand = new DelegateCommand(Fire);

            Targets = new ObservableCollection<TargetViewModel>();
            foreach (var target in targets)
            {
                Targets.Add(new TargetViewModel(target));
            }

            AddNewTarget = new DelegateCommand(AddTarget);

            var t = new Target();
            t.Name = "this is a target";
            TargetsViewModel = new TargetViewModel(t);
        }

        private void AddTarget()
        {
            var newTarget = new Target();
            newTarget.Name = "asdfasdf";

            Targets.Add(new TargetViewModel(newTarget));
        }

        public TargetViewModel TargetsViewModel { get; set; }

        public ObservableCollection<TargetViewModel> Targets
        { get; private set; }

        public void Fire()
        {
            m_launcher.fire();
        }

        public ICommand AddNewTarget { get; private set; }
        public ICommand FireCommand { get; set; }
    }
}
