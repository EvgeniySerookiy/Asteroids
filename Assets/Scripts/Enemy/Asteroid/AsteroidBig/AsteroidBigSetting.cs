using UnityEngine;

namespace Enemy.Asteroid.AsteroidBig
{
    [CreateAssetMenu(fileName = "AsteroidBigSetting", menuName = "Settings/AsteroidBigSetting", order = 2)]
    public class AsteroidBigSetting : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Sprite[] SpriteBig { get; private set; }
    }
}