namespace SEACW2_PathFinding
{
    public class BFSNode
    {
        private Node _node;
        public int DistanceToNode;
        public BFSNode PreviousNode;

        public BFSNode(Node node, int distance)
        {
            _node = node;
            DistanceToNode = distance;
        }
        
        public Node GetNode()
        {
            return _node;
        }
    }
}