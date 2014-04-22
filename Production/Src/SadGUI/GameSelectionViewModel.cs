using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGUI
{
    class GameSelectionViewModel : ViewModelBase
    {
        private int _selectedIndex;
        public ObservableCollection<string> games { get; set; }
        public GameSelectionViewModel()
        {
            games = new ObservableCollection<string>();
            games.Add("game 1");
            games.Add("game 2");
            games.Add("game 3");
            games.Add("game 4");
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex == value) { return; }
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }
        public void Ok() { Console.WriteLine(_selectedIndex); }
        public void Cancel() { }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
