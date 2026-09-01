# City of uMhlathuze — Municipal Services Portal

A Windows Forms (.NET 8) desktop application that lets residents of the City of uMhlathuze report municipal issues and service requests. Built with the municipality's branding for a professional, enterprise-grade experience.

---

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Technology Stack](#technology-stack)
4. [How to Run](#how-to-run)
5. [Application Walkthrough (Screenshots)](#application-walkthrough)
6. [Data Structures Used](#data-structures-used)
7. [Continuous Integration](#continuous-integration)

---

## Overview

The Municipal Services Portal provides residents with a single point of contact to engage with municipal services. On startup, the user is presented with three services:

- **Report Issues** (implemented)
- **Local Events & Announcements** (planned — disabled)
- **Service Request Status** (planned — disabled)

Only the "Report Issues" module is active in this release, in line with the assignment requirements.

---

## Features

- Municipal-branded UI (City of uMhlathuze colours, logo, and slogan)
- Report issues with **location, category, priority, description, and attachments**
- **Location autocomplete** for uMhlathuze suburbs, streets, and landmarks (array-based)
- **Category** dropdown (Sanitation, Roads, Water, Electricity, and more)
- **Priority** selector (Low / Medium / High / Critical) with colour indicator
- **File attachments** restricted to images and documents (JPG, PNG, BMP, PDF, DOC, DOCX, TXT)
- **Description** field with a live character counter (max 1000)
- **Preferred contact method** (Email / Phone / SMS) with format validation
- **Dynamic engagement progress bar** with colour transitions (red → amber → green)
- **Custom branded dialogs** for confirmation and success
- **Unique reference numbers** (e.g., `UMH-2026-0001`)
- **Report history** viewer with a `DataGridView`
- **Export reports** to a formatted text file
- Real-time field validation with visual indicators
- Session report counter, live date/time clock, and personalised welcome

---

## Technology Stack

- **Language:** C#
- **Framework:** .NET 8 (Windows Forms)
- **IDE:** Visual Studio
- **Version Control:** Git / GitHub
- **CI:** GitHub Actions

---

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/mavimbelasakhile-dotcom/Municipal_Application_.git
   ```
2. Open `AdvancedProgrammingAss1.slnx` in Visual Studio.
3. Press **Ctrl + F5** (Run without debugging) or **F5** to launch.

Or from the command line:
```bash
dotnet run
```

---

## Application Walkthrough

### 1. Main Menu (Startup)

The landing screen presented on startup, showing the three services. Only "Report Issues" is enabled.

![Main Menu](screenshots/01-main-menu.png)

---

### 2. Report Issues Form

The Report Issues form when first opened, with the branded header, progress bar, and input fields.

![Report Issues Form](screenshots/02-report-issues-empty.png)

---

### 3. Location Autocomplete

As the user types a location, matching uMhlathuze area suggestions appear in a dropdown.

![Location Autocomplete](screenshots/03-location-autocomplete.png)

---

### 4. Category Selection

A dropdown listing municipal issue categories (Sanitation, Roads, Water Supply, etc.).

![Category Dropdown](screenshots/04-category-dropdown.png)

---

### 5. Priority Selection

The priority dropdown with a colour-coded indicator (Low = green, Critical = red).

![Priority Selector](screenshots/05-priority-selector.png)

---

### 6. Description with Character Counter

A RichTextBox for the issue description, with a live character counter that changes colour near the limit.

![Description](screenshots/06-description.png)

---

### 7. File Attachments

Users can attach supporting images and documents. The list shows each file with its size.

![Attachments](screenshots/07-attachments.png)

---

### 8. Preferred Contact Method — Email

When Email is selected, a single field captures the email address (validated on submit).

![Contact Email](screenshots/08-contact-email.png)

---

### 9. Preferred Contact Method — Phone / SMS

When Phone Call or SMS is selected, the field captures a phone number (validated on submit).

![Contact Phone](screenshots/09-contact-phone.png)

---

### 10. Engagement Progress Bar

A dynamic progress bar encourages completion, changing colour and message as fields are filled.

![Progress Bar](screenshots/10-progress-bar.png)

---

### 11. Confirmation Dialog

A custom branded dialog asks the user to confirm before submitting.

![Confirmation Dialog](screenshots/11-confirm-dialog.png)

---

### 12. Success Dialog

On submission, a custom success dialog displays the unique reference number and a summary.

![Success Dialog](screenshots/12-success-dialog.png)

---

### 13. Report History

A `DataGridView` lists all reports submitted during the session. Double-click a row for full details.

![Report History](screenshots/13-report-history.png)

---

### 14. Export to File

Reports can be exported to a formatted text file for record-keeping.

![Export](screenshots/14-export.png)

---

### 15. Validation Feedback

The application validates input and shows clear error messages (e.g., invalid email format).

![Validation Error](screenshots/15-validation-error.png)

---

## Data Structures Used

- **`string[]` (array)** — stores the location autocomplete suggestions.
- **`List<ReportedIssue>`** — stores all submitted issue reports in memory.
- **`List<string>`** — stores the file paths attached to a report.
- **`ReportedIssue` class** — models each report (reference number, location, category, priority, description, attachments, date, status, contact details).

---

## Continuous Integration

A GitHub Actions workflow (`.github/workflows/build.yml`) builds the application automatically on every push to `master`, verifying that the code compiles in Release mode on a Windows runner.

---

© 2026 City of uMhlathuze — Municipal Services Portal. Built for academic purposes.
