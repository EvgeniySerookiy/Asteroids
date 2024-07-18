using UnityEngine;

namespace Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private GameInput _gameInput;

        private void Start()
        {
            _gameInput.OnAccelerate += _playerFactory.Player.Accelerate;
            _gameInput.OnBreakAcceleration += _playerFactory.Player.BreakAcceleration;
            _gameInput.OnTurnLeft += _playerFactory.Player.TurnLeft;
            _gameInput.OnTurnRight += _playerFactory.Player.TurnRight;
            _gameInput.OnShootBullets += _playerFactory.Player.ShootBullets;
            _gameInput.OnShootLaser += _playerFactory.Player.ShootLaser;
        }
    }
}