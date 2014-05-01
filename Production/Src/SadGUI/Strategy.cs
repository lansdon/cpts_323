using SadLibrary;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SadGUI
{

    public class StrategyResult 
    {
        public StrategyResult(String sName)
        {
            name = sName;
            estimatedMaxPoints = 0;
        }

        String name;
        public List<Target> targets;
        public double estimatedMaxPoints;
    }

    class Strategy
    {
        private IEnumerable<Target> sortedList;
        public Strategy()
        {
            Mediator.Instance.Register("TargetsList", theList);
            Mediator.Instance.Register("Run Strategy", GetStrategy);
            Mediator.Instance.Register("Camera", CameraMode);
            Mediator.Instance.Register("start game", startGame);
        }
        private void startGame(object par)
        {
            foreach(var target in sortedList)
            {
                LauncherViewModel.Instance.FireAt(target.x, 4 + target.y, target.z);
            }
        }
        void theList(object param)
        {
            IEnumerable<Target> list = param as IEnumerable<Target>;
            GetStrategy(list);
        }
        public void GetStrategy(object list)
        {
            IEnumerable<Target> Targets = list as IEnumerable<Target>;
            if (Targets.ElementAt(0).x < -12 || Targets.ElementAt(0).x > 12 || Targets.ElementAt(0).y < 0 || Targets.ElementAt(0).y > 48)
            {
                //get list from camera sending the number of targets to camera
                Mediator.Instance.SendMessage("GetNumberOfTargets", Targets.Count());
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
                        GoBigOrGoHome(Targets);
                    }
                    else
                    {
                        //sort list left to right
                    }
                }
            }
           
        }
       private StrategyResult GoBigOrGoHome(IEnumerable<Target> list)
        {
            var tempsortedList = list.OrderByDescending(c => c.points);
           StrategyResult strat = new StrategyResult("Go Big or Go Home");
		foreach(var temp in tempsortedList)
		{
			Target t = new Target();
			if(temp.spawnRate < 6)
			{
				t= temp;
				strat.targets.Add(t);
                strat.estimatedMaxPoints += t.points;
                strat.targets.Add(t);
                strat.estimatedMaxPoints += t.points;
				
			}
            else
            {
                t= temp;
                strat.targets.Add(t);
                strat.estimatedMaxPoints += t.points;
            }
		}
           return strat;

        }
        private void CameraMode(object value)
        {
            IEnumerable<Point3D> list = value as IEnumerable<Point3D>;
            List<Target> Tlist = new List<Target>();
            foreach (var position in list)
            {
                Target Tar = new Target();
                Tar.x = position.X;
                Tar.y = position.Y;
                Tar.z = position.Z;
                Tlist.Add(Tar);
            }
            sortedList = Tlist.OrderBy(c => c.x);
        }
        private void bestPoints(IEnumerable<Target> list)
        {
            var TempsortedList = list.OrderBy(c => c.spawnRate);
        }


    }
}
