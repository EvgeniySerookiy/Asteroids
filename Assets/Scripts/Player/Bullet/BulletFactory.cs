using System.Collections;
using Score;
using UnityEngine;
using UnityEngine.Pool;

namespace Player.Bullet
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private ScoreController _scoreController;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _poolCapacity;
        
        public ObjectPool<Bullet> BulletPool { get; private set; }

        private void Awake()
        {
            BulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet,
                defaultCapacity: _poolCapacity);
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
            StartCoroutine(LifeTimeCoroutine(bullet.GetLifeTime(), bullet));
        }

        private Bullet CreateBullet()
        {
            return Instantiate(_bulletPrefab);
        }
        
        private IEnumerator LifeTimeCoroutine(float lifeTime, Bullet bullet)
        {
            yield return new WaitForSeconds(lifeTime);
            BulletPool.Release(bullet);
        }
    }
}