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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;


namespace SadGUI
{
    public class LauncherViewModel : ViewModelBase
    {

        static private LauncherViewModel _instance;
        static private ILauncher m_launcher;
        private int _phi, _theta;
        public LauncherViewModel()
        {
            changeLauncher(0);
            _phi = 0;
            _theta = 0;
            FireCommand = new DelegateCommand(Fire);
            UpCommand = new DelegateCommand(Up);
            DownCommand = new DelegateCommand(Down);
            LeftCommand = new DelegateCommand(Left);
            RightCommand = new DelegateCommand(Right);
            ReloadCommand = new DelegateCommand(reload);
            CalibrateCommand = new DelegateCommand(calibrate);
            ClearQueueCommand = new DelegateCommand(ClearQueue);
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
       public void ClearQueue()
           {
               m_launcher.ClearCommandQueue();
           }
        public uint missileCount
        {
            get { return (m_launcher.getMissleCount()); }
            set
            {
              // if(m_launcher.getMissleCount() > 0 || value == 4)
              //    m_launcher.setMissileCount(m_launcher.getMissleCount() - 1);

              //if (m_launcher.getMissleCount() == 0)
              //{
              //    MessageBox.Show("Reload missiles!");
              //    reload();
              //}

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
                m_launcher.movePhi(_phi);
                OnPropertyChanged("phi");

            }

        }
        public int theta
        {
            get { return _theta; }
            set
            {
                _theta = value;
                m_launcher.moveTheta(_theta);
                OnPropertyChanged("theta");
            }
        }
        public void FireAt(double x, double y, double z)
        {
             m_launcher.fireAt(x, y, z);
             missileCount = 0;
        }
        public void MoveToCoords(double x, double y, double z)
        {
            m_launcher.moveCoords(x, y, z);
        }
        public void Fire()
        {
            m_launcher.fire();
            missileCount = 0;
        }
        public void reload()
        {
            m_launcher.reload();
            missileCount = 0;
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
        public ICommand ClearQueueCommand { get; set; }
       
    }
}
