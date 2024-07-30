using UnityEngine;

namespace PlayerCharacter.Laser
{
    [CreateAssetMenu(fileName = "LaserSetting", menuName = "Settings/LaserSetting", order = 2)]
    public class LaserSetting : ScriptableObject
    {
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float LaserShootInterval { get; private set; }
    }
}