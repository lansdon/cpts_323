using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Launcher
{
    public class mockLauncher : BaseLauncher
    {
        public override void moveUp()
        {
            Console.WriteLine("Moving up! Sir!");
        }

        public override void moveDown()
        {
            Console.WriteLine("Moving down! Sir!");
        }

        public override void moveLeft()
        {
            Console.WriteLine("Moving left! Sir!");
        }

        public override void moveRight()
        {
            Console.WriteLine("Moving right! sir!");
        }

        public override void moveBy(double x, double y, double z)
        {
            Console.WriteLine("Pointing to {0}, {1}, {2}! Sir!", x, y, z);
        }

        public override void moveTo(double theta, double phi)
        {
            Console.WriteLine("Pointing to {0} mark {1}! Sir!", theta, phi);
        }

        public override void fire()
        {
            Console.WriteLine("FIRE!FIRE!FIRE!");
        }

        public override void fireAt(double x, double y, double z)
        {
            Console.WriteLine("Firing at target located {0}, {1}, {2}! Sir!", x, y, z);
        }

        public override void calibrate()
        {
            Console.WriteLine("Reseting to start! Sir!");
        }
    }
}
