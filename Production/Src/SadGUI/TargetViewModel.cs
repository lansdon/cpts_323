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
        private ITarget m_target;
        private bool _Alive;
        public TargetViewModel(ITarget target)
        {
            m_target = target;

        }

        public string Name
        {
            get
            {
                return m_target.name;
            }

            set
            {
                m_target.name = value;
                OnPropertyChanged("Name");
            }
        }
        public double x
        {
            get { return m_target.x; }
            set
            {
                m_target.x = value;
                OnPropertyChanged("x");
            }
        }
        public double y
        {
            get { return m_target.y; }
            set
            {
                m_target.y = value;
                OnPropertyChanged("y");
            }
        }
        public double z
        {
            get { return m_target.z; }
            set
            {
                m_target.z = value;
                OnPropertyChanged("z");
            }
        }
        public bool status
        {
            get
            {
                if (m_target.status == 1)
                    return true;
                else
                    return false;
            }

            set
            {
                if (value == true)
                    m_target.status = 1;
                else
                    m_target.status = 0;
                OnPropertyChanged("status");
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
                return _Alive;
            }
            set
            {
                _Alive = value;  
                OnPropertyChanged("Alive");
            }
        }
        public double Points
        {
            get
            {
                return m_target.points;
            }
            set
            {
                m_target.points = value;
                OnPropertyChanged("Points");
            }
        }
        public double FlashRate
        {
            get
            {
                return m_target.dutyCycle;
            }
            set
            {
                m_target.dutyCycle = value;
                OnPropertyChanged("FlashRate");
            }
        }

        public int Hit
        {
            get
            {
                return m_target.hit;
            }
            set
            {
                m_target.hit = value;
                OnPropertyChanged("Hit");
            }
        }

        public ITarget Target() 
        {
            return m_target;
        } 
    }
}
