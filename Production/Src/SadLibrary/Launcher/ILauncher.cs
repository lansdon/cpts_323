using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Launcher
{
    public interface ILauncher
    {
        void moveUp();
        void moveDown();
        void moveLeft();
        void moveRight();
        void moveBy(double x, double y, double z);
        void moveTo(double theta, double phi);
        void fire();
        void fireAt(double x, double y, double z);
        void calibrate();
    }
}
