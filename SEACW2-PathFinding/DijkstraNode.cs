namespace SEACW2_PathFinding
{
    public class DijkstraNode
    {
        private Node _node;
        public int DistanceToNode;
        public Node PreviousNode;

        public DijkstraNode(Node node, int distance)
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