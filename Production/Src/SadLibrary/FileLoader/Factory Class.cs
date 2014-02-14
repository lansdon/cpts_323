using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Factory_Class
{
    class Program
    {
        abstract public class FileTargetLoader
        {
            public FileTargetLoader(string FilePath)
            {
                Console.WriteLine(FilePath);
            }
            // This is just for my coding sake.  Remove later when no longer needed.
        }

        public class FileTargetLoaderIni : FileTargetLoader
        {
            public FileTargetLoaderIni(string FilePath): base (FilePath)
            {
                Console.WriteLine(FilePath);
            }
            // This is just for my coding sake.  Remove later when no longer needed.
        }

        public class Target
        {
            public Target() { }
            // This is just for my coding sake.  Remove later when no longer needed.
        }

        public class Object_Factory
        {
            public Object_Factory() { }

            public FileTargetLoader getParser(string FilePath)
            {
                FileTargetLoader temp = null;
                switch (Path.GetExtension(FilePath).ToLower())
                {
                    case ".ini":
                        temp = (FileTargetLoader)(new FileTargetLoaderIni(FilePath));
                        break;
                    case ".txt":
                        break;
                    case ".xml":
                        break;
                    default:
                        Console.WriteLine("Uknown file extension: " + Path.GetExtension(FilePath));
                        break;
                }
                return temp;
            }
        }


        static void Main(string[] args)
        {
            Object_Factory Factory = new Object_Factory();

            FileTargetLoader Temp = Factory.getParser("blubber.ini");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
