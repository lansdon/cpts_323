using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SadLibrary.Targets
{

    public class Target_Manager
    {
        static private Target_Manager instance;

        private Target_Manager() { }

        public List<Target> Target_List = new List<Target>();

        static public Target_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Target_Manager();
                }
                return instance;
            }
        }

        static public bool addTarget(Target newTarget)
        {
            Instance.Target_List.Add(newTarget);
            return true;
        }

        static public bool removeTarget(Target target)
        {
            bool result = false;
            if (Instance.Target_List.Contains(target))
            {
                Instance.Target_List.Remove(target);
                result = true;
            }
            return result;
        }

        static public bool removeTarget(string Name)
        {
            bool result = false;
            foreach (var target in Instance.Target_List)
            {
                if (target.name == Name)
                {
                    Instance.Target_List.Remove(target);
                    result = true;
                    break;
                }
            }
            return result;
        }

        static public List<Target> getEnemies()
        {
            List<Target> EnemyList = new List<Target>();
            foreach (var target in Instance.Target_List)
            {
                if (target.Friend == false)
                    EnemyList.Add(target);
            }
            return EnemyList;
        }

        static public List<Target> getFriends()
        {
            List<Target> FriendList = new List<Target>();
            foreach (var target in Instance.Target_List)
            {
                if (target.Friend == true)
                    FriendList.Add(target);
            }
            return FriendList;
        }

        static public bool setAlive(string Name)
        {
            bool result = false;
            foreach (var target in Instance.Target_List)
            {
                if (target.name == Name)
                {
                    target.Alive = true;
                    result = true;
                }
            }
            return result;
        }

        static public bool setDead(string Name)
        {
            bool result = false;
            foreach (var target in Instance.Target_List)
            {
                if (target.name == Name)
                {
                    target.Alive = false;
                    result = true;
                }
            }
            return result;
        }

        static public bool resetTargets()
        {
            foreach (var target in Instance.Target_List)
            {
                target.Alive = true;
            }
            return true;
        }

        static public Target getTarget(string Name)
        {
            Target Temp = null;
            foreach (var target in Instance.Target_List)
            {
                if (target.name.ToUpper() == Name.ToUpper())
                {
                    Temp = target;
                }
            }
            return Temp;
        }

        static public void addTarget(List<Target> newTargets)
        {
            Instance.Target_List = Instance.Target_List.Union(newTargets).ToList();
        }

        static public void ListAllTargetNames()
        {
            int num = 1;
            foreach (var target in Instance.Target_List)
            {
                Console.WriteLine(num++ + ": " + target.name);
            }
        }

    }
 
}
