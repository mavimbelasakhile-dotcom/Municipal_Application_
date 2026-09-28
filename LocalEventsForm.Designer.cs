namespace AdvancedProgrammingAss1
{
    partial class LocalEventsForm
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
            this.lblHeaderContact = new Label();
            this.pnlBranding = new Panel();
            this.picLogo = new PictureBox();
            this.lblTitle = new Label();
            this.pnlFeatured = new Panel();
            this.lblFeaturedTitle = new Label();
            this.lblFeaturedDetails = new Label();
            this.pnlSearch = new Panel();
            this.lblSearchTitle = new Label();
            this.lblCategoryFilter = new Label();
            this.cmbSearchCategory = new ComboBox();
            this.chkFilterByDate = new CheckBox();
            this.dtpSearchDate = new DateTimePicker();
            this.btnSearch = new Button();
            this.btnClearSearch = new Button();
            this.lblResultCount = new Label();
            this.pnlEventList = new Panel();
            this.pnlFooter = new Panel();
            this.lblStats = new Label();
            this.btnBackToMenu = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // ========== HEADER ==========
            this.pnlHeader.BackColor = Color.FromArgb(139, 0, 0);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Size = new Size(1000, 35);
            this.lblHeaderContact.AutoSize = true;
            this.lblHeaderContact.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblHeaderContact.ForeColor = Color.White;
            this.lblHeaderContact.Location = new Point(15, 8);
            this.lblHeaderContact.Text = "\u260E 0800 222 827  |  035 907 5000  |  \u2709 talk2us@umhlathuze.gov.za";
            this.pnlHeader.Controls.Add(this.lblHeaderContact);

            // ========== BRANDING ==========
            this.pnlBranding.BackColor = Color.White;
            this.pnlBranding.Dock = DockStyle.Top;
            this.pnlBranding.Location = new Point(0, 35);
            this.pnlBranding.Size = new Size(1000, 70);

            this.picLogo.Location = new Point(10, 5);
            this.picLogo.Size = new Size(320, 60);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = Color.Transparent;
            try
            {
                string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "umhlathuze_logo.png");
                if (File.Exists(logoPath))
                    this.picLogo.Image = Image.FromFile(logoPath);
            }
            catch { }
            this.pnlBranding.Controls.Add(this.picLogo);

            // ========== TITLE ==========
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(25, 115);
            this.lblTitle.Text = "\uD83D\uDCC5 Local Events & Announcements";

            // ========== FEATURED BANNER ==========
            this.pnlFeatured.BackColor = Color.FromArgb(139, 0, 0);
            this.pnlFeatured.Location = new Point(25, 150);
            this.pnlFeatured.Size = new Size(945, 75);

            this.lblFeaturedTitle.AutoSize = true;
            this.lblFeaturedTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblFeaturedTitle.ForeColor = Color.White;
            this.lblFeaturedTitle.Location = new Point(15, 10);
            this.lblFeaturedTitle.Text = "Featured";
            this.pnlFeatured.Controls.Add(this.lblFeaturedTitle);

            this.lblFeaturedDetails.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblFeaturedDetails.ForeColor = Color.FromArgb(255, 220, 220);
            this.lblFeaturedDetails.Location = new Point(15, 36);
            this.lblFeaturedDetails.Size = new Size(915, 34);
            this.lblFeaturedDetails.Text = "";
            this.pnlFeatured.Controls.Add(this.lblFeaturedDetails);

            // ========== SEARCH PANEL ==========
            this.pnlSearch.BackColor = Color.White;
            this.pnlSearch.Location = new Point(25, 235);
            this.pnlSearch.Size = new Size(945, 70);

            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblSearchTitle.ForeColor = Color.FromArgb(139, 0, 0);
            this.lblSearchTitle.Location = new Point(12, 8);
            this.lblSearchTitle.Text = "\uD83D\uDD0D SEARCH EVENTS";
            this.pnlSearch.Controls.Add(this.lblSearchTitle);

            this.lblCategoryFilter.AutoSize = true;
            this.lblCategoryFilter.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblCategoryFilter.Location = new Point(12, 36);
            this.lblCategoryFilter.Text = "Category:";
            this.pnlSearch.Controls.Add(this.lblCategoryFilter);

            this.cmbSearchCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSearchCategory.FlatStyle = FlatStyle.Flat;
            this.cmbSearchCategory.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.cmbSearchCategory.Location = new Point(80, 33);
            this.cmbSearchCategory.Size = new Size(180, 28);
            this.pnlSearch.Controls.Add(this.cmbSearchCategory);

            this.chkFilterByDate.AutoSize = true;
            this.chkFilterByDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.chkFilterByDate.Location = new Point(285, 36);
            this.chkFilterByDate.Text = "Filter by date:";
            this.chkFilterByDate.CheckedChanged += new EventHandler(this.chkFilterByDate_CheckedChanged);
            this.pnlSearch.Controls.Add(this.chkFilterByDate);

            this.dtpSearchDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.dtpSearchDate.Format = DateTimePickerFormat.Long;
            this.dtpSearchDate.Location = new Point(400, 33);
            this.dtpSearchDate.Size = new Size(220, 28);
            this.dtpSearchDate.Enabled = false;
            this.pnlSearch.Controls.Add(this.dtpSearchDate);

            this.btnSearch.BackColor = Color.FromArgb(139, 0, 0);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Cursor = Cursors.Hand;
            this.btnSearch.Location = new Point(650, 32);
            this.btnSearch.Size = new Size(100, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            this.pnlSearch.Controls.Add(this.btnSearch);

            this.btnClearSearch.BackColor = Color.FromArgb(108, 117, 125);
            this.btnClearSearch.FlatStyle = FlatStyle.Flat;
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClearSearch.ForeColor = Color.White;
            this.btnClearSearch.Cursor = Cursors.Hand;
            this.btnClearSearch.Location = new Point(758, 32);
            this.btnClearSearch.Size = new Size(80, 30);
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new EventHandler(this.btnClearSearch_Click);
            this.pnlSearch.Controls.Add(this.btnClearSearch);

            this.lblResultCount.AutoSize = true;
            this.lblResultCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblResultCount.ForeColor = Color.FromArgb(139, 0, 0);
            this.lblResultCount.Location = new Point(850, 40);
            this.lblResultCount.Text = "";
            this.pnlSearch.Controls.Add(this.lblResultCount);

            // ========== EVENT LIST (scrollable) ==========
            this.pnlEventList.BackColor = Color.FromArgb(38, 45, 55);
            this.pnlEventList.Location = new Point(25, 315);
            this.pnlEventList.Size = new Size(945, 300);
            this.pnlEventList.AutoScroll = true;

            // ========== FOOTER ==========
            this.pnlFooter.BackColor = Color.FromArgb(30, 30, 30);
            this.pnlFooter.Dock = DockStyle.Bottom;
            this.pnlFooter.Size = new Size(1000, 40);

            this.lblStats.AutoSize = true;
            this.lblStats.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            this.lblStats.ForeColor = Color.FromArgb(200, 200, 200);
            this.lblStats.Location = new Point(15, 12);
            this.lblStats.Text = "";
            this.pnlFooter.Controls.Add(this.lblStats);

            // ========== BACK BUTTON ==========
            this.btnBackToMenu.BackColor = Color.FromArgb(60, 60, 60);
            this.btnBackToMenu.FlatStyle = FlatStyle.Flat;
            this.btnBackToMenu.FlatAppearance.BorderSize = 0;
            this.btnBackToMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnBackToMenu.ForeColor = Color.White;
            this.btnBackToMenu.Cursor = Cursors.Hand;
            this.btnBackToMenu.Location = new Point(25, 628);
            this.btnBackToMenu.Size = new Size(200, 40);
            this.btnBackToMenu.Text = "\u2190  Back to Main Menu";
            this.btnBackToMenu.UseVisualStyleBackColor = false;
            this.btnBackToMenu.Click += new EventHandler(this.btnBackToMenu_Click);

            // ========== FORM ==========
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(44, 52, 63);
            this.ClientSize = new Size(1000, 720);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlFeatured);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlEventList);
            this.Controls.Add(this.btnBackToMenu);
            this.Controls.Add(this.pnlBranding);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LocalEventsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Local Events & Announcements - City of uMhlathuze";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderContact;
        private Panel pnlBranding;
        private PictureBox picLogo;
        private Label lblTitle;
        private Panel pnlFeatured;
        private Label lblFeaturedTitle;
        private Label lblFeaturedDetails;
        private Panel pnlSearch;
        private Label lblSearchTitle;
        private Label lblCategoryFilter;
        private ComboBox cmbSearchCategory;
        private CheckBox chkFilterByDate;
        private DateTimePicker dtpSearchDate;
        private Button btnSearch;
        private Button btnClearSearch;
        private Label lblResultCount;
        private Panel pnlEventList;
        private Panel pnlFooter;
        private Label lblStats;
        private Button btnBackToMenu;
    }
}
