using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;


namespace Homework1
{
    class targetIniReader: targetReader
    {
        private Hashtable key = new Hashtable();
        private string line;
        
        List<Target> targets = new List<Target>();
        Target tempTargets;

        public override List<Target> targetRead(string path)
        {
            StreamReader input = null;
            string[] key = null;
            input = new StreamReader(path);
            if (File.Exists(path))
            {
                line = input.ReadLine();
                while (line != null)
                {
                    line = line.Trim();
                    if (line != "")
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {

                            tempTargets = new Target();
                            line = line.Substring(1, line.Length - 2).ToUpper();
                            if (line.StartsWith("TARGET"))
                                tempTargets.Tag = line;
                            else
                                throw new MyException("Invalid tag.");
                            line = input.ReadLine();
                            line = line.Trim();

                            while (!string.Equals(line, "[TARGET]", StringComparison.InvariantCultureIgnoreCase) && line != null)
                            {
                                key = line.Split(new char[] { '=' }, 2);
                                key[0] = key[0].Trim().ToUpper();
                                if (key.Length > 1 && key[1].Contains('#'))
                                {
                                    key[1] = key[1].Substring(0, key[1].IndexOf("#")).Trim().ToUpper();
                                }
                                switch (key[0])
                                {
                                    case "NAME":
                                        if (key[1] != "")
                                            if (!key[1].Contains(" "))
                                                tempTargets.Name = key[1].ToUpper();
                                            else
                                                throw new MyException("Name contains space/spaces.");
                                        break;
                                    case "FRIEND":
                                        if (key[1] != "")
                                            tempTargets.Friend = Convert.ToBoolean(key[1]);
                                        break;
                                    case "FLASHRATE":
                                        if (key[1] != "")
                                            tempTargets.FlashRate = Convert.ToDouble(key[1]);
                                        break;
                                    case "POINTS":
                                        if (key[1] != "")
                                            tempTargets.Points = Convert.ToDouble(key[1]);
                                        break;
                                    case "X":
                                        if (key[1] != "")
                                            tempTargets.X = Convert.ToDouble(key[1]);
                                        break;
                                    case "Y":
                                        if (key[1] != "")
                                            tempTargets.Y = Convert.ToDouble(key[1]);
                                        break;
                                    case "Z":
                                        if (key[1] != "")
                                            tempTargets.Z = Convert.ToDouble(key[1]);
                                        break;
                                }

                                line = input.ReadLine();

                            }
                            if (tempTargets.infoCheck())
                                targets.Add(tempTargets);
                            else
                                throw new MyException("Missing Data.");


                        }
                        else
                            throw new MyException("Improper format.");
                       
                    }
                    
                }

                  

                
            }
           return targets;
        }
    }
}
