using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadLibrary.Targets
{
    public interface ITarget
    {
        int id { get; set; }
        int status { get; set; }
        int hit { get; set; }
        bool movingState { get; set; }
        int led { get; set; }
        string name { get; set; }
        double spawnRate { get; set; }
        bool isMoving { get; set; }
        double points { get; set; }
        double startTime { get; set; }
        double x { get; set; }
        double y { get; set; }
        double z { get; set; }
        int input { get; set; }
        double dutyCycle { get; set; }
        string cordsToString();
        void stringToCords(string input);
    }
}
