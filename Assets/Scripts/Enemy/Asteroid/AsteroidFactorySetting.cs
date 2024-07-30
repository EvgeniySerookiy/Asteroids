using UnityEngine;

namespace Enemy.Asteroid
{
    [CreateAssetMenu(fileName = "AsteroidFactorySetting", menuName = "Settings/AsteroidFactorySetting")]
    public class AsteroidFactorySetting : ScriptableObject
    {
        [field: SerializeField] public float MinDistanceFromPlayer { get; private set; }
        [field: SerializeField] public int MaxCountAsteroids { get; private set; }
    }
}