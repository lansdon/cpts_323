using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 *  This is a base class for the file loaders. A derived class should be created to handle different formats.
 * 
 *  Created by: Lansdon Page
 */
namespace hw1_page_lansdon
{
    abstract public class FileTargetLoader
    {
        protected String filepath;
        protected String[] _lines;
        // Public Properties

        // Private Properties
        public FileTargetLoader(String filepath)
        {
            this.filepath = filepath;
            try
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    Console.WriteLine("Successfully opened {0}", filepath);
                    _lines = System.IO.File.ReadAllLines(filepath);
 //                   Parse();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        // Must override parse class for different file types
        abstract public List<Target> Parse();
 
    }
}
