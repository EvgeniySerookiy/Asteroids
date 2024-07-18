using System.Collections;
using UnityEngine;

namespace Enemy.UFO
{
    public class UFO : MonoBehaviour, IEnemy
    {
        [SerializeField] private UFOSetting _ufoSetting;

        private Player.Player _player;
        private Camera _camera;

        public void Initialize(Player.Player player)
        {
            _camera = Camera.main;
            UpdatePlayer(player);
            StartCoroutine(MoveToRandomPoints());
        }

        public void UpdatePlayer(Player.Player player)
        {
            _player = player;
        }

        private IEnumerator MoveToRandomPoints()
        {
            while (true)
            {
                Vector2 targetPosition = GetTargetPosition();
                while ((Vector2)transform.position != targetPosition)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, _ufoSetting.Speed * Time.deltaTime);
                    yield return null;
                }
                yield return new WaitForSeconds(_ufoSetting.WaitTimeAtPoint);
            }
        }

        private Vector2 GetTargetPosition()
        {
            if (_player != null && _player.gameObject.activeInHierarchy)
            {
                return _player.transform.position;
            }
            return GetRandomSpawnPosition();
        }

        private Vector2 GetRandomSpawnPosition()
        {
            float screenWidth = _camera.orthographicSize * _camera.aspect;
            float screenHeight = _camera.orthographicSize;

            return new Vector2(
                Random.Range(-screenWidth, screenWidth),
                Random.Range(-screenHeight, screenHeight)
            );
        }
    }
}

