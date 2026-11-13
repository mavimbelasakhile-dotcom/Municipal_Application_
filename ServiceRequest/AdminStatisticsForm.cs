using AdvancedProgrammingAss1.ServiceRequest;

namespace AdvancedProgrammingAss1
{
    /// <summary>
    /// Administrator statistics view. Shows, at a glance, how many service
    /// requests exist and how they are distributed:
    ///   • Open vs Closed
    ///   • Assigned vs Unassigned
    ///   • A breakdown of how many sit in each lifecycle status
    ///
    /// The chart is drawn with GDI+ (custom-painted bar chart) so the project
    /// has no external charting dependency.
    /// </summary>
    public class AdminStatisticsForm : Form
    {
        private static readonly Color Blue = Color.FromArgb(0, 123, 255);

        private Panel chartPanel = null!;
        private Label lblHeadline = null!;

        // Captured snapshot of the numbers we draw.
        private readonly List<(string Label, int Count, Color Color)> statusBars = new();
        private readonly List<(string Label, int Count, Color Color)> categorySlices = new();
        private readonly List<(string Label, int Count, Color Color)> priorityBars = new();
        private int total, open, closed, assigned, unassigned;

        // A palette used to colour the category pie slices.
        private static readonly Color[] CategoryPalette =
        {
            Color.FromArgb(0, 123, 255),   Color.FromArgb(40, 167, 69),
            Color.FromArgb(255, 140, 0),   Color.FromArgb(220, 53, 69),
            Color.FromArgb(111, 66, 193),  Color.FromArgb(23, 162, 184),
            Color.FromArgb(255, 193, 7),   Color.FromArgb(232, 62, 140),
            Color.FromArgb(102, 16, 242),  Color.FromArgb(32, 201, 151),
            Color.FromArgb(253, 126, 20),  Color.FromArgb(108, 117, 125),
            Color.FromArgb(52, 58, 64)
        };

        public AdminStatisticsForm()
        {
            BuildUi();
            LoadStatistics();

            NotificationService.NotificationsChanged += SafeReload;
            FormClosed += (s, e) => NotificationService.NotificationsChanged -= SafeReload;
        }

        private void BuildUi()
        {
            Text = "Service Request Statistics";
            Size = new Size(900, 680);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            MinimumSize = new Size(760, 560);

            Panel header = new Panel { BackColor = Blue, Dock = DockStyle.Top, Height = 80 };
            Controls.Add(header);

            Label lblTitle = new Label
            {
                Text = "\uD83D\uDCCA  Request Statistics Overview",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 14),
                AutoSize = true
            };
            header.Controls.Add(lblTitle);

            lblHeadline = new Label
            {
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(225, 240, 255),
                Location = new Point(22, 48),
                AutoSize = true
            };
            header.Controls.Add(lblHeadline);

            chartPanel = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(ClientSize.Width - 40, ClientSize.Height - 170),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };
            chartPanel.Paint += ChartPanel_Paint;
            chartPanel.Resize += (s, e) => chartPanel.Invalidate();
            Controls.Add(chartPanel);

            Button btnRefresh = new Button
            {
                Text = "Refresh",
                BackColor = Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(140, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                Location = new Point(20, ClientSize.Height - 55),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) => { LoadStatistics(); chartPanel.Invalidate(); };
            Controls.Add(btnRefresh);

            Button btnClose = new Button
            {
                Text = "Close",
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(140, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 160, ClientSize.Height - 55),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();
            Controls.Add(btnClose);
        }

        /// <summary>
        /// Recomputes every figure from the live request list and rebuilds the
        /// list of bars the chart will paint.
        /// </summary>
        private void LoadStatistics()
        {
            var all = ReportedIssue.GetAllReports();
            total = all.Count;

            // A request is "closed" once it reaches the Closed status; everything
            // else (Submitted ... Resolved) counts as still open.
            closed = all.Count(r => string.Equals(r.Status, ServiceRequestManager.StatusClosed,
                StringComparison.OrdinalIgnoreCase));
            open = total - closed;

            unassigned = all.Count(r => string.IsNullOrWhiteSpace(r.AssignedToUsername));
            assigned = total - unassigned;

            statusBars.Clear();
            foreach (var status in ServiceRequestManager.AllStatuses)
            {
                int count = all.Count(r =>
                    string.Equals(r.Status, status, StringComparison.OrdinalIgnoreCase));
                statusBars.Add((status, count, ServiceRequestManager.ColorFor(status)));
            }

            // ---- Category breakdown (for the pie chart) ----
            categorySlices.Clear();
            var categoryGroups = all
                .Where(r => !string.IsNullOrWhiteSpace(r.Category))
                .GroupBy(r => r.Category)
                .OrderByDescending(gr => gr.Count())
                .ToList();

            int paletteIndex = 0;
            foreach (var gr in categoryGroups)
            {
                Color sliceColor = CategoryPalette[paletteIndex % CategoryPalette.Length];
                categorySlices.Add((gr.Key, gr.Count(), sliceColor));
                paletteIndex++;
            }

            // ---- Priority breakdown (for the bar graph) ----
            priorityBars.Clear();
            foreach (var priority in new[] { "Low", "Medium", "High", "Critical" })
            {
                int count = all.Count(r =>
                    string.Equals(r.Priority, priority, StringComparison.OrdinalIgnoreCase));
                priorityBars.Add((priority, count, ColorForPriority(priority)));
            }

            lblHeadline.Text =
                $"{total} total  •  {open} open  •  {closed} closed  •  {assigned} assigned  •  {unassigned} unassigned";
        }

        private void ChartPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int width = chartPanel.ClientSize.Width;

            if (total == 0)
            {
                using var emptyFont = new Font("Segoe UI", 12F, FontStyle.Italic);
                using var greyBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
                g.DrawString("No service requests have been logged yet.",
                    emptyFont, greyBrush, 30, 30);
                return;
            }

            int y = 20;

            // ---- Summary cards row: Open / Closed / Assigned / Unassigned ----
            var cards = new (string Caption, int Value, Color Color)[]
            {
                ("Open", open, Color.FromArgb(0, 123, 255)),
                ("Closed", closed, Color.FromArgb(60, 60, 60)),
                ("Assigned", assigned, Color.FromArgb(40, 167, 69)),
                ("Unassigned", unassigned, Color.FromArgb(220, 53, 69))
            };

            int cardGap = 16;
            int cardWidth = (width - 40 - cardGap * (cards.Length - 1)) / cards.Length;
            int cardHeight = 90;
            int cardX = 20;

            using (var captionFont = new Font("Segoe UI", 10F, FontStyle.Bold))
            using (var valueFont = new Font("Segoe UI", 24F, FontStyle.Bold))
            {
                foreach (var card in cards)
                {
                    var rect = new Rectangle(cardX, y, cardWidth, cardHeight);
                    using (var fill = new SolidBrush(Color.FromArgb(246, 249, 252)))
                        g.FillRectangle(fill, rect);
                    using (var border = new Pen(card.Color, 2))
                        g.DrawRectangle(border, rect);

                    using (var valBrush = new SolidBrush(card.Color))
                    {
                        var vSize = g.MeasureString(card.Value.ToString(), valueFont);
                        g.DrawString(card.Value.ToString(), valueFont, valBrush,
                            cardX + (cardWidth - vSize.Width) / 2, y + 10);
                    }

                    using (var capBrush = new SolidBrush(Color.FromArgb(70, 70, 70)))
                    {
                        var cSize = g.MeasureString(card.Caption, captionFont);
                        g.DrawString(card.Caption, captionFont, capBrush,
                            cardX + (cardWidth - cSize.Width) / 2, y + cardHeight - 26);
                    }

                    cardX += cardWidth + cardGap;
                }
            }

            y += cardHeight + 30;

            // ---- Bar chart: count in each lifecycle status ----
            using var titleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            using var titleBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
            g.DrawString("Requests by Status", titleFont, titleBrush, 20, y);
            y += 34;

            int chartLeft = 150;
            int chartRight = width - 30;
            int barAreaWidth = chartRight - chartLeft;
            int barHeight = 30;
            int barGap = 18;
            int maxCount = Math.Max(1, statusBars.Max(b => b.Count));

            using var labelFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var countFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var labelBrush = new SolidBrush(Color.FromArgb(60, 60, 60));

            foreach (var bar in statusBars)
            {
                // status label on the left
                var lSize = g.MeasureString(bar.Label, labelFont);
                g.DrawString(bar.Label, labelFont, labelBrush,
                    chartLeft - lSize.Width - 10, y + (barHeight - lSize.Height) / 2);

                // track
                var track = new Rectangle(chartLeft, y, barAreaWidth, barHeight);
                using (var trackBrush = new SolidBrush(Color.FromArgb(238, 240, 243)))
                    g.FillRectangle(trackBrush, track);

                // value bar
                int barWidth = (int)(barAreaWidth * (bar.Count / (double)maxCount));
                if (bar.Count > 0 && barWidth < 4) barWidth = 4;
                var barRect = new Rectangle(chartLeft, y, barWidth, barHeight);
                using (var barBrush = new SolidBrush(bar.Color))
                    g.FillRectangle(barBrush, barRect);

                // count label — inside the bar if it fits, otherwise just after it
                string countText = bar.Count.ToString();
                var cSize = g.MeasureString(countText, countFont);
                if (barWidth > cSize.Width + 12)
                {
                    using var white = new SolidBrush(Color.White);
                    g.DrawString(countText, countFont, white,
                        chartLeft + barWidth - cSize.Width - 8, y + (barHeight - cSize.Height) / 2);
                }
                else
                {
                    using var dark = new SolidBrush(Color.FromArgb(60, 60, 60));
                    g.DrawString(countText, countFont, dark,
                        chartLeft + barWidth + 6, y + (barHeight - cSize.Height) / 2);
                }

                y += barHeight + barGap;
            }

            y += 20;

            // ---- Pie chart: issues by category ----
            y = DrawCategoryPie(g, width, y);

            y += 30;

            // ---- Bar graph: issues by priority ----
            y = DrawPriorityBars(g, width, y);

            // Let the panel scroll if the content is taller than the view.
            chartPanel.AutoScrollMinSize = new Size(0, y + 20);
        }

        /// <summary>
        /// Draws a pie chart of reported issues grouped by category, with a
        /// legend on the right showing each category's colour, count and share.
        /// Returns the Y position just below the chart.
        /// </summary>
        private int DrawCategoryPie(Graphics g, int width, int y)
        {
            using var titleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            using var titleBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
            g.DrawString("Issues by Category", titleFont, titleBrush, 20, y);
            y += 34;

            int categoryTotal = categorySlices.Sum(s => s.Count);
            if (categoryTotal == 0)
            {
                using var greyBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
                using var italic = new Font("Segoe UI", 10F, FontStyle.Italic);
                g.DrawString("No categorised issues to display.", italic, greyBrush, 24, y);
                return y + 24;
            }

            int pieSize = 220;
            int pieX = 30;
            int pieY = y;
            var pieRect = new Rectangle(pieX, pieY, pieSize, pieSize);

            float startAngle = -90f; // start at the top
            foreach (var slice in categorySlices)
            {
                float sweep = (float)(slice.Count / (double)categoryTotal * 360.0);
                using (var sliceBrush = new SolidBrush(slice.Color))
                    g.FillPie(sliceBrush, pieRect, startAngle, sweep);
                using (var outline = new Pen(Color.White, 2))
                    g.DrawPie(outline, pieRect, startAngle, sweep);
                startAngle += sweep;
            }

            // ---- Legend ----
            int legendX = pieX + pieSize + 40;
            int legendY = pieY;
            int swatch = 16;
            using var legendFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            using var legendBrush = new SolidBrush(Color.FromArgb(60, 60, 60));

            foreach (var slice in categorySlices)
            {
                using (var sw = new SolidBrush(slice.Color))
                    g.FillRectangle(sw, legendX, legendY, swatch, swatch);

                double share = slice.Count / (double)categoryTotal * 100.0;
                string text = $"{slice.Label}  —  {slice.Count} ({share:F0}%)";
                g.DrawString(text, legendFont, legendBrush,
                    legendX + swatch + 8, legendY + 1);

                legendY += swatch + 10;
            }

            int bottom = Math.Max(pieY + pieSize, legendY);
            return bottom;
        }

        /// <summary>
        /// Draws a horizontal bar graph of reported issues grouped by priority.
        /// Returns the Y position just below the chart.
        /// </summary>
        private int DrawPriorityBars(Graphics g, int width, int y)
        {
            using var titleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            using var titleBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
            g.DrawString("Issues by Priority", titleFont, titleBrush, 20, y);
            y += 34;

            int priorityTotal = priorityBars.Sum(b => b.Count);
            if (priorityTotal == 0)
            {
                using var greyBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
                using var italic = new Font("Segoe UI", 10F, FontStyle.Italic);
                g.DrawString("No prioritised issues to display.", italic, greyBrush, 24, y);
                return y + 24;
            }

            int chartLeft = 150;
            int chartRight = width - 30;
            int barAreaWidth = chartRight - chartLeft;
            int barHeight = 30;
            int barGap = 18;
            int maxCount = Math.Max(1, priorityBars.Max(b => b.Count));

            using var labelFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var countFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var labelBrush = new SolidBrush(Color.FromArgb(60, 60, 60));

            foreach (var bar in priorityBars)
            {
                var lSize = g.MeasureString(bar.Label, labelFont);
                g.DrawString(bar.Label, labelFont, labelBrush,
                    chartLeft - lSize.Width - 10, y + (barHeight - lSize.Height) / 2);

                var track = new Rectangle(chartLeft, y, barAreaWidth, barHeight);
                using (var trackBrush = new SolidBrush(Color.FromArgb(238, 240, 243)))
                    g.FillRectangle(trackBrush, track);

                int barWidth = (int)(barAreaWidth * (bar.Count / (double)maxCount));
                if (bar.Count > 0 && barWidth < 4) barWidth = 4;
                var barRect = new Rectangle(chartLeft, y, barWidth, barHeight);
                using (var barBrush = new SolidBrush(bar.Color))
                    g.FillRectangle(barBrush, barRect);

                string countText = bar.Count.ToString();
                var cSize = g.MeasureString(countText, countFont);
                if (barWidth > cSize.Width + 12)
                {
                    using var white = new SolidBrush(Color.White);
                    g.DrawString(countText, countFont, white,
                        chartLeft + barWidth - cSize.Width - 8, y + (barHeight - cSize.Height) / 2);
                }
                else
                {
                    using var dark = new SolidBrush(Color.FromArgb(60, 60, 60));
                    g.DrawString(countText, countFont, dark,
                        chartLeft + barWidth + 6, y + (barHeight - cSize.Height) / 2);
                }

                y += barHeight + barGap;
            }

            return y;
        }

        /// <summary>A UI colour for each priority level.</summary>
        private static Color ColorForPriority(string priority)
        {
            switch (priority)
            {
                case "Low":      return Color.FromArgb(40, 167, 69);   // green
                case "Medium":   return Color.FromArgb(255, 193, 7);   // amber
                case "High":     return Color.FromArgb(255, 140, 0);   // orange
                case "Critical": return Color.FromArgb(220, 53, 69);   // red
                default:         return Color.FromArgb(108, 117, 125); // grey
            }
        }

        private void SafeReload()
        {
            if (InvokeRequired) { BeginInvoke(new Action(SafeReload)); return; }
            LoadStatistics();
            chartPanel.Invalidate();
        }
    }
}
