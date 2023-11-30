using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{   
    [SerializeField] private int minRoomWidth = 4;
    [SerializeField] private int minRoomHeight = 4;
    [SerializeField] private int dungeonWidth = 20;
    [SerializeField] private int dungeonHeight = 20;
    [SerializeField] private bool randomWalkRooms = false;
    [SerializeField] private int seed = 0;
    [SerializeField] private bool useRandomSeed = true;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;

    private DungeonData dungeonData;
    public UnityEvent OnGenerationDone;
    
    //private void Awake()
    //{
    //    dungeonData = FindObjectOfType<DungeonData>();
    //    if (dungeonData == null)
    //        dungeonData = gameObject.AddComponent<DungeonData>();
    //    // dungeonData.Reset();
    //}
    
    //void Start()
    //{
    //    GenerateDungeon();
    //}
    
    protected override void RunProceduralGeneration()
    {
        if (!useRandomSeed)
            Random.InitState(seed);
        
        dungeonData = FindObjectOfType<DungeonData>();
        if (dungeonData == null)
            dungeonData = gameObject.AddComponent<DungeonData>();
        
        dungeonData.Reset();
        CreateRooms();
        OnGenerationDone?.Invoke();
    }
    
    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
            new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), 
            minRoomWidth,
            minRoomHeight);
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floorPositions = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floorPositions = CreateSimpleRooms(roomsList);
        }
        
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        List<List<Vector2Int>> corridors = ConnectRooms(roomCenters, floorPositions);
        
        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrush3By3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
            dungeonData.Path.UnionWith(corridors[i]);
        }
        
        // floorPositions.UnionWith(corridors);
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i=0;i<roomsList.Count;i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>(); 
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin + offset) && position.y <= (roomBounds.yMax-offset))
                {
                    floorPositions.Add(position);
                    roomPositions.Add(position);
                }
            }
            dungeonData.Rooms.Add(new Room(roomCenter, roomPositions));
        }
        return floorPositions;
    }

    private List<Vector2Int> IncreaseCorridorBrush3By3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {   
                    newCorridor.Add(corridor[i] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private List<List<Vector2Int>> ConnectRooms(List<Vector2Int> roomCenters, HashSet<Vector2Int> floorPositions)
    {
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            List<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            // corridors.UnionWith(newCorridor);
            corridors.Add(newCorridor);
            floorPositions.UnionWith(newCorridor);
        }

        return corridors;
    }

    private List<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>(); 
            for (int column = offset; column < room.size.x - offset; column++)
            {
                for (int row = offset; row < room.size.y - offset; row ++ )
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floorPositions.Add(position);
                    roomPositions.Add(position);
                }
            }
            dungeonData.Rooms.Add(new Room((Vector2Int)Vector3Int.RoundToInt(room.center), roomPositions));
        }
        return floorPositions;
    }
}
