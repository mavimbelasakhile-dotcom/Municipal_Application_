namespace AdvancedProgrammingAss1
{
    public partial class ReportHistoryForm : Form
    {
        public ReportHistoryForm()
        {
            InitializeComponent();
            LoadReports();
        }

        /// <summary>
        /// Loads all submitted reports into the DataGridView.
        /// </summary>
        private void LoadReports()
        {
            dgvReports.Rows.Clear();
            var reports = ReportedIssue.GetAllReports();

            if (reports.Count == 0)
            {
                lblNoReports.Visible = true;
                dgvReports.Visible = false;
                return;
            }

            lblNoReports.Visible = false;
            dgvReports.Visible = true;

            foreach (var report in reports)
            {
                int rowIndex = dgvReports.Rows.Add(
                    report.ReferenceNumber,
                    report.ReportDate.ToString("dd/MM/yyyy HH:mm"),
                    report.Location,
                    report.Category,
                    report.Priority,
                    report.Status,
                    report.AttachedFiles.Count.ToString()
                );

                // Colour-code priority
                DataGridViewRow row = dgvReports.Rows[rowIndex];
                switch (report.Priority)
                {
                    case "Critical":
                        row.Cells[4].Style.ForeColor = Color.FromArgb(220, 53, 69);
                        row.Cells[4].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        break;
                    case "High":
                        row.Cells[4].Style.ForeColor = Color.FromArgb(255, 140, 0);
                        row.Cells[4].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        break;
                    case "Medium":
                        row.Cells[4].Style.ForeColor = Color.FromArgb(180, 140, 0);
                        break;
                    case "Low":
                        row.Cells[4].Style.ForeColor = Color.FromArgb(40, 167, 69);
                        break;
                }
            }

            lblTotalReports.Text = $"Total Reports: {reports.Count}";
        }

        /// <summary>
        /// Exports all reports to a text file.
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ReportedIssue.GetAllReports().Count == 0)
            {
                MessageBox.Show("No reports to export.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Export Reports";
                saveDialog.Filter = "Text Files (*.txt)|*.txt";
                saveDialog.FileName = $"uMhlathuze_Reports_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string content = ReportedIssue.ExportToText();
                        File.WriteAllText(saveDialog.FileName, content);
                        MessageBox.Show($"Reports exported successfully to:\n{saveDialog.FileName}",
                            "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting reports:\n{ex.Message}",
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Shows full details of a selected report.
        /// </summary>
        private void dgvReports_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var reports = ReportedIssue.GetAllReports();
            if (e.RowIndex >= reports.Count) return;

            var report = reports[e.RowIndex];

            string attachments = report.AttachedFiles.Count > 0
                ? string.Join("\n  ", report.AttachedFiles.Select(f => Path.GetFileName(f)))
                : "None";

            MessageBox.Show(
                $"Reference: {report.ReferenceNumber}\n" +
                $"Date: {report.ReportDate:dd/MM/yyyy HH:mm}\n" +
                $"Status: {report.Status}\n" +
                $"Priority: {report.Priority}\n" +
                $"Location: {report.Location}\n" +
                $"Category: {report.Category}\n\n" +
                $"Description:\n{report.Description}\n\n" +
                $"Attachments:\n  {attachments}",
                $"Report Details - {report.ReferenceNumber}",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
