using UnityEngine;

namespace PlayerCharacter.Bullet
{
    [CreateAssetMenu(fileName = "BulletFactorySetting", menuName = "Settings/BulletFactorySetting")]
    public class BulletFactorySetting : ScriptableObject
    {
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public int PoolCapacity { get; private set; }
    }
}