namespace AdvancedProgrammingAss1
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer clockTimer = null!;

        public Form1()
        {
            InitializeComponent();
            SetupClock();
            UpdateWelcomeMessage();
            UpdateReportBadge();
        }

        /// <summary>
        /// Sets up a timer to update the date/time display every second.
        /// </summary>
        private void SetupClock()
        {
            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000;
            clockTimer.Tick += (s, e) =>
            {
                lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy  |  HH:mm:ss");
            };
            clockTimer.Start();
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy  |  HH:mm:ss");
        }

        /// <summary>
        /// Displays a personalised welcome message using the Windows username.
        /// </summary>
        private void UpdateWelcomeMessage()
        {
            string username = Environment.UserName;
            lblWelcome.Text = $"Welcome, {username}";
        }

        /// <summary>
        /// Updates the report count badge on the Report Issues button.
        /// </summary>
        public void UpdateReportBadge()
        {
            int count = ReportedIssue.GetAllReports().Count;
            if (count > 0)
            {
                lblReportBadge.Text = $"{count} report(s) submitted this session";
                lblReportBadge.Visible = true;
            }
            else
            {
                lblReportBadge.Text = "";
                lblReportBadge.Visible = false;
            }
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm(this);
            reportForm.Show();
            this.Hide();
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            LocalEventsForm eventsForm = new LocalEventsForm(this);
            eventsForm.Show();
            this.Hide();
        }

        private void btnServiceRequest_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be implemented in a future update.\n\n" +
                "You will be able to track your service request status here.",
                "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            ReportHistoryForm historyForm = new ReportHistoryForm();
            historyForm.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            clockTimer?.Stop();
            clockTimer?.Dispose();
        }
    }
}
