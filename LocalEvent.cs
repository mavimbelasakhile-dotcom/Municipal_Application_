namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Represents a single local event or announcement.
    /// </summary>
    public class LocalEvent
    {
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; } // 1 = highest (e.g., urgent announcement), 5 = lowest

        public override string ToString()
        {
            return $"{Title} ({Category}) - {Date:dd MMM yyyy}";
        }
    }

    /// <summary>
    /// Manages the collection of local events using a variety of data structures:
    ///  - SortedDictionary   : events organised by date (auto-sorted) for optimised retrieval
    ///  - Dictionary         : fast lookup of events grouped by category (hash table)
    ///  - HashSet            : unique categories and unique dates
    ///  - Queue              : announcements processed in first-in-first-out order
    ///  - Stack              : most recently added events (last-in-first-out) for "recent activity"
    ///  - PriorityQueue      : events ordered by priority for the "featured" highlight
    /// </summary>
    public static class EventManager
    {
        // SortedDictionary keyed by date -> list of events on that date (auto-sorted by date)
        private static SortedDictionary<DateTime, List<LocalEvent>> eventsByDate
            = new SortedDictionary<DateTime, List<LocalEvent>>();

        // Dictionary (hash table) keyed by category -> list of events in that category
        private static Dictionary<string, List<LocalEvent>> eventsByCategory
            = new Dictionary<string, List<LocalEvent>>(StringComparer.OrdinalIgnoreCase);

        // HashSet of unique categories
        private static HashSet<string> uniqueCategories
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // HashSet of unique dates
        private static HashSet<DateTime> uniqueDates = new HashSet<DateTime>();

        // Queue of announcements (FIFO)
        private static Queue<LocalEvent> announcementQueue = new Queue<LocalEvent>();

        // Stack of recently added events (LIFO)
        private static Stack<LocalEvent> recentEvents = new Stack<LocalEvent>();

        // Master flat list of all events
        private static List<LocalEvent> allEvents = new List<LocalEvent>();

        private static bool seeded = false;

        /// <summary>
        /// Adds an event and updates every underlying data structure.
        /// </summary>
        public static void AddEvent(LocalEvent ev)
        {
            allEvents.Add(ev);

            // SortedDictionary by date
            DateTime dateKey = ev.Date.Date;
            if (!eventsByDate.ContainsKey(dateKey))
                eventsByDate[dateKey] = new List<LocalEvent>();
            eventsByDate[dateKey].Add(ev);

            // Dictionary by category
            if (!eventsByCategory.ContainsKey(ev.Category))
                eventsByCategory[ev.Category] = new List<LocalEvent>();
            eventsByCategory[ev.Category].Add(ev);

            // Sets for unique categories and dates
            uniqueCategories.Add(ev.Category);
            uniqueDates.Add(dateKey);

            // Queue and Stack
            announcementQueue.Enqueue(ev);
            recentEvents.Push(ev);
        }

        /// <summary>
        /// Seeds the manager with sample events (only once).
        /// </summary>
        public static void SeedSampleData()
        {
            if (seeded) return;
            seeded = true;

            AddEvent(new LocalEvent { Title = "Water Maintenance Notice", Category = "Announcement", Date = DateTime.Today.AddDays(1), Venue = "Meerensee & Arboretum", Description = "Scheduled water supply interruption from 08:00 to 14:00 for pipe maintenance.", Priority = 1 });
            AddEvent(new LocalEvent { Title = "Richards Bay Community Cleanup", Category = "Community", Date = DateTime.Today.AddDays(3), Venue = "Alkantstrand Beach", Description = "Join us for a beach and neighbourhood cleanup. Gloves and bags provided.", Priority = 3 });
            AddEvent(new LocalEvent { Title = "Council Public Meeting", Category = "Municipal", Date = DateTime.Today.AddDays(5), Venue = "Civic Centre, Richards Bay", Description = "Quarterly public engagement meeting. Residents welcome to raise concerns.", Priority = 2 });
            AddEvent(new LocalEvent { Title = "Empangeni Farmers Market", Category = "Market", Date = DateTime.Today.AddDays(2), Venue = "Empangeni CBD", Description = "Fresh local produce, crafts, and food stalls every Saturday morning.", Priority = 4 });
            AddEvent(new LocalEvent { Title = "Youth Sports Day", Category = "Sports", Date = DateTime.Today.AddDays(7), Venue = "uMhlathuze Sports Complex", Description = "Soccer, athletics, and netball tournaments for local youth teams.", Priority = 3 });
            AddEvent(new LocalEvent { Title = "Electricity Load Reduction Notice", Category = "Announcement", Date = DateTime.Today.AddDays(1), Venue = "eSikhaleni Sections A-D", Description = "Planned load reduction between 18:00 and 20:00. Please conserve power.", Priority = 1 });
            AddEvent(new LocalEvent { Title = "Small Business Workshop", Category = "Education", Date = DateTime.Today.AddDays(10), Venue = "Empangeni Library Hall", Description = "Free workshop on registering and running a small business in the municipality.", Priority = 3 });
            AddEvent(new LocalEvent { Title = "Heritage Day Celebration", Category = "Community", Date = DateTime.Today.AddDays(14), Venue = "Tuzi Gazi Waterfront", Description = "Cultural performances, food, and celebrations for Heritage Day.", Priority = 2 });
        }

        /// <summary>
        /// Returns all events sorted by date (via the SortedDictionary).
        /// </summary>
        public static List<LocalEvent> GetAllSortedByDate()
        {
            var result = new List<LocalEvent>();
            foreach (var kvp in eventsByDate) // SortedDictionary iterates in key (date) order
                result.AddRange(kvp.Value);
            return result;
        }

        /// <summary>
        /// Returns the sorted set of unique categories (for the search dropdown).
        /// </summary>
        public static List<string> GetUniqueCategories()
        {
            var list = uniqueCategories.ToList();
            list.Sort();
            return list;
        }

        /// <summary>
        /// Returns the sorted set of unique dates.
        /// </summary>
        public static List<DateTime> GetUniqueDates()
        {
            var list = uniqueDates.ToList();
            list.Sort();
            return list;
        }

        /// <summary>
        /// Searches events by category (Dictionary lookup) and/or date.
        /// Passing null/empty category or null date ignores that filter.
        /// </summary>
        public static List<LocalEvent> Search(string? category, DateTime? date)
        {
            IEnumerable<LocalEvent> query;

            // Use the category Dictionary (hash table) for a fast initial filter
            if (!string.IsNullOrWhiteSpace(category) && category != "All Categories"
                && eventsByCategory.ContainsKey(category))
            {
                query = eventsByCategory[category];
            }
            else
            {
                query = GetAllSortedByDate();
            }

            // Apply date filter if provided
            if (date.HasValue)
            {
                query = query.Where(e => e.Date.Date == date.Value.Date);
            }

            return query.OrderBy(e => e.Date).ToList();
        }

        /// <summary>
        /// Returns the highest-priority upcoming event using a PriorityQueue.
        /// </summary>
        public static LocalEvent? GetFeaturedEvent()
        {
            // PriorityQueue orders by priority number (1 = highest priority)
            var pq = new PriorityQueue<LocalEvent, int>();
            foreach (var ev in allEvents)
                pq.Enqueue(ev, ev.Priority);

            return pq.Count > 0 ? pq.Dequeue() : null;
        }

        /// <summary>
        /// Returns the most recently added events using the Stack (LIFO).
        /// </summary>
        public static List<LocalEvent> GetRecentEvents(int count)
        {
            return recentEvents.Take(count).ToList();
        }

        /// <summary>
        /// Returns the next announcement in the Queue (FIFO) without removing it.
        /// </summary>
        public static LocalEvent? PeekNextAnnouncement()
        {
            return announcementQueue.Count > 0 ? announcementQueue.Peek() : null;
        }

        public static int TotalEvents => allEvents.Count;
        public static int TotalCategories => uniqueCategories.Count;
        public static int TotalDates => uniqueDates.Count;
    }
}
