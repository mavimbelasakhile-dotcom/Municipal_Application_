namespace AdvancedProgrammingAss1.DataStructures
{
    /// <summary>
    /// A classic (unbalanced) Binary Search Tree keyed by a request's unique
    /// identifier (string). Demonstrates basic tree + binary tree + BST concepts
    /// and provides O(log n) average-case lookup for tracking requests by ID.
    /// </summary>
    public class BinarySearchTree
    {
        private class Node
        {
            public ServiceRequest Value;
            public Node? Left;
            public Node? Right;
            public Node(ServiceRequest value) => Value = value;
        }

        private Node? root;
        public int Count { get; private set; }

        /// <summary>Inserts a request keyed by its RequestId.</summary>
        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private Node InsertRec(Node? node, ServiceRequest request)
        {
            if (node == null)
            {
                Count++;
                return new Node(request);
            }

            int cmp = string.Compare(request.RequestId, node.Value.RequestId,
                StringComparison.OrdinalIgnoreCase);

            if (cmp < 0)
                node.Left = InsertRec(node.Left, request);
            else if (cmp > 0)
                node.Right = InsertRec(node.Right, request);
            else
                node.Value = request; // duplicate id -> update

            return node;
        }

        /// <summary>Finds a request by its unique id, or null if not present.</summary>
        public ServiceRequest? Search(string requestId)
        {
            Node? current = root;
            while (current != null)
            {
                int cmp = string.Compare(requestId, current.Value.RequestId,
                    StringComparison.OrdinalIgnoreCase);
                if (cmp == 0) return current.Value;
                current = cmp < 0 ? current.Left : current.Right;
            }
            return null;
        }

        /// <summary>Returns all requests via in-order traversal (sorted by id).</summary>
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

        /// <summary>Height of the tree (used to show it is unbalanced vs AVL).</summary>
        public int Height() => HeightRec(root);

        private int HeightRec(Node? node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(HeightRec(node.Left), HeightRec(node.Right));
        }
    }
}
