namespace AdvancedProgrammingAss1.DataStructures
{
    /// <summary>
    /// Central repository for the data-structures subsystem. It loads every
    /// request into several advanced data structures at once, each chosen for
    /// the job it does best:
    ///
    ///   • BinarySearchTree  - simple ordered index by id (teaching baseline)
    ///   • RedBlackTree      - balanced ordered index by id for fast tracking
    ///   • AVLTree           - balanced ordered-by-date list for the status view
    ///   • MinHeap           - priority queue so the most urgent request surfaces
    ///   • ServiceRequestGraph - models related requests + BFS/DFS + MST
    ///
    /// Named <c>ServiceRequestRepository</c> to distinguish it from the active
    /// workflow coordinator <see cref="ServiceRequest.ServiceRequestManager"/>.
    /// </summary>
    public static class ServiceRequestRepository
    {
        private static readonly List<ServiceRequest> all = new List<ServiceRequest>();

        private static readonly BinarySearchTree bst = new BinarySearchTree();
        private static readonly RedBlackTree rbTree = new RedBlackTree();
        private static readonly AVLTree avl = new AVLTree();
        private static readonly MinHeap priorityHeap = new MinHeap();
        private static readonly ServiceRequestGraph graph = new ServiceRequestGraph();

        private static int counter = 0;
        private static bool seeded = false;

        public static BinarySearchTree Bst => bst;
        public static RedBlackTree RedBlack => rbTree;
        public static AVLTree Avl => avl;
        public static MinHeap PriorityHeap => priorityHeap;
        public static ServiceRequestGraph Graph => graph;

        public static string GenerateId()
        {
            counter++;
            return $"SR-{DateTime.Now.Year}-{counter:D4}";
        }

        /// <summary>Adds a request to the master list and every data structure.</summary>
        public static void Add(ServiceRequest request)
        {
            all.Add(request);
            bst.Insert(request);
            rbTree.Insert(request);
            avl.Insert(request);
            priorityHeap.Insert(request);

            graph.AddVertex(request.RequestId);
            foreach (var dep in request.DependsOn)
            {
                // weight = difference in priority + 1, a simple "effort" metric
                int weight = Math.Abs(request.Priority - 2) + 1;
                graph.AddEdge(request.RequestId, dep, weight);
            }
        }

        /// <summary>Fast tracking lookup by unique id (uses the Red-Black tree).</summary>
        public static ServiceRequest? Track(string requestId) => rbTree.Search(requestId.Trim());

        public static List<ServiceRequest> GetAll() => all;

        /// <summary>Requests sorted by submission date (AVL in-order).</summary>
        public static List<ServiceRequest> GetByDate() => avl.InOrder();

        /// <summary>Requests sorted by id (Red-Black in-order).</summary>
        public static List<ServiceRequest> GetById() => rbTree.InOrder();

        /// <summary>Requests in priority order (heap drain).</summary>
        public static List<ServiceRequest> GetByPriority() => priorityHeap.ToSortedList();

        public static ServiceRequest? MostUrgent() => priorityHeap.Peek();

        public static int Total => all.Count;

        public static int CountByStatus(string status) =>
            all.Count(r => string.Equals(r.Status, status, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Seeds sample service requests (once), builds a small dependency graph,
        /// and imports any issues the resident already submitted via Report Issues
        /// so the two features are connected.
        /// </summary>
        public static void SeedSampleData()
        {
            if (seeded) return;
            seeded = true;

            Add(new ServiceRequest
            {
                RequestId = GenerateId(),
                Title = "Burst water pipe on Dollar Drive",
                Category = "Water Supply",
                Location = "Meerensee, Richards Bay",
                Department = "Water & Sanitation",
                Description = "Major pipe burst flooding the road and cutting supply to nearby homes.",
                DateSubmitted = DateTime.Today.AddDays(-6),
                Status = "In Progress",
                Priority = 1
            });

            Add(new ServiceRequest
            {
                RequestId = GenerateId(),
                Title = "Road repair after pipe burst",
                Category = "Roads & Potholes",
                Location = "Meerensee, Richards Bay",
                Department = "Roads & Transport",
                Description = "Road surface damaged by the burst pipe needs resurfacing once water is fixed.",
                DateSubmitted = DateTime.Today.AddDays(-5),
                Status = "On Hold",
                Priority = 2,
                DependsOn = { "SR-" + DateTime.Now.Year + "-0001" }
            });

            Add(new ServiceRequest
            {
                RequestId = GenerateId(),
                Title = "Street light outage",
                Category = "Street Lights",
                Location = "Arboretum, Richards Bay",
                Department = "Electricity",
                Description = "Three street lights out along the main road, safety concern at night.",
                DateSubmitted = DateTime.Today.AddDays(-4),
                Status = "Submitted",
                Priority = 3
            });

            Add(new ServiceRequest
            {
                RequestId = GenerateId(),
                Title = "Power fault linked to street lights",
                Category = "Electricity & Power",
                Location = "Arboretum, Richards Bay",
                Department = "Electricity",
                Description = "Suspected feeder fault causing the street light outage in the same area.",
                DateSubmitted = DateTime.Today.AddDays(-3),
                Status = "In Progress",
                Priority = 2,
                DependsOn = { "SR-" + DateTime.Now.Year + "-0003" }
            });

            Add(new ServiceRequest
            {
                RequestId = GenerateId(),
                Title = "Illegal dumping clearance",
                Category = "Illegal Dumping",
                Location = "Brackenham, Richards Bay",
                Department = "Waste Management",
                Description = "Household and garden waste dumped on the verge needs removal.",
                DateSubmitted = DateTime.Today.AddDays(-2),
                Status = "Submitted",
                Priority = 4
            });

            Add(new ServiceRequest
            {
                RequestId = GenerateId(),
                Title = "Blocked storm drain",
                Category = "Sewerage & Drainage",
                Location = "CBD, Empangeni",
                Department = "Water & Sanitation",
                Description = "Storm drain blocked causing pooling water after rain.",
                DateSubmitted = DateTime.Today.AddDays(-1),
                Status = "Resolved",
                DateResolved = DateTime.Today,
                Priority = 3
            });

            // Extra relationship edges to enrich the graph / MST demonstration.
            string y = DateTime.Now.Year.ToString();
            graph.AddEdge($"SR-{y}-0001", $"SR-{y}-0006", 2); // both Water & Sanitation
            graph.AddEdge($"SR-{y}-0004", $"SR-{y}-0005", 4); // nearby suburbs work cluster

            ImportSubmittedIssues();
        }

        /// <summary>
        /// Converts any issues submitted on the Report Issues screen into trackable
        /// service requests, so a resident can follow up on what they reported.
        /// </summary>
        public static void ImportSubmittedIssues()
        {
            foreach (var issue in ReportedIssue.GetAllReports())
            {
                // Skip if already imported (matched by description + location).
                bool exists = all.Any(r =>
                    r.Description == issue.Description && r.Location == issue.Location);
                if (exists) continue;

                int priority = issue.Priority switch
                {
                    "Critical" => 1,
                    "High" => 2,
                    "Medium" => 3,
                    _ => 4
                };

                Add(new ServiceRequest
                {
                    RequestId = GenerateId(),
                    Title = $"{issue.Category} report",
                    Category = issue.Category,
                    Location = issue.Location,
                    Department = DepartmentFor(issue.Category),
                    Description = issue.Description,
                    DateSubmitted = issue.ReportDate,
                    Status = "Submitted",
                    Priority = priority
                });
            }
        }

        private static string DepartmentFor(string category) => category switch
        {
            "Water Supply" or "Sewerage & Drainage" or "Sanitation" => "Water & Sanitation",
            "Roads & Potholes" or "Traffic Signs & Signals" => "Roads & Transport",
            "Electricity & Power" or "Street Lights" => "Electricity",
            "Illegal Dumping" => "Waste Management",
            "Public Safety" or "Noise Complaint" or "Building Violations" => "Public Safety",
            "Parks & Recreation" => "Parks & Recreation",
            _ => "General Enquiries"
        };
    }
}
