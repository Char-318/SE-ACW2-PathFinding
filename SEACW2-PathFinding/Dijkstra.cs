using System.Collections.Generic;

namespace SEACW2_PathFinding
{
    public class Dijkstra
    {
        private Dictionary<Node, int> _unvisited = new Dictionary<Node, int>();
        private Node _startNode, _endNode;

        public Dijkstra(Node startNode, Node endNode)
        {
            foreach (Node node in Program.nodePool.GetNodePool())
            {
                _unvisited.Add(node, -1);
            }

            _startNode = startNode;
            _endNode = endNode;
        }
    }
}