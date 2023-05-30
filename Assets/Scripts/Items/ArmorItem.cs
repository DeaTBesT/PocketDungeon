using UnityEngine;

public class ArmorItem : ItemObject
{
    [SerializeField] private ArmorType armorType;

    public ArmorType GetArmorType => armorType;

    public enum ArmorType
    {
        Head,
        Body,
        Legs
    }
}