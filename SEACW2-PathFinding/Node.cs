using System;
using System.Collections.Generic;

namespace SEACW2_PathFinding
{
    public class Node
    {
        private string _name;
        private int _id;
        private Dictionary<Node, int> _childNodes = new Dictionary<Node, int>();
        private int _xCoordinate;
        private int _yCoordinate;

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public Dictionary<Node, int> GetChildNodes()
        {
            return _childNodes;
        }

        public int GetXCoordinate()
        {
            return _xCoordinate;
        }
        
        public int GetYCoordinate()
        {
            return _yCoordinate;
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