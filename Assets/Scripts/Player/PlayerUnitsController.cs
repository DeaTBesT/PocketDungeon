using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitsController : Singleton<PlayerUnitsController>
{
    [Header("Main unit")]
    [SerializeField] private PlayerObject mainUnit;
    [SerializeField] private SpriteRenderer mainUnitRenderer;

    [Header("Units")]
    [SerializeField] private PlayerObject[] startUnits;

    #region [PublicVars]

    public PlayerObject GetMainUnit => mainUnit;
    public List<PlayerObject> GetPlayerUnits => playerUnits;

    #endregion

    #region [PrivateVars]

    private List<PlayerObject> playerUnits = new List<PlayerObject>();
     
    #endregion

    private void Start()
    {
        SetupMainUnit();

        playerUnits.Add(mainUnit);

        if (startUnits.Length > 0)
        {
            playerUnits.AddRange(startUnits);
        }
    }

    public void AddUnit(PlayerObject unit)
    {
        playerUnits.Add(unit);
    }

    public void RemoveUnit(PlayerObject unit)
    {
        playerUnits.Remove(unit);
    }

    private void SetupMainUnit()
    {
        mainUnitRenderer.sprite = mainUnit.GetSprite;
    }
}
