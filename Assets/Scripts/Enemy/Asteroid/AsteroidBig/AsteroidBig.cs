using System;
using Enemy.Asteroid.AsteroidMedium;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Asteroid.AsteroidBig
{
    public class AsteroidBig : ScreenBoundaryHandlerBase, IAsteroidBase, IEnemy
    {
        public event Action OnKill; 
        
        [SerializeField] private AsteroidBigSetting _asteroidBigSetting;
        [SerializeField] private AsteroidMediumSetting _asteroidMediumSetting;
        [SerializeField] private AsteroidMedium.AsteroidMedium _asteroidMediumPrefab;
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
            transform.Translate(_direction * (_asteroidBigSetting.Speed * Time.deltaTime));
        }
        
        public void Split()
        {
            var asteroidMedium1 = Instantiate(_asteroidMediumPrefab, transform.position + Vector3.right, Quaternion.identity);
            asteroidMedium1.SetNewSprite(_asteroidMediumSetting.SpriteMedium[GetRandomIndex()]);
            
            var asteroidMedium2 = Instantiate(_asteroidMediumPrefab, transform.position + Vector3.left, Quaternion.identity);
            asteroidMedium2.SetNewSprite(_asteroidMediumSetting.SpriteMedium[GetRandomIndex()]);
            
            DestroyAsteroid();
            OnKill?.Invoke();
        }
        
        private void DestroyAsteroid()
        {
            Destroy(gameObject);
        }
        
        private int GetRandomIndex()
        {
            return Random.Range(0, _asteroidMediumSetting.SpriteMedium.Length);
        }
    }
}