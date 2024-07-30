using System.Collections;
using Score;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace PlayerCharacter.Bullet
{
    public class BulletFactory
    {
        private readonly DiContainer _container;
        private readonly BulletFactorySetting _bulletFactorySetting;
        private readonly ScoreController _scoreController;
        private readonly MonoBehaviour _monoBehaviour;

        public ObjectPool<Bullet> BulletPool { get; }
        
        public BulletFactory(ScoreController scoreController, DiContainer container, 
            BulletFactorySetting bulletFactorySetting, MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            _bulletFactorySetting = bulletFactorySetting;
            _scoreController = scoreController;
            _container = container;
            
            BulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet,
                defaultCapacity: _bulletFactorySetting.PoolCapacity);
        }
        
        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.Deactivate();
            bullet.OnKill -= _scoreController.HandleBulletKill;
        }

        private void OnGetBullet(Bullet bullet)
        {
            bullet.Activate();
            bullet.OnKill += _scoreController.HandleBulletKill;
            _monoBehaviour.StartCoroutine(LifeTimeCoroutine(bullet.GetLifeTime(), bullet));
        }

        private Bullet CreateBullet()
        {
            return _container.InstantiatePrefabForComponent<Bullet>(_bulletFactorySetting.BulletPrefab);
        }
        
        private IEnumerator LifeTimeCoroutine(float lifeTime, Bullet bullet)
        {
            yield return new WaitForSeconds(lifeTime);
            BulletPool.Release(bullet);
        }
    }
}