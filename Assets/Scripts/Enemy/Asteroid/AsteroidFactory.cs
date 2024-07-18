using Enemy.Asteroid.AsteroidBig;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Asteroid
{
    public class AsteroidFactory : MonoBehaviour
    {
        [SerializeField] private AsteroidBig.AsteroidBig _asteroidBigPrefab;
        [SerializeField] private AsteroidBigSetting _asteroidBigSetting;
        [SerializeField] private float _minDistanceFromPlayer = 2f;
        [SerializeField] private int _maxCountAsteroids = 3;
        
        private Camera _camera;
        private Vector2 _playerPosition;
        private AsteroidBig.AsteroidBig _asteroidBig;
        private int _countAsteroids;

        public void Initialize()
        {
            _camera = Camera.main;
        }

        public void UpdateAsteroidFactory(Vector2 playerPosition)
        {
            _playerPosition = playerPosition;
            
            if(_countAsteroids < _maxCountAsteroids)
            {
                SpawnAsteroid();
            }
        }
        
        private void SpawnAsteroid()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            int randomSprite = Random.Range(0, _asteroidBigSetting.SpriteBig.Length);
            _asteroidBig = Instantiate(_asteroidBigPrefab, spawnPosition, Quaternion.identity);
            _asteroidBig.SetNewSprite(_asteroidBigSetting.SpriteBig[randomSprite]);
            _asteroidBig.OnKill += OnAsteroidDestroyed;
            _countAsteroids++;

        }

        private void OnAsteroidDestroyed()
        {
            _countAsteroids--;
            _asteroidBig.OnKill -= OnAsteroidDestroyed;
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float screenWidth = _camera.orthographicSize * _camera.aspect;
            float screenHeight = _camera.orthographicSize;

            Vector2 position;
            do
            {
                position = new Vector2(
                    Random.Range(-screenWidth, screenWidth),
                    Random.Range(-screenHeight, screenHeight)
                );
            }
            while ((Mathf.Abs(position.x) < _minDistanceFromPlayer && Mathf.Abs(position.y) < _minDistanceFromPlayer) ||
                   Vector2.Distance(position, _playerPosition) < _minDistanceFromPlayer);

            return position;
        }
    }
}