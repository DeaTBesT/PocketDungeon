using UnityEngine;

[CreateAssetMenu(menuName = "Unit/New player unit")]
public class PlayerObject : UnitObject
{
    [Header("Weapon")]
    [SerializeField] private ItemObject equippedWeapon;

    [Header("Armor")]
    [SerializeField] private ArmorItem equippedHead;
    [SerializeField] private ArmorItem equippedBody;
    [SerializeField] private ArmorItem equippedLegs;
}
