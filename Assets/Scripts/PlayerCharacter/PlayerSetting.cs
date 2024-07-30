using UnityEngine;

namespace PlayerCharacter
{
    [CreateAssetMenu(fileName = "PlayerSetting", menuName = "Settings/PlayerSetting", order = 0)]
    public class PlayerSetting : ScriptableObject
    {
        [field: SerializeField] public float MoveForce { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public Sprite SpriteShip { get; private set; }
        [field: SerializeField] public Sprite SpriteTail { get; private set; }
        [field: SerializeField] public Player PlayerPrefab { get; private set; }
    }
}