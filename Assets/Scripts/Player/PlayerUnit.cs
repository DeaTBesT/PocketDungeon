using UnityEngine;

public class PlayerUnit : Unit
{
    protected override void Attack()
    {
        base.Attack();

        Unit e_unit = BattleManager.Instance.GetEnemyUnitsOnField[Random.Range(0, BattleManager.Instance.GetEnemyUnitsOnField.Count)];
        e_unit.TakeDamage(damage);
    }

    protected override void Death()
    {
        BattleManager.Instance.PlayerUnitDeath(this);
    }
}
