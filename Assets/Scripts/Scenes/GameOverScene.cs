using PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes
{
    public class GameOverScene : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreGameText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private Button _button;

        private GameSceneManager _gameSceneManager;
        private Player _player;
        
        [Inject]
        public void Construct(GameSceneManager gameSceneManager)
        {
            _gameSceneManager = gameSceneManager;
        }

        public void Initialize(Player player)
        {
            _player = player;
            _player.OnDie += GameOver;
            _button.onClick.AddListener(RestartGame);
            _gameOverText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(true);
            _bestScoreText.gameObject.SetActive(false);
            _button.gameObject.SetActive(false);
            _scoreGameText.gameObject.SetActive(false);
            _scoreGameText.text = "0";
        }

        public void GameOver()
        {
            int score = int.Parse(_scoreText.text);
            int bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PlAYER, 0);

            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PlAYER, bestScore);
                PlayerPrefs.Save();
            }

            _scoreGameText.text = "Score: " + score;
            _bestScoreText.text = "Best score: " + bestScore;

            _gameOverText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(false);
            _bestScoreText.gameObject.SetActive(true);
            _button.gameObject.SetActive(true);
            _scoreGameText.gameObject.SetActive(true);
        }

        public void RestartGame()
        {
            _player.OnDie -= GameOver;
            _gameSceneManager.LoadGameScene();
            _button.onClick.RemoveListener(RestartGame);
        }
    }
}