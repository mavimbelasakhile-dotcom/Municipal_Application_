using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Shared read-only detail view for a single service request, including its
    /// full status history (audit trail).
    /// </summary>
    public static class RequestDetails
    {
        public static void Show(ReportedIssue r)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Reference:    {r.ReferenceNumber}");
            sb.AppendLine($"Logged by:    {(string.IsNullOrWhiteSpace(r.ReporterName) ? "Unknown" : r.ReporterName)}");
            sb.AppendLine($"Date:         {r.ReportDate:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Category:     {r.Category}");
            sb.AppendLine($"Priority:     {r.Priority}");
            sb.AppendLine($"Location:     {r.Location}");
            sb.AppendLine($"Assigned to:  {(string.IsNullOrWhiteSpace(r.AssignedToName) ? "— unassigned —" : r.AssignedToName)}");
            sb.AppendLine($"Status:       {r.Status}");
            sb.AppendLine();
            sb.AppendLine("Description:");
            sb.AppendLine($"  {r.Description}");
            sb.AppendLine();
            sb.AppendLine("Status History:");
            if (r.History.Count == 0)
            {
                sb.AppendLine("  (none)");
            }
            else
            {
                foreach (var h in r.History)
                {
                    sb.AppendLine($"  [{h.Timestamp:dd/MM HH:mm}] {h.Status} — by {h.ChangedByName}" +
                                  (string.IsNullOrWhiteSpace(h.Note) ? "" : $" ({h.Note})"));
                }
            }

            MessageBox.Show(sb.ToString(), $"Request Details — {r.ReferenceNumber}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
