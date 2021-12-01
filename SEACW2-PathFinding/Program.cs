using System;
using System.Collections.Generic;
using System.IO;

namespace SEACW2_PathFinding
{
    class Program
    {
        public static NodeObjectPool nodePool;
        
        static void Main(string[] args)
        {
            //TODO: get the file name from wherever the file is called in console
            ReadFile("../../../../ACW2_test_data_01.txt");
            
            Console.WriteLine("Dijkstra's Algorithm:");
            Node startNode = nodePool.GetNodePool()[0];
            Node endNode = nodePool.GetNodePool()[11];
            Dijkstra dijkstra = new Dijkstra(startNode, endNode);
            string result = dijkstra.Algorithm();
            Console.WriteLine(result);
        }

        public static void ReadFile(string fileName)
        {
            List<Node> nodes = new List<Node>();
            StreamReader reader = new StreamReader(fileName);

            //TODO: Check first line says Nodes
            reader.ReadLine();
            string line = reader.ReadLine();
            
            while (line != "Edges")
            {
                //TODO: Add error checking 
                string[] nodeInfo = line.Split(',');
                int id = int.Parse(nodeInfo[0]);
                string name = nodeInfo[1].Trim('"');
                int xCoordinate = int.Parse(nodeInfo[2]);
                int yCoordinate = int.Parse(nodeInfo[3]);
                
                Node node = new Node(id, name, xCoordinate, yCoordinate);
                nodes.Add(node);
                line = reader.ReadLine();
            }
            
            nodePool = new NodeObjectPool(nodes);

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] edgeInfo = line.Split(',');
                int nodeAId = int.Parse(edgeInfo[0]);
                int nodeBId = int.Parse(edgeInfo[1]);
                int length = int.Parse(edgeInfo[2]);
                Node nodeA = null, nodeB = null;
                
                foreach (Node node in nodePool.GetNodePool())
                {
                    if (node.GetId() == nodeAId)
                    {
                        nodeA = node;
                    }
                    else if (node.GetId() == nodeBId)
                    {
                        nodeB = node;
                    }
                }
                
                nodeA.AddChildNode(nodeB, length);
                nodeB.AddChildNode(nodeA, length);
            }
        }
    }
}