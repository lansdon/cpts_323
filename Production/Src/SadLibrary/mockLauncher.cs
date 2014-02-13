using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary
{
    class mockLauncher : ILauncher  
    {
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

        public void moveBy(double x, double y, double z)
        {
            Console.WriteLine("Pointing to {0}, {1}, {2}! Sir!", x, y, z);
        }

        public void moveTo(double theta, double phi)
        {
            Console.WriteLine("Pointing to {0} mark {1}! Sir!", theta, phi);
        }

        public void fire()
        {
            Console.WriteLine("FIRE!FIRE!FIRE!");
        }

        public void fireAt(double x, double y, double z)
        {
            Console.WriteLine("Firing at target located {0}, {1}, {2}! Sir!", x, y, z);
        }

        public void calibrate()
        {
            Console.WriteLine("Reseting to start! Sir!");
        }
    }
}
