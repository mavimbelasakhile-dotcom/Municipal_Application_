using System.Media;

namespace AdvancedProgrammingAss1
{
    public partial class ReportIssuesForm : Form
    {
        // Reference to main form for badge updates
        private Form1 mainForm;

        // List to store attached file paths for the current report
        private List<string> attachedFiles = new List<string>();

        // Maximum characters allowed in description
        private const int MaxDescriptionChars = 1000;

        // Tooltip provider
        private ToolTip toolTip = null!;

        // Flag to prevent suggestion list from triggering text changed again
        private bool isSuggestionClick = false;

        // Location suggestions array for autocomplete (uMhlathuze area)
        private string[] locationSuggestions = {
            "Veld En Vlei, Richards Bay",
            "Meerensee, Richards Bay",
            "Arboretum, Richards Bay",
            "Birdswood, Richards Bay",
            "Brackenham, Richards Bay",
            "Wildenweide, Richards Bay",
            "Aquadene, Richards Bay",
            "Voelklip, Richards Bay",
            "Die Heuwel, Richards Bay",
            "Mediteraneo, Richards Bay",
            "Hippo Park, Richards Bay",
            "Pelican Heights, Richards Bay",
            "Mandlazini, Richards Bay",
            "CBD, Richards Bay",
            "Alton, Richards Bay",
            "Alton North, Richards Bay",
            "Bayshore, Richards Bay",
            "Veldenvlei Road, Richards Bay",
            "Dollar Drive, Richards Bay",
            "Mark Sobey Avenue, Richards Bay",
            "Lira Link, Richards Bay",
            "Krewelkring Street, Richards Bay",
            "The Doyen, Richards Bay",
            "Tasbet Park, Richards Bay",
            "John Ross Parkway, Richards Bay",
            "Bullion Boulevard, Richards Bay",
            "Tanner Road, Richards Bay",
            "Hibberd Drive, Richards Bay",
            "Anglers Road, Richards Bay",
            "The Doyen Estate, Richards Bay",
            "Nseleni Road, Richards Bay",
            "CBD, Empangeni",
            "Ngwelezane, Empangeni",
            "Civic Centre, Empangeni",
            "Maxwell Street, Empangeni",
            "Union Street, Empangeni",
            "Turnbull Street, Empangeni",
            "Hely Hutchinson Street, Empangeni",
            "Commercial Road, Empangeni",
            "Main Street, Empangeni",
            "Railway Road, Empangeni",
            "Pearce Street, Empangeni",
            "Durdham Road, Empangeni",
            "Canefields, Empangeni",
            "Brackenhill, Empangeni",
            "The Doyen Estate, Empangeni",
            "Section A, eSikhaleni",
            "Section B, eSikhaleni",
            "Section C, eSikhaleni",
            "Section D, eSikhaleni",
            "Section E, eSikhaleni",
            "Section F, eSikhaleni",
            "Main Road, eSikhaleni",
            "Market Area, eSikhaleni",
            "Section A, Nseleni",
            "Section B, Nseleni",
            "Section C, Nseleni",
            "Section D, Nseleni",
            "Main Road, Nseleni",
            "Madlankala, Nseleni",
            "Vulindlela, Ngwelezane",
            "Section A, Ngwelezane",
            "Section B, Ngwelezane",
            "Section C, Ngwelezane",
            "Section D, Ngwelezane",
            "Hospital Road, Ngwelezane",
            "Boardwalk Inkwazi Shopping Centre, Richards Bay",
            "Enseleni Nature Reserve, Richards Bay",
            "Tuzi Gazi Waterfront, Richards Bay",
            "Richards Bay Civic Centre",
            "Empangeni Civic Centre",
            "John Ross College, Richards Bay",
            "Richards Bay Coal Terminal",
            "University of Zululand, KwaDlangezwa",
            "uMhlathuze Sports Complex",
            "Alton Industrial Area, Richards Bay",
            "Foskor, Richards Bay",
            "Mondi Paper Mill, Richards Bay",
            "Richards Bay Harbour",
            "Brackenham Shopping Centre, Richards Bay",
            "Meerensee Shopping Centre, Richards Bay"
        };

        public ReportIssuesForm(Form1 parentForm)
        {
            InitializeComponent();
            this.mainForm = parentForm;
            PopulateCategories();
            PopulatePriorities();
            SetupTooltips();
            SetupLocationAutocomplete();
            UpdateEngagementLabel();
            UpdateCharCounter();
            ValidateFieldVisuals();
            UpdateFileListVisual();
        }

        /// <summary>
        /// Configures the location autocomplete listbox behaviour.
        /// </summary>
        private void SetupLocationAutocomplete()
        {
            lstLocationSuggestions.Visible = false;
            lstLocationSuggestions.Click += LstLocationSuggestions_Click;
            lstLocationSuggestions.KeyDown += LstLocationSuggestions_KeyDown;
        }

        /// <summary>
        /// Filters and shows location suggestions based on typed text.
        /// </summary>
        private void ShowLocationSuggestions(string input)
        {
            if (isSuggestionClick || string.IsNullOrWhiteSpace(input) || input.Length < 2)
            {
                lstLocationSuggestions.Visible = false;
                return;
            }

            string searchText = input.ToLower();
            var matches = locationSuggestions
                .Where(loc => loc.ToLower().Contains(searchText))
                .OrderBy(loc => loc.ToLower().IndexOf(searchText))
                .Take(6)
                .ToList();

            if (matches.Count == 0)
            {
                lstLocationSuggestions.Visible = false;
                return;
            }

            lstLocationSuggestions.Items.Clear();
            foreach (var match in matches)
            {
                lstLocationSuggestions.Items.Add(match);
            }

            // Resize the suggestion list to fit items
            int itemHeight = lstLocationSuggestions.ItemHeight;
            lstLocationSuggestions.Height = Math.Min(matches.Count * itemHeight + 4, 6 * itemHeight + 4);
            lstLocationSuggestions.Visible = true;
            lstLocationSuggestions.BringToFront();
        }

        /// <summary>
        /// Handles clicking on a location suggestion.
        /// </summary>
        private void LstLocationSuggestions_Click(object? sender, EventArgs e)
        {
            if (lstLocationSuggestions.SelectedItem != null)
            {
                isSuggestionClick = true;
                txtLocation.Text = lstLocationSuggestions.SelectedItem.ToString();
                txtLocation.SelectionStart = txtLocation.Text.Length;
                lstLocationSuggestions.Visible = false;
                isSuggestionClick = false;
                txtLocation.Focus();
            }
        }

        /// <summary>
        /// Handles keyboard navigation in the suggestions list.
        /// </summary>
        private void LstLocationSuggestions_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstLocationSuggestions.SelectedItem != null)
            {
                isSuggestionClick = true;
                txtLocation.Text = lstLocationSuggestions.SelectedItem.ToString();
                txtLocation.SelectionStart = txtLocation.Text.Length;
                lstLocationSuggestions.Visible = false;
                isSuggestionClick = false;
                txtLocation.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lstLocationSuggestions.Visible = false;
                txtLocation.Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Populates the category dropdown with municipal issue categories.
        /// </summary>
        private void PopulateCategories()
        {
            cmbCategory.Items.AddRange(new string[]
            {
                "Sanitation",
                "Roads & Potholes",
                "Water Supply",
                "Electricity & Power",
                "Sewerage & Drainage",
                "Public Safety",
                "Parks & Recreation",
                "Street Lights",
                "Illegal Dumping",
                "Noise Complaint",
                "Traffic Signs & Signals",
                "Building Violations",
                "Other"
            });
            cmbCategory.SelectedIndex = -1;
        }

        /// <summary>
        /// Populates the priority dropdown.
        /// </summary>
        private void PopulatePriorities()
        {
            cmbPriority.Items.AddRange(new string[]
            {
                "Low",
                "Medium",
                "High",
                "Critical"
            });
            cmbPriority.SelectedIndex = -1;
        }

        /// <summary>
        /// Sets up helpful tooltips on all form controls.
        /// </summary>
        private void SetupTooltips()
        {
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(txtLocation, "Start typing an address — suggestions will appear automatically");
            toolTip.SetToolTip(cmbCategory, "Select the type of issue you are reporting");
            toolTip.SetToolTip(cmbPriority, "How urgent is this issue? Critical = immediate danger");
            toolTip.SetToolTip(rtbDescription, "Provide as much detail as possible about the issue (max 1000 characters)");
            toolTip.SetToolTip(btnAttachFile, "Attach photos or documents (JPG, PNG, BMP, PDF, DOC, DOCX, TXT)");
            toolTip.SetToolTip(btnSubmit, "Submit your issue report to the municipality");
            toolTip.SetToolTip(btnBackToMenu, "Return to the main menu without submitting");
        }

        /// <summary>
        /// Updates the engagement label and progress bar with colour changes.
        /// </summary>
        private void UpdateEngagementLabel()
        {
            int progress = CalculateProgress();
            progressBar.Value = progress;
            lblProgressPercent.Text = $"{progress}%";

            // Change progress bar colour based on completion
            if (progress < 40)
            {
                progressBar.ForeColor = Color.FromArgb(220, 53, 69); // Red
                lblEngagement.ForeColor = Color.FromArgb(220, 53, 69);
            }
            else if (progress < 80)
            {
                progressBar.ForeColor = Color.FromArgb(255, 193, 7); // Yellow/Amber
                lblEngagement.ForeColor = Color.FromArgb(180, 140, 0);
            }
            else
            {
                progressBar.ForeColor = Color.FromArgb(40, 167, 69); // Green
                lblEngagement.ForeColor = Color.FromArgb(40, 167, 69);
            }

            if (progress == 0)
                lblEngagement.Text = "\uD83D\uDCDD Start filling in the details to report your issue!";
            else if (progress <= 20)
                lblEngagement.Text = "\uD83D\uDC4D Great start! Keep going...";
            else if (progress <= 40)
                lblEngagement.Text = "\uD83D\uDE80 You're making progress! A few more fields to go.";
            else if (progress <= 60)
                lblEngagement.Text = "\uD83C\uDFAF Halfway there! Your report is taking shape.";
            else if (progress < 100)
                lblEngagement.Text = "\u2728 Almost done! Just a little more info needed.";
            else
                lblEngagement.Text = "\u2705 All details filled in! Ready to submit your report.";
        }

        /// <summary>
        /// Calculates completion progress based on filled fields (5 fields now with priority).
        /// </summary>
        private int CalculateProgress()
        {
            int progress = 0;
            int totalFields = 5; // location, category, priority, description, attachment

            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
                progress++;
            if (cmbCategory.SelectedIndex >= 0)
                progress++;
            if (cmbPriority.SelectedIndex >= 0)
                progress++;
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text))
                progress++;
            if (attachedFiles.Count > 0)
                progress++;

            return (int)((double)progress / totalFields * 100);
        }

        /// <summary>
        /// Updates the character counter for the description field.
        /// </summary>
        private void UpdateCharCounter()
        {
            int remaining = MaxDescriptionChars - rtbDescription.Text.Length;
            lblCharCounter.Text = $"{rtbDescription.Text.Length}/{MaxDescriptionChars} characters";

            if (remaining < 50)
                lblCharCounter.ForeColor = Color.FromArgb(220, 53, 69); // Red when close to limit
            else if (remaining < 200)
                lblCharCounter.ForeColor = Color.FromArgb(255, 140, 0); // Orange when getting close
            else
                lblCharCounter.ForeColor = Color.FromArgb(100, 100, 100); // Normal grey
        }

        /// <summary>
        /// Validates field visuals — shows green/red borders via panel backgrounds.
        /// </summary>
        private void ValidateFieldVisuals()
        {
            // Location validation visual
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
                pnlLocationValidator.BackColor = Color.FromArgb(70, 80, 90);
            else
                pnlLocationValidator.BackColor = Color.FromArgb(40, 167, 69);

            // Category validation visual
            if (cmbCategory.SelectedIndex < 0)
                pnlCategoryValidator.BackColor = Color.FromArgb(70, 80, 90);
            else
                pnlCategoryValidator.BackColor = Color.FromArgb(40, 167, 69);

            // Priority validation visual
            if (cmbPriority.SelectedIndex < 0)
                pnlPriorityValidator.BackColor = Color.FromArgb(70, 80, 90);
            else
                pnlPriorityValidator.BackColor = Color.FromArgb(40, 167, 69);

            // Description validation visual
            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
                pnlDescriptionValidator.BackColor = Color.FromArgb(70, 80, 90);
            else
                pnlDescriptionValidator.BackColor = Color.FromArgb(40, 167, 69);
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateEngagementLabel();
            ValidateFieldVisuals();
            ShowLocationSuggestions(txtLocation.Text);
        }

        /// <summary>
        /// Handles keyboard events on location textbox for suggestion navigation.
        /// </summary>
        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (lstLocationSuggestions.Visible)
            {
                if (e.KeyCode == Keys.Down)
                {
                    lstLocationSuggestions.Focus();
                    if (lstLocationSuggestions.Items.Count > 0)
                        lstLocationSuggestions.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    lstLocationSuggestions.Visible = false;
                    e.Handled = true;
                }
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEngagementLabel();
            ValidateFieldVisuals();
        }

        /// <summary>
        /// Updates the placeholder text and layout based on selected contact method.
        /// </summary>
        private void rbContact_CheckedChanged(object sender, EventArgs e)
        {
            // Single full-width textbox for all contact methods
            txtContactDetail.Location = new Point(15, 95);
            txtContactDetail.Size = new Size(448, 32);

            if (rbEmail.Checked)
                txtContactDetail.PlaceholderText = "Enter your email address (name@example.com)";
            else if (rbPhone.Checked)
                txtContactDetail.PlaceholderText = "Enter your phone number (e.g., 082 123 4567)";
            else
                txtContactDetail.PlaceholderText = "Enter your cellphone number (e.g., 082 123 4567)";
        }

        /// <summary>
        /// Validates the contact detail based on the selected contact method.
        /// Returns true if valid, otherwise shows an error and returns false.
        /// </summary>
        private bool ValidateContactDetail()
        {
            string detail = txtContactDetail.Text.Trim();

            // Contact detail is optional — allow empty
            if (string.IsNullOrWhiteSpace(detail))
                return true;

            if (rbEmail.Checked)
            {
                if (!IsValidEmail(detail))
                {
                    MessageBox.Show(
                        "The email address you entered is not valid.\n\n" +
                        "Please enter a valid email in the format:\n" +
                        "name@example.com",
                        "Invalid Email Format",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtContactDetail.Focus();
                    return false;
                }
            }
            else // Phone or SMS
            {
                if (!IsValidPhoneNumber(detail))
                {
                    MessageBox.Show(
                        "The phone number you entered is not valid.\n\n" +
                        "Please enter digits only (7 to 12 digits).\n" +
                        "Example: 82 123 4567",
                        "Invalid Phone Number",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtContactDetail.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates email format using a regular expression.
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                // Standard email pattern: something@something.domain
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates a phone number — digits only (after removing spaces), 7 to 12 digits.
        /// </summary>
        private bool IsValidPhoneNumber(string phone)
        {
            // Remove common formatting characters
            string digitsOnly = phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            // Must be all digits and between 7 and 12 characters
            if (digitsOnly.Length < 7 || digitsOnly.Length > 12)
                return false;

            foreach (char c in digitsOnly)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the contact detail entered by the user.
        /// </summary>
        private string GetFullContactDetail()
        {
            return txtContactDetail.Text.Trim();
        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEngagementLabel();
            ValidateFieldVisuals();
            UpdatePriorityIndicator();
        }

        /// <summary>
        /// Updates the priority colour indicator beside the dropdown.
        /// </summary>
        private void UpdatePriorityIndicator()
        {
            if (cmbPriority.SelectedIndex < 0)
            {
                lblPriorityIndicator.Text = "";
                return;
            }

            string priority = cmbPriority.SelectedItem?.ToString() ?? "";
            switch (priority)
            {
                case "Low":
                    lblPriorityIndicator.Text = "\u25CF Low Priority";
                    lblPriorityIndicator.ForeColor = Color.FromArgb(40, 167, 69);
                    break;
                case "Medium":
                    lblPriorityIndicator.Text = "\u25CF Medium Priority";
                    lblPriorityIndicator.ForeColor = Color.FromArgb(255, 193, 7);
                    break;
                case "High":
                    lblPriorityIndicator.Text = "\u25CF High Priority";
                    lblPriorityIndicator.ForeColor = Color.FromArgb(255, 140, 0);
                    break;
                case "Critical":
                    lblPriorityIndicator.Text = "\u25CF CRITICAL";
                    lblPriorityIndicator.ForeColor = Color.FromArgb(220, 53, 69);
                    break;
            }
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            // Enforce character limit
            if (rtbDescription.Text.Length > MaxDescriptionChars)
            {
                rtbDescription.Text = rtbDescription.Text.Substring(0, MaxDescriptionChars);
                rtbDescription.SelectionStart = rtbDescription.Text.Length;
            }
            UpdateCharCounter();
            UpdateEngagementLabel();
            ValidateFieldVisuals();
        }

        /// <summary>
        /// Opens a file dialog for the user to attach images or documents.
        /// </summary>
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Attach Images or Documents";
                openFileDialog.Filter = "Images (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|" +
                                        "PDF Documents (*.pdf)|*.pdf|" +
                                        "Word Documents (*.doc, *.docx)|*.doc;*.docx|" +
                                        "Text Files (*.txt)|*.txt|" +
                                        "All Supported Files|*.jpg;*.jpeg;*.png;*.bmp;*.pdf;*.doc;*.docx;*.txt";
                openFileDialog.FilterIndex = 5;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int addedCount = 0;
                    foreach (string file in openFileDialog.FileNames)
                    {
                        if (!attachedFiles.Contains(file))
                        {
                            attachedFiles.Add(file);
                            string fileName = Path.GetFileName(file);
                            string fileSize = GetFileSizeString(file);
                            lstAttachedFiles.Items.Add($"{fileName}  ({fileSize})");
                            addedCount++;
                        }
                    }
                    UpdateFileListVisual();
                    UpdateEngagementLabel();
                    if (addedCount > 0)
                    {
                        MessageBox.Show($"{addedCount} file(s) attached successfully.",
                            "Files Attached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Shows or hides the "no files" hint based on the attachment count.
        /// </summary>
        private void UpdateFileListVisual()
        {
            lblNoFiles.Visible = attachedFiles.Count == 0;
        }

        /// <summary>
        /// Returns a human-readable file size string.
        /// </summary>
        private string GetFileSizeString(string filePath)
        {
            try
            {
                long bytes = new FileInfo(filePath).Length;
                if (bytes < 1024) return $"{bytes} B";
                if (bytes < 1048576) return $"{bytes / 1024.0:F1} KB";
                return $"{bytes / 1048576.0:F1} MB";
            }
            catch
            {
                return "Unknown size";
            }
        }

        /// <summary>
        /// Removes the selected attached file from the list.
        /// </summary>
        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            if (lstAttachedFiles.SelectedIndex >= 0)
            {
                int index = lstAttachedFiles.SelectedIndex;
                attachedFiles.RemoveAt(index);
                lstAttachedFiles.Items.RemoveAt(index);
                UpdateFileListVisual();
                UpdateEngagementLabel();
            }
            else
            {
                MessageBox.Show("Please select a file to remove.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Validates and submits the issue report with confirmation dialog.
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter the location of the issue.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category for the issue.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return;
            }

            if (cmbPriority.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a priority level.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPriority.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show("Please provide a description of the issue.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtbDescription.Focus();
                return;
            }

            // Contact detail validation
            if (!ValidateContactDetail())
                return;

            // Confirmation dialog
            DialogResult confirm = CustomDialog.ShowConfirmation(
                "Confirm Submission",
                "Are you sure you want to submit this report?",
                $"Location:       {txtLocation.Text.Trim()}\n" +
                $"Category:       {cmbCategory.SelectedItem}\n" +
                $"Priority:         {cmbPriority.SelectedItem}\n" +
                $"Attachments:  {attachedFiles.Count} file(s)\n" +
                $"Contact:         {(rbEmail.Checked ? "Email" : rbPhone.Checked ? "Phone" : "SMS")} - {GetFullContactDetail()}");

            if (confirm != DialogResult.Yes)
                return;

            // Generate unique reference number
            string refNumber = ReportedIssue.GenerateReferenceNumber();

            // Create the issue object and store it
            ReportedIssue issue = new ReportedIssue
            {
                ReferenceNumber = refNumber,
                Location = txtLocation.Text.Trim(),
                Category = cmbCategory.SelectedItem?.ToString() ?? string.Empty,
                Priority = cmbPriority.SelectedItem?.ToString() ?? string.Empty,
                Description = rtbDescription.Text.Trim(),
                AttachedFiles = new List<string>(attachedFiles),
                ReportDate = DateTime.Now,
                Status = "Submitted",
                ContactMethod = rbEmail.Checked ? "Email" : rbPhone.Checked ? "Phone" : "SMS",
                ContactDetail = GetFullContactDetail()
            };

            ReportedIssue.AddReport(issue);

            // Play success sound
            SystemSounds.Asterisk.Play();

            // Show custom success dialog
            CustomDialog.ShowSuccess(
                issue.ReferenceNumber,
                issue.Location,
                issue.Category,
                issue.Priority,
                issue.AttachedFiles.Count,
                issue.ReportDate,
                issue.ContactMethod,
                issue.ContactDetail);

            // Update main form badge
            mainForm.UpdateReportBadge();

            // Clear the form for a new report
            ClearForm();
        }

        /// <summary>
        /// Clears all form fields after a successful submission.
        /// </summary>
        private void ClearForm()
        {
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            cmbPriority.SelectedIndex = -1;
            rtbDescription.Clear();
            attachedFiles.Clear();
            lstAttachedFiles.Items.Clear();
            UpdateFileListVisual();
            lblPriorityIndicator.Text = "";
            lstLocationSuggestions.Visible = false;
            rbEmail.Checked = true;
            txtContactDetail.Clear();
            UpdateEngagementLabel();
            UpdateCharCounter();
            ValidateFieldVisuals();
        }

        /// <summary>
        /// Navigates back to the main menu.
        /// </summary>
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLocation.Text) ||
                cmbCategory.SelectedIndex >= 0 ||
                !string.IsNullOrWhiteSpace(rtbDescription.Text) ||
                attachedFiles.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to go back?\n\nAll entered data will be lost.",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            mainForm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the form closing event to show main menu.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                mainForm.Show();
            }
        }
    }

    /// <summary>
    /// Data class representing a reported issue with unique reference number.
    /// </summary>
    public class ReportedIssue
    {
        private static List<ReportedIssue> allReports = new List<ReportedIssue>();
        private static int reportCounter = 0;

        public string ReferenceNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> AttachedFiles { get; set; } = new List<string>();
        public DateTime ReportDate { get; set; }
        public string Status { get; set; } = "Submitted";
        public string ContactMethod { get; set; } = string.Empty;
        public string ContactDetail { get; set; } = string.Empty;

        /// <summary>
        /// Generates a unique reference number (e.g., UMH-2026-0001).
        /// </summary>
        public static string GenerateReferenceNumber()
        {
            reportCounter++;
            return $"UMH-{DateTime.Now.Year}-{reportCounter:D4}";
        }

        public static void AddReport(ReportedIssue issue)
        {
            allReports.Add(issue);
        }

        public static List<ReportedIssue> GetAllReports()
        {
            return allReports;
        }

        /// <summary>
        /// Exports all reports to a text file.
        /// </summary>
        public static string ExportToText()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=================================================================");
            sb.AppendLine("     CITY OF uMHLATHUZE - REPORTED ISSUES EXPORT");
            sb.AppendLine($"     Generated: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("=================================================================");
            sb.AppendLine();

            if (allReports.Count == 0)
            {
                sb.AppendLine("No reports have been submitted this session.");
            }
            else
            {
                foreach (var report in allReports)
                {
                    sb.AppendLine($"Reference: {report.ReferenceNumber}");
                    sb.AppendLine($"Date:      {report.ReportDate:dd/MM/yyyy HH:mm}");
                    sb.AppendLine($"Status:    {report.Status}");
                    sb.AppendLine($"Priority:  {report.Priority}");
                    sb.AppendLine($"Location:  {report.Location}");
                    sb.AppendLine($"Category:  {report.Category}");
                    sb.AppendLine($"Description:");
                    sb.AppendLine($"  {report.Description}");
                    sb.AppendLine($"Attachments: {report.AttachedFiles.Count} file(s)");
                    foreach (var file in report.AttachedFiles)
                    {
                        sb.AppendLine($"  - {Path.GetFileName(file)}");
                    }
                    if (!string.IsNullOrWhiteSpace(report.ContactDetail))
                    {
                        sb.AppendLine($"Contact:   {report.ContactMethod} - {report.ContactDetail}");
                    }
                    sb.AppendLine("-----------------------------------------------------------------");
                    sb.AppendLine();
                }
            }

            sb.AppendLine($"Total Reports: {allReports.Count}");
            return sb.ToString();
        }
    }
}
