using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using BFS_c_sharp;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();

            Console.WriteLine($"Number of users: {users.Count}");
            foreach (var user in users)
            {
                Console.WriteLine($"{user} has the following friends: ");
                foreach ( var friend in user.Friends)
                {
                    Console.Write($"{friend};");
                }
                Console.WriteLine('\n');
            }
            Console.WriteLine("-------------------------\n");
            foreach ( var user in users)
            {
                Console.WriteLine($"Current user: {user}\n");
                foreach (var targetUser in users)
                {
                    int pathLength = BFS.BFS.FindPath(users, user, targetUser);
                    switch(pathLength)
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

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
