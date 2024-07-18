using Enemy.Asteroid.AsteroidBig;
using Enemy.Asteroid.AsteroidMedium;
using Enemy.Asteroid.AsteroidSmall;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

namespace Scenes
{
    public class StartScene : MonoBehaviour
    {
        [SerializeField] private GameSceneManager _gameSceneManager;
        [SerializeField] private ScreenBoundaryHandlerBase[] _asteroids;
        [SerializeField] private AsteroidBigSetting _asteroidBigSetting;
        [SerializeField] private AsteroidMediumSetting _asteroidMediumSetting;
        [SerializeField] private AsteroidSmallSetting _asteroidSmallSetting;
        [SerializeField] private Button _button;
        [SerializeField] private int _maxAsteroids = 8;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _button.onClick.AddListener(_gameSceneManager.LoadGameScene);

            for (int i = 0; i < _maxAsteroids; i++)
            {
                SpawnAsteroid();
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_gameSceneManager.LoadGameScene);
        }

        private void SpawnAsteroid()
        {
            int index = Random.Range(0, _asteroids.Length);
            var asteroid = Instantiate(_asteroids[index], GetRandomSpawnPosition(), Quaternion.identity);

            if (asteroid is AsteroidBig asteroidBig)
            {
                asteroidBig.SetNewSprite(GetRandomSprite(_asteroidBigSetting.SpriteBig));
            }
            else if (asteroid is AsteroidMedium asteroidMedium)
            {
                asteroidMedium.SetNewSprite(GetRandomSprite(_asteroidMediumSetting.SpriteMedium));
            }
            else if (asteroid is AsteroidSmall asteroidSmall)
            {
                asteroidSmall.SetNewSprite(GetRandomSprite(_asteroidSmallSetting.SpriteSmall));
            }
        }

        private Sprite GetRandomSprite(Sprite[] spriteArray)
        {
            int index = Random.Range(0, spriteArray.Length);
            return spriteArray[index];
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float screenWidth = _camera.orthographicSize * _camera.aspect;
            float screenHeight = _camera.orthographicSize;

            return new Vector2(
                Random.Range(-screenWidth, screenWidth),
                Random.Range(-screenHeight, screenHeight)
            );
        }
    }
}