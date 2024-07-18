using System.Collections;
using UnityEngine;

namespace Enemy.UFO
{
    public class UFOFactory : MonoBehaviour
    {
        [SerializeField] private UFO _ufoPrefab;
        [SerializeField] private float _spawnAreaPadding;
        [SerializeField] private float _timeOfRebirth;

        private Player.Player _player;
        private Camera _camera;
        private bool _isCoroutineRunning;
        private UFO _ufo;

        public void Initialize(Player.Player player)
        {
            _player = player;
            _camera = Camera.main;
            StartCoroutine(SpawnUFO());
        }

        public void UpdateUFOFactory(Player.Player player)
        {
            _player = player;
            if (_ufo != null)
            {
                _ufo.UpdatePlayer(player);
            }
            if (_ufo == null && !_isCoroutineRunning)
            {
                StartCoroutine(SpawnUFO());
            }
        }

        private IEnumerator SpawnUFO()
        {
            _isCoroutineRunning = true;
            yield return new WaitForSeconds(_timeOfRebirth);
            _ufo = Instantiate(_ufoPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            _ufo.Initialize(_player);
            _isCoroutineRunning = false;
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float screenWidth = _camera.orthographicSize * _camera.aspect;
            float screenHeight = _camera.orthographicSize;

            return Random.Range(0, 4) switch
            {
                0 => new Vector2(Random.Range(-screenWidth, screenWidth), screenHeight + _spawnAreaPadding),
                1 => new Vector2(Random.Range(-screenWidth, screenWidth), -screenHeight - _spawnAreaPadding),
                2 => new Vector2(-screenWidth - _spawnAreaPadding, Random.Range(-screenHeight, screenHeight)),
                3 => new Vector2(screenWidth + _spawnAreaPadding, Random.Range(-screenHeight, screenHeight)),
                _ => Vector2.zero,
            };
        }
    }
}
