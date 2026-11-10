using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Role-based login screen for the Service Request module. Three buttons let
    /// the demo operator log in as a Citizen, the Administrator, or a Staff member.
    /// No passwords / no database — users are seeded in <see cref="UserDirectory"/>.
    /// </summary>
    public class LoginForm : Form
    {
        private static readonly Color Brand = Color.FromArgb(139, 0, 0);
        private static readonly Color Green = Color.FromArgb(40, 167, 69);
        private static readonly Color Blue = Color.FromArgb(0, 123, 255);

        public LoginForm()
        {
            BuildUi();
        }

        private void BuildUi()
        {
            Text = "Service Requests — Login";
            Size = new Size(560, 540);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.White;

            // Header accent
            Panel header = new Panel
            {
                BackColor = Brand,
                Dock = DockStyle.Top,
                Height = 90
            };
            Controls.Add(header);

            Label lblTitle = new Label
            {
                Text = "City of uMhlathuze — Service Requests",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 20),
                AutoSize = true
            };
            header.Controls.Add(lblTitle);

            Label lblSub = new Label
            {
                Text = "Select how you would like to sign in",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(255, 220, 220),
                Location = new Point(27, 55),
                AutoSize = true
            };
            header.Controls.Add(lblSub);

            // Citizen
            AddRoleButton(
                "\uD83D\uDC64  Login as Citizen",
                "Log service requests and track their progress",
                Brand, 120, () => LoginAsCitizen());

            // Administrator
            AddRoleButton(
                "\uD83D\uDEE1\uFE0F  Login as Administrator",
                "Assign requests to staff and oversee all work",
                Blue, 250, () => LoginAsAdministrator());

            // Staff
            AddRoleButton(
                "\uD83D\uDD27  Login as Staff",
                "View requests assigned to you and update status",
                Green, 380, () => LoginAsStaff());
        }

        private void AddRoleButton(string title, string subtitle, Color color, int y, Action onClick)
        {
            Button btn = new Button
            {
                Text = title,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                Size = new Size(490, 70),
                Location = new Point(25, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onClick();
            Controls.Add(btn);

            Label lbl = new Label
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(110, 110, 110),
                Location = new Point(28, y + 72),
                AutoSize = true
            };
            Controls.Add(lbl);
        }

        // ----- Login actions -----

        private void LoginAsCitizen()
        {
            var user = PickUser("Select Citizen", "Who is logging in?", UserDirectory.GetCitizens());
            if (user == null) return;

            UserDirectory.CurrentUser = user;
            var dash = new CitizenDashboardForm(user);
            Hide();
            dash.ShowDialog();
            Show();
        }

        private void LoginAsAdministrator()
        {
            var admin = UserDirectory.Administrator;
            UserDirectory.CurrentUser = admin;
            var dash = new AdminDashboardForm(admin);
            Hide();
            dash.ShowDialog();
            Show();
        }

        private void LoginAsStaff()
        {
            var user = PickUser("Select Staff Member", "Who is logging in?", UserDirectory.GetStaff());
            if (user == null) return;

            UserDirectory.CurrentUser = user;
            var dash = new StaffDashboardForm(user);
            Hide();
            dash.ShowDialog();
            Show();
        }

        /// <summary>
        /// Simple selector dialog to choose which seeded user to sign in as.
        /// </summary>
        private UserAccount? PickUser(string title, string prompt, List<UserAccount> options)
        {
            using Form dlg = new Form
            {
                Text = title,
                Size = new Size(430, 320),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            Label lbl = new Label
            {
                Text = prompt,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Brand,
                Location = new Point(20, 18),
                AutoSize = true
            };
            dlg.Controls.Add(lbl);

            ListBox list = new ListBox
            {
                Location = new Point(20, 50),
                Size = new Size(375, 170),
                Font = new Font("Segoe UI", 11F)
            };
            foreach (var u in options)
                list.Items.Add(u);
            if (list.Items.Count > 0) list.SelectedIndex = 0;
            dlg.Controls.Add(list);

            Button ok = new Button
            {
                Text = "Login",
                BackColor = Brand,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Size = new Size(180, 40),
                Location = new Point(20, 230),
                DialogResult = DialogResult.OK,
                Cursor = Cursors.Hand
            };
            ok.FlatAppearance.BorderSize = 0;
            dlg.Controls.Add(ok);

            Button cancel = new Button
            {
                Text = "Cancel",
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Size = new Size(170, 40),
                Location = new Point(225, 230),
                DialogResult = DialogResult.Cancel,
                Cursor = Cursors.Hand
            };
            cancel.FlatAppearance.BorderSize = 0;
            dlg.Controls.Add(cancel);

            dlg.AcceptButton = ok;
            dlg.CancelButton = cancel;

            return dlg.ShowDialog() == DialogResult.OK ? list.SelectedItem as UserAccount : null;
        }
    }
}
