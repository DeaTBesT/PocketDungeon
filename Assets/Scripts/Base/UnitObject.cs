using UnityEngine;

[CreateAssetMenu(menuName = "Unit/New unit")]
public class UnitObject : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private float health;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;

    [Header("UI")]
    [SerializeField] private Sprite sprite;

    #region [PublicVars]

    public float GetHealth => health;
    public float GetAttackSpeed => attackSpeed;
    public float GetDamage => damage;
    public Sprite GetSprite => sprite;


    #endregion
}
