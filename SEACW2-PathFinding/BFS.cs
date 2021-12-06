using System;
using System.Collections.Generic;
using System.Linq;

namespace SEACW2_PathFinding
{
    public class Bfs
    {
        private Node _startNode, _endNode;
        private Node _currentNode;
        private BFSNode _currentBfsNode;
        public NodeObjectPool NodePool = Program.NodePool;
        private Queue<BFSNode> _unvisited = new Queue<BFSNode>(); 
        private List<BFSNode> _visited = new List<BFSNode>();

        public Bfs(Node startNode, Node endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
        }

        /// <summary>
        /// Calculates if new path is shorter than the current path to that node.
        /// </summary>
        /// <param name="childNode"> The node that the shortest path is being calculated for. </param>
        /// <param name="newDistance"> The newly calculated distance. </param>
        /// <returns> Returns true if the node is in the unvisited queue, false if not. </returns>
        private bool CompareDistances(Node childNode, int newDistance)
        {
            bool inQueue = false;

            foreach (BFSNode node in _unvisited)
            {
                if (node.GetNode() == childNode)
                {
                    inQueue = true;
                    if (node.DistanceToNode == -1 ||
                        node.DistanceToNode > newDistance + _currentBfsNode.DistanceToNode)
                    {
                        node.DistanceToNode = newDistance + _currentBfsNode.DistanceToNode;
                        node.PreviousNode = _currentBfsNode;
                    }
                }
            }

            return inQueue;
        }

        /// <summary>
        /// Checks if a path was created between the start and end node by checking if the end node is in the
        /// visited list.
        /// </summary>
        /// <returns> Returns true if it is in the list, false if not. </returns>
        private bool IsValidPath()
        {
            bool endNodeReached = false;
            
            foreach (BFSNode node in _visited)
            {
                if (node.GetNode() == _endNode)
                {
                    endNodeReached = true;
                    break;
                }
            }

            return endNodeReached;
        }

        /// <summary>
        /// Backtracks from the end node to the start node using the previous nodes to find which path leads to the
        /// shortest route given by the algorithm.
        /// </summary>
        /// <returns> Returns a string of the path followed by the length that path gives. </returns>
        private string DisplayShortestPath()
        {
            string route;
            BFSNode endBfsNode = _currentBfsNode;
            Node previousNode;
            
            foreach (BFSNode node in _visited)
            {
                if (node.GetNode() == _endNode)
                {
                    endBfsNode = node;
                    break;
                }
            }

            int shortestDistance = endBfsNode.DistanceToNode;
            string nodeName = endBfsNode.GetNode().GetName();
            route = nodeName + " " + shortestDistance;

            do
            {
                previousNode = endBfsNode.PreviousNode.GetNode();
                route = route.Insert(0, previousNode.GetName() + "-");
                endBfsNode = endBfsNode.PreviousNode;
            } while (previousNode != _startNode);

            return route;
        }

        /// <summary>
        /// The main method for calculating the breadth first search. It adds the starting node to the visited list and
        /// that node's children to the unvisited queue. It then calculates the path to each of the children and adds
        /// the first child to the visited list and that node's children to the queue. This repeats until the unvisited
        /// queue is empty.
        /// </summary>
        /// <returns> A string of the path followed by the length that path gives. </returns>
        /// <exception cref="FormatException"> Thrown if there is no path to the end node. </exception>
        public string Algorithm()
        {
            _visited.Add(new BFSNode(_startNode, 0));

            do
            {
                _currentBfsNode = _visited.Last();
                _currentNode = _currentBfsNode.GetNode();

                for (int i = 0; i < _currentNode.GetChildNodes().Count; i++)
                {
                    Node childNode = _currentNode.GetChildNodes().ElementAt(i).Key;
                    int distance = _currentNode.GetChildNodes().ElementAt(i).Value;
                    bool isVisited = false;

                    foreach (BFSNode visitedNode in _visited)
                    {
                        if (visitedNode.GetNode() == childNode)
                        {
                            isVisited = true;
                        }
                    }
                    
                    if (!isVisited)
                    {
                        bool inQueue = CompareDistances(childNode, distance);

                        if (!inQueue)
                        {
                            BFSNode newNode = new BFSNode(childNode, distance + _currentBfsNode.DistanceToNode);
                            newNode.PreviousNode = _currentBfsNode;
                            _unvisited.Enqueue(newNode);
                        }
                    }
                }
                
                BFSNode nextNode = _unvisited.Dequeue();
                _visited.Add(nextNode);
            } 
            while (_unvisited.Count != 0);

            bool endNodeReached = IsValidPath();

            if (!endNodeReached)
            {
                throw new FormatException("No path to target node.");
            }

            string route = DisplayShortestPath();
            return route;
        }
    }
}