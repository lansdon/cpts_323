using SadLibrary.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Threading;


namespace SadGUI
{
    class LauncherViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler LauncherPropertyChanged;
        private ILauncher m_launcher;
        private int _phi, _theta;
        
        public LauncherViewModel(ILauncher launcher)
        {
            m_launcher = launcher;
            _phi = 0;
            _theta = 0;
            FireCommand = new DelegateCommand(Fire);
            UpCommand = new DelegateCommand(Up);
            DownCommand = new DelegateCommand(Down);
            LeftCommand = new DelegateCommand(Left);
            RightCommand = new DelegateCommand(Right);
            
        }
        
           
       
      
        public int phi
        {
            get { return _phi; }
            set
            {
                _phi = value;
                OnPropertyChanged("_phi");
            }

        }
        public int theta
        {
            get { return _theta; }
            set
            {
                _theta = value;
                OnPropertyChanged("_theta");
            }
           }
        public void Fire()
        {
            m_launcher.fire();
        }
        public void Up()
        {
            m_launcher.moveUp();
        }
        public void Down()
        {
            m_launcher.moveDown();
        }
        public void Left()
        {
            m_launcher.moveLeft();
        }
        public void Right()
        {

            m_launcher.moveRight();
        }
        protected override void OnPropertyChanged(string propertyName)
        {
            if (LauncherPropertyChanged != null)
            {
                LauncherPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            m_launcher.moveTo(_theta, _phi);
            
        }
        
        public ICommand FireCommand { get; set; }
        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }
        public ICommand RightCommand { get; set; }
        public ICommand LeftCommand { get; set; }
        
       
    }
}
