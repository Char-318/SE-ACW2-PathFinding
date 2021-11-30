using System.Collections.Generic;

namespace SEACW2_PathFinding
{
    public class Node
    {
        private string _name;
        private int _id;
        private Dictionary<int, int> _childNodes { get; } = new Dictionary<int, int>();
        private int _xCoordinate;
        private int _yCoordinate;

        public Node(int id, string name, int xCoordinate, int yCoordinate)
        {
            _id = id;
            _name = name;
            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;
        }

        public void AddChildNode(int childId, int length)
        {
            _childNodes.Add(childId, length);
        }
    }
}