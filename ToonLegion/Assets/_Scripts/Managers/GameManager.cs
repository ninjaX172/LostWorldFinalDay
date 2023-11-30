using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused = false;
    private Room2 _currentRoom;
    PlayerPlacer playerPlacer;
    CameraController cameraController;
    Camera cam;
    private Room2 _previousRoom;
    [SerializeField]
    private GameObject reaperPrefab;

    private int maxBosses = 7;
    private int numOfBossDefeated = 0;

    public bool IsGrimTrial = false;

    //public override void Awake()
    private void Awake()
    {
        Time.timeScale = 1f;
        //base.Awake();
        playerPlacer = GetComponent<PlayerPlacer>();
        cameraController = GetComponent<CameraController>();
        cam = Camera.main;
    }
    public int GetMaxBosses()
    {
        return maxBosses;
    }

    public int GetNumberofBossDefeated()
    {
        return numOfBossDefeated;
    }

    void Start()
    {
        UIManager.Instance.ShowScreen(ScreenType.GameplayScreen);
        Debug.Log("Hello from GameManager");
        //hide if  want random seed
        Random.InitState(1);
        // TestDungeonGenerator();
        DungeonManager.Instance.GenerateRooms(new Vector2Int(0, 0), new Vector2Int(21, 8), new Vector2Int(1, 1));
        DungeonManager.Instance.SetCurrentRoom(new Vector2Int(0, 0));
        playerPlacer.PlacePlayer(Vector2Int.zero);
        cameraController.SetCameraTargetPosition(Vector2Int.zero);
        SpawnGrimReaper(new Vector2Int(250, 250));


        // PlacePlayer(DungeonManager.Instance.GetRoom(Vector2Int.zero));
    }

    void OnEnable()
    {
        EventManager.Instance.PlayerEnterDoor += EventManager_OnPlayerEnterDoor;
        EventManager.Instance.PortalEntered += EventManager_OnPortalEntered;
        EventManager.Instance.LeaveBossRoom += EventManager_OnLeaveBossRoom;
        EventManager.Instance.PlayerEnteredGrimTrial += EventManager_OnPlayerEnteredGrimTrial;
        EventManager.Instance.BossDeath += EventManager_OnBossDeath;
        EventManager.Instance.PlayerDefeatedAllBosses += EventManager_OnPlayerDefeatedAllBosses;
    }



    void OnDisable()
    {
        EventManager.Instance.PlayerEnterDoor -= EventManager_OnPlayerEnterDoor;
        EventManager.Instance.PortalEntered -= EventManager_OnPortalEntered;
        EventManager.Instance.LeaveBossRoom -= EventManager_OnLeaveBossRoom;
        EventManager.Instance.PlayerEnteredGrimTrial -= EventManager_OnPlayerEnteredGrimTrial;
        EventManager.Instance.BossDeath += EventManager_OnBossDeath;
        EventManager.Instance.PlayerDefeatedAllBosses -= EventManager_OnPlayerDefeatedAllBosses;


    }
    private void EventManager_OnPlayerDefeatedAllBosses()
    {
        cameraController.TriggerCameraShakeFor(2, 5f, 5f);
        cam.backgroundColor = new Color(137f / 255f, 45f / 255f, 48f / 255f);

    }
    private void EventManager_OnBossDeath(string arg0, Vector2Int arg1)
    {
        numOfBossDefeated++;
        if (numOfBossDefeated >= maxBosses)
        {
            EventManager.Instance.OnPlayerDefeatedAllBosses();
        }

        cameraController.TriggerCameraShakeFor(2, 5f, 5f);
        //cam.backgroundColor = new Color(137f / 255f, 45f / 255f, 48f / 255f);
    }

    private void EventManager_OnPlayerEnteredGrimTrial()
    {
        IsGrimTrial = true;
        // Background color: 892D30
        cameraController.TriggerCameraShakeFor(3f, 1f, 1f);
        cam.backgroundColor = new Color(137f / 255f, 45f / 255f, 48f / 255f);

    }

    private void EventManager_OnLeaveBossRoom()
    {
        playerPlacer.TeleportPlayer(_previousRoom.RoomCenterPos);
        cameraController.SetCameraTargetPosition(_previousRoom.RoomCenterPos);
        cameraController.SetCameraPosition(_previousRoom.RoomCenterPos);
        cameraController.ResetCameraToFollowTarget();
        DungeonManager.Instance.DestroyBossRoom();
        _currentRoom = _previousRoom;
    }

    private void EventManager_OnPortalEntered()
    {
        if (numOfBossDefeated == maxBosses)
        {
            return;
        }
        _previousRoom = _currentRoom;
        _currentRoom = null;

        var spawnPoint = DungeonManager.Instance.GenerateBossRoomAndGetSpawnPoint(new Vector2Int(1000, 1000));
        var intSpawnPoint = new Vector2Int((int)spawnPoint.x, (int)spawnPoint.y);

        playerPlacer.TeleportPlayer(intSpawnPoint);
        cameraController.SetCameraTargetPosition(intSpawnPoint);
        cameraController.SetCameraPosition(intSpawnPoint);
        cameraController.SetCameraToFollow(playerPlacer.playerInstance);
    }

    private void EventManager_OnPlayerEnterDoor((Vector2Int, Vector2Int) arg0)
    {
        var (roomId, direction) = arg0;
        PlayerStats ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        ps.invincibleDuration = 0.6f;

        DungeonManager.Instance.DisableGameObjectsForRoom(roomId);
        var nextRoom = DungeonManager.Instance.GetNextRoom(roomId, direction, new Vector2Int(21, 8), new Vector2Int(1, 1));
        DungeonManager.Instance.EnableGameObjectsForRoom(nextRoom.RoomId);

        playerPlacer.TeleportPlayer(nextRoom.RoomCenterPos + (nextRoom.BaseDimension * -direction));
        cameraController.SetCameraTargetPosition(nextRoom.RoomCenterPos);
        _previousRoom = _currentRoom;
        _currentRoom = nextRoom;

    }
    void PauseGame()
    {
        Time.timeScale = 0; // Pause the game by setting time scale to 0
        isPaused = true;
        // You may want to display a pause menu or do other actions when the game is paused.
    }

    void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game by setting time scale to 1
        isPaused = false;
        // You may want to hide the pause menu or do other actions when the game is resumed.
    }

    void TestDungeonGenerator()
    {
        // Random.InitState(1);
        DungeonManager.Instance.GenerateRooms(new Vector2Int(0, 0), new Vector2Int(21, 8), new Vector2Int(1, 1));
        // DungeonManager.Instance.GenerateRooms(new Vector2Int(1, 0), new Vector2Int(10, 15), new Vector2Int(1, 1));
        // DungeonManager.Instance.GenerateRooms(new Vector2Int(1, -1), new Vector2Int(10, 10), new Vector2Int(1, 1));
        // DungeonManager.Instance.GenerateRooms(new Vector2Int(-1, 0), new Vector2Int(10, 10), new Vector2Int(1, 1));
    }

    void SpawnGrimReaper(Vector2Int d)
    {
        var possiblePositions = new List<Vector2Int>
        {
            new Vector2Int(-1, 1),
            new Vector2Int(1,1),
            new Vector2Int(-1,-1),
            new Vector2Int(1,-1)
        };
        var direction = possiblePositions[Random.Range(0, possiblePositions.Count)];
        d *= direction;
        Instantiate(reaperPrefab, new Vector3(d.x, d.y, 0), Quaternion.identity);
    }
}
