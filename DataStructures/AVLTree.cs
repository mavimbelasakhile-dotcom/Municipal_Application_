namespace AdvancedProgrammingAss1.DataStructures
{
    /// <summary>
    /// A self-balancing AVL tree ordered by (DateSubmitted, RequestId). Guarantees
    /// O(log n) height so an in-order traversal yields requests sorted by submission
    /// date in balanced time — ideal for the chronological status list.
    /// </summary>
    public class AVLTree
    {
        private class Node
        {
            public ServiceRequest Value;
            public Node? Left;
            public Node? Right;
            public int Height = 1;
            public Node(ServiceRequest value) => Value = value;
        }

        private Node? root;
        public int Count { get; private set; }

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private int CompareKey(ServiceRequest a, ServiceRequest b)
        {
            int byDate = a.DateSubmitted.CompareTo(b.DateSubmitted);
            if (byDate != 0) return byDate;
            return string.Compare(a.RequestId, b.RequestId, StringComparison.OrdinalIgnoreCase);
        }

        private Node InsertRec(Node? node, ServiceRequest request)
        {
            if (node == null)
            {
                Count++;
                return new Node(request);
            }

            if (CompareKey(request, node.Value) < 0)
                node.Left = InsertRec(node.Left, request);
            else
                node.Right = InsertRec(node.Right, request);

            UpdateHeight(node);
            return Balance(node);
        }

        private int H(Node? n) => n?.Height ?? 0;

        private void UpdateHeight(Node n) => n.Height = 1 + Math.Max(H(n.Left), H(n.Right));

        private int BalanceFactor(Node n) => H(n.Left) - H(n.Right);

        private Node Balance(Node node)
        {
            int bf = BalanceFactor(node);

            // Left heavy
            if (bf > 1)
            {
                if (BalanceFactor(node.Left!) < 0)
                    node.Left = RotateLeft(node.Left!);
                return RotateRight(node);
            }
            // Right heavy
            if (bf < -1)
            {
                if (BalanceFactor(node.Right!) > 0)
                    node.Right = RotateRight(node.Right!);
                return RotateLeft(node);
            }
            return node;
        }

        private Node RotateRight(Node y)
        {
            Node x = y.Left!;
            Node? t2 = x.Right;
            x.Right = y;
            y.Left = t2;
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        private Node RotateLeft(Node x)
        {
            Node y = x.Right!;
            Node? t2 = y.Left;
            y.Left = x;
            x.Right = t2;
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        /// <summary>In-order traversal -> requests sorted by date.</summary>
        public List<ServiceRequest> InOrder()
        {
            var result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(Node? node, List<ServiceRequest> acc)
        {
            if (node == null) return;
            InOrderRec(node.Left, acc);
            acc.Add(node.Value);
            InOrderRec(node.Right, acc);
        }

        public int Height() => H(root);
    }
}
