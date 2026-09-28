namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Records a single search the user performed. Used to analyse search
    /// patterns and preferences for the recommendation feature.
    /// </summary>
    public class SearchRecord
    {
        public string Category { get; set; } = "All Categories";
        public DateTime? Date { get; set; }
        public DateTime SearchedAt { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// Analyses the user's search patterns and recommends related events.
    ///
    /// Data structures used:
    ///  - Queue&lt;SearchRecord&gt;            : chronological search history (FIFO)
    ///  - Dictionary&lt;string,int&gt;          : frequency map of searched categories (weighting)
    ///  - PriorityQueue&lt;LocalEvent,double&gt; : ranks candidate events by relevance score
    ///
    /// Algorithm (content-based scoring):
    ///  Each event is given a relevance score built from:
    ///   1. Category affinity  - how often the user has searched that category
    ///   2. Date affinity      - closeness to dates the user searched for
    ///   3. Priority boost     - higher-priority (urgent) events rank slightly higher
    ///  The highest-scoring events (excluding trivial ones) are recommended.
    /// </summary>
    public static class RecommendationEngine
    {
        // FIFO history of every search performed this session.
        private static readonly Queue<SearchRecord> searchHistory = new Queue<SearchRecord>();

        // Frequency map: category -> number of times searched (hash table).
        private static readonly Dictionary<string, int> categoryFrequency
            = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Dates the user has explicitly filtered by (for date-affinity scoring).
        private static readonly List<DateTime> searchedDates = new List<DateTime>();

        private const int MaxHistory = 50;

        /// <summary>
        /// Records a search so its pattern can influence future recommendations.
        /// </summary>
        public static void RecordSearch(string? category, DateTime? date)
        {
            var record = new SearchRecord
            {
                Category = string.IsNullOrWhiteSpace(category) ? "All Categories" : category,
                Date = date
            };

            searchHistory.Enqueue(record);
            while (searchHistory.Count > MaxHistory)
                searchHistory.Dequeue();

            // Only meaningful (non-"All") categories build a preference.
            if (!string.Equals(record.Category, "All Categories", StringComparison.OrdinalIgnoreCase))
            {
                if (!categoryFrequency.ContainsKey(record.Category))
                    categoryFrequency[record.Category] = 0;
                categoryFrequency[record.Category]++;
            }

            if (date.HasValue)
                searchedDates.Add(date.Value.Date);
        }

        /// <summary>
        /// True once the user has performed at least one meaningful search.
        /// </summary>
        public static bool HasSearchHistory => searchHistory.Count > 0;

        /// <summary>
        /// The category the user has searched most often (their top preference),
        /// or null if no category preference has been established yet.
        /// </summary>
        public static string? GetFavouriteCategory()
        {
            if (categoryFrequency.Count == 0) return null;
            return categoryFrequency.OrderByDescending(kvp => kvp.Value)
                                    .ThenBy(kvp => kvp.Key)
                                    .First().Key;
        }

        /// <summary>
        /// Produces up to <paramref name="maxResults"/> recommended events ranked by
        /// relevance to the user's search patterns. Optionally excludes the events
        /// the user is currently looking at so recommendations feel fresh.
        /// </summary>
        public static List<LocalEvent> GetRecommendations(int maxResults, IEnumerable<LocalEvent>? exclude = null)
        {
            var allEvents = EventManager.GetAllSortedByDate();
            if (allEvents.Count == 0) return new List<LocalEvent>();

            var excludeSet = new HashSet<string>(
                (exclude ?? Enumerable.Empty<LocalEvent>()).Select(EventKey),
                StringComparer.OrdinalIgnoreCase);

            // Rank candidates using a PriorityQueue. We negate the score because
            // PriorityQueue dequeues the *lowest* priority first, and we want the
            // highest-scoring event first.
            var ranked = new PriorityQueue<LocalEvent, double>();

            foreach (var ev in allEvents)
            {
                if (excludeSet.Contains(EventKey(ev)))
                    continue;

                double score = ScoreEvent(ev);
                if (score <= 0) continue; // ignore events with no relevance signal

                ranked.Enqueue(ev, -score);
            }

            var results = new List<LocalEvent>();
            while (ranked.Count > 0 && results.Count < maxResults)
                results.Add(ranked.Dequeue());

            return results;
        }

        /// <summary>
        /// Calculates a relevance score for a single event based on the user's
        /// recorded search patterns.
        /// </summary>
        private static double ScoreEvent(LocalEvent ev)
        {
            double score = 0;

            // 1. Category affinity: weight by how often this category was searched.
            if (categoryFrequency.TryGetValue(ev.Category, out int freq) && freq > 0)
            {
                int totalSearches = categoryFrequency.Values.Sum();
                double affinity = (double)freq / totalSearches; // 0..1 share of interest
                score += affinity * 10.0; // dominant signal
            }

            // 2. Date affinity: reward events close to dates the user searched for.
            if (searchedDates.Count > 0)
            {
                int nearestDays = searchedDates.Min(d => Math.Abs((ev.Date.Date - d).Days));
                if (nearestDays == 0) score += 4.0;
                else if (nearestDays <= 3) score += 2.5;
                else if (nearestDays <= 7) score += 1.0;
            }

            // 3. Priority boost: urgent events (Priority 1) get a small lift so
            //    important announcements surface, but never outrank real interest.
            //    Priority 1 -> +1.0, Priority 5 -> +0.2.
            if (score > 0)
                score += (6 - Math.Clamp(ev.Priority, 1, 5)) * 0.2;

            // 4. Slight recency/upcoming boost: prefer future events over past ones.
            if (score > 0 && ev.Date.Date >= DateTime.Today)
                score += 0.5;

            return score;
        }

        /// <summary>
        /// A short, human-friendly explanation of why events are being recommended,
        /// used in the UI to keep the feature transparent and user-friendly.
        /// </summary>
        public static string GetRecommendationReason()
        {
            string? fav = GetFavouriteCategory();
            if (fav != null)
                return $"Because you've been searching \u201C{fav}\u201D events";

            if (searchedDates.Count > 0)
                return "Based on the dates you've been searching";

            return "Popular events you might like";
        }

        /// <summary>
        /// Clears all recorded search patterns (e.g. when starting a fresh session).
        /// </summary>
        public static void Reset()
        {
            searchHistory.Clear();
            categoryFrequency.Clear();
            searchedDates.Clear();
        }

        private static string EventKey(LocalEvent ev) => $"{ev.Title}|{ev.Date:yyyyMMdd}";
    }
}
