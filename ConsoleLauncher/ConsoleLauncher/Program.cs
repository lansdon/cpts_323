using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleLauncher;


namespace LauncherClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the controller class which drives the entire program.
            //Controller controller = new Controller();
            //controller.Run(args);
            missileLauncher gun = new missileLauncher();
            gun.moveBy(-11,-10,5);
            gun.fire();
            gun.moveTo(50, 0);
            gun.fire();
            gun.fireAt(1,5, 4);
            gun.fireAt(1, 5, 1);
            gun.fireAt(1,5, -3);

        }
    }
}
