using System;
using System.Collections.Generic;
using System.IO;

namespace SEACW2_PathFinding
{
    class Program
    {
        public static NodeObjectPool NodePool { get; set; }

        public static string Test = "Test";
        
        static void Main(string[] args)
        {
            //TODO: get the file name from wherever the file is called in console
            FileData fileData = new FileData();
            NodePool = fileData.ReadFile("../../../../ACW2_test_data_01.txt");
            
            Console.WriteLine("BFS Algorithm:");
            Node startNode = NodePool.GetNodePool()[0];
            Node endNode = NodePool.GetNodePool()[11];
            Bfs bfs = new Bfs(startNode, endNode);
            string result = bfs.Algorithm();
            Console.WriteLine(result);
            
            Console.WriteLine("Dijkstra's Algorithm:");
            Dijkstra dijkstra = new Dijkstra(startNode, endNode);
            result = dijkstra.Algorithm();
            Console.WriteLine(result);

            Console.WriteLine("A* Algorithm");
            AStar aStar = new AStar(startNode, endNode);
            result = aStar.Algorithm();
            Console.WriteLine(result);
        }
    }
}