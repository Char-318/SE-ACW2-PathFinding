using System;
using System.Collections.Generic;

namespace SEACW2_PathFinding
{
    public class Node : ICloneable
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

        public object Clone()
        {
            var nodeClone = new Node(_id, _name, _xCoordinate, _yCoordinate);
            nodeClone._childNodes = _childNodes;

            return nodeClone;
        }
    }
}