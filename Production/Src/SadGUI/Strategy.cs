using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadGUI
{
    class Strategy
    {
        public void GetStrategy(object list)
        {
            IEnumerable<TargetViewModel> Targets = list as IEnumerable<TargetViewModel>;
            foreach (var target in Targets)
            {
                
            }
        }
    }
}
