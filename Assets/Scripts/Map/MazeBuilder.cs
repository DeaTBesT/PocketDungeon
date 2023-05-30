using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MazeBuilder : Singleton<MazeBuilder>
{
    [Header("Maze settings")]
    [SerializeField] private int minRooms;
    [SerializeField] private int maxRooms;

    [Header("Prefabs")]
    [SerializeField] private Room[] roomPrefabs;
    [SerializeField] private Room startingRoom;

    #region [PublicVars]

    public Room[,] GetSpawnedRooms => spawnedRooms;
    public Room GetStartRoom => startingRoom;

    public UnityAction OnMapCreated { get; set; }

    #endregion

    #region [PrivateVars]

    private Room[,] spawnedRooms;

    private List<Room> roomsList = new List<Room>();
    private List<Vector2Int> roomsPosition = new List<Vector2Int>();

    #endregion

    private void Start()
    {
        RoomSpawnSetup();
    }

    private void RoomSpawnSetup()
    {
        spawnedRooms = new Room[10, 10];
        spawnedRooms[0, 0] = startingRoom;

        roomsList.Add(startingRoom);
        roomsPosition.Add(Vector2Int.zero);

        int randomValue = Random.Range(minRooms, maxRooms);

        for (int i = 0; i < randomValue; i++)
        {
            PlaceOneRoom(i);
        }

        int roomID = 0;

        foreach (Room room in roomsList)
        {
            CheckForNeighbours2OpenDoors(room, roomsPosition[roomID]);
            room.gameObject.SetActive(false);

            roomID++;
        }

        OnMapCreated?.Invoke();
    }

    private void PlaceOneRoom(int id)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null)
                {
                    continue;
                }

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x - 1, y));
                }
                if (y > 0 && spawnedRooms[x, y - 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x, y - 1));
                }
                if (x < maxX && spawnedRooms[x + 1, y] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x + 1, y));
                }
                if (y < maxY && spawnedRooms[x, y + 1] == null)
                {
                    vacantPlaces.Add(new Vector2Int(x, y + 1));
                }
            }
        }

        // Эту строчку можно заменить на выбор комнаты с учётом её вероятности, вроде как в ChunksPlacer.GetRandomChunk()
        Room newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                roomsList.Add(newRoom);
                roomsPosition.Add(position);

                newRoom.transform.position = new Vector2(position.x, position.y);
                newRoom.SetupRoom(position, GameManager.Instance.GetRandomEvent());
                spawnedRooms[position.x, position.y] = newRoom;
                newRoom.name = $"Room {id} ";
                return;
            }
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null)
        {
            neighbours.Add(Vector2Int.up);
        }
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null)
        {
            neighbours.Add(Vector2Int.down);
        }
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null)
        {
            neighbours.Add(Vector2Int.right);
        }
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null)
        {
            neighbours.Add(Vector2Int.left);
        }

        if (neighbours.Count == 0)
        {
            return false;
        }

        return true;
    }

    private void CheckForNeighbours2OpenDoors(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) 
        { 
            neighbours.Add(Vector2Int.up); 
        }
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) 
        { 
            neighbours.Add(Vector2Int.down); 
        }
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) 
        { 
            neighbours.Add(Vector2Int.right);
        }
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) 
        { 
            neighbours.Add(Vector2Int.left); 
        }

        if (neighbours.Count == 0)
        {
            return;
        }

        foreach (Vector2Int selectedDirection in neighbours)
        {
            Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

            if (selectedDirection == Vector2Int.up)
            {
                room.DoorU.SetActive(false);
                selectedRoom.DoorD.SetActive(false);
            }
            if (selectedDirection == Vector2Int.down)
            {
                room.DoorD.SetActive(false);
                selectedRoom.DoorU.SetActive(false);
            }
            if (selectedDirection == Vector2Int.right)
            {
                room.DoorR.SetActive(false);
                selectedRoom.DoorL.SetActive(false);
            }
            if (selectedDirection == Vector2Int.left)
            {
                room.DoorL.SetActive(false);
                selectedRoom.DoorR.SetActive(false);
            }
        }
    }
}
