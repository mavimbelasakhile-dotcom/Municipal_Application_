namespace AdvancedProgrammingAss1
{
    partial class ReportIssuesForm
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
            this.lblMunicipalityName = new Label();
            this.lblSlogan = new Label();
            this.pnlFooter = new Panel();
            this.lblFooter = new Label();
            this.pnlContent = new Panel();

            this.lblFormTitle = new Label();
            this.lblFormSubtitle = new Label();

            // Progress/Engagement
            this.pnlEngagement = new Panel();
            this.lblEngagement = new Label();
            this.progressBar = new ProgressBar();
            this.lblProgressPercent = new Label();

            // Left column
            this.lblLocation = new Label();
            this.txtLocation = new TextBox();
            this.lstLocationSuggestions = new ListBox();
            this.pnlLocationValidator = new Panel();
            this.lblCategory = new Label();
            this.cmbCategory = new ComboBox();
            this.pnlCategoryValidator = new Panel();
            this.lblPriority = new Label();
            this.cmbPriority = new ComboBox();
            this.pnlPriorityValidator = new Panel();
            this.lblPriorityIndicator = new Label();
            this.lblDescription = new Label();
            this.rtbDescription = new RichTextBox();
            this.pnlDescriptionValidator = new Panel();
            this.lblCharCounter = new Label();

            // Right column
            this.pnlAttachments = new Panel();
            this.lblAttachments = new Label();
            this.btnAttachFile = new Button();
            this.btnRemoveFile = new Button();
            this.lstAttachedFiles = new ListBox();

            // Action buttons
            this.btnSubmit = new Button();
            this.btnBackToMenu = new Button();

            this.SuspendLayout();

            // ========== HEADER ==========
            this.pnlHeader.BackColor = Color.FromArgb(139, 0, 0);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(1000, 35);

            this.lblHeaderContact.AutoSize = true;
            this.lblHeaderContact.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblHeaderContact.ForeColor = Color.White;
            this.lblHeaderContact.Location = new Point(15, 8);
            this.lblHeaderContact.Text = "\u260E 0800 222 827  |  035 907 5000  |  \u2709 talk2us@umhlathuze.gov.za";
            this.pnlHeader.Controls.Add(this.lblHeaderContact);

            // ========== BRANDING ==========
            this.pnlBranding.BackColor = Color.White;
            this.pnlBranding.Dock = DockStyle.Top;
            this.pnlBranding.Location = new Point(0, 35);
            this.pnlBranding.Name = "pnlBranding";
            this.pnlBranding.Size = new Size(1000, 80);

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
            this.lblMunicipalityName.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblMunicipalityName.ForeColor = Color.FromArgb(139, 0, 0);
            this.lblMunicipalityName.Location = new Point(85, 12);
            this.lblMunicipalityName.Text = "CITY OF uMHLATHUZE";
            this.lblMunicipalityName.Visible = false;

            this.lblSlogan.AutoSize = true;
            this.lblSlogan.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblSlogan.ForeColor = Color.FromArgb(100, 100, 100);
            this.lblSlogan.Location = new Point(89, 48);
            this.lblSlogan.Text = "Vision Into Action";
            this.lblSlogan.Visible = false;

            this.pnlBranding.Controls.Add(this.picLogo);
            this.pnlBranding.Controls.Add(this.lblMunicipalityName);
            this.pnlBranding.Controls.Add(this.lblSlogan);

            // ========== FOOTER ==========
            this.pnlFooter.BackColor = Color.FromArgb(30, 30, 30);
            this.pnlFooter.Dock = DockStyle.Bottom;
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new Size(1000, 30);

            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblFooter.ForeColor = Color.FromArgb(180, 180, 180);
            this.lblFooter.Location = new Point(15, 8);
            this.lblFooter.Text = "\u00A9 2026 City of uMhlathuze. All rights reserved.  |  www.umhlathuze.gov.za";
            this.pnlFooter.Controls.Add(this.lblFooter);

            // ========== CONTENT ==========
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = Color.FromArgb(44, 52, 63);
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Name = "pnlContent";

            // ========== TITLE ==========
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblFormTitle.ForeColor = Color.White;
            this.lblFormTitle.Location = new Point(25, 10);
            this.lblFormTitle.Text = "Report an Issue";

            this.lblFormSubtitle.AutoSize = true;
            this.lblFormSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblFormSubtitle.ForeColor = Color.FromArgb(180, 190, 200);
            this.lblFormSubtitle.Location = new Point(27, 42);
            this.lblFormSubtitle.Text = "Help us improve your community. Fields marked with * are required.";

            // ========== PROGRESS BAR (TOP) ==========
            this.pnlEngagement.BackColor = Color.White;
            this.pnlEngagement.BorderStyle = BorderStyle.FixedSingle;
            this.pnlEngagement.Location = new Point(25, 65);
            this.pnlEngagement.Name = "pnlEngagement";
            this.pnlEngagement.Size = new Size(935, 50);

            this.lblEngagement.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblEngagement.ForeColor = Color.FromArgb(139, 0, 0);
            this.lblEngagement.Location = new Point(12, 6);
            this.lblEngagement.Name = "lblEngagement";
            this.lblEngagement.Size = new Size(500, 18);
            this.lblEngagement.Text = "";

            this.progressBar.Location = new Point(12, 27);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(855, 15);
            this.progressBar.Style = ProgressBarStyle.Continuous;

            this.lblProgressPercent.AutoSize = true;
            this.lblProgressPercent.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblProgressPercent.ForeColor = Color.FromArgb(80, 80, 80);
            this.lblProgressPercent.Location = new Point(875, 25);
            this.lblProgressPercent.Name = "lblProgressPercent";
            this.lblProgressPercent.Text = "0%";

            this.pnlEngagement.Controls.Add(this.lblEngagement);
            this.pnlEngagement.Controls.Add(this.progressBar);
            this.pnlEngagement.Controls.Add(this.lblProgressPercent);

            // ========== LOCATION ==========
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblLocation.ForeColor = Color.White;
            this.lblLocation.Location = new Point(25, 130);
            this.lblLocation.Text = "Location *";

            this.txtLocation.BackColor = Color.White;
            this.txtLocation.BorderStyle = BorderStyle.FixedSingle;
            this.txtLocation.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.txtLocation.Location = new Point(25, 155);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.PlaceholderText = "Start typing an address (e.g., Meerensee, Tanner Road...)";
            this.txtLocation.Size = new Size(400, 30);
            this.txtLocation.TabIndex = 0;
            this.txtLocation.TextChanged += new EventHandler(this.txtLocation_TextChanged);
            this.txtLocation.KeyDown += new KeyEventHandler(this.txtLocation_KeyDown);

            // Location suggestions dropdown
            this.lstLocationSuggestions.BackColor = Color.White;
            this.lstLocationSuggestions.BorderStyle = BorderStyle.FixedSingle;
            this.lstLocationSuggestions.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.lstLocationSuggestions.ItemHeight = 22;
            this.lstLocationSuggestions.Location = new Point(25, 186);
            this.lstLocationSuggestions.Name = "lstLocationSuggestions";
            this.lstLocationSuggestions.Size = new Size(400, 100);
            this.lstLocationSuggestions.Visible = false;

            this.pnlLocationValidator.BackColor = Color.FromArgb(70, 80, 90);
            this.pnlLocationValidator.Location = new Point(430, 155);
            this.pnlLocationValidator.Size = new Size(5, 30);

            // ========== CATEGORY ==========
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblCategory.ForeColor = Color.White;
            this.lblCategory.Location = new Point(25, 198);
            this.lblCategory.Text = "Category *";

            this.cmbCategory.BackColor = Color.White;
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = FlatStyle.Flat;
            this.cmbCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new Point(25, 223);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new Size(400, 31);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.SelectedIndexChanged += new EventHandler(this.cmbCategory_SelectedIndexChanged);

            this.pnlCategoryValidator.BackColor = Color.FromArgb(70, 80, 90);
            this.pnlCategoryValidator.Location = new Point(430, 223);
            this.pnlCategoryValidator.Size = new Size(5, 31);

            // ========== PRIORITY ==========
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPriority.ForeColor = Color.White;
            this.lblPriority.Location = new Point(25, 268);
            this.lblPriority.Text = "Priority *";

            this.cmbPriority.BackColor = Color.White;
            this.cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPriority.FlatStyle = FlatStyle.Flat;
            this.cmbPriority.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new Point(25, 293);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new Size(200, 31);
            this.cmbPriority.TabIndex = 2;
            this.cmbPriority.SelectedIndexChanged += new EventHandler(this.cmbPriority_SelectedIndexChanged);

            this.pnlPriorityValidator.BackColor = Color.FromArgb(70, 80, 90);
            this.pnlPriorityValidator.Location = new Point(230, 293);
            this.pnlPriorityValidator.Size = new Size(5, 31);

            this.lblPriorityIndicator.AutoSize = true;
            this.lblPriorityIndicator.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPriorityIndicator.Location = new Point(245, 298);
            this.lblPriorityIndicator.Name = "lblPriorityIndicator";
            this.lblPriorityIndicator.Text = "";

            // ========== DESCRIPTION ==========
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblDescription.ForeColor = Color.White;
            this.lblDescription.Location = new Point(25, 340);
            this.lblDescription.Text = "Description *";

            this.rtbDescription.BackColor = Color.White;
            this.rtbDescription.BorderStyle = BorderStyle.FixedSingle;
            this.rtbDescription.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.rtbDescription.Location = new Point(25, 365);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new Size(400, 140);
            this.rtbDescription.TabIndex = 3;
            this.rtbDescription.Text = "";
            this.rtbDescription.TextChanged += new EventHandler(this.rtbDescription_TextChanged);

            this.pnlDescriptionValidator.BackColor = Color.FromArgb(70, 80, 90);
            this.pnlDescriptionValidator.Location = new Point(430, 365);
            this.pnlDescriptionValidator.Size = new Size(5, 140);

            this.lblCharCounter.AutoSize = true;
            this.lblCharCounter.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblCharCounter.ForeColor = Color.FromArgb(160, 170, 180);
            this.lblCharCounter.Location = new Point(25, 508);
            this.lblCharCounter.Name = "lblCharCounter";
            this.lblCharCounter.Text = "0/1000 characters";

            // ========== ATTACHMENTS (RIGHT SIDE) ==========
            this.pnlAttachments.BackColor = Color.White;
            this.pnlAttachments.BorderStyle = BorderStyle.FixedSingle;
            this.pnlAttachments.Location = new Point(480, 130);
            this.pnlAttachments.Name = "pnlAttachments";
            this.pnlAttachments.Size = new Size(480, 205);

            this.lblAttachments.AutoSize = true;
            this.lblAttachments.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblAttachments.ForeColor = Color.FromArgb(50, 50, 50);
            this.lblAttachments.Location = new Point(15, 12);
            this.lblAttachments.Text = "\uD83D\uDCCE Attached Documents & Images";

            this.btnAttachFile.BackColor = Color.FromArgb(139, 0, 0);
            this.btnAttachFile.Cursor = Cursors.Hand;
            this.btnAttachFile.FlatAppearance.BorderSize = 0;
            this.btnAttachFile.FlatAppearance.MouseOverBackColor = Color.FromArgb(165, 0, 0);
            this.btnAttachFile.FlatStyle = FlatStyle.Flat;
            this.btnAttachFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnAttachFile.ForeColor = Color.White;
            this.btnAttachFile.Location = new Point(15, 42);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new Size(150, 34);
            this.btnAttachFile.TabIndex = 4;
            this.btnAttachFile.Text = "+ Attach File(s)";
            this.btnAttachFile.UseVisualStyleBackColor = false;
            this.btnAttachFile.Click += new EventHandler(this.btnAttachFile_Click);

            this.btnRemoveFile.BackColor = Color.FromArgb(80, 80, 80);
            this.btnRemoveFile.Cursor = Cursors.Hand;
            this.btnRemoveFile.FlatAppearance.BorderSize = 0;
            this.btnRemoveFile.FlatStyle = FlatStyle.Flat;
            this.btnRemoveFile.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnRemoveFile.ForeColor = Color.White;
            this.btnRemoveFile.Location = new Point(175, 42);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new Size(110, 34);
            this.btnRemoveFile.TabIndex = 5;
            this.btnRemoveFile.Text = "\u2716 Remove";
            this.btnRemoveFile.UseVisualStyleBackColor = false;
            this.btnRemoveFile.Click += new EventHandler(this.btnRemoveFile_Click);

            // File list container with light background
            this.pnlFileListContainer = new Panel();
            this.pnlFileListContainer.BackColor = Color.FromArgb(248, 248, 250);
            this.pnlFileListContainer.BorderStyle = BorderStyle.FixedSingle;
            this.pnlFileListContainer.Location = new Point(15, 88);
            this.pnlFileListContainer.Name = "pnlFileListContainer";
            this.pnlFileListContainer.Size = new Size(448, 100);

            this.lblNoFiles = new Label();
            this.lblNoFiles.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            this.lblNoFiles.ForeColor = Color.FromArgb(150, 150, 150);
            this.lblNoFiles.Location = new Point(10, 38);
            this.lblNoFiles.Size = new Size(428, 20);
            this.lblNoFiles.Text = "No files attached yet. Supported: JPG, PNG, PDF, DOC, TXT.";
            this.lblNoFiles.TextAlign = ContentAlignment.MiddleCenter;

            this.lstAttachedFiles.BorderStyle = BorderStyle.None;
            this.lstAttachedFiles.BackColor = Color.FromArgb(248, 248, 250);
            this.lstAttachedFiles.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.lstAttachedFiles.FormattingEnabled = true;
            this.lstAttachedFiles.ItemHeight = 20;
            this.lstAttachedFiles.Location = new Point(4, 4);
            this.lstAttachedFiles.Name = "lstAttachedFiles";
            this.lstAttachedFiles.Size = new Size(438, 90);
            this.lstAttachedFiles.TabIndex = 6;

            this.pnlFileListContainer.Controls.Add(this.lstAttachedFiles);
            this.pnlFileListContainer.Controls.Add(this.lblNoFiles);

            this.pnlAttachments.Controls.Add(this.lblAttachments);
            this.pnlAttachments.Controls.Add(this.btnAttachFile);
            this.pnlAttachments.Controls.Add(this.btnRemoveFile);
            this.pnlAttachments.Controls.Add(this.pnlFileListContainer);

            // ========== CONTACT METHOD (RIGHT SIDE) ==========
            this.pnlContact = new Panel();
            this.lblContact = new Label();
            this.rbEmail = new RadioButton();
            this.rbPhone = new RadioButton();
            this.rbSMS = new RadioButton();
            this.txtContactDetail = new TextBox();

            this.pnlContactHeader = new Panel();

            this.pnlContact.BackColor = Color.White;
            this.pnlContact.BorderStyle = BorderStyle.FixedSingle;
            this.pnlContact.Location = new Point(480, 350);
            this.pnlContact.Name = "pnlContact";
            this.pnlContact.Size = new Size(480, 145);

            // Header strip
            this.pnlContactHeader.BackColor = Color.FromArgb(245, 240, 240);
            this.pnlContactHeader.Location = new Point(0, 0);
            this.pnlContactHeader.Size = new Size(478, 38);

            this.lblContact.AutoSize = true;
            this.lblContact.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblContact.ForeColor = Color.FromArgb(139, 0, 0);
            this.lblContact.Location = new Point(12, 8);
            this.lblContact.Text = "\uD83D\uDCDE Preferred Contact Method for Updates";
            this.pnlContactHeader.Controls.Add(this.lblContact);

            // Radio buttons row
            this.rbEmail.AutoSize = true;
            this.rbEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.rbEmail.ForeColor = Color.FromArgb(50, 50, 50);
            this.rbEmail.Location = new Point(15, 52);
            this.rbEmail.Name = "rbEmail";
            this.rbEmail.Text = "Email";
            this.rbEmail.Checked = true;
            this.rbEmail.CheckedChanged += new EventHandler(this.rbContact_CheckedChanged);

            this.rbPhone.AutoSize = true;
            this.rbPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.rbPhone.ForeColor = Color.FromArgb(50, 50, 50);
            this.rbPhone.Location = new Point(120, 52);
            this.rbPhone.Name = "rbPhone";
            this.rbPhone.Text = "Phone Call";
            this.rbPhone.CheckedChanged += new EventHandler(this.rbContact_CheckedChanged);

            this.rbSMS.AutoSize = true;
            this.rbSMS.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.rbSMS.ForeColor = Color.FromArgb(50, 50, 50);
            this.rbSMS.Location = new Point(250, 52);
            this.rbSMS.Name = "rbSMS";
            this.rbSMS.Text = "SMS";
            this.rbSMS.CheckedChanged += new EventHandler(this.rbContact_CheckedChanged);

            this.txtContactDetail.BackColor = Color.White;
            this.txtContactDetail.BorderStyle = BorderStyle.FixedSingle;
            this.txtContactDetail.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.txtContactDetail.Location = new Point(15, 95);
            this.txtContactDetail.Name = "txtContactDetail";
            this.txtContactDetail.PlaceholderText = "Enter your email address (name@example.com)";
            this.txtContactDetail.Size = new Size(448, 32);
            this.txtContactDetail.TabIndex = 7;

            this.pnlContact.Controls.Add(this.rbEmail);
            this.pnlContact.Controls.Add(this.rbPhone);
            this.pnlContact.Controls.Add(this.rbSMS);
            this.pnlContact.Controls.Add(this.txtContactDetail);
            this.pnlContact.Controls.Add(this.pnlContactHeader);

            // ========== ACTION BUTTONS ==========
            this.btnSubmit.BackColor = Color.FromArgb(139, 0, 0);
            this.btnSubmit.Cursor = Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatAppearance.MouseOverBackColor = Color.FromArgb(165, 0, 0);
            this.btnSubmit.FlatStyle = FlatStyle.Flat;
            this.btnSubmit.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnSubmit.ForeColor = Color.White;
            this.btnSubmit.Location = new Point(480, 510);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(230, 50);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "\u2714  Submit Report";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);

            this.btnBackToMenu.BackColor = Color.FromArgb(60, 60, 60);
            this.btnBackToMenu.Cursor = Cursors.Hand;
            this.btnBackToMenu.FlatAppearance.BorderSize = 0;
            this.btnBackToMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
            this.btnBackToMenu.FlatStyle = FlatStyle.Flat;
            this.btnBackToMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnBackToMenu.ForeColor = Color.White;
            this.btnBackToMenu.Location = new Point(720, 510);
            this.btnBackToMenu.Name = "btnBackToMenu";
            this.btnBackToMenu.Size = new Size(240, 50);
            this.btnBackToMenu.TabIndex = 8;
            this.btnBackToMenu.Text = "\u2190  Back to Main Menu";
            this.btnBackToMenu.UseVisualStyleBackColor = false;
            this.btnBackToMenu.Click += new EventHandler(this.btnBackToMenu_Click);

            // ========== ADD TO CONTENT ==========
            this.pnlContent.Controls.Add(this.lblFormTitle);
            this.pnlContent.Controls.Add(this.lblFormSubtitle);
            this.pnlContent.Controls.Add(this.pnlEngagement);
            this.pnlContent.Controls.Add(this.lblLocation);
            this.pnlContent.Controls.Add(this.txtLocation);
            this.pnlContent.Controls.Add(this.lstLocationSuggestions);
            this.pnlContent.Controls.Add(this.pnlLocationValidator);
            this.pnlContent.Controls.Add(this.lblCategory);
            this.pnlContent.Controls.Add(this.cmbCategory);
            this.pnlContent.Controls.Add(this.pnlCategoryValidator);
            this.pnlContent.Controls.Add(this.lblPriority);
            this.pnlContent.Controls.Add(this.cmbPriority);
            this.pnlContent.Controls.Add(this.pnlPriorityValidator);
            this.pnlContent.Controls.Add(this.lblPriorityIndicator);
            this.pnlContent.Controls.Add(this.lblDescription);
            this.pnlContent.Controls.Add(this.rtbDescription);
            this.pnlContent.Controls.Add(this.pnlDescriptionValidator);
            this.pnlContent.Controls.Add(this.lblCharCounter);
            this.pnlContent.Controls.Add(this.pnlAttachments);
            this.pnlContent.Controls.Add(this.pnlContact);
            this.pnlContent.Controls.Add(this.btnSubmit);
            this.pnlContent.Controls.Add(this.btnBackToMenu);

            // ========== FORM ==========
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(44, 52, 63);
            this.ClientSize = new Size(1000, 680);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlBranding);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportIssuesForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Report Issues - City of uMhlathuze";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeaderContact;
        private Panel pnlBranding;
        private PictureBox picLogo;
        private Label lblMunicipalityName;
        private Label lblSlogan;
        private Panel pnlFooter;
        private Label lblFooter;
        private Panel pnlContent;
        private Label lblFormTitle;
        private Label lblFormSubtitle;
        private Panel pnlEngagement;
        private Label lblEngagement;
        private ProgressBar progressBar;
        private Label lblProgressPercent;
        private Label lblLocation;
        private TextBox txtLocation;
        private ListBox lstLocationSuggestions;
        private Panel pnlLocationValidator;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Panel pnlCategoryValidator;
        private Label lblPriority;
        private ComboBox cmbPriority;
        private Panel pnlPriorityValidator;
        private Label lblPriorityIndicator;
        private Label lblDescription;
        private RichTextBox rtbDescription;
        private Panel pnlDescriptionValidator;
        private Label lblCharCounter;
        private Panel pnlAttachments;
        private Label lblAttachments;
        private Button btnAttachFile;
        private Button btnRemoveFile;
        private Panel pnlFileListContainer;
        private Label lblNoFiles;
        private ListBox lstAttachedFiles;
        private Button btnSubmit;
        private Button btnBackToMenu;
        private Panel pnlContact;
        private Panel pnlContactHeader;
        private Label lblContact;
        private RadioButton rbEmail;
        private RadioButton rbPhone;
        private RadioButton rbSMS;
        private TextBox txtContactDetail;
    }
}
