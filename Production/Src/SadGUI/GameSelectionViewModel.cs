using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TargetServerCommunicator;

namespace SadGUI
{
    class GameSelectionViewModel : ViewModelBase
    {
        private int _selectedIndex;
        public ObservableCollection<string> games { get; set; }
        
        public GameSelectionViewModel()
        {
            Mediator.Instance.Register("to games", toGames);
            games = new ObservableCollection<string>();
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }
        private void toGames(object parameter)
        {
            var gameserver = parameter as IGameServer;
            var data = gameserver.RetrieveGameList();
            
            foreach(string game in data)
            {
                string[] Games = game.Split(',');
                
                foreach(string g in Games)
                     games.Add(g.Trim());
            }
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
