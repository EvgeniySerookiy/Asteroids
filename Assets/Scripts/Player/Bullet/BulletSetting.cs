using UnityEngine;

namespace Player.Bullet
{
    [CreateAssetMenu(fileName = "BulletSetting", menuName = "Settings/BulletSetting", order = 1)]
    public class BulletSetting : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
    }
}