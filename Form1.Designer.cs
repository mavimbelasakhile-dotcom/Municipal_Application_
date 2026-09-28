namespace AdvancedProgrammingAss1
{
    partial class Form1
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
            this.lblDateTime = new Label();
            this.pnlBranding = new Panel();
            this.lblMunicipalityName = new Label();
            this.lblSlogan = new Label();
            this.pnlContent = new Panel();
            this.lblWelcome = new Label();
            this.lblInstruction = new Label();
            this.btnReportIssues = new Button();
            this.lblReportBadge = new Label();
            this.btnLocalEvents = new Button();
            this.btnServiceRequest = new Button();
            this.btnViewReports = new Button();
            this.pnlFooter = new Panel();
            this.lblFooter = new Label();
            this.SuspendLayout();

            // ========== HEADER ==========
            this.pnlHeader.BackColor = Color.FromArgb(139, 0, 0);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(900, 35);

            this.lblHeaderContact.AutoSize = true;
            this.lblHeaderContact.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblHeaderContact.ForeColor = Color.White;
            this.lblHeaderContact.Location = new Point(15, 8);
            this.lblHeaderContact.Name = "lblHeaderContact";
            this.lblHeaderContact.Text = "\u260E 0800 222 827  |  035 907 5000  |  \u2709 talk2us@umhlathuze.gov.za";
            this.pnlHeader.Controls.Add(this.lblHeaderContact);

            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblDateTime.ForeColor = Color.FromArgb(255, 200, 200);
            this.lblDateTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblDateTime.Location = new Point(580, 8);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Text = "";
            this.pnlHeader.Controls.Add(this.lblDateTime);

            // ========== BRANDING ==========
            this.pnlBranding.BackColor = Color.White;
            this.pnlBranding.Dock = DockStyle.Top;
            this.pnlBranding.Location = new Point(0, 35);
            this.pnlBranding.Name = "pnlBranding";
            this.pnlBranding.Size = new Size(900, 80);

            // picLogo - horizontal logo with crest + text
            this.picLogo = new PictureBox();
            this.picLogo.Location = new Point(10, 5);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(350, 70);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = Color.Transparent;
            try
            {
                string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "umhlathuze_logo.png");
                if (File.Exists(logoPath))
                    this.picLogo.Image = Image.FromFile(logoPath);
            }
            catch { }

            this.lblMunicipalityName.AutoSize = true;
            this.lblMunicipalityName.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblMunicipalityName.ForeColor = Color.FromArgb(139, 0, 0);
            this.lblMunicipalityName.Location = new Point(85, 10);
            this.lblMunicipalityName.Name = "lblMunicipalityName";
            this.lblMunicipalityName.Text = "CITY OF uMHLATHUZE";
            this.lblMunicipalityName.Visible = false;

            this.lblSlogan.AutoSize = true;
            this.lblSlogan.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblSlogan.ForeColor = Color.FromArgb(100, 100, 100);
            this.lblSlogan.Location = new Point(89, 52);
            this.lblSlogan.Name = "lblSlogan";
            this.lblSlogan.Text = "Vision Into Action";
            this.lblSlogan.Visible = false;

            this.pnlBranding.Controls.Add(this.picLogo);
            this.pnlBranding.Controls.Add(this.lblMunicipalityName);
            this.pnlBranding.Controls.Add(this.lblSlogan);

            // ========== CONTENT ==========
            this.pnlContent.BackColor = Color.FromArgb(44, 52, 63);
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblWelcome.ForeColor = Color.White;
            this.lblWelcome.Location = new Point(50, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Text = "Welcome";

            // lblInstruction
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblInstruction.ForeColor = Color.FromArgb(180, 190, 200);
            this.lblInstruction.Location = new Point(50, 58);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Text = "Select a service below to engage with your municipality:";

            // btnReportIssues
            this.btnReportIssues.BackColor = Color.FromArgb(139, 0, 0);
            this.btnReportIssues.Cursor = Cursors.Hand;
            this.btnReportIssues.FlatAppearance.BorderSize = 0;
            this.btnReportIssues.FlatAppearance.MouseOverBackColor = Color.FromArgb(165, 0, 0);
            this.btnReportIssues.FlatStyle = FlatStyle.Flat;
            this.btnReportIssues.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnReportIssues.ForeColor = Color.White;
            this.btnReportIssues.Location = new Point(50, 100);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new Size(380, 70);
            this.btnReportIssues.TabIndex = 0;
            this.btnReportIssues.Text = "\uD83D\uDCCB  Report Issues";
            this.btnReportIssues.TextAlign = ContentAlignment.MiddleLeft;
            this.btnReportIssues.Padding = new Padding(20, 0, 0, 0);
            this.btnReportIssues.UseVisualStyleBackColor = false;
            this.btnReportIssues.Click += new EventHandler(this.btnReportIssues_Click);

            // lblReportBadge
            this.lblReportBadge.AutoSize = true;
            this.lblReportBadge.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblReportBadge.ForeColor = Color.FromArgb(255, 180, 180);
            this.lblReportBadge.Location = new Point(440, 130);
            this.lblReportBadge.Name = "lblReportBadge";
            this.lblReportBadge.Text = "";
            this.lblReportBadge.Visible = false;

            // btnLocalEvents
            this.btnLocalEvents.BackColor = Color.FromArgb(139, 0, 0);
            this.btnLocalEvents.Cursor = Cursors.Hand;
            this.btnLocalEvents.FlatAppearance.BorderSize = 0;
            this.btnLocalEvents.FlatAppearance.MouseOverBackColor = Color.FromArgb(165, 0, 0);
            this.btnLocalEvents.FlatStyle = FlatStyle.Flat;
            this.btnLocalEvents.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnLocalEvents.ForeColor = Color.White;
            this.btnLocalEvents.Location = new Point(50, 185);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new Size(380, 70);
            this.btnLocalEvents.TabIndex = 1;
            this.btnLocalEvents.Text = "\uD83D\uDCC5  Local Events & Announcements";
            this.btnLocalEvents.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLocalEvents.Padding = new Padding(20, 0, 0, 0);
            this.btnLocalEvents.UseVisualStyleBackColor = false;
            this.btnLocalEvents.Click += new EventHandler(this.btnLocalEvents_Click);

            // btnServiceRequest
            this.btnServiceRequest.BackColor = Color.FromArgb(200, 200, 200);
            this.btnServiceRequest.Enabled = false;
            this.btnServiceRequest.FlatAppearance.BorderSize = 0;
            this.btnServiceRequest.FlatStyle = FlatStyle.Flat;
            this.btnServiceRequest.Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnServiceRequest.ForeColor = Color.FromArgb(120, 120, 120);
            this.btnServiceRequest.Location = new Point(50, 270);
            this.btnServiceRequest.Name = "btnServiceRequest";
            this.btnServiceRequest.Size = new Size(380, 70);
            this.btnServiceRequest.TabIndex = 2;
            this.btnServiceRequest.Text = "\uD83D\uDD0D  Service Request Status";
            this.btnServiceRequest.TextAlign = ContentAlignment.MiddleLeft;
            this.btnServiceRequest.Padding = new Padding(20, 0, 0, 0);
            this.btnServiceRequest.UseVisualStyleBackColor = false;
            this.btnServiceRequest.Click += new EventHandler(this.btnServiceRequest_Click);

            // btnViewReports
            this.btnViewReports.BackColor = Color.FromArgb(60, 60, 60);
            this.btnViewReports.Cursor = Cursors.Hand;
            this.btnViewReports.FlatAppearance.BorderSize = 0;
            this.btnViewReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            this.btnViewReports.FlatStyle = FlatStyle.Flat;
            this.btnViewReports.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnViewReports.ForeColor = Color.White;
            this.btnViewReports.Location = new Point(50, 360);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Size = new Size(380, 45);
            this.btnViewReports.TabIndex = 3;
            this.btnViewReports.Text = "\uD83D\uDCC4  View Report History";
            this.btnViewReports.TextAlign = ContentAlignment.MiddleLeft;
            this.btnViewReports.Padding = new Padding(20, 0, 0, 0);
            this.btnViewReports.UseVisualStyleBackColor = false;
            this.btnViewReports.Click += new EventHandler(this.btnViewReports_Click);

            this.pnlContent.Controls.Add(this.lblWelcome);
            this.pnlContent.Controls.Add(this.lblInstruction);
            this.pnlContent.Controls.Add(this.btnReportIssues);
            this.pnlContent.Controls.Add(this.lblReportBadge);
            this.pnlContent.Controls.Add(this.btnLocalEvents);
            this.pnlContent.Controls.Add(this.btnServiceRequest);
            this.pnlContent.Controls.Add(this.btnViewReports);

            // ========== FOOTER ==========
            this.pnlFooter.BackColor = Color.FromArgb(30, 30, 30);
            this.pnlFooter.Dock = DockStyle.Bottom;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new Size(900, 40);

            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblFooter.ForeColor = Color.FromArgb(180, 180, 180);
            this.lblFooter.Location = new Point(15, 12);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Text = "\u00A9 2026 City of uMhlathuze. All rights reserved.  |  www.umhlathuze.gov.za";
            this.pnlFooter.Controls.Add(this.lblFooter);

            // ========== FORM ==========
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(44, 52, 63);
            this.ClientSize = new Size(900, 560);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlBranding);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "City of uMhlathuze - Municipal Services";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderContact;
        private Label lblDateTime;
        private Panel pnlBranding;
        private PictureBox picLogo;
        private Label lblMunicipalityName;
        private Label lblSlogan;
        private Panel pnlContent;
        private Label lblWelcome;
        private Label lblInstruction;
        private Button btnReportIssues;
        private Label lblReportBadge;
        private Button btnLocalEvents;
        private Button btnServiceRequest;
        private Button btnViewReports;
        private Panel pnlFooter;
        private Label lblFooter;
    }
}
