using Enemy.Asteroid.AsteroidsSmall;
using UnityEngine;

namespace Enemy.Asteroid.AsteroidsMedium
{
    public class AsteroidMedium : ScreenBoundaryHandlerBase, IAsteroidBase, IEnemy
    {
        [SerializeField] private AsteroidMediumSetting _asteroidMediumSetting;
        [SerializeField] private AsteroidSmallSetting _asteroidSmallSetting;
        [SerializeField] private AsteroidSmall _asteroidSmallPrefab;
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
        
        public void Split()
        {
            CreateSmallAsteroid(Vector3.right);
            CreateSmallAsteroid(Vector3.left);
            DestroyAsteroid();
        }

        private void Move()
        {
            transform.Translate(_direction * (_asteroidMediumSetting.Speed * Time.deltaTime));
        }

        private void CreateSmallAsteroid(Vector3 offset)
        {
            var asteroidSmall = Instantiate(_asteroidSmallPrefab, transform.position + offset, Quaternion.identity);
            asteroidSmall.SetNewSprite(_asteroidSmallSetting.SpriteSmall[GetRandomIndex()]);
        }

        private void DestroyAsteroid()
        {
            Destroy(gameObject);
        }

        private int GetRandomIndex()
        {
            return UnityEngine.Random.Range(0, _asteroidSmallSetting.SpriteSmall.Length);
        }
    }
}