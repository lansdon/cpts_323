using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    abstract class targetReader
    {
        public abstract List<Target> targetRead(string path);
    }
}
