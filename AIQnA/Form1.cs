using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIQnA
{
    public partial class Form1 : Form
    {
        private const string API_KEY = "YOUR_API_KEY_HERE";
        private const string API_URL = "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent";

        private readonly HttpClient _httpClient = new HttpClient();
        private int _questionCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnAsk_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text.Trim();

            if (string.IsNullOrEmpty(question))
            {
                MessageBox.Show("Koi sawal likho pehle!", "Empty Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetLoadingState(true);
            try
            {
                string answer = await GetAIAnswerAsync(question);
                _questionCount++;
                DisplayAnswer(question, answer);
                txtQuestion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private async System.Threading.Tasks.Task<string> GetAIAnswerAsync(string question)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = question }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.7,
                    maxOutputTokens = 4000,
                    topP = 0.95,
                    topK = 40
                }
            };

            string json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            string urlWithKey = $"{API_URL}?key={API_KEY}";

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(urlWithKey, content);
                string responseJson = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(responseJson))
                {
                    // Check for error
                    if (doc.RootElement.TryGetProperty("error", out JsonElement error))
                    {
                        string errorMsg = error.GetProperty("message").GetString();
                        return $"Error: {errorMsg}";
                    }

                    // Parse successful response
                    if (doc.RootElement.TryGetProperty("candidates", out JsonElement candidates) &&
                        candidates.GetArrayLength() > 0)
                    {
                        var candidate = candidates[0];

                        // Get the content
                        if (candidate.TryGetProperty("content", out JsonElement contentElement) &&
                            contentElement.TryGetProperty("parts", out JsonElement parts) &&
                            parts.GetArrayLength() > 0 &&
                            parts[0].TryGetProperty("text", out JsonElement text))
                        {
                            string answer = text.GetString()?.Trim() ?? "Koi jawab nahi mila.";

                            // Check if truncated
                            if (candidate.TryGetProperty("finishReason", out JsonElement finishReason) &&
                                finishReason.GetString() == "MAX_TOKENS")
                            {
                                answer += "\n\n[⚠️ Jawab adhoora hai. Next time sawal chhota kar ke pucho.]";
                            }

                            return answer;
                        }
                    }

                    return "Koi jawab nahi mila.";
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Network Error: {ex.Message}";
            }
            catch (JsonException ex)
            {
                return $"JSON Parse Error: {ex.Message}";
            }
        }

        private void DisplayAnswer(string question, string answer)
        {
            rtbHistory.SelectionColor = Color.Blue;
            rtbHistory.AppendText($"\n[Q{_questionCount}] {question}\n");
            rtbHistory.SelectionColor = Color.Green;
            rtbHistory.AppendText($"Jawab: {answer}\n");
            rtbHistory.SelectionColor = Color.Gray;
            rtbHistory.AppendText("─────────────────────────────\n");
            rtbHistory.ScrollToCaret();
            lblCount.Text = $"Total Sawal: {_questionCount}";
        }

        private void SetLoadingState(bool isLoading)
        {
            btnAsk.Enabled = !isLoading;
            txtQuestion.Enabled = !isLoading;
            lblStatus.Text = isLoading ? "⏳ Jawab aa raha hai..." : "✅ Ready";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbHistory.Clear();
            _questionCount = 0;
            lblCount.Text = "Total Sawal: 0";
        }

        private void txtQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAsk_Click(sender, e);
            }
        }
    }
}