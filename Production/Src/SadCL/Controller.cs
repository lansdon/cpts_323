using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadLibrary;




namespace SadCL
{

    // Type safe enums! Isn't that neat?
    public sealed class Commands
    {
        private readonly String command;
        private readonly int value;

        public static readonly Commands EXIT = new Commands(1, "EXIT");
//        public static readonly Commands PRINT = new Commands(2, "PRINT");
        public static readonly Commands LOAD = new Commands(3, "LOAD");
        public static readonly Commands FIRE = new Commands(4, "FIRE");
        public static readonly Commands MOVE = new Commands(5, "MOVE");
        public static readonly Commands MOVEBY = new Commands(6, "MOVEBY");
        public static readonly Commands RELOAD = new Commands(7, "RELOAD");
        public static readonly Commands SCOUNDRELS = new Commands(8, "SCOUNDRELS");
        public static readonly Commands FRIENDS = new Commands(9, "FRIENDS");
        public static readonly Commands KILL = new Commands(10, "KILL");
        public static readonly Commands STATUS = new Commands(11, "STATUS");

        private Commands(int value, String cmd)
        {
            this.command = cmd;
            this.value = value;
        }

        public override String ToString()
        {
            return command;
        }
    }

    class Controller
    {
        private List<SadLibrary.Targets.Target> targets = new List<SadLibrary.Targets.Target>();

        private SadLibrary.Launcher.ILauncher launcher = SadLibrary.Launcher.LauncherFactory.NewLauncher(LauncherType.LAUCH_TYPE_USB);

        /*
         *  This is the main entrypoint for the program. This class will
         *  handle the main program loop, and drive all the logic that
         *  pieces the components of the application together.
         * 
         */
        public void Run(string[] args)
        {
            // Do all the setup stuff here!! Initialize all the sub systems etc.

            // Start the program loop after initialization is done
            Console.WriteLine("Fire a gun! Launch a missile! DO SOME DAMAGE! (Ready to go, sir!)");
            MainLoop();
        }

        private void MainLoop() {
            // Menu loop runs until process command returns false
            while ( ProcessCommand() )
            {
                // Add extra logic/events inbetween commands here

            }
        }

        void LoadTargetsFromFile(string filename)
        {
            SadLibrary.FileLoader.FileTargetLoader fLoader = SadLibrary.FileLoader.FileLoaderFactory.GetFileLoader(SadLibrary.FileLoader.FileLoaderTypes.FILE_INI, filename);
            SadLibrary.Targets.Target_Manager.Instance.Target_List = fLoader.Parse();
        }

        /*
         * This will capture all input on the command line and route the commands to it's corresponding method.
         * 
         * Returns false if exit condition occurs.
         * 
         */
        bool ProcessCommand()
        {
            Console.Write("Command->");
            string input = Console.ReadLine();
            String[] commands = input.Split(new Char[] { ' ' });
            string command = "";
            string arg = "";
            if (commands.Count() > 0) command = commands[0].ToUpper();
            if (commands.Count() > 1) arg = commands[1].ToUpper();


            if (command == Commands.EXIT.ToString())
            {
                // Return false to indicate program exit
                return false;
            }
            else if (command == Commands.LOAD.ToString())
            {
                CmdLoad(commands);
            }
            else if (command == Commands.FIRE.ToString())
            {
                CmdFire();
            }
            else if (command == Commands.FRIENDS.ToString())
            {
                CmdFriends();
            }
            else if (command == Commands.KILL.ToString())
            {
                CmdKill(commands);
            }
            else if (command == Commands.MOVE.ToString())
            {
                CmdMove(commands);
            }
            else if (command == Commands.MOVEBY.ToString())
            {
                CmdMoveBy(commands);
            }
            else if (command == Commands.RELOAD.ToString())
            {
                CmdReload();
            }
            else if (command == Commands.SCOUNDRELS.ToString())
            {
                CmdScoundrels();
            }
            else if (command == Commands.STATUS.ToString())
            {
                CmdStatus();
            }
            return true;    // Always return true, unless EXIT is selected.
         }

 
        /*
         * ARGUMENT CONVERSIONS
         */
        private bool doubleArgument(string[] args, int argNum, out double result) 
        {
            if ( args.Count() > argNum ) 
            {
                result = Double.Parse(args[argNum]);
                return true;
            }
            result = 0.0;
            return false;
        }

        private bool intArgument(string[] args, int argNum, out int result)
        {
            if (args.Count() > argNum)
            {
                result = int.Parse(args[argNum]);
                return true;
            }
            result = 0;
            return false;
        }

        private bool stringArgument(string[] args, int argNum, out string result)
        {
            if (args.Count() > argNum)
            {
                result = args[argNum];
                return true;
            }
            result ="";
            return false;
        }

        /*
         * COMMANDS - All logic is defined in these methods for running a command
         */

        private void CmdFire()
        {
            Console.WriteLine("CmdFire");
            launcher.fire();
        }

        private void CmdFriends()
        {
            Console.WriteLine("CmdFriends");
            List<SadLibrary.Targets.Target> friends = SadLibrary.Targets.Target_Manager.getFriends();
            foreach (var target in friends)
            {
                target.Print();
            }
        }
        private void CmdLoad(string[] args)
        {
            string filename;
            if (stringArgument(args, 1, out filename))
            {
                LoadTargetsFromFile(filename);
            }
            else
            {
                Console.WriteLine("Error Parsing filename arguments for LOAD command.");
            }
            Console.WriteLine("LOAD: {0}", filename);
        }


        private void CmdKill(string[] args)
        {
            Console.WriteLine("CmdKill");
            string targetName;
            if ( stringArgument(args, 1, out targetName) )
            {
                SadLibrary.Targets.Target target = SadLibrary.Targets.Target_Manager.getTarget(targetName);
                if (target != null && target.Friend == false)
                {
                    launcher.fireAt(target.X, target.Y, target.Z);
                }
                else
                {
                    Console.WriteLine("Sorry Captain, we don’t permit friendly fire, yar");
                }
            }
            else
            {
                Console.WriteLine("Error Parsing Target Name arguments for KILL command.");
            }
        }

        private void CmdMove(string[] args)
        {
            double phi = 0.0, theta = 0.0;
            if (doubleArgument(args, 1, out phi) && doubleArgument(args, 2, out theta))
            {
                launcher.moveTo(theta, phi);
            }
            else
            {
                Console.WriteLine("Error Parsing Phi Theta arguments for MOVE command.");
            }
            Console.WriteLine("CmdMove: Phi = {0}, Theta = {1}", phi, theta);      
        }

        private void CmdMoveBy(string[] args)
        {
            double phi = 0.0, theta = 0.0;
            if (doubleArgument(args, 1, out phi) && doubleArgument(args, 2, out theta))
            {
               launcher.moveBy(theta, phi);
                // ******** WARNING *****
                // The spec sheet says this should take phi theta! Our function takes x, y z. Problem!
               // throw new NotImplementedException();
            }
            else
            {
                Console.WriteLine("Error Parsing Phi Theta arguments for MOVE command.");
            }
            Console.WriteLine("CmdMove: Phi = {0}, Theta = {1}", phi, theta);
            Console.WriteLine("CmdMoveBy");
//            launcher.moveBy()
        }

        private void CmdReload()
        {
            Console.WriteLine("CmdReload");
   
            // NEED THE TARGET SINGLETON!
            launcher.reload();
        }

        void CmdStatus()
        {
            Console.WriteLine("CmdStatus");
            Console.WriteLine("Launcher: {0}", launcher.getName());
            Console.WriteLine("Missiles: {0} of {1}", launcher.getMissleCount(), launcher.getMaxCount());
            
        }

        void CmdScoundrels()
        {
            Console.WriteLine("CmdScounderels");
            List<SadLibrary.Targets.Target> scoundrels = SadLibrary.Targets.Target_Manager.getEnemies();
            foreach (var target in scoundrels)
            {
                target.Print();
            }
        }


    } // End class





}
