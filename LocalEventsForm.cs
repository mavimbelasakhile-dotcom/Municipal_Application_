namespace AdvancedProgrammingAss1
{
    public partial class LocalEventsForm : Form
    {
        private Form1 mainForm;

        public LocalEventsForm(Form1 parentForm)
        {
            InitializeComponent();
            this.mainForm = parentForm;
            EventManager.SeedSampleData();
            PopulateCategoryFilter();
            LoadFeaturedEvent();
            DisplayEvents(EventManager.GetAllSortedByDate());
            UpdateStats();
        }

        /// <summary>
        /// Fills the category filter dropdown from the unique-category set.
        /// </summary>
        private void PopulateCategoryFilter()
        {
            cmbSearchCategory.Items.Add("All Categories");
            foreach (string category in EventManager.GetUniqueCategories())
                cmbSearchCategory.Items.Add(category);
            cmbSearchCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads the highest-priority event into the featured banner (PriorityQueue).
        /// </summary>
        private void LoadFeaturedEvent()
        {
            LocalEvent? featured = EventManager.GetFeaturedEvent();
            if (featured != null)
            {
                lblFeaturedTitle.Text = $"\u2605 FEATURED: {featured.Title}";
                lblFeaturedDetails.Text = $"{featured.Date:dddd, dd MMMM yyyy}  •  {featured.Venue}\n{featured.Description}";
            }
            else
            {
                lblFeaturedTitle.Text = "No featured events";
                lblFeaturedDetails.Text = "";
            }
        }

        /// <summary>
        /// Renders event cards into the scrollable panel.
        /// </summary>
        private void DisplayEvents(List<LocalEvent> events)
        {
            pnlEventList.Controls.Clear();

            if (events.Count == 0)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "No events found for the selected filters.";
                lblEmpty.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
                lblEmpty.ForeColor = Color.FromArgb(180, 190, 200);
                lblEmpty.AutoSize = true;
                lblEmpty.Location = new Point(20, 20);
                pnlEventList.Controls.Add(lblEmpty);
                return;
            }

            int y = 10;
            foreach (var ev in events)
            {
                Panel card = CreateEventCard(ev, y);
                pnlEventList.Controls.Add(card);
                y += card.Height + 12;
            }
        }

        /// <summary>
        /// Builds a single styled event card.
        /// </summary>
        private Panel CreateEventCard(LocalEvent ev, int y)
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(pnlEventList.Width - 40, 110);
            card.Location = new Point(10, y);
            card.BorderStyle = BorderStyle.None;

            // Left colour bar based on category
            Panel colourBar = new Panel();
            colourBar.BackColor = GetCategoryColour(ev.Category);
            colourBar.Size = new Size(6, card.Height);
            colourBar.Location = new Point(0, 0);
            card.Controls.Add(colourBar);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = ev.Title;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 12);
            card.Controls.Add(lblTitle);

            // Category badge
            Label lblBadge = new Label();
            lblBadge.Text = "  " + ev.Category + "  ";
            lblBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblBadge.ForeColor = Color.White;
            lblBadge.BackColor = GetCategoryColour(ev.Category);
            lblBadge.AutoSize = true;
            lblBadge.Location = new Point(card.Width - 130, 14);
            lblBadge.Padding = new Padding(2);
            card.Controls.Add(lblBadge);

            // Date + venue
            Label lblMeta = new Label();
            lblMeta.Text = $"\uD83D\uDCC5 {ev.Date:ddd, dd MMM yyyy}     \uD83D\uDCCD {ev.Venue}";
            lblMeta.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblMeta.ForeColor = Color.FromArgb(120, 120, 120);
            lblMeta.AutoSize = true;
            lblMeta.Location = new Point(20, 42);
            card.Controls.Add(lblMeta);

            // Description
            Label lblDesc = new Label();
            lblDesc.Text = ev.Description;
            lblDesc.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblDesc.ForeColor = Color.FromArgb(70, 70, 70);
            lblDesc.Location = new Point(20, 66);
            lblDesc.Size = new Size(card.Width - 40, 38);
            card.Controls.Add(lblDesc);

            return card;
        }

        /// <summary>
        /// Assigns a colour to each event category.
        /// </summary>
        private Color GetCategoryColour(string category)
        {
            switch (category.ToLower())
            {
                case "announcement": return Color.FromArgb(139, 0, 0);
                case "community": return Color.FromArgb(40, 167, 69);
                case "municipal": return Color.FromArgb(0, 102, 153);
                case "market": return Color.FromArgb(230, 145, 0);
                case "sports": return Color.FromArgb(120, 40, 140);
                case "education": return Color.FromArgb(0, 130, 130);
                default: return Color.FromArgb(90, 90, 90);
            }
        }

        /// <summary>
        /// Runs the search using category + optional date.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string category = cmbSearchCategory.SelectedItem?.ToString() ?? "All Categories";
            DateTime? date = chkFilterByDate.Checked ? dtpSearchDate.Value.Date : (DateTime?)null;

            List<LocalEvent> results = EventManager.Search(category, date);
            DisplayEvents(results);
            lblResultCount.Text = $"{results.Count} event(s) found";
        }

        /// <summary>
        /// Clears the search filters and shows all events.
        /// </summary>
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            cmbSearchCategory.SelectedIndex = 0;
            chkFilterByDate.Checked = false;
            DisplayEvents(EventManager.GetAllSortedByDate());
            lblResultCount.Text = "";
        }

        private void chkFilterByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpSearchDate.Enabled = chkFilterByDate.Checked;
        }

        /// <summary>
        /// Updates the stats footer showing data-structure counts.
        /// </summary>
        private void UpdateStats()
        {
            lblStats.Text = $"Total Events: {EventManager.TotalEvents}   |   " +
                            $"Unique Categories (Set): {EventManager.TotalCategories}   |   " +
                            $"Unique Dates (Set): {EventManager.TotalDates}";
        }

        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
                mainForm.Show();
        }
    }
}
