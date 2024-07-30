using System;
using Enemy;
using PlayerCharacter.Bullet;
using PlayerCharacter.Laser;
using UnityEngine;
using Zenject;

namespace PlayerCharacter
{
    public class Player : ScreenBoundaryHandlerBase
    {
        public event Action OnDie;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _muzzle;
        
        private BulletFactory _bulletFactory;
        private LaserFactory _laserFactory;
        private SpriteRenderer _spriteRenderer;
        private PlayerSetting _playerSetting;

        [Inject]
        public void Construct(BulletFactory bulletFactory, LaserFactory laserFactory)
        {
            _bulletFactory = bulletFactory;
            _laserFactory = laserFactory;
        }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(PlayerSetting playerSetting)
        {
            ApplyPlayerSettings(playerSetting);
        }

        public void Accelerate()
        {
            if (gameObject.activeSelf)
            {
                _rigidbody.AddForce(transform.up * (_playerSetting.MoveForce * Time.deltaTime), ForceMode2D.Force);
                _spriteRenderer.sprite = _playerSetting.SpriteTail;
            }
        }
        
        public void BreakAcceleration()
        {
            if (gameObject.activeSelf) 
                _spriteRenderer.sprite = _playerSetting.SpriteShip;
        }

        public void TurnLeft()
        {
            if (gameObject.activeSelf) 
                transform.Rotate(Vector3.forward * (Time.deltaTime * _playerSetting.RotationSpeed));
        }

        public void TurnRight()
        {
            if (gameObject.activeSelf) 
                transform.Rotate(Vector3.forward * (Time.deltaTime * -_playerSetting.RotationSpeed));
        }
        
        public void ShootBullets()
        {
            if (gameObject.activeSelf)
            {
                if (_bulletFactory == null) 
                    return;

                var bullet = _bulletFactory.BulletPool.Get();
                bullet.transform.position = _muzzle.position;
                bullet.SetDirection(_muzzle.up);
            }
        }
        
        public void ShootLaser()
        {
            if (gameObject.activeSelf)
            {
                if (_laserFactory == null) 
                    return;

                _laserFactory.GetLaser(_muzzle);
            }
        }
        
        private void ApplyPlayerSettings(PlayerSetting playerSetting)
        {
            _playerSetting = playerSetting;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out IEnemy enemy))
            {
                gameObject.SetActive(false);
                OnDie?.Invoke();
            }
        }
    }
}

