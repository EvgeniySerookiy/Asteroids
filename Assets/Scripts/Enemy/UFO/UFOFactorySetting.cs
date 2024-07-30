using UnityEngine;

namespace Enemy.UFO
{
    [CreateAssetMenu(fileName = "UFOFactorySetting", menuName = "Settings/UFOFactorySetting")]
    public class UFOFactorySetting : ScriptableObject
    {
        [field: SerializeField] public UFO UfoPrefab { get; private set; }
        [field: SerializeField] public float SpawnAreaPadding { get; private set; }
        [field: SerializeField] public float TimeOfRebirth { get; private set; }
    }
}