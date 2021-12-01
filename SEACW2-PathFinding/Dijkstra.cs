using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SEACW2_PathFinding
{
    public class Dijkstra
    {
        private List<DijkstraNode> _unvisited = new List<DijkstraNode>(); 
        private List<DijkstraNode> _visited = new List<DijkstraNode>();
        
        private Node _startNode, _endNode;

        public Dijkstra(Node startNode, Node endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
            
            foreach (Node node in Program.nodePool.GetNodePool())
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
        }

        public string Algorithm()
        {
            while (_unvisited.Count != 0)
            {
                int distanceToCurrentNode = -1;
                Node currentNode = _startNode;
                
                for (int i = 0; i < _unvisited.Count; i++)
                {
                    int nodeDistance = _unvisited[i]._distanceToNode;
                    
                    if (nodeDistance != -1 && (nodeDistance < distanceToCurrentNode || distanceToCurrentNode == -1))
                    {
                        distanceToCurrentNode = nodeDistance;
                        currentNode = _unvisited[i].GetNode();
                    }
                }

                Dictionary<Node, int> childNodes = currentNode.GetChildNodes();

                for (int i = 0; i < childNodes.Count; i++)
                {
                    Node targetNode = childNodes.ElementAt(i).Key;

                    for (int j = 0; j < _unvisited.Count; j++)
                    {
                        if (_unvisited[j].GetNode() == targetNode)
                        {
                            if (_unvisited[j]._distanceToNode == -1 || 
                                _unvisited[j]._distanceToNode > distanceToCurrentNode + childNodes[targetNode])
                            {
                                _unvisited[j]._distanceToNode = distanceToCurrentNode + childNodes[targetNode];
                                _unvisited[j]._previousNode = currentNode;
                            }
                        }
                    }
                }

                for (int i = 0; i < _unvisited.Count; i++)
                {
                    if (_unvisited[i].GetNode() == currentNode)
                    {
                        _visited.Add(_unvisited[i]);
                        _unvisited.RemoveAt(i);
                    }
                }
            }

            string route = "";
            Node endingNode = _endNode;
            Node previousNode = _endNode;
            
            for (int i = 0; i < _visited.Count; i++)
            {
                if (_visited[i].GetNode() == endingNode)
                {
                    int shortestDistance = _visited[i]._distanceToNode;
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
                        previousNode = _visited[i]._previousNode;

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
    }
}