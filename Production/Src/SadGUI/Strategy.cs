using SadLibrary;
using SadLibrary.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
﻿using SadLibrary;
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

        public String name;
        public List<Target> targets;
        public double estimatedMaxPoints;
    }

    class Strategy
    {
        private List<Target> sortedList;
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
            GetStrategy(list.Where(c => c.status == 0));
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
                // Super Strategy
                /*
                 * 1) Shoot from left to right. (minimize the play in launcher movement when it goes back and forth)
                 * 2) Count the targets that have respawn time > 5 seconds. They will be shot at once.
                 * 3) Count the targets that have respawn time < 5 seconds. They will divide the remaining rapid fire shots.
                 * 
                 * Example 3 enemy targets.  1 has respawn time 10.0 seconds. (> 5)  2 are < 5
                 * In 60 second game, we will assume 10 shots (given reloads) 
                 * 1 shot for Target 1... leaves 9 shots remaining. Those are divided among other two. target 2 gets 5, target three gets 4.
                 */ 
                //check for outstanding points else left to right

                // 1) Sort the list left to right
                var tempsortedList = Targets.OrderByDescending(c => c.x).Reverse();

                // 2) Count the targets that have respawn time > 5 seconds. They will be shot at once.
                int targetsWithFastRespawn = 0;
                foreach(var target in Targets)
                {
                    if(target.status == 0)
                    {
                        if(target.spawnRate < 5.0)
                        {
                            ++targetsWithFastRespawn;
                        }
                    }
                }

                // 3) Count the targets that have respawn time < 5 seconds. They will divide the remaining rapid fire shots.
                foreach(var target in Targets)
                {
                    if(target.status == 0)
                    {
                        if(target.spawnRate < 5.0)
                        {
                            for(int i = 0; i < targetsWithFastRespawn / (10-(Targets.Count() - targetsWithFastRespawn)); ++i)
                            {
                                sortedList.Add(target);
                            }
                        }
                        else
                        {
                            sortedList.Add(target);
                        }
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
