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
        void moveBy(double theta, double phi);
        void moveCoords(double x, double y, double z);
        void moveTo(double theta, double phi);
        void fire();
        void fireAt(double x, double y, double z);
        void calibrate();
        uint getMissleCount();
        string getName();
        uint getMaxCount();
        void reload();
        void setMissileCount(uint value);
        double toPhi(double x,double y,double z);
        double toTheta(double x, double y);
        void moveTheta(double theta);
        void movePhi(double phi);
        private void ClearCommandQueue();
    }
}
