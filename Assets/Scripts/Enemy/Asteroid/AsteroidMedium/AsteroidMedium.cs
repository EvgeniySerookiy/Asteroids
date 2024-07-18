using Enemy.Asteroid.AsteroidSmall;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Asteroid.AsteroidMedium
{
    public class AsteroidMedium : ScreenBoundaryHandlerBase, IAsteroidBase, IEnemy
    {
        [SerializeField] private AsteroidMediumSetting _asteroidMediumSetting;
        [SerializeField] private AsteroidSmallSetting _asteroidSmallSetting;
        [SerializeField] private AsteroidSmall.AsteroidSmall _asteroidSmallPrefab;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private Vector2 _direction;

        private void Awake()
        {
            _direction = Random.insideUnitCircle.normalized;
        }
        
        private void Update()
        {
            CheckScreenBoundaries();
            Move();
        }
        
        public void SetNewSprite(Sprite newSprite)
        {
            _spriteRenderer.sprite = newSprite;
        }
        
        private void Move()
        {
            transform.Translate(_direction * (_asteroidMediumSetting.Speed * Time.deltaTime));
        }
        
        public void Split()
        {
            var asteroidMedium1 = Instantiate(_asteroidSmallPrefab, transform.position + Vector3.right, Quaternion.identity);
            asteroidMedium1.SetNewSprite(_asteroidSmallSetting.SpriteSmall[GetRandomIndex()]);
            
            var asteroidMedium2 = Instantiate(_asteroidSmallPrefab, transform.position + Vector3.left, Quaternion.identity);
            asteroidMedium2.SetNewSprite(_asteroidSmallSetting.SpriteSmall[GetRandomIndex()]);
            
            DestroyAsteroid();
        }

        private void DestroyAsteroid()
        {
            Destroy(gameObject);
        }

        private int GetRandomIndex()
        {
            return Random.Range(0, _asteroidSmallSetting.SpriteSmall.Length);
        }
    }
}