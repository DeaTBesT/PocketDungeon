using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Unit : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float damage;

    [Header("UI")]
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Animations")]
    [SerializeField] private Animator _animator;

    #region [PublicVars]

    public float Health
    {
        get => health;
        protected set
        {
            health = value;
        }
    }

    public Sprite GetSprite => spriteRenderer.sprite;

    #endregion

    #region [PrivateVars]

    private Coroutine currentBattle;

    private readonly string ATTACK = "Attack";

    #endregion

    public virtual void SetupUnit(UnitObject unit)
    {
        Health = unit.GetHealth;
        attackSpeed = unit.GetAttackSpeed;
        damage = unit.GetDamage;

        spriteRenderer.sprite = unit.GetSprite;
    }

    public virtual void TakeDamage(float ammount)
    {
        Health -= ammount;

        if (Health > 0)
        {
            return;   
        }

        Death();
    }

    protected virtual void Death()
    {
        Debug.Log("Unit is die");
    }

    public virtual void StartBattle()
    {
        if (currentBattle != null)
        {
            return;
        }

        currentBattle = StartCoroutine(Battle());
    }

    public virtual void EndBattle()
    {
        if (currentBattle == null)
        {
            return;
        }

        StopCoroutine(currentBattle);
        currentBattle = null;
    }

    private IEnumerator Battle()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);

            Attack();
        }
    }

    protected virtual void Attack()
    {
        _animator.SetTrigger(ATTACK);
    }
}
