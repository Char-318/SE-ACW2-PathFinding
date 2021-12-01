using System.Collections.Generic;
using System.Linq;

namespace SEACW2_PathFinding
{
    public class Dijkstra
    {
        private Dictionary<Node, int> _unvisited = new Dictionary<Node, int>();
        private Dictionary<Node, int> _visited = new Dictionary<Node, int>();
        private Node _startNode, _endNode;

        public Dijkstra(Node startNode, Node endNode)
        {
            foreach (Node node in Program.nodePool.GetNodePool())
            {
                _unvisited.Add(node, -1);
            }
            
            _startNode = startNode;
            _endNode = endNode;

            _unvisited[_startNode] = 0;
        }

        public string Algorithm()
        {
            while (_unvisited.Count != 0)
            {
                int distance = -1;
                Node shortestNode = _startNode;
                
                for (int i = 0; i < _unvisited.Count - 1; i++)
                {
                    int nodeDistance = _unvisited.ElementAt(i).Value;
                    
                    if (nodeDistance != -1 && (nodeDistance < distance || distance == -1))
                    {
                        distance = nodeDistance;
                        shortestNode = _unvisited.ElementAt(i).Key;
                    }
                }

                Dictionary<Node, int> childNodes = shortestNode.GetChildNodes();
                
                
            }

            return "Placeholder";
        }
    }
}