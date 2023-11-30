using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class AgentPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<int> enemyWeights;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int playerRoomIndex;
    [SerializeField] private int minEnemiesPerRoom;
    [SerializeField] private int maxEnemiesPerRoom;
    [SerializeField] private CinemachineVirtualCamera  vCamera;
    [SerializeField] private GameObject agentContainer;
    [SerializeField] private GameObject playerContainer;

    private DungeonData dungeonData;

    //private void Awake()
    //{
    //    dungeonData = FindObjectOfType<DungeonData>();
    //}

    public void PlaceAgents()
    {
        dungeonData = FindObjectOfType<DungeonData>();
        if (dungeonData == null)
            return;
        WeightedRandomSelector<GameObject> enemyWeightedRandomSelector = new WeightedRandomSelector<GameObject>();
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            enemyWeightedRandomSelector.Add(enemyPrefabs[i], enemyWeights[i]);
        }
        for (var i = 0; i < dungeonData.Rooms.Count; i++)
        {
            var room = dungeonData.Rooms[i];
            RoomGraph roomGraph = new RoomGraph(room.FloorTiles);
            HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>(room.FloorTiles);
            roomFloor.IntersectWith(dungeonData.Path);
            //if (roomFloor.Count == 0)
            //{
            //    continue;
            //}
            // print(roomFloor.Count);
            Dictionary<Vector2Int, Vector2Int> reachableTiles = roomGraph.GetReachableTilesByBFS(roomFloor.First(), room);
            room.PositionsAccessibleFromPath = reachableTiles.Keys.OrderBy(x => Random.value).ToList();
            PlaceEnemies(room, enemyWeightedRandomSelector, Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom + 1));
        }
        // print(dungeonData.Rooms[playerRoomIndex]);
        PlacePlayer(dungeonData.Rooms[playerRoomIndex]);
    }

    private void PlaceEnemies(Room room, WeightedRandomSelector<GameObject> randomSelector,int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            if (room.PositionsAccessibleFromPath.Count <= i)
            {
                return;
            }
            GameObject enemy = Instantiate(randomSelector.GetRandomWeightedItem());
            var temp = room.PositionsAccessibleFromPath[i] + Vector2.one * 0.5f;
            Vector3 newPosition = new Vector3(temp.x, temp.y, -2f);
            enemy.transform.localPosition = newPosition;
            enemy.transform.parent = agentContainer.transform;
            room.EnemiesInTheRoom.Add(enemy);
        }
    }

    private void PlacePlayer(Room room)
    {
        GameObject player = Instantiate(playerPrefab);
        var temp = room.RoomCenterPos + Vector2.one * 0.5f;
        player.transform.localPosition = new Vector3(temp.x, temp.y, -2f);
        player.transform.parent = playerContainer.transform;
        vCamera.Follow = player.transform;
        vCamera.LookAt = player.transform;
        dungeonData.PlayerReference = player;
    }
}
