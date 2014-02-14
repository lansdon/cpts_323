using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Targets
{
    public class Target
    {
        public string Name { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public bool Friend { set; get; }
        public int Points { set; get; }
        public int FlashRate { set; get; }
        public bool Alive { get; set; }

        public void Print() 
        {
            Console.WriteLine("Target: {0}", Name);
            Console.WriteLine("Friend: {0}", (Friend == true) ? "One of us" : "No, he's a scoundrel in a clever disguise!");
            Console.WriteLine("Position: X={0}, Y={1}, Z={2}", X, Y, Z);
            Console.WriteLine("Points: {0}", Points);
            Console.WriteLine("Status: {0}", Alive ? "At large" : "Deadsky");
        }
        /*
         * Target: Buddy
            Friend: No He’s a scoundrel with a clever disguise 
         * Position: x=50, y=23, z=34
Points: 100
Status: At Large
         */
    }
}
