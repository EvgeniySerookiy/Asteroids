using System;
using Enemy.Asteroid;
using Enemy.UFO;
using UnityEngine;

namespace Player.Bullet
{
    public class Bullet : ScreenBoundaryHandlerBase
    {
        public event Action<Type> OnKill;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private BulletSetting _bulletSetting;
        
        private Vector2 _direction;
        
        private void Update()
        {
            CheckScreenBoundaries();
            _rigidbody.velocity = _direction * _bulletSetting.Speed;
        }
        
        public void SetDirection(Vector2 newDirection)
        {
            _direction = newDirection;
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public float GetLifeTime()
        {
            return _bulletSetting.LifeTime;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out IAsteroidBase asteroid))
            {
                asteroid.Split();
                OnKill?.Invoke(asteroid.GetType());
                Deactivate();
            }
            
            if (collider.gameObject.TryGetComponent(out UFO ufo))
            {
                Destroy(ufo.gameObject);
                OnKill?.Invoke(typeof(UFO));
                Deactivate();
            }
        }
    }
}