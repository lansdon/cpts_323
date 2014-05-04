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
            Mediator.Instance.Register("SetCoordListFromCamera", CameraMode);
            Mediator.Instance.Register("start game", startGame);
        }
        private void startGame(object par)
        {
            if (sortedList != null)
            {
                foreach (var target in sortedList)
                {
                    LauncherViewModel.Instance.FireAt(target.x, 4 + target.y, target.z);
                }
            }
            // This will instantly stop the game getting the default 60 points... 
            else {
                Twitterizer.SendTweet("Happy ending stwategy! We quit! +60");

                Mediator.Instance.SendMessage("End Game", 0);
            }
        }

        void theList(object param)
        {
            IEnumerable<Target> list = param as IEnumerable<Target>;
            GetStrategy(list);
            LauncherViewModel.Instance.calibrate();
            Mediator.Instance.SendMessage("Reset Score", 0);
        }
        public void GetStrategy(object list)
        {
            IEnumerable<Target> Targets = list as IEnumerable<Target>;

            if (Targets.ElementAt(0).x < -12 || Targets.ElementAt(0).x > 12 || Targets.ElementAt(0).y < 0 || Targets.ElementAt(0).y > 48)
            {
                //get list from camera sending the number of targets to camera
                Mediator.Instance.SendMessage("DetectTargetsFromCamera", Targets.Count());
            }
            else
            {
                buildTargetListFromSourceList(Targets);
            }
        }

        private void buildTargetListFromSourceList(IEnumerable<Target> Targets)
        {
            // Super Strategy
            /*
                * 1) Shoot from left to right. (minimize the play in launcher movement when it goes back and forth)
                * 2) Count the targets that have respawn time > 5 seconds. They will be shot at once.
                * 3) Count the targets that have respawn time < 5 seconds. They will divide the remaining rapid fire shots.
                * 4) If the total points for all targets in the list don't add to more than 60, stop immediately and get free 60 points. 
                * 
                * Example 3 enemy targets.  1 has respawn time 10.0 seconds. (> 5)  2 are < 5
                * In 60 second game, we will assume 10 shots (given reloads) 
                * 1 shot for Target 1... leaves 9 shots remaining. Those are divided among other two. target 2 gets 5, target three gets 4.
                */ 

            // 1) Sort the list left to right
            Targets.OrderByDescending(c => c.x);

            // 2) Count the targets that have respawn time > 5 seconds. They will be shot at once.
            int targetsWithFastRespawn = 0;
            foreach(var target in Targets)
            {
                if(target.status == 0)
                {
                    if(target.spawnRate > 0.0 && target.spawnRate < 5.0)
                    {
                        ++targetsWithFastRespawn;
                    }
                }
            }

            // 3) Count the targets that have respawn time < 5 seconds. They will divide the remaining rapid fire shots.
            List<Target> finalTargetList = new List<Target>();
            foreach(var target in Targets)
            {
                if(target.status == 0)
                {
                    if (target.spawnRate > 0.0 && target.spawnRate < 5.0)
                    {
                        for(int i = 0; i <=  (10-(Targets.Count() - targetsWithFastRespawn)) / targetsWithFastRespawn; ++i)
                        {
                            finalTargetList.Add(target);
                        }
                    }
                    else
                    {
                        finalTargetList.Add(target);
                    }
                }
            }
            sortedList = finalTargetList;

            // 4)  GIMME MY FREE POINTS STRATEGY = Check if targets can add up to more than 60 points. If not, stop now.
            double totalPossible = 0;
            foreach(var target in finalTargetList)
            {
                totalPossible += target.points;
            }
            if(totalPossible < 60.0)
            {
                sortedList = null;
            }  
        }
        
  
        private void CameraMode(object value)
        {
            IEnumerable<Point3D> list = value as IEnumerable<Point3D>;
            List<Target> Tlist = new List<Target>();
            int i = 0;
            foreach (var position in list)
            {
                Target Tar = new Target();
                Tar.name = "Visually Acquired Target" + ++i;
                Tar.x = position.X;
                Tar.y = position.Y;
                Tar.z = position.Z;
                Tlist.Add(Tar);
            }
            sortedList = Tlist;
            sortedList.OrderBy(c => c.x);
        }


     }
}
