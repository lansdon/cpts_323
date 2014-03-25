using SadLibrary.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadGUI
{
    class LauncherConnectViewModel : ViewModelBase
    {
        private bool IsChecked;
        public LauncherConnectViewModel()
        {
            IsChecked = false;
        }

        public bool LauncherConnect_CheckBox_IsChecked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                IsChecked = value;
                Process_CheckBox();
                OnPropertyChanged("LauncherConnect_CheckBox_IsChecked");
            }
        }

        private void Process_CheckBox()
        {
            if (IsChecked == true)
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
