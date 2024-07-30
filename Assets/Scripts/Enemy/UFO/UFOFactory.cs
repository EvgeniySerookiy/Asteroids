using System.Collections;
using PlayerCharacter;
using UnityEngine;
using Zenject;

namespace Enemy.UFO
{
    public class UFOFactory
    {
        private readonly UFOFactorySetting _ufoFactorySetting;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly DiContainer _container;

        private Player _player;
        private Camera _camera;
        private bool _isCoroutineRunning;
        private UFO _ufo;

        public UFOFactory(UFOFactorySetting ufoFactorySetting, MonoBehaviour monoBehaviour, DiContainer container)
        {
            _ufoFactorySetting = ufoFactorySetting;
            _monoBehaviour = monoBehaviour;
            _container = container;
        }

        public void Initialize(Player player)
        {
            _player = player;
            _camera = Camera.main;
            _monoBehaviour.StartCoroutine(SpawnUFO());
        }

        public void UpdateUFOFactory(Player player)
        {
            _player = player;
            if (_ufo != null)
            {
                _ufo.UpdatePlayer(player);
            }

            if (_ufo == null && !_isCoroutineRunning)
            {
                _monoBehaviour.StartCoroutine(SpawnUFO());
            }
        }

        private IEnumerator SpawnUFO()
        {
            _isCoroutineRunning = true;
            
            yield return new WaitForSeconds(_ufoFactorySetting.TimeOfRebirth);
            
            _ufo = _container.InstantiatePrefabForComponent<UFO>(_ufoFactorySetting.UfoPrefab);
            _ufo.transform.position = GetRandomSpawnPosition();
            _ufo.transform.rotation = Quaternion.identity;
            _ufo.Initialize(_player);
            
            _isCoroutineRunning = false;
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float screenWidth = _camera.orthographicSize * _camera.aspect;
            float screenHeight = _camera.orthographicSize;

            return Random.Range(0, 4) switch
            {
                0 => new Vector2(Random.Range(-screenWidth, screenWidth), screenHeight + _ufoFactorySetting.SpawnAreaPadding),
                1 => new Vector2(Random.Range(-screenWidth, screenWidth), -screenHeight - _ufoFactorySetting.SpawnAreaPadding),
                2 => new Vector2(-screenWidth - _ufoFactorySetting.SpawnAreaPadding, Random.Range(-screenHeight, screenHeight)),
                3 => new Vector2(screenWidth + _ufoFactorySetting.SpawnAreaPadding, Random.Range(-screenHeight, screenHeight)),
                _ => Vector2.zero,
            };
        }
    }
}
