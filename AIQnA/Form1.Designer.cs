namespace AIQnA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Button btnAsk;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtbHistory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.btnAsk = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtQuestion
            this.txtQuestion.Location = new System.Drawing.Point(12, 50);
            this.txtQuestion.Size = new System.Drawing.Size(500, 23);
            this.txtQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuestion_KeyDown);

            // btnAsk
            this.btnAsk.Location = new System.Drawing.Point(520, 49);
            this.btnAsk.Size = new System.Drawing.Size(80, 25);
            this.btnAsk.Text = "Ask AI";
            this.btnAsk.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnAsk.ForeColor = System.Drawing.Color.White;
            this.btnAsk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsk.Click += new System.EventHandler(this.btnAsk_Click);

            // btnClear
            this.btnClear.Location = new System.Drawing.Point(608, 49);
            this.btnClear.Size = new System.Drawing.Size(70, 25);
            this.btnClear.Text = "Clear";
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // rtbHistory
            this.rtbHistory.Location = new System.Drawing.Point(12, 90);
            this.rtbHistory.Size = new System.Drawing.Size(760, 400);
            this.rtbHistory.ReadOnly = true;
            this.rtbHistory.BackColor = System.Drawing.Color.FromArgb(250, 250, 252);

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(12, 500);
            this.lblStatus.Size = new System.Drawing.Size(300, 20);
            this.lblStatus.Text = "✅ Ready";
            this.lblStatus.ForeColor = System.Drawing.Color.Green;

            // lblCount
            this.lblCount.Location = new System.Drawing.Point(550, 500);
            this.lblCount.Size = new System.Drawing.Size(200, 20);
            this.lblCount.Text = "Total Sawal: 0";

            // Label - Question
            var lblQ = new System.Windows.Forms.Label();
            lblQ.Text = "Apna Sawal Likho:";
            lblQ.Location = new System.Drawing.Point(12, 28);
            lblQ.AutoSize = true;

            // Form
            this.Text = "AI General Knowledge Q&A";
            this.Size = new System.Drawing.Size(800, 560);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(lblQ);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.btnAsk);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rtbHistory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCount);
            this.ResumeLayout(false);
        }
    }
}