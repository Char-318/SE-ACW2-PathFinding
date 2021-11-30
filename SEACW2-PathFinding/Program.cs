using System;
using System.Collections.Generic;
using System.IO;

namespace SEACW2_PathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile("../../../../ACW2_test_data_01.txt");
        }

        public static List<Node> ReadFile(string fileName)
        {
            List<Node> nodes = new List<Node>();
            StreamReader reader = new StreamReader(fileName);

            reader.ReadLine();
            string line = reader.ReadLine();
            
            while (line != "Edges")
            {
                string[] nodeInfo = line.Split(',');
                int id = int.Parse(nodeInfo[0]);
                string name = nodeInfo[1].Trim('"');
                int xCoordinate = int.Parse(nodeInfo[2]);
                int yCoordinate = int.Parse(nodeInfo[3]);
                
                Node node = new Node(id, name, xCoordinate, yCoordinate);
                nodes.Add(node);
                line = reader.ReadLine();
            }

            return nodes;
        }
    }
}