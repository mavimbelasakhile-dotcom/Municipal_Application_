namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Represents a single municipal service request that a resident can track.
    /// </summary>
    public class ServiceRequest
    {
        public string RequestId { get; set; } = string.Empty;   // unique identifier, e.g. SR-2026-0001
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateResolved { get; set; }
        public string Status { get; set; } = "Submitted"; // Submitted, In Progress, On Hold, Resolved
        public int Priority { get; set; } = 3;             // 1 = Critical ... 4 = Low

        /// <summary>
        /// Other request IDs this request depends on (used to build the dependency graph).
        /// </summary>
        public List<string> DependsOn { get; set; } = new List<string>();

        public string PriorityText => Priority switch
        {
            1 => "Critical",
            2 => "High",
            3 => "Medium",
            _ => "Low"
        };

        public override string ToString()
        {
            return $"{RequestId} - {Title} ({Status})";
        }
    }
}
