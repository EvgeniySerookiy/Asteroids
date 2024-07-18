using UnityEngine;

namespace Enemy.UFO
{
    [CreateAssetMenu(fileName = "UFOSetting", menuName = "Settings/UFOSetting", order = 6)]
    public class UFOSetting : ScriptableObject
    {
        
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float WaitTimeAtPoint { get; private set; }
    }
}