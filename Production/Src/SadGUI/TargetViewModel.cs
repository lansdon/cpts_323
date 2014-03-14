using SadGUI;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadGUI
{
    public class TargetViewModel : ViewModelBase
    {
        private Target m_target;

        public TargetViewModel(Target target)
        {
            m_target = target;
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

        public double X
        {
            get
            {
                return m_target.X;
            }
            set
            {
                m_target.X = value;
                OnPropertyChanged("X");
            }
        }
    }
}
