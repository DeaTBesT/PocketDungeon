using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Transform playerRenderer;
    
    [Header("Raycast")]
    [SerializeField] private LayerMask roomsLayer;

    #region [PrivateVars]

    private Room currentRoom;
    private Camera _camera;

    #endregion

    private void Awake()
    {
        MazeBuilder.Instance.OnMapCreated += () =>
        {
            SelectRoom(MazeBuilder.Instance.GetStartRoom);
        };
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        SelectingRoom();
    }

    private void SelectingRoom()
    {
        if (Input.anyKeyDown)
        {
            RaycastHit2D hit;

            hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, roomsLayer);

            if (hit.transform == null)
            {
                return;
            }

            if (hit.transform.TryGetComponent(out Room room))
            {
                SelectRoom(room);
            }
        }
    }

    private void SelectRoom(Room room)
    {
        SetActiveRooms(false);

        currentRoom = room;
        currentRoom.ActivateRoomEvent();

        _camera.transform.position = currentRoom.transform.position + Vector3.forward * -10;
        playerRenderer.position = currentRoom.transform.position;

        SetActiveRooms(true);
    }

    private void SetActiveRooms(bool isActive)
    {
        if (currentRoom != null)
        {
            currentRoom.gameObject.SetActive(isActive);

            Room roomU = MazeBuilder.Instance.GetSpawnedRooms.GetLength(1) > currentRoom.GetRoomPosition.y + 1 ?
                MazeBuilder.Instance.GetSpawnedRooms[currentRoom.GetRoomPosition.x, currentRoom.GetRoomPosition.y + 1] : null;
            Room roomD = 0 < currentRoom.GetRoomPosition.y - 1 ? 
                MazeBuilder.Instance.GetSpawnedRooms[currentRoom.GetRoomPosition.x, currentRoom.GetRoomPosition.y - 1] : null;
            Room roomR = MazeBuilder.Instance.GetSpawnedRooms.GetLength(0) > currentRoom.GetRoomPosition.x + 1 ? 
                MazeBuilder.Instance.GetSpawnedRooms[currentRoom.GetRoomPosition.x + 1, currentRoom.GetRoomPosition.y] : null;
            Room roomL = 0 < currentRoom.GetRoomPosition.x - 1 ? 
                MazeBuilder.Instance.GetSpawnedRooms[currentRoom.GetRoomPosition.x - 1, currentRoom.GetRoomPosition.y] : null;

            if (roomU != null)
            {
                roomU.gameObject.SetActive(isActive);
            }
            if (roomD != null)
            {
                roomD.gameObject.SetActive(isActive);
            }
            if (roomR != null)
            {
                roomR.gameObject.SetActive(isActive);
            }
            if (roomL != null)
            {
                roomL.gameObject.SetActive(isActive);
            }
        }
    }
}
