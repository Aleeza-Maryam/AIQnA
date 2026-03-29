using System;
using System.Collections.Generic;
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
        private const string API_KEY = "";
        private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

        private readonly HttpClient _httpClient = new HttpClient();
        private int _questionCount = 0;

        // Conversation history storage
        private List<object> _conversationHistory = new List<object>();

        public Form1()
        {
            InitializeComponent();
            rtbHistory.SelectionAlignment = HorizontalAlignment.Center;
            rtbHistory.SelectionColor = Color.FromArgb(80, 80, 80);
            rtbHistory.SelectionFont = new Font("Segoe UI", 9f, FontStyle.Italic);
            rtbHistory.AppendText("\n\n🤖 AI Q&A System\nAsk any question — I will answer!\n\n");

            // System instruction (remembers name, previous questions)
            _conversationHistory.Add(new { role = "user", parts = new[] { new { text = "You are a helpful AI assistant that remembers everything the user tells you. Remember the user's name and previous questions. Always respond in English or Urdu as appropriate. Keep answers concise but helpful. If you need current information, you can use Google Search." } } });
            _conversationHistory.Add(new { role = "model", parts = new[] { new { text = "Sure! I will remember our conversation, and I can use Google Search to give you the latest information when needed." } } });
        }

        private async void btnAsk_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text.Trim();

            if (string.IsNullOrEmpty(question))
            {
                MessageBox.Show("Please write a question first!", "Empty Input",
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

        private async Task<string> GetAIAnswerAsync(string question)
        {
            // Add user's question to history
            _conversationHistory.Add(new { role = "user", parts = new[] { new { text = question } } });

            // Request body with Google Search grounding (tools)
            var requestBody = new
            {
                contents = _conversationHistory,
                tools = new[]
                {
                    new { googleSearch = new object() }   // Enable Google Search
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

                        // Get the text answer
                        if (candidate.TryGetProperty("content", out JsonElement contentElement) &&
                            contentElement.TryGetProperty("parts", out JsonElement parts) &&
                            parts.GetArrayLength() > 0 &&
                            parts[0].TryGetProperty("text", out JsonElement text))
                        {
                            string answer = text.GetString()?.Trim() ?? "No answer found.";

                            // Check if truncated
                            if (candidate.TryGetProperty("finishReason", out JsonElement finishReason) &&
                                finishReason.GetString() == "MAX_TOKENS")
                            {
                                answer += "\n\n[⚠️ Answer incomplete. Please ask a shorter question.]";
                            }

                            // Add a note if Google Search was used
                            if (candidate.TryGetProperty("groundingMetadata", out _))
                            {
                                answer += "\n\n(🔍 Fresh information from Google Search)";
                            }

                            // Save assistant's response to history
                            _conversationHistory.Add(new { role = "model", parts = new[] { new { text = answer } } });

                            // Limit history length (keep last 30 messages, keep initial system instructions)
                            const int maxMessages = 30;
                            if (_conversationHistory.Count > maxMessages)
                            {
                                _conversationHistory.RemoveRange(2, _conversationHistory.Count - maxMessages);
                            }

                            return answer;
                        }
                    }

                    return "No answer found.";
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
            // User message (right bubble)
            rtbHistory.SelectionAlignment = HorizontalAlignment.Right;
            rtbHistory.SelectionColor = Color.FromArgb(130, 130, 130);
            rtbHistory.SelectionFont = new Font("Segoe UI", 9f);
            rtbHistory.AppendText("\n🧑 You\n");

            rtbHistory.SelectionAlignment = HorizontalAlignment.Right;
            rtbHistory.SelectionBackColor = Color.FromArgb(0, 100, 210);
            rtbHistory.SelectionColor = Color.White;
            rtbHistory.SelectionFont = new Font("Segoe UI", 12f, FontStyle.Bold);
            rtbHistory.AppendText($"  {question}  ");
            rtbHistory.SelectionBackColor = Color.FromArgb(18, 18, 18);
            rtbHistory.SelectionFont = new Font("Segoe UI", 4f);
            rtbHistory.AppendText("\n\n");

            // AI message (left bubble)
            rtbHistory.SelectionAlignment = HorizontalAlignment.Left;
            rtbHistory.SelectionColor = Color.FromArgb(80, 200, 80);
            rtbHistory.SelectionFont = new Font("Segoe UI", 9f);
            rtbHistory.AppendText("🤖 Gemini AI\n");

            rtbHistory.SelectionAlignment = HorizontalAlignment.Left;
            rtbHistory.SelectionBackColor = Color.FromArgb(40, 40, 40);
            rtbHistory.SelectionColor = Color.FromArgb(230, 230, 230);
            rtbHistory.SelectionFont = new Font("Segoe UI", 12f);
            rtbHistory.AppendText($"  {answer.Trim()}  ");
            rtbHistory.SelectionBackColor = Color.FromArgb(18, 18, 18);
            rtbHistory.SelectionFont = new Font("Segoe UI", 4f);
            rtbHistory.AppendText("\n\n");

            // Divider
            rtbHistory.SelectionAlignment = HorizontalAlignment.Center;
            rtbHistory.SelectionColor = Color.FromArgb(50, 50, 50);
            rtbHistory.SelectionFont = new Font("Segoe UI", 8f);
            rtbHistory.AppendText("· · · · · · · · · · · · · · · · · ·\n\n");

            rtbHistory.ScrollToCaret();
            lblCount.Text = $"Total Questions: {_questionCount}";
        }

        private void SetLoadingState(bool isLoading)
        {
            btnAsk.Enabled = !isLoading;
            txtQuestion.Enabled = !isLoading;
            lblStatus.Text = isLoading ? "⏳ Getting answer..." : "✅ Ready";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to clear all history? This will also reset the chatbot's memory.", "Confirm Clear",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                rtbHistory.Clear();
                _questionCount = 0;
                lblCount.Text = "Total Questions: 0";

                // Reset conversation history but keep system instruction
                _conversationHistory.Clear();
                _conversationHistory.Add(new { role = "user", parts = new[] { new { text = "You are a helpful AI assistant that remembers everything the user tells you. Remember the user's name and previous questions. Always respond in English or Urdu as appropriate. Keep answers concise but helpful. If you need current information, you can use Google Search." } } });
                _conversationHistory.Add(new { role = "model", parts = new[] { new { text = "Sure! I will remember our conversation, and I can use Google Search to give you the latest information when needed." } } });

                // Re-add welcome message
                rtbHistory.SelectionAlignment = HorizontalAlignment.Center;
                rtbHistory.SelectionColor = Color.FromArgb(80, 80, 80);
                rtbHistory.SelectionFont = new Font("Segoe UI", 9f, FontStyle.Italic);
                rtbHistory.AppendText("\n\n🤖 AI Q&A System\nAsk any question — I will answer!\n\n");
            }
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