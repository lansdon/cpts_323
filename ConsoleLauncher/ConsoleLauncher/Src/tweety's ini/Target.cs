using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    class Target
    {
        private double x,y,z,flashRate,points;
        private string name,tag;
        private bool friend,_hasX=false,_hasY=false,_hasZ=false,_hasName=false,_hasFriend=false,_hasFlash=false,_hasPoints=false,_hasTag=false;


        public double X
        {
            get{return x;}
            set { x = value; _hasX = true; }
        }
        
        public double Y
        {
            get{return y;}
            set { y = value; _hasY = true; }
        }
        public double Z
        {
            get{return z;}
            set { z = value; _hasZ = true; }
        }
        public string Name
        {
            get{return name;}
            set { name = value; _hasName = true; }
        }
        public bool Friend
        {
            get{return friend;}
            set { friend = value; _hasFriend = true; }
        }
        public double Points
        {
            get { return points; }
            set { points = value; _hasPoints = true; }
        }
        public double FlashRate
        {
            get { return flashRate; }
            set { flashRate = value; _hasFlash = true; }
        }
        public string Tag
        {
            get { return tag; }
            set { tag = value; _hasTag = true; }
        }
        public bool infoCheck()
        {
            bool check=true;
            if(_hasName==false)
            {
                check = false;
            } 
            if (_hasTag == false)
            {
                check = false;
            } 
            if (_hasX == false)
            {
                check = false;
            } 
            if (_hasY == false)
            {
                check = false;
            } 
            if (_hasZ == false)
            {
                check = false;
            }
            if (_hasFriend == false)
            {
                check = false;
            }
            if (_hasFlash == false)
            {
                check = false;
            }
            if (_hasPoints == false)
            {
                check = false;
            }
            return check;
        }
        public void print()
        {
            Console.WriteLine(tag);
            Console.WriteLine("Name " + name);
            Console.WriteLine("X " + x);
            Console.WriteLine("Y " + y);
            Console.WriteLine("Z " + z);
            Console.WriteLine("Frind " + friend);
            Console.WriteLine("Points " + points);
            Console.WriteLine("Flash Rate " + flashRate);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

