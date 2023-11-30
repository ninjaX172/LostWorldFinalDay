using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLength = 14;
    [SerializeField] private int corridorCount = 3;
    [SerializeField] 
    [Range(0f, 1f)]
    private float roomPercent = 0.8f;
    [SerializeField] private int seed = 0;
    [SerializeField] private bool useRandomSeed = true;
    private DungeonData dungeonData;
    public UnityEvent OnGenerationDone;
    
    //private void Awake()
    //{
    //    dungeonData = FindObjectOfType<DungeonData>();
    //    if (dungeonData == null)
    //        dungeonData = gameObject.AddComponent<DungeonData>();
    //}
    //void Start()
    //{
    //    // GenerateDungeon();
    //}
    
    protected override void RunProceduralGeneration()
    {
        if (!useRandomSeed)
            Random.InitState(seed);
        
        dungeonData = FindObjectOfType<DungeonData>();
        if (dungeonData == null)
            dungeonData = gameObject.AddComponent<DungeonData>();
        
        dungeonData.Reset();
        CorridorFirstGeneration();
        OnGenerationDone?.Invoke();

    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrush3By3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
            dungeonData.Path.UnionWith(corridors[i]);
        }
        
        
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, tilemapVisualizer);
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

    

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var deadEnd in deadEnds)
        {
            if (roomPositions.Contains(deadEnd))
                continue;
            var roomPosition = RunRandomWalk(randomWalkParameters, deadEnd);
            roomPositions.UnionWith(roomPosition);
            dungeonData.Rooms.Add(new Room(deadEnd, roomPosition));
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighbourCount = 0;
            foreach (var direction in Direction2D.cardinalDirections)
            {
                if (floorPositions.Contains(position + direction)) 
                    neighbourCount++;
            }
            if (neighbourCount == 1) 
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid())
            .Take(roomToCreateCount)
            .ToList();
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
            dungeonData.Rooms.Add(new Room(roomPosition, roomFloor));
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();
        
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
            dungeonData.Path.UnionWith(corridor);
        }
        
        return corridors;
    }
}
