using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private GameObject doorU;
    [SerializeField] private GameObject doorR;
    [SerializeField] private GameObject doorD;
    [SerializeField] private GameObject doorL;

    #region [PublicVars]

    public GameObject DoorU => doorU;
    public GameObject DoorR => doorR;
    public GameObject DoorD => doorD;
    public GameObject DoorL => doorL;

    public Vector2Int GetRoomPosition => roomPosition;

    #endregion

    #region [PrivateVars]

    private Vector2Int roomPosition;

    private RoomEvent currentRoomEvent;

    #endregion

    public void SetupRoom(Vector2Int roomPosition, RoomEvent roomEvent)
    {
        this.roomPosition = roomPosition;
        currentRoomEvent = roomEvent;
    }

    public void ActivateRoomEvent()
    {
        if (currentRoomEvent == null)
        {
            return;
        }

        currentRoomEvent.ActivateEvent();
    }

    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);

        for (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 0, -90);

            GameObject tmp = doorL;
            doorL = doorD;
            doorD = doorR;
            doorR = doorU;
            doorU = tmp;
        }
    }
}
