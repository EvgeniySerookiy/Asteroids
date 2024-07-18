using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private int _score;

        private void UpdateScore()
        {
            _scoreText.text = _score.ToString();
        }
        
        public void AddScore(int points)
        {
            _score += points;
            UpdateScore();
            
        }
    }
}