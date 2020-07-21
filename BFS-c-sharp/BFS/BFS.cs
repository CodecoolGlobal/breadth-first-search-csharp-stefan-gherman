using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.BFS
{
    public class BFS
    {
        public static Dictionary<UserNode, bool> visitedNodes;
        public static void InitializeVisited(List<UserNode> graph)
        {
            visitedNodes = new Dictionary<UserNode, bool>();
            foreach (var obj in graph)
            {
                visitedNodes.Add(obj, false);
            }
        }
        public static int FindPath(List<UserNode> graph, UserNode start, UserNode end)
        {
            if (start.Equals(end))
            {
                return 0;
            }
            InitializeVisited(graph);

            Queue<UserNode> searchQueue = new Queue<UserNode>();
            searchQueue.Enqueue(start);
            visitedNodes[start] = true;
            Dictionary<UserNode, int> nodePerLevel = new Dictionary<UserNode, int>();
            nodePerLevel.Add(start, 0);
            while (searchQueue.Count != 0)
            {
                var currentFirst = searchQueue.Dequeue();
               

                foreach (var friend in currentFirst.Friends)
                {
                    
                    if (friend.Equals(end))
                    {
                        return nodePerLevel[currentFirst] + 1;
                    }
                    else
                    {
                        if (visitedNodes[friend] == false)
                        {
                            searchQueue.Enqueue(friend);
                            visitedNodes[friend] = true;
                            nodePerLevel.Add(friend, nodePerLevel[currentFirst] + 1);

                        }

                    }
                }
                
                
            }

            return -1;
        }
    }
}
