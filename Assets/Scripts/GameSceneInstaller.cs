using Enemy.Asteroid;
using Enemy.Asteroid.AsteroidsBig;
using Enemy.UFO;
using PlayerCharacter;
using PlayerCharacter.Bullet;
using PlayerCharacter.Laser;
using Scenes;
using Score;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private BulletFactorySetting _bulletFactorySetting;
    [SerializeField] private LaserFactorySetting _laserFactorySetting;
    [SerializeField] private UFOFactorySetting _ufoFactorySetting;
    [SerializeField] private AsteroidBigSetting _asteroidBigSetting;
    [SerializeField] private AsteroidFactorySetting _asteroidFactorySetting;
    [SerializeField] private PlayerSetting _playerSetting;

    public override void InstallBindings()
    {
        Container.Bind<ScoreView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameOverScene>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScoreController>().AsSingle();
        
        Container.Bind<PlayerSetting>().FromInstance(_playerSetting).AsSingle();
        
        Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletFactorySetting);
        Container.Bind<LaserFactory>().AsSingle().WithArguments(_laserFactorySetting);
        Container.Bind<UFOFactory>().AsSingle().WithArguments(_ufoFactorySetting);
        Container.Bind<AsteroidFactory>().AsSingle().WithArguments(_asteroidFactorySetting, _asteroidBigSetting);
        
        Container.Bind<MonoBehaviour>().FromInstance(this).AsSingle();
        Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
        Container.BindInterfacesTo<PlayerMoveController>().AsSingle();
        Container.BindInterfacesTo<GameController>().AsSingle();

        Container.Bind<Player>().FromFactory<PlayerFactory>().AsSingle().WithArguments(_playerSetting);
    }
}