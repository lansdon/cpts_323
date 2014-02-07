using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using hw1_page_lansdon;


namespace ConsoleLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Target> targets = new List<Target>();

           string filename = "targets.ini";      // For testing use default name.
           //if (args != null && args.Length < 1)
           // {
           //     System.Console.WriteLine("You must provide a filename!");
 //               return;       // Uncomment when for release
            //}
            //else
            //{
//                filename = args[0];
                FileTargetLoader loader = new FileLTargetoaderIni(filename);
                targets = loader.Parse();
            //}

 //           Console.WriteLine("Args = {0}", args);

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

                else if (command.ToLower() == "convert")
                {
                    storePigFile(arg, targets);
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
        static private void storePigFile(string filename, List<Target> targets) {
            List<String> pigLines = new List<String>();

            foreach (Target target in targets)
            {
                pigLines.Add(convertToPigLatin("target"));
                pigLines.Add(convertToPigLatin(target.name));
                pigLines.Add(convertToPigLatin(""));
            }

            System.IO.File.WriteAllLines(filename, pigLines);
        }

        static private string convertToPigLatin(string inputStr)
        {
            string vowels = "AEIOUaeiou";

            for (int i = 0; i < inputStr.Length; ++i)
            {
                // Stop when we hit a vowel
                if (vowels.IndexOf(inputStr[i]) != -1) {
                    // If it's the first letter, add "way"
                    if (i == 0)
                    {
                        return inputStr + "way";
                    }
                    else
                    {
                        string firstPart = inputStr.Substring(0, i);
                        string secondPart = inputStr.Substring(i, inputStr.Length - i);
                        return secondPart + firstPart + "ay";
                    }
                }
            }
            return "";
        }
     }
}
