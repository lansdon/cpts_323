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
//        public static readonly Commands IS_FRIEND = new Commands(3, "IS_FRIEND");
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
        private List<SadLibrary.Target> targets = new List<Target>();

        private SadLibrary.Launcher.BaseLauncher launcher = SadLibrary.Launcher.LauncherFactory.NewLauncher(LauncherType.LAUNCH_TYPE_MOCK);

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
            FileTargetoaderIni fLoader = new FileTargetoaderIni(filename);
            this.targets = fLoader.Parse();
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
                CmdKill();
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
                CmdScounderels();
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
            throw new NotImplementedException();
           
        }

        private void CmdKill()
        {
            Console.WriteLine("CmdKill");
            throw new NotImplementedException();
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
//                launcher.moveBy(theta, phi);
                // ******** WARNING *****
                // The spec sheet says this should take phi theta! Our function takes x, y z. Problem!
                throw new NotImplementedException();
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
            throw new NotImplementedException();

        }

        void CmdStatus()
        {
            Console.WriteLine("CmdStatus");
            throw new NotImplementedException();
        }

        void CmdScounderels()
        {
            Console.WriteLine("CmdScounderels");
            throw new NotImplementedException();
        }


    } // End class





}
