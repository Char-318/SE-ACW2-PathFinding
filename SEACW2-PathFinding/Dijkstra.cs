using System;
using System.Collections.Generic;
using System.Linq;

namespace SEACW2_PathFinding
{
    public class Dijkstra
    {
        private List<DijkstraNode> _unvisited = new List<DijkstraNode>(); 
        private List<DijkstraNode> _visited = new List<DijkstraNode>();
        private Node _startNode, _endNode;
        private Node _currentNode;
        public NodeObjectPool NodePool = Program.NodePool;
        
        public Dijkstra(Node startNode, Node endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
        }

        /// <summary>
        /// Finds the node with the current shortest distance in the unvisited list. 
        /// </summary>
        /// <returns> The distance to the node that has the shortest distance at this point. </returns>
        private int FindShortestDistance()
        {
            int distanceToCurrentNode = -1;
                
            for (int i = 0; i < _unvisited.Count; i++)
            {
                int nodeDistance = _unvisited[i].DistanceToNode;
                    
                if (nodeDistance != -1 && (nodeDistance < distanceToCurrentNode || distanceToCurrentNode == -1))
                {
                    distanceToCurrentNode = nodeDistance;
                    _currentNode = _unvisited[i].GetNode();
                }
            }

            return distanceToCurrentNode;
        }

        /// <summary>
        /// Adds the current node to the visited list.
        /// </summary>
        private void AddToVisited()
        {
            for (int i = 0; i < _unvisited.Count; i++)
            {
                if (_unvisited[i].GetNode() == _currentNode)
                {
                    _visited.Add(_unvisited[i]);
                    _unvisited.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Backtracks through each node to work out what the shortest path is using the previous nodes and turns that
        /// into a string. 
        /// </summary>
        /// <returns> A string of the shortest path and the length of that. </returns>
        private string DisplayShortestPath()
        {
            string route = "";
            Node endingNode = _endNode;
            Node previousNode = _endNode;
            
            for (int i = 0; i < _visited.Count; i++)
            {
                if (_visited[i].GetNode() == endingNode)
                {
                    int shortestDistance = _visited[i].DistanceToNode;
                    string nodeName = _visited[i].GetNode().GetName();
                    route = nodeName + " " + shortestDistance;
                    break;
                }
            }
            
            while (previousNode != null)
            {
                for (int i = 0; i < _visited.Count; i++)
                {
                    if (_visited[i].GetNode() == endingNode)
                    {
                        previousNode = _visited[i].PreviousNode;

                        for (int j = 0; j < _visited.Count; j++)
                        {
                            if (_visited[j].GetNode() == previousNode)
                            {
                                string prevNodeName = _visited[j].GetNode().GetName();
                                route = route.Insert(0, prevNodeName + "-");
                                endingNode = previousNode;
                            } 
                        }
                    }
                }
            }

            return route;
        }

        /// <summary>
        /// The main method for Dijkstra's algorithm. Adds all nodes to the unvisited list with the starting node
        /// having a distance of 0. Every cycle of the algorithm will check which node in the unvisited list has the
        /// shortest distance to it, add it to the visited list, then works out the distances to the child nodes from
        /// that node and if this is a shorter distance, it replaces the distance to that node and sets the previous
        /// node to the current node. This is repeated until the unvisited list is empty.
        /// </summary>
        /// <returns> A string of the shortest path followed by the length of it. </returns>
        /// <exception cref="FormatException"> Thrown when there is no path between the end and start node. </exception>
        public string Algorithm()
        {
            foreach (Node node in NodePool.GetNodePool())
            {
                if (node == _startNode)
                {
                    _unvisited.Add(new DijkstraNode(node, 0));
                }
                else
                {
                    _unvisited.Add(new DijkstraNode(node, -1));
                }
            }
            
            while (_unvisited.Count != 0)
            {
                int distanceToCurrentNode = FindShortestDistance();

                if (distanceToCurrentNode == -1)
                {
                    throw new FormatException("No path to target node.");
                }
                
                Dictionary<Node, int> childNodes = _currentNode.GetChildNodes();

                for (int i = 0; i < childNodes.Count; i++)
                {
                    Node targetNode = childNodes.ElementAt(i).Key;

                    for (int j = 0; j < _unvisited.Count; j++)
                    {
                        if (_unvisited[j].GetNode() == targetNode && (_unvisited[j].DistanceToNode == -1 || 
                            _unvisited[j].DistanceToNode > distanceToCurrentNode + childNodes[targetNode]))
                        {
                            _unvisited[j].DistanceToNode = distanceToCurrentNode + childNodes[targetNode];
                            _unvisited[j].PreviousNode = _currentNode;
                        }
                    }
                }

                AddToVisited();
            }

            string route = DisplayShortestPath();
            return route;
        }
    }
}