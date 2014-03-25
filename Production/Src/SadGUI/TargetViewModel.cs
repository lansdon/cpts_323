using Microsoft.Win32;
using SadGUI;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SadGUI
{
    public class TargetViewModel : ViewModelBase
    {
        private Target m_target;
        public ICommand OpenFileCommand { get; set; }

        public TargetViewModel(Target target)
        {
            m_target = target;
            OpenFileCommand = new DelegateCommand(OpenIniFile);
        }

        public string Name
        {
            get
            {
                return m_target.Name;
            }

            set
            {

                m_target.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public bool Friend
        {
            get
            {
                return m_target.Friend;
            }

            set
            {

                m_target.Friend = value;
                OnPropertyChanged("Friend");
            }
        }
        public string CordsToString
        {
            get
            {
                return m_target.cordsToString();
            }
            set
            {
                m_target.stringToCords(value);
                OnPropertyChanged("CordsToString");
            }
        }
        public bool Alive
        {
            get
            {
                return m_target.Alive;
            }
            set
            {
                m_target.Alive = value;  
                OnPropertyChanged("Alive");
            }
        }
        public int Points
        {
            get
            {
                return m_target.Points;
            }
            set
            {
                m_target.Points = value;
                OnPropertyChanged("Points");
            }
        }
        public int FlashRate
        {
            get
            {
                return m_target.FlashRate;
            }
            set
            {
                m_target.FlashRate = value;
                OnPropertyChanged("FlashRate");
            }
        }

        public void OpenIniFile()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
            }
        }
    }
}
