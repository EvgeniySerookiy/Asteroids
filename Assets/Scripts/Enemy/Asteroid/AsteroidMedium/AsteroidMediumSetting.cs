using UnityEngine;

namespace Enemy.Asteroid.AsteroidMedium
{
    [CreateAssetMenu(fileName = "AsteroidMediumSetting", menuName = "Settings/AsteroidMediumSetting", order = 3)]
    public class AsteroidMediumSetting : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Sprite[] SpriteMedium { get; private set; }
    }
}