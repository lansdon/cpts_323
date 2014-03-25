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



        static private LauncherViewModel _instance;
        static private ILauncher m_launcher;
        private int _phi, _theta;
        private uint _missileCount;
        public LauncherViewModel()
        {
            changeLauncher(0);
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
       
        static public LauncherViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LauncherViewModel();
                }
                return _instance;
            }
        }
           public void changeLauncher(int value)
           {
               if (m_launcher != null)
               {
                   m_launcher = LauncherFactory.NewLauncher((LauncherType)value);
               }
               else
                    m_launcher = LauncherFactory.NewLauncher((LauncherType)0);
           }
       
        public uint missileCount
        {
            get { return (_missileCount); }
            set
            {

               if(_missileCount > 0 || value == 4)
                   _missileCount = value;
               
                   
               OnPropertyChanged("missileCount");
               

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
                OnPropertyChanged("phi");
            }

        }
        public int theta
        {
            get { return _theta; }
            set
            {
                _theta = value;
                OnPropertyChanged("theta");
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
            phi++;
            m_launcher.moveUp();
           
        }
        public void Down()
        {
            m_launcher.moveDown();
            phi--;
        }
        public void Left()
        {
            m_launcher.moveLeft();
            theta--;
        }
        public void Right()
        {

            m_launcher.moveRight();
            theta++;
        }

        
        public ICommand FireCommand { get; set; }
        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }
        public ICommand RightCommand { get; set; }
        public ICommand LeftCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand CalibrateCommand { get; set; }
       
    }
}
