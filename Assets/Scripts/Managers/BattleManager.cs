using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [Header("Start battle")]
    [SerializeField] private Camera cameraDungeon;
    [SerializeField] private GameObject battleField;
    [SerializeField] private GameObject battlePanel;

    [Header("Battle field")]
    [SerializeField] private Unit[] playerPositions;
    [SerializeField] private EnemyUnit[] enemyPositions;

    #region [PublicVars]

    public List<Unit> GetPlayerUnitsOnField => playerUnitsOnField;
    public List<Unit> GetEnemyUnitsOnField => enemyUnitsOnField;
    public List<Unit> GetUnitsOnField => unitsOnField;

    #endregion

    #region [PrivateVars]

    private List<Unit> playerUnitsOnField = new List<Unit>();
    private List<Unit> enemyUnitsOnField = new List<Unit>();
    private List<Unit> unitsOnField = new List<Unit>();

    #endregion

    public void StartBattle(List<EnemyObject> enemyUnit)
    {
        playerUnitsOnField.Clear();
        enemyUnitsOnField.Clear();
        unitsOnField.Clear();
        battleField.SetActive(true);
        battlePanel.SetActive(true);

        List<PlayerObject> playerUnit = PlayerUnitsController.Instance.GetPlayerUnits;

        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerPositions[i].gameObject.SetActive(false);
            enemyPositions[i].gameObject.SetActive(false);
        }

        cameraDungeon.gameObject.SetActive(false);

        int placeId = 0;

        foreach (PlayerObject p_unit in playerUnit)
        {
            playerUnitsOnField.Add(playerPositions[placeId]);
            unitsOnField.Add(playerPositions[placeId]);
            playerPositions[placeId].gameObject.SetActive(true);
            playerPositions[placeId].SetupUnit(p_unit);

            placeId++;
        }

        placeId = 0;

        foreach (EnemyObject e_unit in enemyUnit)
        {
            enemyUnitsOnField.Add(enemyPositions[placeId]);
            unitsOnField.Add(enemyPositions[placeId]);
            enemyPositions[placeId].gameObject.SetActive(true);
            enemyPositions[placeId].SetupUnit(e_unit);

            placeId++;
        }

        foreach (Unit unit in unitsOnField)
        {
            unit.StartBattle();
        }
    }

    private void EndBattle(bool isWin)
    {
        Debug.Log($"Is player win : {isWin}");

        battleField.SetActive(false);
        battlePanel.SetActive(false);
        cameraDungeon.gameObject.SetActive(true);

        foreach (Unit p_unit in playerPositions)
        {
            p_unit.EndBattle();
        }

        foreach (Unit e_unit in enemyPositions)
        {
            e_unit.EndBattle();
        }
    }

    public void PlayerUnitDeath(Unit unit)
    {
        playerUnitsOnField.Remove(unit);

        if (playerUnitsOnField.Count > 0)
        {
            return;
        }

        EndBattle(false);
    }

    public void EnemyUnitDeath(Unit unit)
    {
        enemyUnitsOnField.Remove(unit);

        if (enemyUnitsOnField.Count > 0)
        {
            return;
        }

        EndBattle(true);
    }
}
