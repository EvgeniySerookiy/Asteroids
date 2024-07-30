using Enemy.Asteroid;
using Enemy.UFO;
using PlayerCharacter;
using Scenes;
using Zenject;

public class GameController : ITickable, IInitializable
{
    private GameOverScene _gameOverScene;
    private AsteroidFactory _asteroidFactory;
    private UFOFactory _ufoFactory;
    private Player _player;
    
    public GameController(Player player, UFOFactory ufoFactory,
        AsteroidFactory asteroidFactory, GameOverScene gameOverScene)
    {
        _gameOverScene = gameOverScene;
        _asteroidFactory = asteroidFactory;
        _ufoFactory = ufoFactory;
        _player = player;
    }
    
    public void Initialize()
    {
        _ufoFactory.Initialize(_player);
        _gameOverScene.Initialize(_player);
    }
    
    public void Tick()
    {
        _player.CheckScreenBoundaries();
        _asteroidFactory.UpdateAsteroidFactory(_player.transform.position);
        _ufoFactory.UpdateUFOFactory(_player);
    }
}