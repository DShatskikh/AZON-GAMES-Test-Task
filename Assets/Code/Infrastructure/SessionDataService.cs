using TMPro;

namespace Code.Infrastructure
{
    public class SessionDataService
    {
        private readonly TextMeshProUGUI _scoreText;
        private int _score;

        public SessionDataService(TextMeshProUGUI scoreText)
        {
            _scoreText = scoreText;
        }
        
        public void AddScore()
        {
            ++_score;
            UpdateScoreText();
        }

        private void UpdateScoreText() => 
            _scoreText.text = _score.ToString();
    }
}