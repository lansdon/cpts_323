using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Targets
{
    public class Target : ITarget
    {
        private string _name { set; get; }  //
        private double _x { set; get; }//
        private double _y { set; get; }//
        private double _z { set; get; }//
        public bool Friend { set; get; }
        private double _points { set; get; }
        public int FlashRate { set; get; }
        public bool Alive { get; set; }
        public string cordsToString()
        {
            string temp = string.Format("({0}, {1}, {2})",_x,_y,_z);
            return temp;
        }
        public void stringToCords(string input)
        {
            char[] seperators = new char[]{'X','Y','Z',':',',','(',')',' '};
            string[] parts = input.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            _x = Convert.ToDouble(parts[0]);
            _y = Convert.ToDouble(parts[1]);
            _z = Convert.ToDouble(parts[3]);
        }
        public void Print() 
        {
            Console.WriteLine("Target: {0}", _name);
            Console.WriteLine("Friend: {0}", (Friend == true) ? "One of us" : "No, he's a scoundrel in a clever disguise!");
            Console.WriteLine("Position: X={0}, Y={1}, Z={2}", _x, _y, _z);
            Console.WriteLine("Points: {0}", _points);
            Console.WriteLine("Status: {0}", Alive ? "At large" : "Deadsky");
        }
        /*
         * Target: Buddy
            Friend: No He’s a scoundrel with a clever disguise 
         * Position: x=50, y=23, z=34
Points: 100
Status: At Large
         */

        public int id
        {
            get;
            set;
        }

        public int status
        {
            get;
            set;
        }

        public int hit
        {
            get;
            set;
        }

        public bool movingState
        {
            get;
            set;
        }

        public int led
        {
            get;
            set;
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public double spawnRate
        {
            get;
            set;
        }

        public bool isMoving
        {
            get;
            set;
        }

        public double points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }

        public double startTime
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public double x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public double z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }

        public int input
        {
            get;
            set;
        }

        public double dutyCycle
        {
            get;
            set;
        }
    }
}
