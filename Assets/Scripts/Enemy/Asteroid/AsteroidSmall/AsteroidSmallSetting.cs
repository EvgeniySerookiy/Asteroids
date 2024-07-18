using UnityEngine;

namespace Enemy.Asteroid.AsteroidSmall
{
    [CreateAssetMenu(fileName = "AsteroidSmallSetting", menuName = "Settings/AsteroidSmallSetting", order = 4)]
    public class AsteroidSmallSetting : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Sprite[] SpriteSmall { get; private set; }
    }
}