using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Administrator view: sees every service request and assigns (or re-assigns)
    /// each one to a staff member. Assigning fires notifications to the assignee
    /// and the citizen who logged the request.
    /// </summary>
    public class AdminDashboardForm : Form
    {
        private static readonly Color Brand = Color.FromArgb(139, 0, 0);
        private static readonly Color Blue = Color.FromArgb(0, 123, 255);

        private readonly UserAccount admin;
        private DataGridView grid = null!;
        private ComboBox cmbStaff = null!;
        private Label lblEmpty = null!;
        private Label lblSummary = null!;
        private Panel graphPanel = null!;

        // Status breakdown used to paint the bar graph below the request list.
        private readonly List<(string status, int count, Color color)> statusBars
            = new List<(string, int, Color)>();

        public AdminDashboardForm(UserAccount administrator)
        {
            admin = administrator;
            BuildUi();
            NotificationService.NotificationsChanged += SafeReload;
            FormClosed += (s, e) => NotificationService.NotificationsChanged -= SafeReload;
        }

        private void BuildUi()
        {
            Text = "Administrator Dashboard";
            Size = new Size(1000, 660);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(840, 560);

            Panel header = new Panel { BackColor = Blue, Dock = DockStyle.Top, Height = 80 };
            Controls.Add(header);

            Label lblTitle = new Label
            {
                Text = "\uD83D\uDEE1\uFE0F  Administrator — Assign Service Requests",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 14),
                AutoSize = true
            };
            header.Controls.Add(lblTitle);

            lblSummary = new Label
            {
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(225, 240, 255),
                Location = new Point(22, 48),
                AutoSize = true
            };
            header.Controls.Add(lblSummary);

            // Assignment bar
            Label lblAssign = new Label
            {
                Text = "Assign selected request to:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Brand,
                Location = new Point(20, 100),
                AutoSize = true
            };
            Controls.Add(lblAssign);

            cmbStaff = new ComboBox
            {
                Location = new Point(230, 97),
                Size = new Size(320, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            foreach (var staff in UserDirectory.GetStaff())
                cmbStaff.Items.Add(staff);
            if (cmbStaff.Items.Count > 0) cmbStaff.SelectedIndex = 0;
            Controls.Add(cmbStaff);

            Button btnAssign = new Button
            {
                Text = "Assign / Re-assign",
                BackColor = Brand,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(200, 34),
                Location = new Point(570, 95),
                Cursor = Cursors.Hand
            };
            btnAssign.FlatAppearance.BorderSize = 0;
            btnAssign.Click += BtnAssign_Click;
            Controls.Add(btnAssign);

            grid = new DataGridView
            {
                Location = new Point(20, 145),
                Size = new Size(940, 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
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
            grid.Columns.Add("Priority", "Priority");
            grid.Columns.Add("Location", "Location");
            grid.Columns.Add("Reporter", "Logged By");
            grid.Columns.Add("Assigned", "Assigned To");
            grid.Columns.Add("Status", "Status");
            grid.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var all = ReportedIssue.GetAllReports();
                if (e.RowIndex < all.Count) RequestDetails.Show(all[e.RowIndex]);
            };
            Controls.Add(grid);

            // ---- Status breakdown graph, directly below the request list ----
            Label lblGraphTitle = new Label
            {
                Text = "\uD83D\uDCC8  Service Requests by Status",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Brand,
                Location = new Point(20, 405),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            Controls.Add(lblGraphTitle);

            graphPanel = new Panel
            {
                Location = new Point(20, 432),
                Size = new Size(940, 133),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            graphPanel.Paint += GraphPanel_Paint;
            Controls.Add(graphPanel);

            lblEmpty = new Label
            {
                Text = "No service requests have been logged yet.",
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(30, 180),
                AutoSize = true,
                Visible = false
            };
            Controls.Add(lblEmpty);

            Button btnStats = new Button
            {
                Text = "\uD83D\uDCCA  View Statistics",
                BackColor = Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(190, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                Location = new Point(20, ClientSize.Height - 55),
                Cursor = Cursors.Hand
            };
            btnStats.FlatAppearance.BorderSize = 0;
            btnStats.Click += (s, e) => new AdminStatisticsForm().ShowDialog();
            Controls.Add(btnStats);

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

        private void BtnAssign_Click(object? sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request to assign.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStaff.SelectedItem is not UserAccount staff)
            {
                MessageBox.Show("Please select a staff member.", "No Staff Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = grid.SelectedRows[0].Index;
            var all = ReportedIssue.GetAllReports();
            if (rowIndex >= all.Count) return;

            var request = all[rowIndex];

            if (request.Status == ServiceRequestManager.StatusClosed)
            {
                MessageBox.Show("This request is already closed and cannot be re-assigned.",
                    "Closed Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ServiceRequestManager.AssignRequest(request, staff, admin);

            MessageBox.Show(
                $"Request {request.ReferenceNumber} assigned to {staff.DisplayName}.\n\n" +
                $"{staff.DisplayName} will be alerted on login, and {request.ReporterName} has been notified.",
                "Request Assigned", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadRequests();
        }

        private void LoadRequests()
        {
            grid.Rows.Clear();
            var all = ReportedIssue.GetAllReports();

            lblEmpty.Visible = all.Count == 0;
            grid.Visible = all.Count > 0;

            int unassigned = 0;
            foreach (var r in all)
            {
                if (string.IsNullOrWhiteSpace(r.AssignedToUsername)) unassigned++;

                int i = grid.Rows.Add(
                    r.ReferenceNumber,
                    r.ReportDate.ToString("dd/MM/yyyy HH:mm"),
                    r.Category,
                    r.Priority,
                    r.Location,
                    string.IsNullOrWhiteSpace(r.ReporterName) ? "Unknown" : r.ReporterName,
                    string.IsNullOrWhiteSpace(r.AssignedToName) ? "— unassigned —" : r.AssignedToName,
                    r.Status);

                grid.Rows[i].Cells[7].Style.ForeColor = ServiceRequestManager.ColorFor(r.Status);
                grid.Rows[i].Cells[7].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            lblSummary.Text = $"{all.Count} total request(s)  •  {unassigned} awaiting assignment";

            // Rebuild the status breakdown and repaint the graph.
            statusBars.Clear();
            foreach (var status in ServiceRequestManager.AllStatuses)
            {
                int count = all.Count(r =>
                    string.Equals(r.Status, status, StringComparison.OrdinalIgnoreCase));
                statusBars.Add((status, count, ServiceRequestManager.ColorFor(status)));
            }
            graphPanel?.Invalidate();
        }

        /// <summary>
        /// Draws a horizontal bar graph of the request counts for each status,
        /// giving the administrator an at-a-glance view below the request list.
        /// </summary>
        private void GraphPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int padding = 14;
            int labelWidth = 110;
            int valueWidth = 42;
            int rowCount = statusBars.Count;
            if (rowCount == 0) return;

            int available = graphPanel.Height - padding * 2;
            int rowHeight = Math.Max(10, available / rowCount);
            int barAreaWidth = graphPanel.Width - padding * 2 - labelWidth - valueWidth;
            if (barAreaWidth < 10) barAreaWidth = 10;

            int maxCount = Math.Max(1, statusBars.Max(b => b.count));

            using var labelFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using var valueFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            using var textBrush = new SolidBrush(Color.FromArgb(60, 60, 60));

            int y = padding;
            foreach (var (status, count, color) in statusBars)
            {
                int barHeight = Math.Max(8, rowHeight - 8);
                int barY = y + (rowHeight - barHeight) / 2;

                // Status label (left)
                var labelRect = new RectangleF(padding, barY, labelWidth - 6, barHeight);
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(status, labelFont, textBrush, labelRect, sf);

                // Track (background) + value bar
                int trackX = padding + labelWidth;
                using var trackBrush = new SolidBrush(Color.FromArgb(230, 232, 235));
                g.FillRectangle(trackBrush, trackX, barY, barAreaWidth, barHeight);

                int barWidth = (int)((double)count / maxCount * barAreaWidth);
                if (count > 0 && barWidth < 4) barWidth = 4;
                using var barBrush = new SolidBrush(color);
                g.FillRectangle(barBrush, trackX, barY, barWidth, barHeight);

                // Count value (right)
                var valueRect = new RectangleF(trackX + barAreaWidth + 4, barY, valueWidth - 4, barHeight);
                var vf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(count.ToString(), valueFont, textBrush, valueRect, vf);

                y += rowHeight;
            }
        }

        private void SafeReload()
        {
            if (InvokeRequired) { BeginInvoke(new Action(LoadRequests)); return; }
            LoadRequests();
        }
    }
}
