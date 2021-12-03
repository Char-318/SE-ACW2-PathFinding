using System;
using System.Linq;

namespace SEACW2_PathFinding
{
    public class AStarNode
    {
        private Node _currentNode;
        private AStarNode _previousAStarNode;
        private Node _endNode;
        private double _f = 0;
        private int _g = 0;
        private double _h = 0;

        public AStarNode(Node currentNode, AStarNode previousNode, Node endNode)
        {
            _currentNode = currentNode;
            _previousAStarNode = previousNode;
            _endNode = endNode;
        }

        public AStarNode(Node currentNode, Node endNode)
        {
            _currentNode = currentNode;
            _endNode = endNode;
        }

        public double GetF()
        {
            return _f;
        }

        public int GetG()
        {
            return _g;
        }

        public Node GetCurrentNode()
        {
            return _currentNode;
        }
        
        public void CalculateF()
        {
            Node previousNode = _previousAStarNode._currentNode;
            
            for (int i = 0; i < previousNode.GetChildNodes().Count; i++)
            {
                if (previousNode.GetChildNodes().ElementAt(i).Key == _currentNode)
                {
                    _g = previousNode.GetChildNodes().ElementAt(i).Value + _previousAStarNode.GetG();
                    break;
                }
            }

            int xCurrent = _currentNode.GetXCoordinate();
            int yCurrent = _currentNode.GetYCoordinate();
            int xEnd = _endNode.GetXCoordinate();
            int yEnd = _endNode.GetYCoordinate();

            int xDiff = xEnd - xCurrent;
            int yDiff = yEnd - yCurrent;

            _h = Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2);

            _f = _g + _h;
        }
    }
}