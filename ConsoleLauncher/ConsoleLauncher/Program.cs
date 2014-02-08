using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace LauncherClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the controller class which drives the entire program.
            Controller controller = new Controller();
            controller.Run(args);
        }
    }
}
