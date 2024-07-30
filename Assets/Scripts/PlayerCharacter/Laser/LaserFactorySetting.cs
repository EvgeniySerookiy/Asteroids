using UnityEngine;

namespace PlayerCharacter.Laser
{
    [CreateAssetMenu(fileName = "LaserFactorySetting", menuName = "Settings/LaserFactorySetting")]
    public class LaserFactorySetting : ScriptableObject
    {
        [field: SerializeField] public Laser LaserPrefab { get; private set; }
        [field: SerializeField] public int PoolCapacity { get; private set; }
    }
}