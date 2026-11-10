namespace AdvancedProgrammingAss1.ServiceRequest
{
    /// <summary>
    /// The three roles supported by the simple role-based login.
    /// </summary>
    public enum UserRole
    {
        Citizen,
        Administrator,
        Staff
    }

    /// <summary>
    /// A lightweight in-memory user account. No database / no password store —
    /// users are seeded once and selected via the login screen.
    /// </summary>
    public class UserAccount
    {
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        /// <summary>Department shown for staff members (e.g. "Water &amp; Sanitation").</summary>
        public string Department { get; set; } = string.Empty;

        public override string ToString() =>
            Role == UserRole.Staff && !string.IsNullOrWhiteSpace(Department)
                ? $"{DisplayName}  —  {Department}"
                : DisplayName;
    }

    /// <summary>
    /// Central, in-memory directory of all demo users. Acts as the single source
    /// of truth for the currently logged-in user during a session.
    /// </summary>
    public static class UserDirectory
    {
        private static readonly List<UserAccount> users = new List<UserAccount>
        {
            // Citizens (people who log requests)
            new UserAccount { Username = "citizen.sipho",  DisplayName = "Sipho Khumalo",  Role = UserRole.Citizen },
            new UserAccount { Username = "citizen.lerato", DisplayName = "Lerato Mokoena",  Role = UserRole.Citizen },
            new UserAccount { Username = "citizen.anita",  DisplayName = "Anita van Wyk",   Role = UserRole.Citizen },

            // Administrator (assigns work)
            new UserAccount { Username = "admin",          DisplayName = "Municipal Administrator", Role = UserRole.Administrator },

            // Staff (assignees who resolve requests)
            new UserAccount { Username = "staff.thabo",  DisplayName = "Thabo Ndlovu",   Role = UserRole.Staff, Department = "Water & Sanitation" },
            new UserAccount { Username = "staff.nadia",  DisplayName = "Nadia Pillay",   Role = UserRole.Staff, Department = "Roads & Potholes" },
            new UserAccount { Username = "staff.johan",  DisplayName = "Johan Botha",    Role = UserRole.Staff, Department = "Electricity & Power" },
        };

        /// <summary>The user currently logged in for this session.</summary>
        public static UserAccount? CurrentUser { get; set; }

        public static List<UserAccount> GetByRole(UserRole role) =>
            users.Where(u => u.Role == role).ToList();

        public static List<UserAccount> GetStaff() => GetByRole(UserRole.Staff);

        public static List<UserAccount> GetCitizens() => GetByRole(UserRole.Citizen);

        public static UserAccount? GetByUsername(string username) =>
            users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));

        public static UserAccount? GetByDisplayName(string displayName) =>
            users.FirstOrDefault(u => string.Equals(u.DisplayName, displayName, StringComparison.OrdinalIgnoreCase));

        /// <summary>Returns the single administrator account.</summary>
        public static UserAccount Administrator =>
            users.First(u => u.Role == UserRole.Administrator);
    }
}
