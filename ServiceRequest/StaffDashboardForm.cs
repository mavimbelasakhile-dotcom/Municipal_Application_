using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Staff view: shows only the requests assigned to this staff member, and lets
    /// them advance each one through the lifecycle (In Progress, On Hold, Resolved,
    /// Closed). Every change notifies the citizen who logged the request.
    /// On login, the staff member sees "a new request has been assigned to you".
    /// </summary>
    public class StaffDashboardForm : Form
    {
        private static readonly Color Brand = Color.FromArgb(139, 0, 0);
        private static readonly Color Green = Color.FromArgb(40, 167, 69);

        private readonly UserAccount staff;
        private DataGridView grid = null!;
        private ComboBox cmbNextStatus = null!;
        private Label lblEmpty = null!;
        private Label lblSummary = null!;

        public StaffDashboardForm(UserAccount staffMember)
        {
            staff = staffMember;
            BuildUi();
            NotificationService.NotificationsChanged += SafeReload;
            FormClosed += (s, e) => NotificationService.NotificationsChanged -= SafeReload;

            Load += (s, e) => NotificationPopup.ShowUnreadOnLogin(staff);
        }

        private void BuildUi()
        {
            Text = "Staff Dashboard";
            Size = new Size(1000, 660);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(840, 560);

            Panel header = new Panel { BackColor = Green, Dock = DockStyle.Top, Height = 80 };
            Controls.Add(header);

            Label lblTitle = new Label
            {
                Text = $"\uD83D\uDD27  {staff.DisplayName} — {staff.Department}",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 14),
                AutoSize = true
            };
            header.Controls.Add(lblTitle);

            lblSummary = new Label
            {
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(225, 255, 235),
                Location = new Point(22, 48),
                AutoSize = true
            };
            header.Controls.Add(lblSummary);

            // Status change bar
            Label lblMove = new Label
            {
                Text = "Move selected request to:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Brand,
                Location = new Point(20, 100),
                AutoSize = true
            };
            Controls.Add(lblMove);

            cmbNextStatus = new ComboBox
            {
                Location = new Point(225, 97),
                Size = new Size(240, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            Controls.Add(cmbNextStatus);

            Button btnUpdate = new Button
            {
                Text = "Update Status",
                BackColor = Brand,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(170, 34),
                Location = new Point(485, 95),
                Cursor = Cursors.Hand
            };
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += BtnUpdate_Click;
            Controls.Add(btnUpdate);

            grid = new DataGridView
            {
                Location = new Point(20, 145),
                Size = new Size(940, 420),
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
            grid.Columns.Add("Priority", "Priority");
            grid.Columns.Add("Location", "Location");
            grid.Columns.Add("Reporter", "Logged By");
            grid.Columns.Add("Status", "Status");
            grid.SelectionChanged += (s, e) => PopulateNextStatuses();
            grid.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var mine = ServiceRequestManager.GetAssignedTo(staff.Username);
                if (e.RowIndex < mine.Count) RequestDetails.Show(mine[e.RowIndex]);
            };
            Controls.Add(grid);

            lblEmpty = new Label
            {
                Text = "No requests are currently assigned to you.\nYou'll be notified here when the administrator assigns one.",
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(30, 180),
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
            var mine = ServiceRequestManager.GetAssignedTo(staff.Username);

            lblEmpty.Visible = mine.Count == 0;
            grid.Visible = mine.Count > 0;

            int open = 0;
            foreach (var r in mine)
            {
                if (r.Status != ServiceRequestManager.StatusClosed &&
                    r.Status != ServiceRequestManager.StatusResolved) open++;

                int i = grid.Rows.Add(
                    r.ReferenceNumber,
                    r.ReportDate.ToString("dd/MM/yyyy HH:mm"),
                    r.Category,
                    r.Priority,
                    r.Location,
                    string.IsNullOrWhiteSpace(r.ReporterName) ? "Unknown" : r.ReporterName,
                    r.Status);

                grid.Rows[i].Cells[6].Style.ForeColor = ServiceRequestManager.ColorFor(r.Status);
                grid.Rows[i].Cells[6].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            lblSummary.Text = $"{mine.Count} request(s) assigned to you  •  {open} still open";
            PopulateNextStatuses();
        }

        private void PopulateNextStatuses()
        {
            cmbNextStatus.Items.Clear();
            if (grid.SelectedRows.Count == 0) return;

            var mine = ServiceRequestManager.GetAssignedTo(staff.Username);
            int idx = grid.SelectedRows[0].Index;
            if (idx >= mine.Count) return;

            foreach (var s in ServiceRequestManager.GetNextStatuses(mine[idx].Status))
                cmbNextStatus.Items.Add(s);

            if (cmbNextStatus.Items.Count > 0) cmbNextStatus.SelectedIndex = 0;
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a request to update.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNextStatus.SelectedItem is not string nextStatus)
            {
                MessageBox.Show("This request has no further status it can move to.",
                    "No Transition Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var mine = ServiceRequestManager.GetAssignedTo(staff.Username);
            int idx = grid.SelectedRows[0].Index;
            if (idx >= mine.Count) return;

            var request = mine[idx];
            ServiceRequestManager.ChangeStatus(request, nextStatus, staff);

            MessageBox.Show(
                $"Request {request.ReferenceNumber} moved to \"{nextStatus}\".\n\n" +
                $"{request.ReporterName} has been notified of the update.",
                "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadRequests();
        }

        private void SafeReload()
        {
            if (InvokeRequired) { BeginInvoke(new Action(LoadRequests)); return; }
            LoadRequests();
        }
    }
}
