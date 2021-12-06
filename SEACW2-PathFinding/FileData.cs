using System;
using System.Collections.Generic;
using System.IO;

namespace SEACW2_PathFinding
{
    public class FileData
    {
        public NodeObjectPool NodePool = Program.NodePool;
        
        public Node FormatNodes(string[] nodeInfo)
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

        public void FormatEdges(string[] edgeInfo, out Node nodeA, out Node nodeB, out int length)
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
                
            foreach (Node node in NodePool.GetNodePool())
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

        public NodeObjectPool ReadFile(string fileName)
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
            
            NodePool = new NodeObjectPool(nodes);

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

            return NodePool;
        }
    }
}