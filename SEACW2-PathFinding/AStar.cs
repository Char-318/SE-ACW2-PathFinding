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

        public string Algorithm()
        {
            AStarNode currentNode = new AStarNode(_startNode, _endNode);
            _open.Add(currentNode);

            while (_open.Count != 0)
            {
                double shortestF = -1; 
                    
                foreach (AStarNode node in _open)
                {
                    if (shortestF == -1 || node.GetF() < shortestF)
                    {
                        shortestF = node.GetF();
                        currentNode = node;
                    }
                }
                
                _closed.Add(currentNode);
                _open.Remove(currentNode);

                if (currentNode.GetCurrentNode() == _endNode)
                {
                    break;
                }

                foreach (Node child in currentNode.GetCurrentNode().GetChildNodes().Keys)
                {
                    bool isClosed = false;
                    
                    foreach (AStarNode closedNode in _closed)
                    {
                        if (child == closedNode.GetCurrentNode())
                        {
                            isClosed = true;
                            break;
                        }
                    }

                    if (!isClosed)
                    {
                        bool isOpen = false;
                        
                        foreach (AStarNode openNode in _open)
                        {
                            if (child == openNode.GetCurrentNode())
                            {
                                isOpen = true;
                                
                                AStarNode newNode = new AStarNode(child, currentNode, _endNode);
                                newNode.CalculateF();

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
                            AStarNode newNode = new AStarNode(child, currentNode, _endNode);
                            newNode.CalculateF();
                            _open.Add(newNode);
                        }
                    }
                }
            }

            return "placeholder"; 
        }
    }
}