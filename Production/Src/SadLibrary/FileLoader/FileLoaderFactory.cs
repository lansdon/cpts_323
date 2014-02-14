using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SadLibrary.FileLoader
{
    public enum FileLoaderTypes
    {
        FILE_INI,
        FILE_XML
    }

    public class FileLoaderFactory 
    {
        public static FileTargetLoader GetFileLoader(FileLoaderTypes type, string filename) 
        {
            switch (type) 
            {
                case FileLoaderTypes.FILE_INI: return new FileTargetLoaderIni(filename);
//                case FileLoaderTypes.FILE_XML: return new FileTargetLoaderIni(filename);
                default: return new FileTargetLoaderIni(filename);
            }
        }
    }

 }
