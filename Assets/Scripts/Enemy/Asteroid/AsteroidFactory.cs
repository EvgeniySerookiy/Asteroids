using Enemy.Asteroid.AsteroidsBig;
using UnityEngine;
using Zenject;

namespace Enemy.Asteroid
{
    public class AsteroidFactory
    {
        private readonly AsteroidBigSetting _asteroidBigSetting;
        private readonly AsteroidFactorySetting _asteroidFactorySetting;
        private readonly DiContainer _container;
        private readonly Camera _camera;

        private AsteroidBig _asteroidBig;
        private Vector2 _playerPosition;
        private int _countAsteroids;

        public AsteroidFactory(AsteroidFactorySetting asteroidFactorySetting, AsteroidBigSetting asteroidBigSetting, DiContainer container)
        {
            _asteroidFactorySetting = asteroidFactorySetting;
            _asteroidBigSetting = asteroidBigSetting;
            _container = container;
            _camera = Camera.main;
        }

        public void UpdateAsteroidFactory(Vector2 playerPosition)
        {
            _playerPosition = playerPosition;
            
            if (_countAsteroids < _asteroidFactorySetting.MaxCountAsteroids)
            {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid()
        {
            int randomSpriteIndex = Random.Range(0, _asteroidBigSetting.SpriteBig.Length);
            _asteroidBig = _container.InstantiatePrefabForComponent<AsteroidBig>(_asteroidBigSetting.AsteroidBigPrefab);
            _asteroidBig.transform.position = GetRandomSpawnPosition();
            _asteroidBig.transform.rotation = Quaternion.identity;
            _asteroidBig.SetNewSprite(_asteroidBigSetting.SpriteBig[randomSpriteIndex]);
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
            while (IsTooCloseToPlayer(position));

            return position;
        }

        private bool IsTooCloseToPlayer(Vector2 position)
        {
            return (Mathf.Abs(position.x) < _asteroidFactorySetting.MinDistanceFromPlayer && Mathf.Abs(position.y) < _asteroidFactorySetting.MinDistanceFromPlayer)
                || Vector2.Distance(position, _playerPosition) < _asteroidFactorySetting.MinDistanceFromPlayer;
        }
    }
}
