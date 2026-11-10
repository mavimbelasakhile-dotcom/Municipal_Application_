namespace AdvancedProgrammingAss1.DataStructures
{
    /// <summary>
    /// A Red-Black tree ordered by RequestId. Like the AVL tree it stays balanced,
    /// but uses colour rules (rather than strict height balancing) which makes
    /// insertions cheaper while still guaranteeing O(log n) operations. Used here
    /// to keep an always-sorted index of requests by reference number.
    /// </summary>
    public class RedBlackTree
    {
        private enum Colour { Red, Black }

        private class Node
        {
            public ServiceRequest Value;
            public Colour Colour = Colour.Red;
            public Node? Left, Right, Parent;
            public Node(ServiceRequest value) => Value = value;
        }

        private Node? root;
        public int Count { get; private set; }

        private int Compare(ServiceRequest a, ServiceRequest b) =>
            string.Compare(a.RequestId, b.RequestId, StringComparison.OrdinalIgnoreCase);

        public void Insert(ServiceRequest request)
        {
            var node = new Node(request);
            Node? parent = null;
            Node? current = root;

            while (current != null)
            {
                parent = current;
                int cmp = Compare(request, current.Value);
                if (cmp < 0) current = current.Left;
                else if (cmp > 0) current = current.Right;
                else { current.Value = request; return; } // duplicate -> update
            }

            node.Parent = parent;
            if (parent == null) root = node;
            else if (Compare(request, parent.Value) < 0) parent.Left = node;
            else parent.Right = node;

            Count++;
            FixInsert(node);
        }

        private void FixInsert(Node node)
        {
            while (node.Parent != null && node.Parent.Colour == Colour.Red)
            {
                Node parent = node.Parent;
                Node grandparent = parent.Parent!;

                if (parent == grandparent.Left)
                {
                    Node? uncle = grandparent.Right;
                    if (uncle != null && uncle.Colour == Colour.Red)
                    {
                        parent.Colour = Colour.Black;
                        uncle.Colour = Colour.Black;
                        grandparent.Colour = Colour.Red;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parent.Right)
                        {
                            node = parent;
                            RotateLeft(node);
                            parent = node.Parent!;
                            grandparent = parent.Parent!;
                        }
                        parent.Colour = Colour.Black;
                        grandparent.Colour = Colour.Red;
                        RotateRight(grandparent);
                    }
                }
                else
                {
                    Node? uncle = grandparent.Left;
                    if (uncle != null && uncle.Colour == Colour.Red)
                    {
                        parent.Colour = Colour.Black;
                        uncle.Colour = Colour.Black;
                        grandparent.Colour = Colour.Red;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == parent.Left)
                        {
                            node = parent;
                            RotateRight(node);
                            parent = node.Parent!;
                            grandparent = parent.Parent!;
                        }
                        parent.Colour = Colour.Black;
                        grandparent.Colour = Colour.Red;
                        RotateLeft(grandparent);
                    }
                }
            }
            root!.Colour = Colour.Black;
        }

        private void RotateLeft(Node x)
        {
            Node y = x.Right!;
            x.Right = y.Left;
            if (y.Left != null) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }

        private void RotateRight(Node x)
        {
            Node y = x.Left!;
            x.Left = y.Right;
            if (y.Right != null) y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Right) x.Parent.Right = y;
            else x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }

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

        public int Height() => HeightRec(root);

        private int HeightRec(Node? node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(HeightRec(node.Left), HeightRec(node.Right));
        }
    }
}
