using System.Collections.Generic;

namespace SEACW2_PathFinding
{
    public class NodeObjectPool
    {
        private List<Node> _nodes = new List<Node>();
        
        public NodeObjectPool(List<Node> nodes)
        {
            _nodes = nodes;
        }
    }
}