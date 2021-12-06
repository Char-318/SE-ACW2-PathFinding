using System;
using System.Collections.Generic;
using System.IO;

namespace SEACW2_PathFinding
{
    public class FileData
    {
        public NodeObjectPool NodePool = Program.NodePool;
        
        /// <summary>
        /// Checks that each line in the input file under Nodes is in the correct format. Must be int, string, int, int.
        /// </summary>
        /// <param name="nodeInfo"> A list of each item in the line separated by ','. </param>
        /// <returns> The node created from the information given. </returns>
        /// <exception cref="FormatException"> Thrown when the line is not in the correct format. </exception>
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

        /// <summary>
        /// Checks that each line in the input file under Edges is in the correct format, should be an integer array.
        /// </summary>
        /// <param name="edgeInfo"> A list of each item in the line, separated by ','. </param>
        /// <param name="nodeA"> One of the nodes the edge is connected to. </param>
        /// <param name="nodeB"> The other node the edge is connected to. </param>
        /// <param name="length"> The length of the edge. </param>
        /// <exception cref="FormatException"> Thrown when the line is not in the correct format. </exception>
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

        /// <summary>
        /// Reads the input file and creates nodes from the information provided.
        /// </summary>
        /// <param name="fileName"> The name of the input file. </param>
        /// <returns> An object pool of each node in the graph. </returns>
        /// <exception cref="FormatException"> Thrown if the file is not structured correctly. </exception>
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

        /// <summary>
        /// Writes the result of the algorithm to a file and if the user specifies, to the console as well.
        /// </summary>
        /// <param name="outputFile"> The file the algorithm should be output into. </param>
        /// <param name="result"> The string of the shortest path followed by the length of it. </param>
        /// <param name="outputToConsole"> True if the result should be output to the console as well. </param>
        public void WriteToFile(string outputFile, string result, bool outputToConsole)
        {
            StreamWriter writer = new StreamWriter(outputFile);
            writer.WriteLine(result);
            writer.Close();
                
            if (outputToConsole)
            {
                Console.WriteLine(result);
            }
        }
    }
}