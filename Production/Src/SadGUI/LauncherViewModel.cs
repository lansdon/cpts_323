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
<<<<<<< HEAD
        
        
=======
        public event PropertyChangedEventHandler LauncherPropertyChanged;
>>>>>>> 20e01bacfbc510ce4c191b0a3adc4c1341d85c39
        private ILauncher m_launcher;
        private int _phi, _theta;
        private uint _missileCount;
        public LauncherViewModel(ILauncher launcher)
        {
            m_launcher = launcher;
            _phi = 0;
            _theta = 0;
            _missileCount = m_launcher.getMissleCount();
            FireCommand = new DelegateCommand(Fire);
            UpCommand = new DelegateCommand(Up);
            DownCommand = new DelegateCommand(Down);
            LeftCommand = new DelegateCommand(Left);
            RightCommand = new DelegateCommand(Right);
            ReloadCommand = new DelegateCommand(reload);
            CalibrateCommand = new DelegateCommand(calibrate);
        }
        
           
       
        public uint missileCount
        {
            get { return (_missileCount); }
            set
            {
<<<<<<< HEAD
               if(_missileCount > 0 || value == 4)
                   _missileCount = value;
               
                   
               OnPropertyChanged("missileCount");
               
=======
                _missileCount = (uint)value;
               OnPropertyChanged("_missileCount");
>>>>>>> 20e01bacfbc510ce4c191b0a3adc4c1341d85c39
            }
        }
        public void calibrate()
        {
            m_launcher.calibrate();
            missileCount = 4;
            phi = 0;
            theta = 0;
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
            missileCount--;
        }
        public void reload()
        {
            m_launcher.reload();
            missileCount = 4;
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
<<<<<<< HEAD
        
=======
        protected void OnPropertyChanged(string propertyName)
        {
            if (LauncherPropertyChanged != null)
            {
                LauncherPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            
           
            m_launcher.moveTo(_theta, _phi);
            
        }
>>>>>>> 20e01bacfbc510ce4c191b0a3adc4c1341d85c39
        
        public ICommand FireCommand { get; set; }
        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }
        public ICommand RightCommand { get; set; }
        public ICommand LeftCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand CalibrateCommand { get; set; }
       
    }
}
