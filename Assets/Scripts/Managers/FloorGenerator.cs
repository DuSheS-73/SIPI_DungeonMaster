using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Provides logic for level generation
/// </summary>
public class FloorGenerator : LoadingAffector
{
    private static FloorGenerator _instance;
    public static FloorGenerator Instance => _instance;

    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface _navMeshSurface;

    [Header("Prefabs")]
    [SerializeField] private Room _startingRoom;
    [SerializeField] private Room[] _roomPrefabs;
    [SerializeField] private Room _bossRoom;

    [SerializeField] private FloorProperties _properties;

    private Room[,] _spawnedRooms;
    private Vector2Int _startingRoomPosition;

    public FloorProperties Properties => _properties; 

    private void Start()
    {
        _instance = this;

        _startingRoomPosition = new Vector2Int(_properties.MaxRoomsInRow / 2, _properties.MaxRoomsInCol / 2);

        _spawnedRooms = new Room[_properties.MaxRoomsInRow, _properties.MaxRoomsInCol];
        _spawnedRooms[_startingRoomPosition.x, _startingRoomPosition.y] = _startingRoom;

        SpawnRooms();
        SpawnBossRoom();
        ConnectRooms();

        _navMeshSurface.BuildNavMesh();

        Ready = true;
    }

    /// <summary>
    /// Finds the room in the grid, what locates at the specified grid coordinates
    /// </summary>
    /// <param name="position">Room's grid position</param>
    /// <returns>Object type of Room</returns>
    public Room GetRoomAt(Vector2Int position)
    {
        position += _startingRoomPosition;

        try
        {
            Room room = _spawnedRooms[position.x, position.y];
            return room;
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }


    /// <summary>
    /// Spawns from FloorProperties.MinAmountOfRooms to FloorProperties.MaxAmountOfRooms rooms.
    /// </summary>
    private void SpawnRooms()
    {
        int amountOfRooms = Random.Range(_properties.MinAmountOfRooms, _properties.MaxAmountOfRooms);
        for (int i = 0; i < amountOfRooms; i++)
        {
            SpawnOneRoom();
        }
    }


    /// <summary>
    /// Spawns one room at random grid position and instantiates room prefab in the worldspace
    /// </summary>
    private void SpawnOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                if (_spawnedRooms[x, y] == null)
                    continue;

                if (x > 0 && _spawnedRooms[x - 1, y] == null)
                    vacantPlaces.Add(new Vector2Int(x - 1, y));

                if (y > 0 && _spawnedRooms[x, y - 1] == null)
                    vacantPlaces.Add(new Vector2Int(x, y - 1));

                if (x < maxX && _spawnedRooms[x + 1, y] == null)
                    vacantPlaces.Add(new Vector2Int(x + 1, y));

                if (y < maxY && _spawnedRooms[x, y + 1] == null)
                    vacantPlaces.Add(new Vector2Int(x, y + 1));
            }   
        }

        InstantiateRoomAtGridPositon(
            _roomPrefabs[Random.Range(0, _roomPrefabs.Length)],
            vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count))
        );
    }

    /// <summary>
    /// Spawns the bossroom at random grid position and instantiates bossroom prefab in the worldspace
    /// </summary>
    private void SpawnBossRoom()
    {
        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                if (_spawnedRooms[x, y] != null)
                    continue;

                bool roomAtTheTop = y < maxY && _spawnedRooms[x, y + 1] != null;
                bool roomOnTheRight = x < maxX && _spawnedRooms[x + 1, y] != null;
                bool roomAtTheBottom = y > 0 && _spawnedRooms[x, y - 1] != null;
                bool roomOnTheLeft = x > 0 && _spawnedRooms[x - 1, y] != null;

                if (roomAtTheTop ^ roomOnTheRight ^ roomAtTheBottom ^ roomOnTheLeft)
                {
                    InstantiateRoomAtGridPositon(_bossRoom, new Vector2Int(x, y));
                    return;
                }
            }   
        }
    }


    /// <summary>
    /// Removes unnecessary doors from created rooms
    /// </summary>
    private void ConnectRooms()
    {
        int maxX = _spawnedRooms.GetLength(0) - 1;
        int maxY = _spawnedRooms.GetLength(1) - 1;

        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                if (_spawnedRooms[x, y] == null)
                    continue;

                if (x == 0 || x > 0 && _spawnedRooms[x - 1, y] == null)
                    _spawnedRooms[x, y].DisableDoor(DoorTypes.Left);

                if (y == 0 || y > 0 && _spawnedRooms[x, y - 1] == null)
                    _spawnedRooms[x, y].DisableDoor(DoorTypes.Bottom);

                if (x == maxX || x < maxX && _spawnedRooms[x + 1, y] == null)
                     _spawnedRooms[x, y].DisableDoor(DoorTypes.Right);

                if (y == maxY || y < maxY && _spawnedRooms[x, y + 1] == null)
                    _spawnedRooms[x, y].DisableDoor(DoorTypes.Top);
            }   
        }
    }

    private void InstantiateRoomAtGridPositon(Room roomPrefab, Vector2Int position)
    {
        Room room = Instantiate(roomPrefab);

        Vector2Int roomPosition = position - _startingRoomPosition;
        room.Init(roomPosition);

        int worldSpaceXPosition = roomPosition.x * _properties.RoomWidth + (int)_startingRoom.transform.position.x;
        int worldSpaceZPosition = roomPosition.y * _properties.RoomHeight + (int)_startingRoom.transform.position.z;

        room.transform.position = new Vector3(worldSpaceXPosition, 0, worldSpaceZPosition);

        _spawnedRooms[position.x, position.y] = room;
    }
}
