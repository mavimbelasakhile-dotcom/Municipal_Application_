namespace AdvancedProgrammingAss1
{
    partial class ReportHistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.lblTotalReports = new Label();
            this.dgvReports = new DataGridView();
            this.lblNoReports = new Label();
            this.pnlBottom = new Panel();
            this.btnExport = new Button();
            this.btnClose = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();

            // ========== HEADER ==========
            this.pnlHeader.BackColor = Color.FromArgb(139, 0, 0);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(900, 60);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(20, 12);
            this.lblTitle.Text = "\uD83D\uDCC4  Report History";
            this.pnlHeader.Controls.Add(this.lblTitle);

            this.lblTotalReports.AutoSize = true;
            this.lblTotalReports.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblTotalReports.ForeColor = Color.FromArgb(255, 200, 200);
            this.lblTotalReports.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblTotalReports.Location = new Point(720, 20);
            this.lblTotalReports.Text = "Total Reports: 0";
            this.pnlHeader.Controls.Add(this.lblTotalReports);

            // ========== DATAGRIDVIEW ==========
            this.dgvReports.AllowUserToAddRows = false;
            this.dgvReports.AllowUserToDeleteRows = false;
            this.dgvReports.AllowUserToResizeRows = false;
            this.dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReports.BackgroundColor = Color.White;
            this.dgvReports.BorderStyle = BorderStyle.None;
            this.dgvReports.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvReports.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvReports.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(50, 50, 50),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0)
            };
            this.dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReports.ColumnHeadersHeight = 35;
            this.dgvReports.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9F),
                SelectionBackColor = Color.FromArgb(220, 230, 240),
                SelectionForeColor = Color.Black,
                Padding = new Padding(5, 0, 0, 0)
            };
            this.dgvReports.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 248, 248)
            };
            this.dgvReports.Dock = DockStyle.Fill;
            this.dgvReports.EnableHeadersVisualStyles = false;
            this.dgvReports.GridColor = Color.FromArgb(230, 230, 230);
            this.dgvReports.Location = new Point(0, 60);
            this.dgvReports.MultiSelect = false;
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.ReadOnly = true;
            this.dgvReports.RowHeadersVisible = false;
            this.dgvReports.RowTemplate.Height = 30;
            this.dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvReports.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvReports_CellDoubleClick);

            // Add columns
            this.dgvReports.Columns.Add("colRef", "Reference #");
            this.dgvReports.Columns.Add("colDate", "Date");
            this.dgvReports.Columns.Add("colLocation", "Location");
            this.dgvReports.Columns.Add("colCategory", "Category");
            this.dgvReports.Columns.Add("colPriority", "Priority");
            this.dgvReports.Columns.Add("colStatus", "Status");
            this.dgvReports.Columns.Add("colFiles", "Files");

            this.dgvReports.Columns["colRef"].FillWeight = 15;
            this.dgvReports.Columns["colDate"].FillWeight = 15;
            this.dgvReports.Columns["colLocation"].FillWeight = 22;
            this.dgvReports.Columns["colCategory"].FillWeight = 16;
            this.dgvReports.Columns["colPriority"].FillWeight = 12;
            this.dgvReports.Columns["colStatus"].FillWeight = 10;
            this.dgvReports.Columns["colFiles"].FillWeight = 8;

            // ========== NO REPORTS LABEL ==========
            this.lblNoReports.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblNoReports.ForeColor = Color.FromArgb(120, 120, 120);
            this.lblNoReports.Location = new Point(250, 200);
            this.lblNoReports.Name = "lblNoReports";
            this.lblNoReports.Size = new Size(400, 50);
            this.lblNoReports.Text = "No reports have been submitted yet.\nSubmit a report to see it here.";
            this.lblNoReports.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNoReports.Visible = false;

            // ========== BOTTOM PANEL ==========
            this.pnlBottom.BackColor = Color.FromArgb(245, 245, 245);
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new Size(900, 55);

            this.btnExport.BackColor = Color.FromArgb(40, 167, 69);
            this.btnExport.Cursor = Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnExport.ForeColor = Color.White;
            this.btnExport.Location = new Point(20, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(200, 38);
            this.btnExport.Text = "\uD83D\uDCBE  Export to File";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new EventHandler(this.btnExport_Click);

            this.btnClose.BackColor = Color.FromArgb(60, 60, 60);
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(680, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(200, 38);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            this.pnlBottom.Controls.Add(this.btnExport);
            this.pnlBottom.Controls.Add(this.btnClose);

            // ========== FORM ==========
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(900, 500);
            this.Controls.Add(this.dgvReports);
            this.Controls.Add(this.lblNoReports);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportHistoryForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Report History - City of uMhlathuze";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblTotalReports;
        private DataGridView dgvReports;
        private Label lblNoReports;
        private Panel pnlBottom;
        private Button btnExport;
        private Button btnClose;
    }
}
