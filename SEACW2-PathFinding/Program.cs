using System;
using System.IO;

namespace SEACW2_PathFinding
{
    class Program
    {
        public static NodeObjectPool NodePool { get; set; }
        private static string startingNode = null, endingNode = null, algorithm = null, inputFile = null, outputFile = null;
        private static string startingNodeName = null, endingNodeName = null;
        private static int startingNodeId = -1, endingNodeId = -1;
        private static bool outputToConsole = false;
        private static Node startNode = null, endNode = null;
        
        static void Main(string[] args)
        {
            if (args.Length != 10)
            {
                throw new ArgumentException("Incorrect amount of arguments passed");
            }
            
            ValidateArgs(args);

            if (startingNode == null || endingNode == null || algorithm == null 
                || inputFile == null || outputFile == null)
            {
                throw new ArgumentException("Incorrect arguments passed");
            }
            
            FileData fileData = new FileData();
            NodePool = fileData.ReadFile(inputFile);
            SetStartAndEndNodes();

            if (startNode == null || endNode == null)
            {
                throw new ArgumentException("Start or end node is invalid");
            }
            
            if (algorithm == "BF")
            {
                Bfs bfs = new Bfs(startNode, endNode);
                string result = bfs.Algorithm();
                fileData.WriteToFile(outputFile, result, outputToConsole);
            }
            else if (algorithm == "DIJKSTRA")
            {
                Dijkstra dijkstra = new Dijkstra(startNode, endNode);
                string result = dijkstra.Algorithm();
                fileData.WriteToFile(outputFile, result, outputToConsole);
            }
            else
            {
                AStar aStar = new AStar(startNode, endNode);
                string result = aStar.Algorithm();
                fileData.WriteToFile(outputFile, result, outputToConsole);
            }
        }
        
        /// <summary>
        /// Makes sure the arguments provided through the console are valid.
        /// </summary>
        /// <param name="args"> Arguments obtained from the console. </param>
        /// <exception cref="ArgumentException">
        /// Throws an ArgumentException if any of the arguments passed are not in the correct format.
        /// </exception>
        public static void ValidateArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-s")
                {
                    startingNode = args[i + 1];
                    
                    if (startingNode.StartsWith('"') && startingNode.EndsWith('"'))
                    {
                        startingNodeName = args[i + 1].Trim('"');
                    }
                    else
                    {
                        if (!int.TryParse(args[i + 1], out startingNodeId))
                        {
                            throw new ArgumentException("Invalid starting node id");
                        }
                    }
                }
                
                else if (args[i] == "-e")
                {
                    endingNode = args[i + 1];
                    
                    if (endingNode.StartsWith('"') && endingNode.EndsWith('"'))
                    {
                        endingNodeName = args[i + 1].Trim('"');
                    }
                    else
                    {
                        if (!int.TryParse(args[i + 1], out endingNodeId))
                        {
                            throw new ArgumentException("Invalid ending node id");
                        }
                    }
                }

                else if (args[i] == "-a")
                {
                    algorithm = args[i + 1];
                    
                    if (algorithm != "BF" && algorithm != "DIJKSTRA" && algorithm != "ASTAR")
                    {
                        throw new ArgumentException("Invalid algorithm");
                    }
                } 
                
                else if (args[i] == "-f")
                {
                    inputFile = args[i + 1];

                    try
                    {
                        new StreamReader(inputFile);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Invalid input filename");
                    }
                }
                
                else if (args[i] == "-o" || args[i] == "-O")
                {
                    if (args[i] == "-O")
                    {
                        outputToConsole = true;
                    }

                    outputFile = args[i + 1];
                    
                    try
                    {
                        new StreamWriter(outputFile);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Invalid output filename");
                    }
                }
            }
        }

        /// <summary>
        /// Sets the start node and end node for the algorithms. Allows for if the user enters the id or the name of
        /// the node.
        /// </summary>
        public static void SetStartAndEndNodes()
        {
            foreach (Node node in NodePool.GetNodePool())
            {
                if (startingNodeName != null)
                {
                    if (node.GetName() == startingNodeName)
                    {
                        startNode = node;
                        break;
                    }
                }
                else
                {
                    if (node.GetId() == startingNodeId)
                    {
                        startNode = node;
                        break;
                    }
                }
            }

            foreach (Node node in NodePool.GetNodePool())
            {
                if (endingNodeName != null)
                {
                    if (node.GetName() == endingNodeName)
                    {
                        endNode = node;
                        break;
                    }
                }
                else
                {
                    if (node.GetId() == endingNodeId)
                    {
                        endNode = node;
                        break;
                    }
                }
            }
        }
    }
}