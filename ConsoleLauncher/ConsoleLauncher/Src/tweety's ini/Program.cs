//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace Homework1
//{
//    class Program
//    {
      
//        static int Main(string[] args)
//        {
//            string command;
//            string[] commandSplit;
//            List<Target> mylist = null;
//            targetReader TReader = new targetIniReader();
            
//            if (args.Length > 0)
//            {
//                try
//                {
//                    mylist = TReader.targetRead(args[0]);
//                }
//                catch(MyException e)
//                {
//                    System.Console.WriteLine(e.ToString());
//                    System.Console.ReadKey();
//                    return 0;
//                }
                
//            }
//            else
//            {
//                Console.Write("no input file.");
                
//            }
//            command = Console.ReadLine().ToUpper().Trim();
//            while (command != "EXIT")
//            {
//                commandSplit = command.Split(new char[] { ' ' }, 2);
//                commandSplit[0].Trim().ToUpper();
//                switch(commandSplit[0])
//                { 
//                    case "PRINT":
//                    if(commandSplit.Length >1)
//                    {
//                        commandSplit[1].ToUpper().Trim();
//                        foreach(Target element in mylist)
//                        {
//                            if(element.Name == commandSplit[1])
//                            {
//                                element.print();
//                            }
//                        }
//                    }
//                    else
//                        foreach (Target element in mylist)
//                        {
//                            element.print();
//                        }
//                    break;
//                    case "ISFRIEND":
//                        foreach(Target element in mylist)
//                        {
//                            if(element.Name == commandSplit[1])
//                            {
//                                if(element.Friend==true)
//                                {
//                                    Console.WriteLine("Aye Captain!");
//                                }
//                                else
//                                {
//                                    Console.WriteLine("Nay, Scallywag!");
//                                }
//                            }
//                            else
//                            {
//                                Console.WriteLine("That Don't Be A Target in Ye List!");
//                            }
//                        }
//                    break;
//                    case "CONVERT":
//                        StreamWriter writer;
//                        if(commandSplit[1].Length>0)
//                        {
//                            if(commandSplit[1].EndsWith("INI"))
//                            writer = new StreamWriter(commandSplit[1]);
//                            else
//                                writer = new StreamWriter(commandSplit[1] +".ini");

                        
//                            foreach(Target element in mylist)
//                            {
                            
//                                writer.WriteLine(piggy(element.Tag));
                            
//                                writer.Write(piggy("NAME") +" ");
                            
//                                writer.WriteLine(piggy(element.Name));
//                            }
//                        }
//                        else
//                        {
//                            Console.WriteLine("Command needs file name 'convert <file name>'");
//                        }
//                    break;

//                }
//                command = Console.ReadLine().ToUpper().Trim();
               
//            }
//            return 0;
//        }
    
//        public static string piggy(string s)
//        {
//            if(s.StartsWith("A")||s.StartsWith("E")||s.StartsWith("I")||s.StartsWith("O")||s.StartsWith("U")||s.StartsWith("Y"))
//            {
//                s=s + "WAY";
//                return s;
//            }
//            else
//            {
//                string temp;
//                int count=0;
//                foreach(char thing in s)
//                {
                    
//                    if(thing=='A'||thing=='E'||thing=='I'||thing=='O'||thing=='U'||thing=='Y')
//                    {
//                        break;   
//                    }
//                    count++;
//                }
//                temp = s.Substring(0, count);
//                s = s.Substring(count, s.Length-1);
//                s = s + temp + "AY";
//                return s;
//            }
//        }
    
//    }
//}
