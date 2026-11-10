namespace AdvancedProgrammingAss1.DataStructures
{
    /// <summary>
    /// A weighted, undirected graph that models relationships between service
    /// requests (e.g. two requests in the same area / department that are related
    /// or dependent). Supports BFS and DFS traversal to discover connected work,
    /// and builds a Minimum Spanning Tree (Kruskal's algorithm) to show the
    /// cheapest set of links that keeps all related requests connected — useful
    /// for planning the most efficient route/schedule to resolve a cluster.
    /// </summary>
    public class ServiceRequestGraph
    {
        /// <summary>A single weighted edge between two request ids.</summary>
        public class Edge
        {
            public string From = string.Empty;
            public string To = string.Empty;
            public int Weight;
        }

        // Adjacency list: requestId -> list of (neighbourId, weight)
        private readonly Dictionary<string, List<(string to, int weight)>> adjacency
            = new Dictionary<string, List<(string, int)>>(StringComparer.OrdinalIgnoreCase);

        private readonly List<Edge> edges = new List<Edge>();

        public void AddVertex(string id)
        {
            if (!adjacency.ContainsKey(id))
                adjacency[id] = new List<(string, int)>();
        }

        public void AddEdge(string from, string to, int weight)
        {
            AddVertex(from);
            AddVertex(to);
            adjacency[from].Add((to, weight));
            adjacency[to].Add((from, weight)); // undirected
            edges.Add(new Edge { From = from, To = to, Weight = weight });
        }

        public int VertexCount => adjacency.Count;
        public int EdgeCount => edges.Count;

        /// <summary>
        /// Breadth-First Search from a start request. Returns the order in which
        /// requests are visited (shows how related requests ripple outward).
        /// </summary>
        public List<string> BreadthFirst(string start)
        {
            var order = new List<string>();
            if (!adjacency.ContainsKey(start)) return order;

            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var queue = new Queue<string>();
            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                string node = queue.Dequeue();
                order.Add(node);
                foreach (var (to, _) in adjacency[node].OrderBy(n => n.to))
                {
                    if (!visited.Contains(to))
                    {
                        visited.Add(to);
                        queue.Enqueue(to);
                    }
                }
            }
            return order;
        }

        /// <summary>Depth-First Search from a start request.</summary>
        public List<string> DepthFirst(string start)
        {
            var order = new List<string>();
            if (!adjacency.ContainsKey(start)) return order;

            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DfsRec(start, visited, order);
            return order;
        }

        private void DfsRec(string node, HashSet<string> visited, List<string> order)
        {
            visited.Add(node);
            order.Add(node);
            foreach (var (to, _) in adjacency[node].OrderBy(n => n.to))
            {
                if (!visited.Contains(to))
                    DfsRec(to, visited, order);
            }
        }

        /// <summary>
        /// Builds a Minimum Spanning Tree using Kruskal's algorithm with a
        /// union-find (disjoint set) structure. Returns the chosen edges and the
        /// total weight — the least-cost way to connect all related requests.
        /// </summary>
        public (List<Edge> treeEdges, int totalWeight) MinimumSpanningTree()
        {
            var result = new List<Edge>();
            int total = 0;

            // Union-Find initialisation
            var parent = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var v in adjacency.Keys) parent[v] = v;

            string Find(string x)
            {
                while (parent[x] != x)
                {
                    parent[x] = parent[parent[x]]; // path compression
                    x = parent[x];
                }
                return x;
            }

            bool Union(string a, string b)
            {
                string ra = Find(a), rb = Find(b);
                if (ra == rb) return false;
                parent[ra] = rb;
                return true;
            }

            foreach (var edge in edges.OrderBy(e => e.Weight))
            {
                if (Union(edge.From, edge.To))
                {
                    result.Add(edge);
                    total += edge.Weight;
                }
            }
            return (result, total);
        }

        public List<string> Vertices => adjacency.Keys.OrderBy(k => k).ToList();
    }
}
