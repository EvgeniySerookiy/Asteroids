using IInitializable = Zenject.IInitializable;

namespace PlayerCharacter
{
    public class PlayerMoveController : IInitializable
    {
        private readonly Player _player;
        private readonly GameInput _gameInput;
        
        public PlayerMoveController(Player player, GameInput gameInput)
        {
            _player = player;
            _gameInput = gameInput;
        }

        public void Initialize()
        {
            _gameInput.OnAccelerate += _player.Accelerate;
            _gameInput.OnBreakAcceleration += _player.BreakAcceleration;
            _gameInput.OnTurnLeft += _player.TurnLeft;
            _gameInput.OnTurnRight += _player.TurnRight;
            _gameInput.OnShootBullets += _player.ShootBullets;
            _gameInput.OnShootLaser += _player.ShootLaser;
        }
    }
}