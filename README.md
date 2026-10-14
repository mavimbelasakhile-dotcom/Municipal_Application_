# City of uMhlathuze — Municipal Services Portal

A Windows Forms (.NET 8) desktop application that lets residents of the City of uMhlathuze report municipal issues and browse local events and announcements. Built with the municipality's branding for a professional, enterprise-grade experience.

---

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Technology Stack](#technology-stack)
4. [How to Compile](#how-to-compile)
5. [How to Run](#how-to-run)
6. [How to Use the Software](#how-to-use-the-software)
7. [Data Structures Used](#data-structures-used)
8. [Source Code Highlights](#source-code-highlights)
9. [Example Output](#example-output)
10. [Project Structure](#project-structure)
11. [Continuous Integration](#continuous-integration)

> For a detailed step-by-step user guide with screenshots, see [WALKTHROUGH.md](WALKTHROUGH.md).

---

## Overview

The Municipal Services Portal provides residents with a single point of contact to engage with municipal services. On startup, the Main Menu presents three services:

- **Report Issues** (implemented in Part 1)
- **Local Events & Announcements** (implemented in Part 2)
- **Service Request Status** (planned — disabled)

---

## Features

### Report Issues (Part 1)
- Report issues with location, category, priority, description, and attachments
- Location autocomplete, live character counter, and preferred contact method
- Dynamic engagement progress bar and custom branded dialogs
- Unique reference numbers (e.g., `UMH-2026-0001`), report history, and export

### Local Events & Announcements (Part 2)
- Aesthetically pleasing, colour-coded event cards
- A **Featured** banner showing the highest-priority event (`PriorityQueue`)
- **Search** by category and/or date
- **Smart Recommendations** sidebar that learns from your searches
- Live stats footer showing unique categories and dates (`HashSet`)

---

## Technology Stack

- **Language:** C#
- **Framework:** .NET 8 (Windows Forms) — target `net8.0-windows`
- **IDE:** Visual Studio 2022
- **Version Control:** Git / GitHub
- **CI:** GitHub Actions (builds in Release on `windows-latest`)

---

## How to Compile

### Option A — Visual Studio
1. Open `AdvancedProgrammingAss1.slnx` in Visual Studio 2022.
2. Select **Build > Build Solution** (or press `Ctrl + Shift + B`).

### Option B — Command line (.NET 8 SDK)
```bash
dotnet restore
dotnet build --configuration Release
```

A successful build prints:
```
AdvancedProgrammingAss1 net8.0-windows succeeded → bin\Release\net8.0-windows\AdvancedProgrammingAss1.dll
Build succeeded.
```

---

## How to Run

### Option A — Visual Studio
Press **F5** (debug) or **Ctrl + F5** (run without debugging).

### Option B — Command line
```bash
dotnet run
```

### Option C — Run the built executable
```bash
bin\Release\net8.0-windows\AdvancedProgrammingAss1.exe
```

> **Note:** This is a Windows Forms application and must be run on **Windows**.

---

## How to Use the Software

1. **Launch** the app to see the Main Menu with a live clock and a personalised welcome.
2. Click **Report Issues** to log a municipal issue (location, category, priority, description, attachments, contact method), then **Submit** to receive a unique reference number.
3. Click **Local Events & Announcements** to:
   - Browse upcoming events as colour-coded cards.
   - See the **Featured** (highest-priority) event in the top banner.
   - **Search** by selecting a category and/or ticking **Filter by date**, then clicking **Search**.
   - Watch the **Recommended for You** sidebar update based on your search patterns.
4. Click **View Report History** to review and **export** submitted reports.
5. **Service Request Status** is disabled (planned for a future release).

---

## Data Structures Used

| Structure | Where | Purpose |
|---|---|---|
| `SortedDictionary<DateTime, List<LocalEvent>>` | `EventManager` | Events auto-sorted by date for optimised retrieval |
| `Dictionary<string, List<LocalEvent>>` | `EventManager` | Fast category lookup (hash table) during search |
| `HashSet<string>` / `HashSet<DateTime>` | `EventManager` | Unique categories and unique dates |
| `Queue<LocalEvent>` | `EventManager` | Announcements processed first-in-first-out |
| `Stack<LocalEvent>` | `EventManager` | Most recently added events (last-in-first-out) |
| `PriorityQueue<LocalEvent, int>` | `EventManager` | Selecting the highest-priority featured event |
| `Queue<SearchRecord>` | `RecommendationEngine` | Chronological search history |
| `Dictionary<string, int>` | `RecommendationEngine` | Category frequency weighting |
| `PriorityQueue<LocalEvent, double>` | `RecommendationEngine` | Ranking candidate events by relevance score |

---

## Source Code Highlights

### 1. Managing event data with multiple data structures (`LocalEvent.cs`)

```csharp
public static class EventManager
{
    // SortedDictionary keyed by date -> events on that date (auto-sorted)
    private static SortedDictionary<DateTime, List<LocalEvent>> eventsByDate = new();

    // Dictionary (hash table) keyed by category -> events in that category
    private static Dictionary<string, List<LocalEvent>> eventsByCategory
        = new(StringComparer.OrdinalIgnoreCase);

    // HashSets for unique categories and dates
    private static HashSet<string> uniqueCategories = new(StringComparer.OrdinalIgnoreCase);
    private static HashSet<DateTime> uniqueDates = new();

    // Queue (FIFO) and Stack (LIFO)
    private static Queue<LocalEvent> announcementQueue = new();
    private static Stack<LocalEvent> recentEvents = new();

    public static void AddEvent(LocalEvent ev)
    {
        DateTime dateKey = ev.Date.Date;
        if (!eventsByDate.ContainsKey(dateKey)) eventsByDate[dateKey] = new();
        eventsByDate[dateKey].Add(ev);

        if (!eventsByCategory.ContainsKey(ev.Category)) eventsByCategory[ev.Category] = new();
        eventsByCategory[ev.Category].Add(ev);

        uniqueCategories.Add(ev.Category);
        uniqueDates.Add(dateKey);
        announcementQueue.Enqueue(ev);
        recentEvents.Push(ev);
    }
}
```

### 2. Priority Queue for the featured event (`LocalEvent.cs`)

```csharp
public static LocalEvent? GetFeaturedEvent()
{
    // PriorityQueue orders by priority number (1 = highest priority)
    var pq = new PriorityQueue<LocalEvent, int>();
    foreach (var ev in allEvents)
        pq.Enqueue(ev, ev.Priority);

    return pq.Count > 0 ? pq.Dequeue() : null;
}
```

### 3. Searching by category (dictionary) and date (`LocalEvent.cs`)

```csharp
public static List<LocalEvent> Search(string? category, DateTime? date)
{
    IEnumerable<LocalEvent> query;

    if (!string.IsNullOrWhiteSpace(category) && category != "All Categories"
        && eventsByCategory.ContainsKey(category))
        query = eventsByCategory[category];   // fast hash-table filter
    else
        query = GetAllSortedByDate();

    if (date.HasValue)
        query = query.Where(e => e.Date.Date == date.Value.Date);

    return query.OrderBy(e => e.Date).ToList();
}
```

### 4. Smart recommendations — learning from searches (`RecommendationEngine.cs`)

```csharp
public static void RecordSearch(string? category, DateTime? date)
{
    var record = new SearchRecord { Category = category ?? "All Categories", Date = date };
    searchHistory.Enqueue(record);                 // FIFO history

    if (!string.Equals(record.Category, "All Categories", StringComparison.OrdinalIgnoreCase))
    {
        if (!categoryFrequency.ContainsKey(record.Category)) categoryFrequency[record.Category] = 0;
        categoryFrequency[record.Category]++;      // frequency weighting
    }
    if (date.HasValue) searchedDates.Add(date.Value.Date);
}

public static List<LocalEvent> GetRecommendations(int maxResults, IEnumerable<LocalEvent>? exclude = null)
{
    var ranked = new PriorityQueue<LocalEvent, double>();
    foreach (var ev in EventManager.GetAllSortedByDate())
    {
        double score = ScoreEvent(ev);
        if (score > 0) ranked.Enqueue(ev, -score); // negate: highest score first
    }

    var results = new List<LocalEvent>();
    while (ranked.Count > 0 && results.Count < maxResults)
        results.Add(ranked.Dequeue());
    return results;
}
```

---

## Example Output

### Console / build output
```
> dotnet build --configuration Release
Restore complete (0.7s)
  AdvancedProgrammingAss1 net8.0-windows succeeded → bin\Release\net8.0-windows\AdvancedProgrammingAss1.dll
Build succeeded in 6.5s
```

### Local Events screen (textual representation)

```
================================================================
  Local Events & Announcements - City of uMhlathuze
================================================================
 ★ FEATURED: Water Maintenance Notice
   Tuesday, 15 October 2026 • Meerensee & Arboretum
   Scheduled water supply interruption from 08:00 to 14:00.
----------------------------------------------------------------
 SEARCH EVENTS   Category: [All Categories v]  [ ] Filter by date
                                             [ Search ] [ Clear ]
----------------------------------------------------------------
 EVENTS                              | ✨ RECOMMENDED FOR YOU
 ┌────────────────────────────────┐ | (Search to get suggestions)
 │ Water Maintenance Notice   [Announcement]
 │ Tue, 15 Oct 2026 • Meerensee   │ |
 ├────────────────────────────────┤ |
 │ Electricity Load Reduction [Announcement]
 │ Tue, 15 Oct 2026 • eSikhaleni  │ |
 ├────────────────────────────────┤ |
 │ Empangeni Farmers Market       [Market]
 │ Wed, 16 Oct 2026 • Empangeni   │ |
 └────────────────────────────────┘ |
----------------------------------------------------------------
 Total Events: 8 | Unique Categories (Set): 6 | Unique Dates (Set): 7
================================================================
```

### After searching "Community" — recommendations populate

```
 ✨ RECOMMENDED FOR YOU
 Because you've been searching "Community" events
 ┌────────────────────────────────┐
 │ Heritage Day Celebration        │  Sat, 25 Oct 2026 • Community
 ├────────────────────────────────┤
 │ Richards Bay Community Cleanup  │  Fri, 17 Oct 2026 • Community
 └────────────────────────────────┘
```

### Report submission success (Part 1)

```
┌──────────────────────────────────────────┐
│   ✓ Report Submitted Successfully         │
│                                            │
│   Reference Number:  UMH-2026-0001         │
│   Category: Water Supply   Priority: High  │
│   Location: Meerensee, Richards Bay        │
│                                            │
│   Please save your reference number.       │
└──────────────────────────────────────────┘
```

---

## Project Structure

```
AdvancedProgrammingAss1/
├── Program.cs                      # Application entry point
├── Form1.cs / .Designer.cs         # Main Menu
├── ReportIssuesForm.cs / .Designer # Report Issues (Part 1)
├── ReportHistoryForm.cs / .Designer# Report history + export
├── CustomDialog.cs                 # Branded confirm/success dialogs
├── LocalEventsForm.cs / .Designer  # Local Events & Announcements (Part 2)
├── LocalEvent.cs                   # LocalEvent model + EventManager
├── RecommendationEngine.cs         # Search-pattern analysis + recommendations
├── Resources/umhlathuze_logo.png   # Branding
├── README.md / WALKTHROUGH.md      # Documentation
└── .github/workflows/build.yml     # CI pipeline
```

---

## Continuous Integration

A GitHub Actions workflow (`.github/workflows/build.yml`) builds the application in **Release** mode on a Windows runner for every push and pull request to `master`, verifying that the code compiles cleanly.

```yaml
- name: Build
  run: dotnet build --configuration Release --no-restore
```

---

© 2026 City of uMhlathuze — Municipal Services Portal. Built for academic purposes.
