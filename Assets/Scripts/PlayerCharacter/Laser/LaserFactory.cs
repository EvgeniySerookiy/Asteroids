using System.Collections;
using Score;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace PlayerCharacter.Laser
{
    public class LaserFactory
    {
        
        private readonly ScoreController _scoreController;
        private readonly LaserFactorySetting _laserFactorySetting;
        private readonly DiContainer _container;
        private readonly MonoBehaviour _monoBehaviour;
        private bool _canShootLaser = true;
        public ObjectPool<Laser> LaserPool { get; }
        
        public LaserFactory(ScoreController scoreController, DiContainer container,
            LaserFactorySetting laserFactorySetting, MonoBehaviour monoBehaviour)
        {
            _container = container;
            _laserFactorySetting = laserFactorySetting;
            _scoreController = scoreController;
            _monoBehaviour = monoBehaviour;
            LaserPool = new ObjectPool<Laser>(CreateLaser, OnGetLaser, OnReleaseLasert,
                defaultCapacity: _laserFactorySetting.PoolCapacity);
        }
        
        private void OnReleaseLasert(Laser laser)
        {
            laser.Deactivate();
            laser.OnKill -= _scoreController.HandleBulletKill;
        }
        
        private void OnGetLaser(Laser laser)
        {
            laser.Activate();
            laser.OnKill += _scoreController.HandleBulletKill;
        }

        private Laser CreateLaser()
        {
            return _container.InstantiatePrefabForComponent<Laser>(_laserFactorySetting.LaserPrefab);
        }

        public void GetLaser(Transform muzzle)
        {
            if(!_canShootLaser)
                return;
            
            _canShootLaser = false;
            var laser = LaserPool.Get();
            laser.transform.position = muzzle.position;
            laser.SetDirection(muzzle.up);
            _monoBehaviour.StartCoroutine(LaserShootCooldown(laser));
        }
        
        private IEnumerator LaserShootCooldown(Laser laser)
        {
            yield return new WaitForSeconds(laser.GetLifeTime());
            LaserPool.Release(laser);
            yield return new WaitForSeconds(laser.GetLaserShootInterval());
            _canShootLaser = true;
        }
    }
}