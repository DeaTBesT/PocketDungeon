using UnityEngine;

public class EnemyUnit : Unit
{
    protected override void Attack()
    {
        base.Attack();

        Unit e_unit = BattleManager.Instance.GetPlayerUnitsOnField[Random.Range(0, BattleManager.Instance.GetPlayerUnitsOnField.Count)];
        e_unit.TakeDamage(damage);
    }

    protected override void Death()
    {
        BattleManager.Instance.EnemyUnitDeath(this);
    }
}
