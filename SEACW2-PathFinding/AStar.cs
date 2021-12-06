using System;
using System.Collections.Generic;
using System.Linq;

namespace SEACW2_PathFinding
{
    public class AStar
    {
        private Node _startNode;
        private Node _endNode;
        private List<AStarNode> _open = new List<AStarNode>();
        private List<AStarNode> _closed = new List<AStarNode>();
        private AStarNode _currentNode;

        public AStar(Node startNode, Node endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
        }
        
        public Node GetStartNode()
        {
            return _startNode;
        }

        public Node GetEndNode()
        {
            return _endNode;
        }

        /// <summary>
        /// Calculates which node in the open list has the smallest value of f and sets the current node to that.
        /// </summary>
        private void FindShortestF()
        {
            double shortestF = -1; 
                    
            foreach (AStarNode node in _open)
            {
                if (shortestF == -1 || node.GetF() < shortestF)
                {
                    shortestF = node.GetF();
                    _currentNode = node;
                }
            }
        }

        /// <summary>
        /// Creates a new AStarNode and calculates the value of f. Used when a new node might need to be added to
        /// the open list.
        /// </summary>
        /// <param name="child"> The node that the new AStarNode will refer to. </param>
        /// <returns> The new AStarNode that was created. </returns>
        private AStarNode CreateNewNode(Node child)
        {
            AStarNode newNode = new AStarNode(child, _currentNode, _endNode);
            newNode.CalculateF();

            return newNode;
        }

        /// <summary>
        /// Adds a new node to the open list if the node isn't already there and replaces one if a new shortest path
        /// to it is found.
        /// </summary>
        /// <param name="child"> The node that needs to be found in the open list. </param>
        private void AlterOpenList(Node child)
        {
            bool isOpen = false;
                        
            foreach (AStarNode openNode in _open)
            {
                if (child == openNode.GetNode())
                {
                    isOpen = true;
                    AStarNode newNode = CreateNewNode(child);

                    if (newNode.GetG() < openNode.GetG())
                    {
                        _open.Remove(openNode);
                        _open.Add(newNode);
                    }
                                
                    break;
                }
            }

            if (!isOpen)
            {
                AStarNode newNode = CreateNewNode(child);
                _open.Add(newNode);
            }
        }

        /// <summary>
        /// Backtracks through the nodes previous nodes to find the shortest path to the end node.
        /// </summary>
        /// <returns> Returns a string of the shortest path followed by the length of the path. </returns>
        private string DisplayShortestPath()
        {
            string route;
            AStarNode endingAStarNode = _closed.Last();
            Node previousNode;
            
            int shortestDistance = endingAStarNode.GetG();
            string nodeName = endingAStarNode.GetNode().GetName();
            route = nodeName + " " + shortestDistance;

            do
            {
                previousNode = endingAStarNode.GetPreviousNode().GetNode();
                route = route.Insert(0, previousNode.GetName() + "-");
                endingAStarNode = endingAStarNode.GetPreviousNode();
            } while (previousNode != _startNode);

            return route;
        }

        /// <summary>
        /// Main method for the A* algorithm. Finds the node in the open list with the smallest f value and adds it to
        /// the closed list. The shortest f values of the child nodes from this node are calculated. This is run until
        /// the end node is added to the closed list or it reaches the end of the open list.    
        /// </summary>
        /// <returns> Returns a string of the shortest path followed by the length of the path. </returns>
        /// <exception cref="FormatException"> Throws this exception if there is no valid path to the end node. </exception>
        public string Algorithm()
        {
            _currentNode = new AStarNode(_startNode, _endNode);
            _open.Add(_currentNode);

            while (_open.Count != 0)
            {
                FindShortestF();
                _closed.Add(_currentNode);
                _open.Remove(_currentNode);

                if (_currentNode.GetNode() == _endNode)
                {
                    break;
                }

                foreach (Node child in _currentNode.GetNode().GetChildNodes().Keys)
                {
                    bool isClosed = false;
                    
                    foreach (AStarNode closedNode in _closed)
                    {
                        if (child == closedNode.GetNode())
                        {
                            isClosed = true;
                            break;
                        }
                    }

                    if (!isClosed)
                    {
                        AlterOpenList(child);
                    }
                }
            }

            bool endNodeReached = false;
            
            foreach (AStarNode node in _closed)
            {
                if (node.GetNode() == _endNode)
                {
                    endNodeReached = true;
                    break;
                }
            }

            if (!endNodeReached)
            {
                throw new FormatException("No path to target node.");
            }

            string route = DisplayShortestPath();
            return route; 
        }
    }
}