namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Custom branded dialog forms to replace default MessageBox.
    /// </summary>
    public static class CustomDialog
    {
        /// <summary>
        /// Shows a confirmation dialog with Yes/No buttons in uMhlathuze branding.
        /// </summary>
        public static DialogResult ShowConfirmation(string title, string message, string details)
        {
            Form dialog = new Form();
            dialog.Text = title;
            dialog.Size = new Size(550, 360);
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.FormBorderStyle = FormBorderStyle.None;
            dialog.BackColor = Color.White;
            dialog.ShowInTaskbar = false;

            // Border
            Panel pnlBorder = new Panel();
            pnlBorder.BackColor = Color.FromArgb(180, 180, 180);
            pnlBorder.Dock = DockStyle.Fill;
            dialog.Controls.Add(pnlBorder);

            Panel pnlInner = new Panel();
            pnlInner.BackColor = Color.White;
            pnlInner.Location = new Point(1, 1);
            pnlInner.Size = new Size(548, 358);
            pnlBorder.Controls.Add(pnlInner);

            // Top accent bar
            Panel pnlTop = new Panel();
            pnlTop.BackColor = Color.FromArgb(139, 0, 0);
            pnlTop.Location = new Point(0, 0);
            pnlTop.Size = new Size(548, 6);
            pnlInner.Controls.Add(pnlTop);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.FromArgb(139, 0, 0);
            lblTitle.Location = new Point(25, 20);
            lblTitle.AutoSize = true;
            pnlInner.Controls.Add(lblTitle);

            // Message
            Label lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblMessage.ForeColor = Color.FromArgb(60, 60, 60);
            lblMessage.Location = new Point(25, 55);
            lblMessage.AutoSize = true;
            pnlInner.Controls.Add(lblMessage);

            // Details box
            TextBox txtDetails = new TextBox();
            txtDetails.Multiline = true;
            txtDetails.ReadOnly = true;
            txtDetails.BackColor = Color.FromArgb(248, 248, 250);
            txtDetails.BorderStyle = BorderStyle.FixedSingle;
            txtDetails.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtDetails.ForeColor = Color.FromArgb(40, 40, 40);
            txtDetails.Location = new Point(25, 90);
            txtDetails.Size = new Size(498, 160);
            txtDetails.Text = details.Replace("\n", Environment.NewLine);
            txtDetails.ScrollBars = ScrollBars.Vertical;
            txtDetails.SelectionStart = 0;
            txtDetails.SelectionLength = 0;
            pnlInner.Controls.Add(txtDetails);

            // Yes button
            Button btnYes = new Button();
            btnYes.Text = "Yes, Submit";
            btnYes.BackColor = Color.FromArgb(139, 0, 0);
            btnYes.ForeColor = Color.White;
            btnYes.FlatStyle = FlatStyle.Flat;
            btnYes.FlatAppearance.BorderSize = 0;
            btnYes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnYes.Size = new Size(180, 45);
            btnYes.Location = new Point(175, 265);
            btnYes.Cursor = Cursors.Hand;
            btnYes.DialogResult = DialogResult.Yes;
            pnlInner.Controls.Add(btnYes);

            // No button
            Button btnNo = new Button();
            btnNo.Text = "Cancel";
            btnNo.BackColor = Color.FromArgb(108, 117, 125);
            btnNo.ForeColor = Color.White;
            btnNo.FlatStyle = FlatStyle.Flat;
            btnNo.FlatAppearance.BorderSize = 0;
            btnNo.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnNo.Size = new Size(140, 45);
            btnNo.Location = new Point(365, 265);
            btnNo.Cursor = Cursors.Hand;
            btnNo.DialogResult = DialogResult.No;
            pnlInner.Controls.Add(btnNo);

            dialog.AcceptButton = btnYes;
            dialog.CancelButton = btnNo;

            return dialog.ShowDialog();
        }

        /// <summary>
        /// Shows a success dialog with report details in uMhlathuze branding.
        /// </summary>
        public static void ShowSuccess(string referenceNumber, string location, string category,
            string priority, int attachmentCount, DateTime reportDate, string contactMethod, string contactDetail)
        {
            Form dialog = new Form();
            dialog.Text = "Report Submitted Successfully";
            dialog.Size = new Size(580, 480);
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.FormBorderStyle = FormBorderStyle.None;
            dialog.BackColor = Color.White;
            dialog.ShowInTaskbar = false;

            // Border
            Panel pnlBorder = new Panel();
            pnlBorder.BackColor = Color.FromArgb(180, 180, 180);
            pnlBorder.Dock = DockStyle.Fill;
            dialog.Controls.Add(pnlBorder);

            Panel pnlInner = new Panel();
            pnlInner.BackColor = Color.White;
            pnlInner.Location = new Point(1, 1);
            pnlInner.Size = new Size(578, 478);
            pnlBorder.Controls.Add(pnlInner);

            // Top green accent bar
            Panel pnlTop = new Panel();
            pnlTop.BackColor = Color.FromArgb(40, 167, 69);
            pnlTop.Location = new Point(0, 0);
            pnlTop.Size = new Size(578, 6);
            pnlInner.Controls.Add(pnlTop);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Report Submitted Successfully!";
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.FromArgb(40, 167, 69);
            lblTitle.Location = new Point(25, 20);
            lblTitle.AutoSize = true;
            pnlInner.Controls.Add(lblTitle);

            // Subtitle
            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Your issue has been logged. Please save your reference number for tracking.";
            lblSubtitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubtitle.ForeColor = Color.FromArgb(100, 100, 100);
            lblSubtitle.Location = new Point(25, 55);
            lblSubtitle.AutoSize = true;
            pnlInner.Controls.Add(lblSubtitle);

            // Reference number highlight box
            Panel pnlRef = new Panel();
            pnlRef.BackColor = Color.FromArgb(139, 0, 0);
            pnlRef.Location = new Point(25, 85);
            pnlRef.Size = new Size(528, 50);
            pnlInner.Controls.Add(pnlRef);

            Label lblRef = new Label();
            lblRef.Text = $"Reference Number:   {referenceNumber}";
            lblRef.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblRef.ForeColor = Color.White;
            lblRef.Location = new Point(20, 12);
            lblRef.AutoSize = true;
            pnlRef.Controls.Add(lblRef);

            // Details section
            int y = 155;
            int labelX = 30;
            int valueX = 160;

            string contactInfo = string.IsNullOrWhiteSpace(contactDetail) ? "Not provided" : $"{contactMethod} - {contactDetail}";

            string[,] rows = {
                { "Location:", location },
                { "Category:", category },
                { "Priority:", priority },
                { "Attachments:", $"{attachmentCount} file(s)" },
                { "Date:", reportDate.ToString("dd MMMM yyyy, HH:mm") },
                { "Contact:", contactInfo }
            };

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                Label lbl = new Label();
                lbl.Text = rows[i, 0];
                lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
                lbl.ForeColor = Color.FromArgb(60, 60, 60);
                lbl.Location = new Point(labelX, y);
                lbl.AutoSize = true;
                pnlInner.Controls.Add(lbl);

                Label val = new Label();
                val.Text = rows[i, 1];
                val.UseMnemonic = false;
                val.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
                val.ForeColor = Color.FromArgb(30, 30, 30);
                val.Location = new Point(valueX, y);
                val.AutoSize = true;
                pnlInner.Controls.Add(val);

                y += 32;
            }

            // Separator line
            Panel pnlLine = new Panel();
            pnlLine.BackColor = Color.FromArgb(220, 220, 220);
            pnlLine.Location = new Point(25, y + 5);
            pnlLine.Size = new Size(528, 1);
            pnlInner.Controls.Add(pnlLine);

            // Thank you message
            Label lblThanks = new Label();
            lblThanks.Text = "Thank you for helping improve our community!";
            lblThanks.Font = new Font("Segoe UI", 10.5F, FontStyle.Italic, GraphicsUnit.Point);
            lblThanks.ForeColor = Color.FromArgb(139, 0, 0);
            lblThanks.Location = new Point(25, y + 18);
            lblThanks.AutoSize = true;
            pnlInner.Controls.Add(lblThanks);

            // Close button
            Button btnClose = new Button();
            btnClose.Text = "Done";
            btnClose.BackColor = Color.FromArgb(40, 167, 69);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Size = new Size(528, 45);
            btnClose.Location = new Point(25, y + 52);
            btnClose.Cursor = Cursors.Hand;
            btnClose.DialogResult = DialogResult.OK;
            pnlInner.Controls.Add(btnClose);

            dialog.AcceptButton = btnClose;
            dialog.ShowDialog();
        }
    }
}
