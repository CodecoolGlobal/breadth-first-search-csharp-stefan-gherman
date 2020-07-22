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

        public static HashSet<UserNode> FriendsOfFriendsAtDistance(List<UserNode> graph, UserNode target, int distance)
        {
            HashSet<UserNode> friendsOfFriends = new HashSet<UserNode>();
            if (distance == 0)
            {
                friendsOfFriends.Add(target);
                return friendsOfFriends;
            }

            InitializeVisited(graph);


            Queue<UserNode> searchQueue = new Queue<UserNode>();

            searchQueue.Enqueue(target);
            visitedNodes[target] = true;
            Dictionary<UserNode, int> nodePerLevel = new Dictionary<UserNode, int>();
            nodePerLevel.Add(target, 0);

            while (searchQueue.Count != 0)
            {
                var currentFirst = searchQueue.Dequeue();
                foreach (var friend in currentFirst.Friends)
                {
                    if (visitedNodes[friend] == false)
                    {
                        searchQueue.Enqueue(friend);
                        visitedNodes[friend] = true;
                        nodePerLevel.Add(friend, nodePerLevel[currentFirst] + 1);
                    }

                    if (nodePerLevel[friend] == distance)
                    {
                        friendsOfFriends.Add(friend);
                    }
                }
            }

            return friendsOfFriends;
        }

        public static List<List<UserNode>> GetShortestPath(List<UserNode> graph, UserNode start, UserNode end)
        {
            List<List<UserNode>> allPaths = new List<List<UserNode>>();


            if (start == end)
            {
                return new List<List<UserNode>>() { new List<UserNode>() { start } };
            }

            InitializeVisited(graph);
            Queue<List<UserNode>> searchQueue = new Queue<List<UserNode>>();
            searchQueue.Enqueue(new List<UserNode>() { start });
            Queue<UserNode> searchQueueNodes = new Queue<UserNode>();
            searchQueueNodes.Enqueue(start);
            while (searchQueue.Count != 0)
            {
                var currentPath = searchQueue.Dequeue();
                var lastNode = currentPath[currentPath.Count - 1];

                if (lastNode.Equals(end))
                {
                    allPaths.Add(new List<UserNode>(currentPath));
                }
                else
                {
                    foreach (var friend in lastNode.Friends)
                    {
                        if (!searchQueueNodes.Contains(friend))
                        {
                            List<UserNode> addToPath = new List<UserNode>(currentPath);
                            addToPath.Add(friend);
                            searchQueue.Enqueue(addToPath);
                            if (friend != end)
                                searchQueueNodes.Enqueue(friend);
                        }

                    }
                }
            }
            return allPaths.OrderBy(innerList => innerList.Count).
                Where(innerList => innerList.Count == allPaths[0].Count).ToList();

        }
        public static List<List<UserNode>> GetAllPaths(List<UserNode> graph, UserNode start, UserNode end)
        {
            List<List<UserNode>> allPaths = new List<List<UserNode>>();


            if (start == end)
            {
                return new List<List<UserNode>>() { new List<UserNode>() { start } };
            }

            InitializeVisited(graph);
            Queue<List<UserNode>> searchQueue = new Queue<List<UserNode>>();
            searchQueue.Enqueue(new List<UserNode>() { start });
            Queue<UserNode> searchQueueNodes = new Queue<UserNode>();
            searchQueueNodes.Enqueue(start);
            while (searchQueue.Count != 0)
            {
                var currentPath = searchQueue.Dequeue();
                var lastNode = currentPath[currentPath.Count - 1];

                if (lastNode.Equals(end))
                {
                    allPaths.Add(new List<UserNode>(currentPath));
                }
                else
                {
                    foreach (var friend in lastNode.Friends)
                    {
                        if (!searchQueueNodes.Contains(friend))
                        {
                            List<UserNode> addToPath = new List<UserNode>(currentPath);
                            addToPath.Add(friend);
                            searchQueue.Enqueue(addToPath);
                            if (friend != end)
                                searchQueueNodes.Enqueue(friend);
                        }

                    }
                }
            }
            return allPaths.OrderBy(innerList => innerList.Count).ToList();


        }
    }
}
