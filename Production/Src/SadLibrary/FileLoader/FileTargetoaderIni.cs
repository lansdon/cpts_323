using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SadLibrary.Targets;

namespace SadLibrary.FileLoader
{
    enum TargetFields
    {
        F_TARGET, F_NAME, F_X, F_Y, F_Z, F_FRIEND, F_POINTS, F_FLASH, F_COMPLETE
    };


    public class FileTargetLoaderIni : FileTargetLoader
    {
 
        // Constructor
        public FileTargetLoaderIni(String filepath) : base(filepath)
        {

        }

        override public List<SadLibrary.Targets.Target> Parse()
        {
            // Error, nothig to parse.
            if (_lines == null) return new List<SadLibrary.Targets.Target>();

 //           const int TARGET_ATTRIBUTE_COUNT = 8;
            TargetFields currentAttributeNum = TargetFields.F_TARGET;
            SadLibrary.Targets.Target currentTarget = new SadLibrary.Targets.Target();        // The target being built.
            List<SadLibrary.Targets.Target> result = new List<SadLibrary.Targets.Target>();
            bool error = false;
            for (int i = 0; i < _lines.Count(); ++i)
            {
                List<string> lineSegments = new List<string>(_lines[i].Split(new Char[] {'=', '#'}));
                string lineId = lineSegments[0].ToLower();
                string lineValue = "";
                if(lineSegments.Count >= 2)
                    lineValue = lineSegments[1];

                // HEADER
                if(currentAttributeNum == TargetFields.F_TARGET && lineId == "[target]")
                {
                    error = false;
                    currentTarget = new Target();
                    currentAttributeNum++;
                } 
                
                // NAME
                else if (currentAttributeNum == TargetFields.F_NAME && lineId == "name") 
                {
                    currentTarget.Name = lineValue;
                    currentAttributeNum++;
                } 
                
                // X
                else if (currentAttributeNum == TargetFields.F_X && lineId == "x") 
                {
                    double x = -1.0;
                    if (Double.TryParse(lineValue, out x))
                    {
                        currentTarget.X = x;
                        currentAttributeNum++;
                    }
                    else error = true;
               } 
                
                // Y
                else if (currentAttributeNum == TargetFields.F_Y && lineId == "y") 
                {
                    double y = -1.0;
                    if (Double.TryParse(lineValue, out y))
                    {
                        currentTarget.Y = y;
                        currentAttributeNum++;
                    }
                    else error = true;
                } 
                
                // Z
                else if (currentAttributeNum == TargetFields.F_Z && lineId == "z") 
                {
                    double z = -1.0;
                    if (Double.TryParse(lineValue, out z))
                    {
                        currentTarget.Z = z;
                        currentAttributeNum++;
                    }
                    else error = true;
                } 
                
                // FRIEND
                else if (currentAttributeNum == TargetFields.F_FRIEND && lineId == "friend") 
                {
                    currentTarget.Friend =  (lineValue.ToLower() == "true" ) ? true : false;
                    currentAttributeNum++;
                }
                else if (currentAttributeNum == TargetFields.F_POINTS && lineId == "points")
                {
                        int points = 0;
                        if (Int32.TryParse(lineValue, out points))
                        {
                            currentTarget.Points = points;
                        }
                        else error = true;
                        currentAttributeNum++;
                 } 
                
                // FLASHRATE
                else if (currentAttributeNum == TargetFields.F_FLASH && lineId == "flashrate") 
                {
                        int flashRate = 0;
                        if (Int32.TryParse(lineValue, out flashRate))
                        {
                            currentTarget.FlashRate = flashRate;
                        }
                        else error = true;
                        currentAttributeNum++;
                } 

                if (currentAttributeNum == TargetFields.F_COMPLETE)
                {
                    if (error == false) {
                        result.Add(currentTarget);
//                       System.Console.WriteLine(currentTarget);
                        currentAttributeNum = TargetFields.F_TARGET;
                    }
                    else break;
                }
            }
             return result;
        }
    }
}
