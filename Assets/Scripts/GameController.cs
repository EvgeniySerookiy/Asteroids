using Enemy.Asteroid;
using Enemy.UFO;
using Player;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerFactory _playerFactory;
    [SerializeField] private AsteroidFactory _asteroidFactory;
    [SerializeField] private UFOFactory _ufoFactory;
    [SerializeField] private GameOverScene _gameOverScene;
    

    private void Awake()
    {
        _playerFactory.Initialize();
        _asteroidFactory.Initialize();
        _ufoFactory.Initialize(_playerFactory.Player);
        _gameOverScene.Initialize(_playerFactory.Player);
    }

    private void Update()
    {
        _playerFactory.Player.CheckScreenBoundaries();
        _asteroidFactory.UpdateAsteroidFactory(_playerFactory.Player.transform.position);
        _ufoFactory.UpdateUFOFactory(_playerFactory.Player);
    }
}