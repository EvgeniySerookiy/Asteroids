using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Asteroid.AsteroidSmall
{
    public class AsteroidSmall : ScreenBoundaryHandlerBase, IAsteroidBase, IEnemy
    {
        [SerializeField] private AsteroidSmallSetting _asteroidSmallSetting;
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
            transform.Translate(_direction * (_asteroidSmallSetting.Speed * Time.deltaTime));
        }

        public void Split()
        {
            DestroyAsteroid();
        }

        private void DestroyAsteroid()
        {
            Destroy(gameObject);
        }
    }
}