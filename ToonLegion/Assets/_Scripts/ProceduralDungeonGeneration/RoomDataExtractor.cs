using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDataExtractor : MonoBehaviour
{
    private DungeonData dungeonData;

    [SerializeField] private bool showGizmo = false;
    public UnityEvent OnFinishedRoomProcessing;

    //private void Awake()
    //{
    //    dungeonData = FindObjectOfType<DungeonData>();
    //}

    public void ProcessRooms()
    {
        dungeonData = FindObjectOfType<DungeonData>();
        if (dungeonData == null)
            return;

        foreach (Room room in dungeonData.Rooms)
        {
            foreach (Vector2Int tilePosition in room.FloorTiles)
            {
                int neighborsCount = 8;
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.up) == false)
                {
                    room.NearWallTilesUp.Add(tilePosition);
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.down) == false)
                {
                    room.NearWallTilesDown.Add(tilePosition);
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.left) == false)
                {
                    room.NearWallTilesLeft.Add(tilePosition);
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.right) == false)
                {
                    room.NearWallTilesRight.Add(tilePosition);
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.up + Vector2Int.left) == false)
                {
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.up + Vector2Int.right) == false)
                {
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.down + Vector2Int.left) == false)
                {
                    neighborsCount--;
                }
                if (room.FloorTiles.Contains(tilePosition + Vector2Int.down + Vector2Int.right) == false)
                {
                    neighborsCount--;
                }
                
                if (neighborsCount == 8)
                {
                    room.InnerTiles.Add(tilePosition);
                }

                if (neighborsCount <= 4)
                {
                    room.CornerTiles.Add(tilePosition);
                }
                room.NearWallTilesUp.ExceptWith(room.CornerTiles);
                room.NearWallTilesDown.ExceptWith(room.CornerTiles);
                room.NearWallTilesLeft.ExceptWith(room.CornerTiles);
                room.NearWallTilesRight.ExceptWith(room.CornerTiles);
            }
        }

        // OnDrawGizmosSelected();
        // Invoke("RunEvent",1);
        OnFinishedRoomProcessing?.Invoke();
    }

    public void RunEvent()
    {
        OnFinishedRoomProcessing?.Invoke();
    }
    private void OnDrawGizmosSelected()
    {
        if (dungeonData == null || showGizmo == false)
            return;
        foreach (Room room in dungeonData.Rooms)
        {
            //Draw inner tiles
            Gizmos.color = Color.yellow;
            foreach (Vector2Int floorPosition in room.InnerTiles)
            {
                if (dungeonData.Path.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles UP
            Gizmos.color = Color.blue;
            foreach (Vector2Int floorPosition in room.NearWallTilesUp)
            {
                if (dungeonData.Path.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles DOWN
            Gizmos.color = Color.green;
            foreach (Vector2Int floorPosition in room.NearWallTilesDown)
            {
                if (dungeonData.Path.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles RIGHT
            Gizmos.color = Color.white;
            foreach (Vector2Int floorPosition in room.NearWallTilesRight)
            {
                if (dungeonData.Path.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles LEFT
            Gizmos.color = Color.cyan;
            foreach (Vector2Int floorPosition in room.NearWallTilesLeft)
            {
                if (dungeonData.Path.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
            //Draw near wall tiles CORNERS
            Gizmos.color = Color.magenta;
            foreach (Vector2Int floorPosition in room.CornerTiles)
            {
                if (dungeonData.Path.Contains(floorPosition))
                    continue;
                Gizmos.DrawCube(floorPosition + Vector2.one * 0.5f, Vector2.one);
            }
        }
    }
}
