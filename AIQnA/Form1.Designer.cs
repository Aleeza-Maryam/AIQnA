using System.Collections.Generic;

namespace AIQnA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.RichTextBox rtbHistory;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Button btnAsk;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.btnAsk = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // ── Form ──────────────────────────────────────
            this.Text = "🤖 AI Q&A System";
            this.Size = new System.Drawing.Size(860, 640);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            // ── Top Panel ─────────────────────────────────
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 65;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);

            this.lblTitle.Text = "🤖  AI General Knowledge Q&A";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);

            this.lblSubtitle.Text = "Powered by Google Gemini  •  Ask any question!";
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(130, 130, 130);
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(18, 38);

            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblSubtitle);

            // ── Chat History ──────────────────────────────
            this.rtbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHistory.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.rtbHistory.ForeColor = System.Drawing.Color.White;
            this.rtbHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbHistory.ReadOnly = true;
            this.rtbHistory.Font = new System.Drawing.Font("Segoe UI", 10.5f);
            this.rtbHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbHistory.Padding = new System.Windows.Forms.Padding(15);

            // ── Bottom Panel ──────────────────────────────
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height = 90;
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(28, 28, 28);

            // txtQuestion
            this.txtQuestion.Location = new System.Drawing.Point(15, 18);
            this.txtQuestion.Size = new System.Drawing.Size(620, 30);
            this.txtQuestion.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            this.txtQuestion.ForeColor = System.Drawing.Color.White;
            this.txtQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuestion.Font = new System.Drawing.Font("Segoe UI", 11f);
            this.txtQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuestion_KeyDown);

            // btnAsk
            this.btnAsk.Location = new System.Drawing.Point(645, 17);
            this.btnAsk.Size = new System.Drawing.Size(95, 32);
            this.btnAsk.Text = "▶  Send";
            this.btnAsk.BackColor = System.Drawing.Color.FromArgb(0, 132, 255);
            this.btnAsk.ForeColor = System.Drawing.Color.White;
            this.btnAsk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsk.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnAsk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsk.FlatAppearance.BorderSize = 0;
            this.btnAsk.Click += new System.EventHandler(this.btnAsk_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(748, 17);
            this.btnClear.Size = new System.Drawing.Size(85, 32);
            this.btnClear.Text = "🗑  Clear";
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(55, 55, 55);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(15, 58);
            this.lblStatus.Size = new System.Drawing.Size(400, 20);
            this.lblStatus.Text = "✅ Ready — Ask any question!";
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(80, 200, 80);
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5f);

            // lblCount
            this.lblCount.Location = new System.Drawing.Point(640, 58);
            this.lblCount.Size = new System.Drawing.Size(195, 20);
            this.lblCount.Text = "Total Questions: 0";
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.pnlBottom.Controls.Add(this.txtQuestion);
            this.pnlBottom.Controls.Add(this.btnAsk);
            this.pnlBottom.Controls.Add(this.btnClear);
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Controls.Add(this.lblCount);

            // ── Add to Form ───────────────────────────────
            this.Controls.Add(this.rtbHistory);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);

            this.ResumeLayout(false);
        }
    }
}