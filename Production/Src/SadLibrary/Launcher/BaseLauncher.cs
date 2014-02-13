using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Launcher
{

    public abstract class BaseLauncher : ILauncher 
    {
        protected double myPhi, myTheta;
        protected double degreeDelay = 19;
        public uint missileCount;
        public uint MAX_MISSILE_COUNT = 4;
        public string name = "";

        public void reload()
        {
            missileCount = MAX_MISSILE_COUNT;
        }

        public virtual void moveUp()
        {
 	        throw new NotImplementedException();
        }

        public virtual void moveDown()
        {
 	        throw new NotImplementedException();
        }

        public virtual void moveLeft()
        {
 	        throw new NotImplementedException();
        }

        public virtual void moveRight()
        {
 	        throw new NotImplementedException();
        }

        public virtual void moveBy(double x, double y, double z)
        {
 	        throw new NotImplementedException();
        }

        public virtual void moveTo(double theta, double phi)
        {
 	        throw new NotImplementedException();
        }

        public virtual void fire()
        {
 	        throw new NotImplementedException();
        }

        public virtual void fireAt(double x, double y, double z)
        {
 	        throw new NotImplementedException();
        }

        public virtual void calibrate()
        {
 	        throw new NotImplementedException();
        }


    }
}
