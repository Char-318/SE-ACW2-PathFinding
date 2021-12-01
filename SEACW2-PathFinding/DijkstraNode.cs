namespace SEACW2_PathFinding
{
    public class DijkstraNode
    {
        private Node _node;
        public int _distanceToNode;
        public Node _previousNode;

        public DijkstraNode(Node node, int distance)
        {
            _node = node;
            _distanceToNode = distance;
        }

        public Node GetNode()
        {
            return _node;
        }
    }
}