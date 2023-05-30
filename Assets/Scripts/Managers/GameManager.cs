using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private RoomEvent[] avaiableRoomEvents;

    #region [PublicVars]

    public RoomEvent[] GetAvaiableRoomEvents => avaiableRoomEvents;

    #endregion

    #region [PrivateVars]

    private WeightedRandomList<RoomEvent> roomEvents = new WeightedRandomList<RoomEvent>();

    #endregion

    private void Awake()
    {
        foreach (RoomEvent roomEvent in avaiableRoomEvents)
        {
            roomEvents.Add(roomEvent,
                roomEvent.GetEventChance
                );
        }
    }

    public RoomEvent GetRandomEvent()
    {
        return roomEvents.GetRandom();
    }
}
