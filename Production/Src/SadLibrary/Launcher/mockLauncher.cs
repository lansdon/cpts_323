using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Launcher
{
    public class mockLauncher : ILauncher
    {
        public uint missileCount;
        public uint MAX_MISSILE_COUNT = 4;
        public string name = "";
        public void reload()
        {
            missileCount = MAX_MISSILE_COUNT;
        }
        public mockLauncher()
        {
            name = "Meatless Meat";

            calibrate();
            reload();
        }
        public void ClearCommandQueue()
        {

        }
        public void moveUp()
        {
            Console.WriteLine("Moving up! Sir!");
        }

        public void moveDown()
        {
            Console.WriteLine("Moving down! Sir!");
        }

        public void moveLeft()
        {
            Console.WriteLine("Moving left! Sir!");
        }

        public void moveRight()
        {
            Console.WriteLine("Moving right! sir!");
        }
        public void moveTheta(double theta)
        {
            Console.WriteLine("Moving to {0} Sir!", theta);
        }
        public void movePhi(double phi)
        {
            Console.WriteLine("Moving to {0} Sir!", phi);
        }
        public void moveBy(double theta, double phi)
        {
            Console.WriteLine("Moving to {0}, {1}! Sir!", theta, phi);
        }

        public void moveTo(double theta, double phi)
        {
            Console.WriteLine("Pointing to {0} mark {1}! Sir!", theta, phi);
        }

        public void fire()
        {
            if (missileCount > 0)
            {
                Console.WriteLine("FIRE!FIRE!FIRE!");
 //               command_Fire();
                --missileCount;
            }
            else
            {
                Console.WriteLine("I just can’t do it cap’tin, we just don’t have tha power");
            }
        }

        public void fireAt(double x, double y, double z)
        {
            Console.WriteLine("Firing at target located {0}, {1}, {2}! Sir!", x, y, z);
        }

        public void calibrate()
        {
            Console.WriteLine("Reseting to start! Sir!");
            reload();
        }

        public void setMissileCount(uint value)
        {
            missileCount = value;
        }
        

        public void moveCoords(double x, double y, double z)
        {
            Console.WriteLine("Move to coords {0}, {1}, {2}! Sir!", x, y, z);
        }

        public uint getMissleCount()
        {
            return missileCount;
        }

        public string getName()
        {
            return name;
        }

        public uint getMaxCount()
        {
            return MAX_MISSILE_COUNT;
        }
        //Function to convert x, y to a theta for spherical coordinates.
        public double toTheta(double x, double y)
        {
            if (x >= 0)
                return (Math.Atan2(x, y) * (180 / Math.PI));
            else
                return (180 - (Math.Atan2(x, y) * (180 / Math.PI)));
        }

        //function to convert the x, y, z to phi for spherical coordinates.
        public double toPhi(double x, double y, double z)
        {
            double squaredRoot = Math.Sqrt((x * x) + (y * y));
            if (z >= 0)
                return (90 - (Math.Atan2(squaredRoot, z) * (180 / Math.PI)));
            else
                return (90 - (180 - (Math.Atan2(squaredRoot, z) * (180 / Math.PI))));
        }
    }
}
