using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadLibrary;



namespace SadCL
{
    class Controller
    {
        private List<SadLibrary.Target> targets; // = new List<Target>();


        /*
         *  This is the main entrypoint for the program. This class will
         *  handle the main program loop, and drive all the logic that
         *  pieces the components of the application together.
         * 
         */
        public void Run(string[] args)
        {
            // Do all the setup stuff here!! Initialize all the sub systems etc.
            LoadTargetsFromFile(args.Count() > 0 ? args[0] : "targets.ini");


            // Start the program loop after initialization is done
            MainLoop();
        }


        private void MainLoop() {
            // Menu loop
            while (true)
            {
                Console.Write("Command->");
                string input = Console.ReadLine();
                String [] commands = input.Split(new Char[] {' '});
                string command = "";
                string arg = "";
                if (commands.Count() > 0) command = commands[0].ToLower();
                if (commands.Count() > 1) arg = commands[1].ToLower();
 
                if (command.ToLower() == "print")
                {
                    // Argument Provided - Search for the specific object
                    if(arg.Length > 0) {
                       Target result = targets.Find(x => x.name.ToLower() == arg);
                       if (result != null)
                       {
                           Console.WriteLine("Name={0}", result.name);
                           Console.WriteLine("X={0}", result.X);
                           Console.WriteLine("Y={0}", result.Y);
                           Console.WriteLine("Z={0}", result.Z);
                           Console.WriteLine("Points={0}", result.Points);
                           Console.WriteLine("Friend={0}", result.Friend);
                       }
                       else Console.WriteLine("{0} not found.", arg);
                    }
                    // All targets
                    else
                    {
                        targets.ForEach(delegate(Target target)
                        {
                            Console.WriteLine(target.name);
                        });
                    }
                    Console.WriteLine("Target count = {0}", targets.Count);
                }

                else if (command.ToLower() == "isfriend")
                {
                    Target result = targets.Find(x => x.name.ToLower() == arg);
                    if (result != null)
                    {
                        if(result.Friend) {
                            Console.WriteLine("$5 Love you long time!");
                        } else {
                            Console.WriteLine("I'm going to kill you 'til you die from it.");
                        }
                    }
                    else Console.WriteLine("{0} not found.", arg);
                 }

                else if (command.ToLower() == "exit")
                {
                    Console.WriteLine("Bubye!");
                    break;
                }


            }
        }
        void LoadTargetsFromFile(string filename)
        {
            FileTargetoaderIni fLoader = new FileTargetoaderIni(filename);
            this.targets = fLoader.Parse();
        }


    } // End class





}
