using Player.Bullet;
using Player.Laser;
using UnityEngine;

namespace Player
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private PlayerSetting playerSetting;
        [SerializeField] private BulletFactory bulletFactory;
        [SerializeField] private LaserFactory laserFactory;
        public Player Player { get; private set; }
        
        public void Initialize()
        {
            Player = Instantiate(_playerPrefab);
            Player.Initialize(bulletFactory, laserFactory, playerSetting);
        }
    }
}