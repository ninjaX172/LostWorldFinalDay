using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;


// [System.Serializable]
// public class RoomTypeSpawnWeight
// {
//     public RoomType roomType;
//     public int spawnWeight;
// }


public class DungeonManager : Singleton<DungeonManager>
{
    private readonly Dictionary<Vector2Int, Room2> _dungeonMap = new();
    private RoomGenerator _roomGenerator;
    private AgentSpawner _agentSpawner;
    private RoomTypeProbabilityCalculator _roomTypeProbabilityCalculator;
    public Vector2Int currentRoomId;
    public Difficulty currentDifficulty = Difficulty.Easy;


    private BossRoomController _bossRoomController;

    //public override void Awake()
    private void Awake()
    {
        //base.Awake();
        _roomGenerator = GetComponent<RoomGenerator>();
        _agentSpawner = GetComponent<AgentSpawner>();
        _roomTypeProbabilityCalculator = GetComponent<RoomTypeProbabilityCalculator>();
        _bossRoomController = GetComponent<BossRoomController>();
    }

    public void OnEnable()
    {
        EventManager.Instance.DifficultyUpdate += EventManager_OnDifficultyUpdate;
    }


    public void OnDisable()
    {
        EventManager.Instance.DifficultyUpdate -= EventManager_OnDifficultyUpdate;
    }

    private void EventManager_OnDifficultyUpdate(Difficulty arg0)
    {
        currentDifficulty = arg0;
        Debug.Log("Difficulty updated to " + arg0);
    }

    public Room2 GetRoom(Vector2Int roomId)
    {
        if (!_dungeonMap.ContainsKey(roomId)) return null;
        return _dungeonMap[roomId];
    }

    public void SetCurrentRoom(Vector2Int roomId)
    {
        if (currentRoomId != null)
        {
            _dungeonMap[currentRoomId].DisableEdgeColliders();
        }
        _dungeonMap[roomId].EnableEdgeColliders();
        currentRoomId = roomId;
    }

    public Room2 GetNextRoom(Vector2Int currentRoomId, Vector2Int direction, Vector2Int dimension, Vector2Int doorDimension)
    {
        var nextRoomId = currentRoomId + direction;
        GenerateRooms(nextRoomId, dimension, doorDimension);
        SetCurrentRoom(nextRoomId);

        return _dungeonMap[nextRoomId];
    }

    public void GenerateRooms(Vector2Int roomId, Vector2Int dimension, Vector2Int doorDimension)
    {
        if (!_dungeonMap.ContainsKey(roomId))
        {
            // var room = roomGenerator.GenerateMonsterRoom(roomId, GetAdjacentRooms(roomId), dimension, doorDimension);
            var randomRoomType = _roomTypeProbabilityCalculator.GetRandomRoomType();
            if (roomId == Vector2Int.zero)
            {
                randomRoomType = RoomType.Monster;
            }
            var room = _roomGenerator.GenerateRoomByType(roomId, randomRoomType, GetAdjacentRooms(roomId), dimension,
                doorDimension);
            _dungeonMap.Add(roomId, room);
            if (randomRoomType == RoomType.Monster)
            {
                PopulateRoomWithEnemies(roomId, currentDifficulty);
            }
        }

        foreach (var direction in Direction2D.cardinalDirections)
        {
            var randomRoomType = _roomTypeProbabilityCalculator.GetRandomRoomType();
            var neighborPosition = roomId + direction;
            if (_dungeonMap.ContainsKey(neighborPosition)) continue;
            // if (!dungeonMap[roomId].DoorDirections.Contains(direction)) continue;
            if (_dungeonMap[roomId].Doors.All(door => door.Direction != direction)) continue;

            var neighborAdjacentRooms = GetAdjacentRooms(neighborPosition);
            // var room = roomGenerator.GenerateMonsterRoom(neighborPosition, neighborAdjacentRooms, dimension, doorDimension);
            var room = _roomGenerator.GenerateRoomByType(neighborPosition, randomRoomType, neighborAdjacentRooms, dimension,
                doorDimension);
            _dungeonMap.Add(neighborPosition, room);
            if (randomRoomType == RoomType.Monster)
            {
                PopulateRoomWithEnemies(neighborPosition, currentDifficulty);
            }

            if (randomRoomType == RoomType.Chill)
            {
                PopulateRoomWithMiniMinions(neighborPosition);
            }
        }
    }

    public void PopulateRoomWithEnemies(Vector2Int roomId, Difficulty difficulty)
    {
        var enemies = _agentSpawner.SpawnEnemiesGivenPossiblePositions(_dungeonMap[roomId].ValidEnemySpawnPositions.ToList(), difficulty);
        _dungeonMap[roomId].EnemiesReferences = enemies;
    }

    public void PopulateRoomWithMiniMinions(Vector2Int roomId)
    {
        var enemies = _agentSpawner.SpawnMiniMinionsGivenPossiblePositions(_dungeonMap[roomId].ValidEnemySpawnPositions.ToList(), 15);
        _dungeonMap[roomId].EnemiesReferences = enemies;

    }



    private Dictionary<Vector2Int, Room2> GetAdjacentRooms(Vector2Int roomId)
    {
        var adjacentRooms = new Dictionary<Vector2Int, Room2>();
        foreach (var direction in Direction2D.cardinalDirections)
        {
            var neighborPosition = roomId + direction;
            if (!_dungeonMap.ContainsKey(neighborPosition)) continue;
            adjacentRooms.Add(neighborPosition, _dungeonMap[neighborPosition]);
        }

        return adjacentRooms;
    }

    public Vector2 GenerateBossRoomAndGetSpawnPoint(Vector2Int position)
    {

        _bossRoomController.InstantiateRandomBossRoom(position);

        return _bossRoomController.GetPlayerSpawnPoint();
    }

    public void DestroyBossRoom()
    {
        _bossRoomController.DestroyBossRoom();
    }

    public void EnableGameObjectsForRoom(Vector2Int roomId)
    {
        _dungeonMap[roomId].EnableProps();
        _dungeonMap[roomId].EnableEnemies();
    }

    public void DisableGameObjectsForRoom(Vector2Int roomId)
    {
        _dungeonMap[roomId].DisableProps();
        _dungeonMap[roomId].DisableEnemies();
    }

}
