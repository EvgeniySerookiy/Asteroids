using System;
using Enemy.Asteroid;
using Enemy.UFO;
using UnityEngine;

namespace PlayerCharacter.Laser
{
    public class Laser : MonoBehaviour
    {
        public event Action<Type> OnKill;
        
        [SerializeField] private LaserSetting _laserSetting;

        private Vector2 _direction;

        private void Update()
        {
            transform.up = _direction;
        }

        public float GetLaserShootInterval()
        {
            return _laserSetting.LaserShootInterval;
        }
        
        public float GetLifeTime()
        {
            return _laserSetting.LifeTime;
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
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out IAsteroidBase asteroid))
            {
                asteroid.Split();
                OnKill?.Invoke(asteroid.GetType());
            }
            
            if (collider.gameObject.TryGetComponent(out UFO ufo))
            {
                Destroy(ufo.gameObject);
                OnKill?.Invoke(typeof(UFO));
            }
        }
    }
}