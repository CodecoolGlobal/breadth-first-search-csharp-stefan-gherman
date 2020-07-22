using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using BFS_c_sharp;
using System.Linq;
using System.IO;
using BFS_c_sharp.BFS;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();
            PrintUsers(users);
            FindPath(users);
            FriendsAtDistance(users, 2);
            var lastUser = users[users.Count - 1];
            var secondToLast = users[users.Count - 3];
            lastUser.AddFriend(secondToLast);
            users[12].AddFriend(users[14]);
            users[14].FirstName = "Sile";
            users[14].LastName = "Camataru'";
            FindShortestPaths(users);


            AllPathsCyclicGraph();
            Console.WriteLine("Done");

            Console.ReadKey();
        }

        private static void AllPathsCyclicGraph()
        {
            UserNode NodeA = new UserNode("Node", "A");
            UserNode NodeB = new UserNode("Node", "B");
            UserNode NodeC = new UserNode("Node", "C");
            UserNode NodeD = new UserNode("Node", "D");

            NodeA.AddFriend(NodeB);
            NodeA.AddFriend(NodeC);
            NodeB.AddFriend(NodeD);
            NodeB.AddFriend(NodeC);
            NodeC.AddFriend(NodeD);

            List<UserNode> graph = new List<UserNode>() { NodeA, NodeB, NodeC, NodeD };
            foreach (var node in graph)
            {
                foreach (var targetNode in graph)
                {
                    Console.WriteLine($"\nPath(s) from node {node} to {targetNode} is:");
                    var currentPaths = BFS.BFS.GetAllPaths(graph, node, targetNode);

                    foreach (var path in currentPaths)
                    {
                        Console.Write("Path: ");
                        foreach (var pathMemeber in path)
                        {
                            Console.Write($"{pathMemeber}, ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine('\n');
            }
            //var paths = BFS.BFS.GetAllPaths(graph, NodeA, NodeB);
            //Console.WriteLine("DDB");
            //Console.WriteLine(paths);
            //foreach (var path in paths)
            //{
            //    foreach(var pathMember in path)
            //    {
            //        Console.Write($"{pathMember},");
            //    }
            //    Console.WriteLine();
            //}
        }

        private static void FindShortestPaths(List<UserNode> users)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Shortest Path from each node to others\n");
            foreach (var user in users)
            {
                foreach (var targetUser in users)
                {
                    Console.WriteLine($"\nShortest path(s) from node {user} to {targetUser} is:");
                    var currentPaths = BFS.BFS.GetShortestPath(users, user, targetUser);

                    foreach (var path in currentPaths)
                    {
                        Console.Write("Path: ");
                        foreach (var node in path)
                        {
                            Console.Write($"{node}, ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine('\n');
            }
        }

        private static void FriendsAtDistance(List<UserNode> users, int distance)
        {
            Console.WriteLine("-------------------------\n");
            Console.WriteLine("Get Friends at a distance:");
            foreach (var user in users)
            {
                var friendsAtADistance = BFS.BFS.FriendsOfFriendsAtDistance(users, user, distance);
                Console.WriteLine($"Friends of {user} at distance {distance} are:");
                foreach (var friend in friendsAtADistance)
                {
                    Console.WriteLine(friend);
                }
                Console.WriteLine();
            }
        }

        private static void FindPath(List<UserNode> users)
        {
            
            Console.WriteLine("-------------------------\n");
            foreach (var user in users)
            {
                Console.WriteLine($"Current user: {user}\n");
                foreach (var targetUser in users)
                {
                    int pathLength = BFS.BFS.FindPath(users, user, targetUser);
                    switch (pathLength)
                    {
                        case 0:
                            Console.WriteLine($"Path from node {user} to {targetUser} is {pathLength}, same node");
                            break;
                        case -1:
                            Console.WriteLine($"No path between {user} and {targetUser}");
                            break;
                        default:
                            Console.WriteLine($"Path from node {user} to {targetUser} is {pathLength}");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void PrintUsers(List<UserNode> users)
        {
            Console.WriteLine($"Number of users: {users.Count}");
            foreach (var user in users)
            {
                Console.WriteLine($"{user} has the following friends: ");
                foreach (var friend in user.Friends)
                {
                    Console.Write($"{friend};");
                }
                Console.WriteLine('\n');
            }
        }
    }
}
