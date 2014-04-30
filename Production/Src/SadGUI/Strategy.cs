using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SadGUI
{
    class Strategy
    {
        public Strategy()
        {
            Mediator.Instance.Register("Run Strategy", GetStrategy);
            Mediator.Instance.Register("Camera", CameraMode);
        }
        public void GetStrategy(object list)
        {
            IEnumerable<ITarget> Targets = list as IEnumerable<ITarget>;
            if (Targets.ElementAt(0).x < -12 || Targets.ElementAt(0).x > 12 || Targets.ElementAt(0).y < 0 || Targets.ElementAt(0).y > 48)
            {
                //get list from camera sending the number of targets to camera
            }
            else
            {
                if(Targets.ElementAt(0).spawnRate>0)
                {
                    //run algo for highest posible points or run spawn camping strategy
                }
                else
                {
                    //check for outstanding points else left to right
                    bool outstandin = false;
                    foreach(var target in Targets)
                    {
                        if (target.points > 10000)
                            outstandin = true;
                    }
                    if(outstandin)
                    {
                        //find highest point, use go big or go home strategy
                    }
                    else
                    {
                        //sort list left to right
                    }
                }
            }
           
        }
        private void CameraMode(object value)
        {
            IEnumerable<ITarget> list = value as IEnumerable<ITarget>;
            var sortedList = list.OrderBy(c => c.x);
            foreach (var target in sortedList)
            {
                LauncherViewModel.Instance.FireAt(target.x, target.y - 2, target.z);
            }

        }
        private void bestPoints(IEnumerable<ITarget> list)
        {

        }
    }
}
