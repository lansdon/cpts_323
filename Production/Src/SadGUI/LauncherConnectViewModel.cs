using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadGUI;
using SadGUI.mizaWindows;

namespace SadGUI
{
    public class LauncherConnectViewModel: ViewModelBase
    {
        private bool _IsChecked;
        public LauncherConnectViewModel()
        {
            _IsChecked = false;
        }

        public bool LauncherConnect_CheckBox_IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
                Process_CheckBox();
                OnPropertyChanged("LauncherConnect_CheckBox_IsChecked");
            }
        }

        private void Process_CheckBox()
        {
            if (_IsChecked == true)
            {
                LauncherViewModel.Instance.changeLauncher(1);
            }
            else
            {
                LauncherViewModel.Instance.changeLauncher(0);
            }
            

        }
    }
}
