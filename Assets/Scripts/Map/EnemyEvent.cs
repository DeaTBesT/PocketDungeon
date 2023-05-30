using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/New enemy event")]
public class EnemyEvent : RoomEvent
{
    [SerializeField] private int minUnits = 1;
    [SerializeField] private int maxUnits = 3;

    [Header("Prefabs")]
    [SerializeField] private EnemyObject[] avaiableEnemyUnits;

    public override void ActivateEvent()
    {
        int countUnits = Random.Range(minUnits, maxUnits);
        List<EnemyObject> addedUnits = new List<EnemyObject>();

        for (int i = 0; i < countUnits; i++)
        {
            EnemyObject unit = avaiableEnemyUnits[Random.Range(0, avaiableEnemyUnits.Length)];
            addedUnits.Add(unit);
        }

        BattleManager.Instance.StartBattle(addedUnits);
    }
}
