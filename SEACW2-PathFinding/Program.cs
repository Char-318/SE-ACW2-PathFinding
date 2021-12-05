using System;
using System.Collections.Generic;
using System.IO;

namespace SEACW2_PathFinding
{
    class Program
    {
        public static NodeObjectPool _nodePool;
        
        static void Main(string[] args)
        {
            //TODO: get the file name from wherever the file is called in console
            ReadFile("../../../../ACW2_test_data_01.txt");
            
            Console.WriteLine("Dijkstra's Algorithm:");
            Node startNode = _nodePool.GetNodePool()[0];
            Node endNode = _nodePool.GetNodePool()[11];
            Dijkstra dijkstra = new Dijkstra(startNode, endNode);
            string result = dijkstra.Algorithm();
            Console.WriteLine(result);

            Console.WriteLine("A* Algorithm");
            AStar aStar = new AStar(startNode, endNode);
            result = aStar.Algorithm();
            Console.WriteLine(result);
        }

        private static Node FormatNodes(string[] nodeInfo)
        {
            if (!int.TryParse(nodeInfo[0], out int id))
            {
                throw new FormatException("File is in invalid format.");
            }
                
            string name = nodeInfo[1].Trim('"');
                
            if (!int.TryParse(nodeInfo[2], out int xCoordinate))
            {
                throw new FormatException("File is in invalid format.");
            }

            if (!int.TryParse(nodeInfo[3], out int yCoordinate))
            {
                throw new FormatException("File is in invalid format.");
            }
                
            Node node = new Node(id, name, xCoordinate, yCoordinate);

            return node;
        }

        private static void FormatEdges(string[] edgeInfo, out Node nodeA, out Node nodeB, out int length)
        {
            if (!int.TryParse(edgeInfo[0], out int nodeAId))
            {
                throw new FormatException("File is in invalid format.");
            }

            if (!int.TryParse(edgeInfo[1], out int nodeBId))
            {
                throw new FormatException("File is in invalid format.");
            }

            if (!int.TryParse(edgeInfo[2], out length))
            {
                throw new FormatException("File is in invalid format.");
            }

            nodeA = null;
            nodeB = null;
                
            foreach (Node node in _nodePool.GetNodePool())
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
        }
        
        private static void ReadFile(string fileName)
        {
            List<Node> nodes = new List<Node>();
            StreamReader reader = new StreamReader(fileName);

            string line = reader.ReadLine();
            if (line != "Nodes")
            {
                throw new FormatException("File is in invalid format.");
            }
            
            line = reader.ReadLine();
            
            while (line != "Edges")
            {
                string[] nodeInfo = line.Split(',');

                Node node = FormatNodes(nodeInfo);
                nodes.Add(node);
                line = reader.ReadLine();
            }
            
            _nodePool = new NodeObjectPool(nodes);

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] edgeInfo = line.Split(',');

                FormatEdges(edgeInfo, out Node nodeA, out Node nodeB, out int length);

                if (nodeA == null || nodeB == null)
                {
                    throw new FormatException("Edge connects to invalid node");
                }
                
                nodeA.AddChildNode(nodeB, length);
                nodeB.AddChildNode(nodeA, length);
            }
        }
    }
}