namespace AdvancedProgrammingAss1.ServiceRequest
{
    /// <summary>
    /// One entry in a request's audit trail — who changed it, to what status, when.
    /// </summary>
    public class StatusHistoryEntry
    {
        public string Status { get; set; } = string.Empty;
        public string ChangedByName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Note { get; set; } = string.Empty;
    }

    /// <summary>
    /// Coordinates the service-request lifecycle: assignment, status transitions,
    /// and the notifications that fire to the reporter and assignee at each step.
    ///
    /// Lifecycle:  Submitted -> Assigned -> In Progress -> Resolved -> Closed
    /// (On Hold can be set from Assigned / In Progress, then resumed.)
    /// </summary>
    public static class ServiceRequestManager
    {
        public const string StatusSubmitted = "Submitted";
        public const string StatusAssigned = "Assigned";
        public const string StatusInProgress = "In Progress";
        public const string StatusOnHold = "On Hold";
        public const string StatusResolved = "Resolved";
        public const string StatusClosed = "Closed";
        public const string StatusRejected = "Rejected";

        /// <summary>All statuses in lifecycle order (used for display / colours).</summary>
        public static readonly string[] AllStatuses =
        {
            StatusSubmitted, StatusAssigned, StatusInProgress, StatusOnHold, StatusResolved, StatusClosed, StatusRejected
        };

        /// <summary>
        /// Returns the valid next statuses a staff member may move a request into,
        /// based on where it currently is in the lifecycle.
        /// </summary>
        public static List<string> GetNextStatuses(string currentStatus)
        {
            switch (currentStatus)
            {
                case StatusAssigned:
                    return new List<string> { StatusInProgress, StatusOnHold };
                case StatusInProgress:
                    return new List<string> { StatusResolved, StatusOnHold };
                case StatusOnHold:
                    return new List<string> { StatusInProgress };
                case StatusResolved:
                    return new List<string> { StatusClosed };
                default:
                    return new List<string>();
            }
        }

        /// <summary>
        /// Administrator assigns (or re-assigns) a request to a staff member.
        /// Moves status to "Assigned" and alerts both the assignee and the reporter.
        /// </summary>
        public static void AssignRequest(ReportedIssue request, UserAccount staff, UserAccount admin)
        {
            request.AssignedToUsername = staff.Username;
            request.AssignedToName = staff.DisplayName;
            request.Status = StatusAssigned;

            AddHistory(request, StatusAssigned, admin.DisplayName,
                $"Assigned to {staff.DisplayName} ({staff.Department})");

            // Alert the assignee — this is what they'll see on login.
            NotificationService.Notify(staff.Username, request.ReferenceNumber,
                $"A new service request ({request.ReferenceNumber}) has been assigned to you: " +
                $"{request.Category} at {request.Location}.");

            // Keep the reporter informed too.
            if (!string.IsNullOrWhiteSpace(request.ReporterUsername))
            {
                NotificationService.Notify(request.ReporterUsername, request.ReferenceNumber,
                    $"Your request {request.ReferenceNumber} has been assigned to {staff.DisplayName} " +
                    $"({staff.Department}) and is now being handled.");
            }
        }

        /// <summary>
        /// Staff member advances the request to a new status.
        /// Alerts the reporter about the change, and re-alerts the assignee for record.
        /// </summary>
        public static void ChangeStatus(ReportedIssue request, string newStatus, UserAccount changedBy, string note = "")
        {
            request.Status = newStatus;
            AddHistory(request, newStatus, changedBy.DisplayName, note);

            // Always keep the reporter in the loop on any status movement.
            if (!string.IsNullOrWhiteSpace(request.ReporterUsername))
            {
                string friendly = StatusMessageForReporter(newStatus, request.ReferenceNumber);
                NotificationService.Notify(request.ReporterUsername, request.ReferenceNumber, friendly);
            }
        }

        private static string StatusMessageForReporter(string status, string reference)
        {
            switch (status)
            {
                case StatusInProgress:
                    return $"Good news — work has started on your request {reference}. Status: In Progress.";
                case StatusOnHold:
                    return $"Your request {reference} has been placed On Hold. We'll resume it shortly.";
                case StatusResolved:
                    return $"Your request {reference} has been marked Resolved. Please confirm you're satisfied.";
                case StatusClosed:
                    return $"Your request {reference} has been Closed. Thank you for reporting it.";
                default:
                    return $"Your request {reference} status changed to: {status}.";
            }
        }

        private static void AddHistory(ReportedIssue request, string status, string changedBy, string note)
        {
            request.History.Add(new StatusHistoryEntry
            {
                Status = status,
                ChangedByName = changedBy,
                Timestamp = DateTime.Now,
                Note = note
            });
        }

        /// <summary>Requests assigned to a particular staff member.</summary>
        public static List<ReportedIssue> GetAssignedTo(string staffUsername) =>
            ReportedIssue.GetAllReports()
                .Where(r => string.Equals(r.AssignedToUsername, staffUsername, StringComparison.OrdinalIgnoreCase))
                .ToList();

        /// <summary>Requests logged by a particular citizen.</summary>
        public static List<ReportedIssue> GetReportedBy(string reporterUsername) =>
            ReportedIssue.GetAllReports()
                .Where(r => string.Equals(r.ReporterUsername, reporterUsername, StringComparison.OrdinalIgnoreCase))
                .ToList();

        /// <summary>Requests that still need an assignee.</summary>
        public static List<ReportedIssue> GetUnassigned() =>
            ReportedIssue.GetAllReports()
                .Where(r => string.IsNullOrWhiteSpace(r.AssignedToUsername))
                .ToList();

        /// <summary>A UI colour for each status.</summary>
        public static Color ColorFor(string status)
        {
            switch (status)
            {
                case StatusSubmitted:  return Color.FromArgb(108, 117, 125); // grey
                case StatusAssigned:   return Color.FromArgb(0, 123, 255);    // blue
                case StatusInProgress: return Color.FromArgb(255, 140, 0);    // orange
                case StatusOnHold:     return Color.FromArgb(180, 140, 0);    // amber
                case StatusResolved:   return Color.FromArgb(40, 167, 69);    // green
                case StatusClosed:     return Color.FromArgb(60, 60, 60);     // dark
                case StatusRejected:   return Color.FromArgb(220, 53, 69);    // red
                default:               return Color.FromArgb(108, 117, 125);
            }
        }
    }
}
