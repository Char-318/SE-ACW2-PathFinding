using System;
using System.Collections.Generic;
using System.Linq;

namespace SEACW2_PathFinding
{
    public class Bfs
    {
        private Node _startNode, _endNode;
        private Node _currentNode;
        private BFSNode _currentBfsNode;
        public NodeObjectPool NodePool = Program.NodePool;
        private Queue<BFSNode> _unvisited = new Queue<BFSNode>(); 
        private List<BFSNode> _visited = new List<BFSNode>();

        public Bfs(Node startNode, Node endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
        }

        public string Algorithm()
        {
            foreach (Node node in NodePool.GetNodePool())
            {
                if (node == _startNode)
                {
                    _visited.Add(new BFSNode(node, 0));
                    break;
                }
            }

            do
            {
                _currentBfsNode = _visited.Last();
                _currentNode = _currentBfsNode.GetNode();

                for (int i = 0; i < _currentNode.GetChildNodes().Count; i++)
                {
                    Node childNode = _currentNode.GetChildNodes().ElementAt(i).Key;
                    int distance = _currentNode.GetChildNodes().ElementAt(i).Value;
                    bool inQueue = false;
                    bool isVisited = false;

                    foreach (BFSNode visitedNode in _visited)
                    {
                        if (visitedNode.GetNode() == childNode)
                        {
                            isVisited = true;
                        }
                    }
                    
                    if (!isVisited)
                    {
                        foreach (BFSNode node in _unvisited)
                        {
                            if (node.GetNode() == childNode)
                            {
                                inQueue = true;
                                if (node.DistanceToNode == -1 ||
                                    node.DistanceToNode > distance + _currentBfsNode.DistanceToNode)
                                {
                                    node.DistanceToNode = distance + _currentBfsNode.DistanceToNode;
                                    node.PreviousNode = _currentBfsNode;
                                }
                            }
                        }

                        if (!inQueue)
                        {
                            BFSNode newNode = new BFSNode(childNode, distance + _currentBfsNode.DistanceToNode);
                            newNode.PreviousNode = _currentBfsNode;
                            _unvisited.Enqueue(newNode);
                            
                        }
                    }
                }
                
                BFSNode nextNode = _unvisited.Dequeue();
                _visited.Add(nextNode);
            } 
            while (_unvisited.Count != 0);
            
            bool endNodeReached = false;
            
            foreach (BFSNode node in _visited)
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
            
            string route;
            BFSNode endBfsNode = _currentBfsNode;
            Node previousNode;
            
            foreach (BFSNode node in _visited)
            {
                if (node.GetNode() == _endNode)
                {
                    endBfsNode = node;
                    break;
                }
            }

            int shortestDistance = endBfsNode.DistanceToNode;
            string nodeName = endBfsNode.GetNode().GetName();
            route = nodeName + " " + shortestDistance;

            do
            {
                previousNode = endBfsNode.PreviousNode.GetNode();
                route = route.Insert(0, previousNode.GetName() + "-");
                endBfsNode = endBfsNode.PreviousNode;
            } while (previousNode != _startNode);

            return route;
        }
    }
}