namespace AdvancedProgrammingAss1.ServiceRequest
{
    /// <summary>
    /// A single alert addressed to a specific user (by username).
    /// </summary>
    public class Notification
    {
        public string TargetUsername { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }

    /// <summary>
    /// In-memory notification hub. Every status change in the service-request
    /// lifecycle pushes alerts here, addressed to the reporter and/or assignee.
    /// On login a user pulls their unread notifications.
    /// </summary>
    public static class NotificationService
    {
        private static readonly List<Notification> notifications = new List<Notification>();

        /// <summary>Raised whenever a notification is added, so open dashboards can refresh badges.</summary>
        public static event Action? NotificationsChanged;

        public static void Notify(string targetUsername, string referenceNumber, string message)
        {
            if (string.IsNullOrWhiteSpace(targetUsername))
                return;

            notifications.Add(new Notification
            {
                TargetUsername = targetUsername,
                ReferenceNumber = referenceNumber,
                Message = message,
                Timestamp = DateTime.Now,
                IsRead = false
            });

            NotificationsChanged?.Invoke();
        }

        /// <summary>All notifications for a user, newest first.</summary>
        public static List<Notification> GetFor(string username) =>
            notifications
                .Where(n => string.Equals(n.TargetUsername, username, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(n => n.Timestamp)
                .ToList();

        /// <summary>Unread notifications for a user, newest first.</summary>
        public static List<Notification> GetUnreadFor(string username) =>
            GetFor(username).Where(n => !n.IsRead).ToList();

        public static int UnreadCount(string username) =>
            notifications.Count(n =>
                string.Equals(n.TargetUsername, username, StringComparison.OrdinalIgnoreCase) && !n.IsRead);

        public static void MarkAllRead(string username)
        {
            foreach (var n in notifications.Where(n =>
                         string.Equals(n.TargetUsername, username, StringComparison.OrdinalIgnoreCase)))
            {
                n.IsRead = true;
            }
            NotificationsChanged?.Invoke();
        }
    }
}
