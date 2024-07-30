using Zenject;

namespace PlayerCharacter
{
    public class PlayerFactory : IFactory<Player>
    {
        private readonly DiContainer _container;
        private readonly PlayerSetting _playerSetting;
        
        public PlayerFactory(DiContainer container, PlayerSetting playerSetting)
        {
            _container = container;
            _playerSetting = playerSetting;
        }

        public Player Create()
        {
            var player = _container.InstantiatePrefabForComponent<Player>(_playerSetting.PlayerPrefab);
            player.Initialize(_playerSetting);
            return player;
        }
    }
}