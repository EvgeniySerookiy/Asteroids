using System.Collections;
using Score;
using UnityEngine;
using UnityEngine.Pool;

namespace Player.Laser
{
    public class LaserFactory : MonoBehaviour
    {
        [SerializeField] private ScoreController _scoreController;
        
        [SerializeField] private Laser _laserPrefab;
        [SerializeField] private int _poolCapacity;
        
        public ObjectPool<Laser> LaserPool { get; private set; }
        private bool _canShootLaser = true;

        private void Awake()
        {
            LaserPool = new ObjectPool<Laser>(CreateLaser, OnGetLaser, OnReleaseLasert,
                defaultCapacity: _poolCapacity);
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
            return Instantiate(_laserPrefab);
        }

        public void GetLaser(Transform muzzle)
        {
            if(!_canShootLaser)
                return;
            
            _canShootLaser = false;
            var laser = LaserPool.Get();
            laser.transform.position = muzzle.position;
            laser.SetDirection(muzzle.up);
            StartCoroutine(LaserShootCooldown(laser));
        }
        
        private IEnumerator LaserShootCooldown(global::Player.Laser.Laser laser)
        {
            yield return new WaitForSeconds(laser.GetLifeTime());
            LaserPool.Release(laser);
            yield return new WaitForSeconds(laser.GetLaserShootInterval());
            _canShootLaser = true;
        }
    }
}