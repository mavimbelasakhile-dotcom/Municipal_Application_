using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Citizen view: shows the requests this person logged and their live status,
    /// plus a notifications panel that updates as staff/admin move the request
    /// through the lifecycle.
    /// </summary>
    public class CitizenDashboardForm : Form
    {
        private static readonly Color Brand = Color.FromArgb(139, 0, 0);

        private readonly UserAccount user;
        private DataGridView grid = null!;
        private Label lblBadge = null!;
        private Label lblEmpty = null!;

        public CitizenDashboardForm(UserAccount currentUser)
        {
            user = currentUser;
            BuildUi();
            NotificationService.NotificationsChanged += RefreshBadge;
            FormClosed += (s, e) => NotificationService.NotificationsChanged -= RefreshBadge;

            Load += (s, e) =>
            {
                NotificationPopup.ShowUnreadOnLogin(user);
                RefreshBadge();
            };
        }

        private void BuildUi()
        {
            Text = "Citizen Dashboard";
            Size = new Size(900, 620);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(760, 500);

            Panel header = new Panel { BackColor = Brand, Dock = DockStyle.Top, Height = 80 };
            Controls.Add(header);

            Label lblTitle = new Label
            {
                Text = $"\uD83D\uDC64  Welcome, {user.DisplayName}",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 14),
                AutoSize = true
            };
            header.Controls.Add(lblTitle);

            lblBadge = new Label
            {
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 230, 230),
                Location = new Point(22, 48),
                AutoSize = true
            };
            header.Controls.Add(lblBadge);

            Button btnBell = new Button
            {
                Text = "\uD83D\uDD14  My Notifications",
                BackColor = Color.White,
                ForeColor = Brand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(170, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 190, 20),
                Cursor = Cursors.Hand
            };
            btnBell.FlatAppearance.BorderSize = 0;
            btnBell.Click += (s, e) => { ShowAllNotifications(); RefreshBadge(); };
            header.Controls.Add(btnBell);

            Label lblSection = new Label
            {
                Text = "My Service Requests",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Brand,
                Location = new Point(20, 95),
                AutoSize = true
            };
            Controls.Add(lblSection);

            grid = new DataGridView
            {
                Location = new Point(20, 125),
                Size = new Size(840, 400),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9.5F)
            };
            grid.Columns.Add("Ref", "Reference");
            grid.Columns.Add("Date", "Date");
            grid.Columns.Add("Category", "Category");
            grid.Columns.Add("Location", "Location");
            grid.Columns.Add("Assigned", "Assigned To");
            grid.Columns.Add("Status", "Status");
            grid.CellDoubleClick += Grid_CellDoubleClick;
            Controls.Add(grid);

            lblEmpty = new Label
            {
                Text = "You haven't logged any service requests yet.\nUse the Report Issues screen to create one.",
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(30, 160),
                AutoSize = true,
                Visible = false
            };
            Controls.Add(lblEmpty);

            Button btnClose = new Button
            {
                Text = "Logout",
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(140, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 160, ClientSize.Height - 55),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();
            Controls.Add(btnClose);

            LoadRequests();
        }

        private void LoadRequests()
        {
            grid.Rows.Clear();
            var mine = ServiceRequestManager.GetReportedBy(user.Username);

            lblEmpty.Visible = mine.Count == 0;
            grid.Visible = mine.Count > 0;

            foreach (var r in mine)
            {
                int i = grid.Rows.Add(
                    r.ReferenceNumber,
                    r.ReportDate.ToString("dd/MM/yyyy HH:mm"),
                    r.Category,
                    r.Location,
                    string.IsNullOrWhiteSpace(r.AssignedToName) ? "— unassigned —" : r.AssignedToName,
                    r.Status);

                grid.Rows[i].Cells[5].Style.ForeColor = ServiceRequestManager.ColorFor(r.Status);
                grid.Rows[i].Cells[5].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
        }

        private void Grid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var mine = ServiceRequestManager.GetReportedBy(user.Username);
            if (e.RowIndex >= mine.Count) return;
            RequestDetails.Show(mine[e.RowIndex]);
        }

        private void RefreshBadge()
        {
            if (InvokeRequired) { BeginInvoke(new Action(RefreshBadge)); return; }
            int unread = NotificationService.UnreadCount(user.Username);
            lblBadge.Text = unread > 0
                ? $"\uD83D\uDD14 {unread} unread notification(s)"
                : "No unread notifications";
            LoadRequests();
        }

        private void ShowAllNotifications()
        {
            var all = NotificationService.GetFor(user.Username);
            if (all.Count == 0)
            {
                MessageBox.Show("You have no notifications yet.", "Notifications",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string text = string.Join(Environment.NewLine + Environment.NewLine,
                all.Select(n => $"[{n.Timestamp:dd/MM HH:mm}] {(n.IsRead ? "" : "(NEW) ")}{n.Message}"));

            MessageBox.Show(text, "My Notifications", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NotificationService.MarkAllRead(user.Username);
        }
    }
}
