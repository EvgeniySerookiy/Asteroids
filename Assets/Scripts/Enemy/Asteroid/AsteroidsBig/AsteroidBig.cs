using System;
using Enemy.Asteroid.AsteroidsMedium;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Asteroid.AsteroidsBig
{
    public class AsteroidBig : ScreenBoundaryHandlerBase, IAsteroidBase, IEnemy
    {
        public event Action OnKill; 
        
        [SerializeField] private AsteroidBigSetting _asteroidBigSetting;
        [SerializeField] private AsteroidMediumSetting _asteroidMediumSetting;
        [SerializeField] private AsteroidMedium _asteroidMediumPrefab;
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
            CreateMediumAsteroid(Vector3.right);
            CreateMediumAsteroid(Vector3.left);
            DestroyAsteroid();
            OnKill?.Invoke();
        }
        
        private void Move()
        {
            transform.Translate(_direction * (_asteroidBigSetting.Speed * Time.deltaTime));
        }
        
        private void CreateMediumAsteroid(Vector3 offset)
        {
            var asteroidMedium = Instantiate(_asteroidMediumPrefab, transform.position + offset, Quaternion.identity);
            asteroidMedium.SetNewSprite(_asteroidMediumSetting.SpriteMedium[GetRandomIndex()]);
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