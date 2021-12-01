using System.Collections.Generic;

namespace SEACW2_PathFinding
{
    public class Node
    {
        private string _name;
        private int _id;
        //TODO: Change to <Node, int>
        private Dictionary<Node, int> _childNodes { get; } = new Dictionary<Node, int>();
        private int _xCoordinate;
        private int _yCoordinate;

        public int GetId()
        {
            return _id;
        }

        public Node(int id, string name, int xCoordinate, int yCoordinate)
        {
            _id = id;
            _name = name;
            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;
        }

        public void AddChildNode(Node childNode, int length)
        {
            _childNodes.Add(childNode, length);
        }
    }
}