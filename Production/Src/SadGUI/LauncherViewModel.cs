using SadLibrary.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;


namespace SadGUI
{
    class LauncherViewModel : ViewModelBase
    {
        private ILauncher m_launcher;

        public LauncherViewModel(ILauncher launcher)
        {
            m_launcher = launcher;

            FireCommand = new DelegateCommand(Fire);
            UpCommand = new DelegateCommand(Up);
            DownCommand = new DelegateCommand(Down);
            LeftCommand = new DelegateCommand(Left);
            RightCommand = new DelegateCommand(Right);
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
        public ICommand FireCommand { get; set; }
        public ICommand UpCommand { get; set; }
        public ICommand DownCommand { get; set; }
        public ICommand RightCommand { get; set; }
        public ICommand LeftCommand { get; set; }
    }
}
