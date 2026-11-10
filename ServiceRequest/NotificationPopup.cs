using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Helper that shows a user's unread notifications on login as a branded popup,
    /// then marks them read. Shared by all three dashboards.
    /// </summary>
    public static class NotificationPopup
    {
        private static readonly Color Brand = Color.FromArgb(139, 0, 0);

        /// <summary>
        /// Shows any unread alerts for the user. Returns true if something was shown.
        /// </summary>
        public static bool ShowUnreadOnLogin(UserAccount user)
        {
            var unread = NotificationService.GetUnreadFor(user.Username);
            if (unread.Count == 0)
                return false;

            using Form dlg = new Form
            {
                Text = "New Notifications",
                Size = new Size(560, 430),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            Panel top = new Panel { BackColor = Brand, Dock = DockStyle.Top, Height = 64 };
            dlg.Controls.Add(top);

            Label lblTitle = new Label
            {
                Text = $"\uD83D\uDD14  You have {unread.Count} new notification(s)",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 18),
                AutoSize = true
            };
            top.Controls.Add(lblTitle);

            ListBox list = new ListBox
            {
                Location = new Point(20, 80),
                Size = new Size(510, 270),
                Font = new Font("Segoe UI", 10F),
                HorizontalScrollbar = true
            };
            foreach (var n in unread)
                list.Items.Add($"[{n.Timestamp:dd/MM HH:mm}]  {n.Message}");
            dlg.Controls.Add(list);

            Button ok = new Button
            {
                Text = "Got it",
                BackColor = Brand,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Size = new Size(510, 42),
                Location = new Point(20, 358),
                DialogResult = DialogResult.OK,
                Cursor = Cursors.Hand
            };
            ok.FlatAppearance.BorderSize = 0;
            dlg.Controls.Add(ok);
            dlg.AcceptButton = ok;

            dlg.ShowDialog();
            NotificationService.MarkAllRead(user.Username);
            return true;
        }
    }
}
