using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door
{
    public Vector2Int Direction;
    public Vector2Int DistanceFromDoorCenter;
    public Vector2Int Center;
}

public class Room2
{
    public Room2(Vector2Int roomId, Vector2Int baseDimension, Vector2Int doorDimension)
    {
        BaseDimension = baseDimension;
        DoorDimension = doorDimension;
        RoomDimension = new Vector2Int(BaseDimension.x * 2 + DoorDimension.x, BaseDimension.y * 2 + DoorDimension.y);
        RoomId = roomId;
        // RoomCenterPos = new Vector2Int(RoomId.x * BaseDimensions.x * 3, RoomId.y * BaseDimensions.y * 5);
        RoomCenterPos = new Vector2Int(RoomId.x * (RoomDimension.x + 15), RoomId.y * (RoomDimension.y + 15));
    }
    public RoomType RoomType { get; set; }
    public Vector2Int RoomId { get; set; }
    public Vector2Int RoomCenterPos { get; set; }
    public HashSet<Vector2Int> FloorPositions { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> WallPositions { get; set; } = new HashSet<Vector2Int>();
    // public List<Vector2Int> DoorDirections { get; set; } = new List<Vector2Int>(); 
    public List<Door> Doors { get; set; } = new List<Door>();
    public Vector2Int BaseDimension { get; set; }
    public List<GameObject> EdgeColliders { get; set; } = new List<GameObject>();
    private Vector2Int RoomDimension { get; set; }
    public Vector2Int DoorDimension { get; set; }
    public HashSet<Vector2Int> ValidEnemySpawnPositions { get; set; } = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> PropsPositions { get; set; } = new HashSet<Vector2Int>();
    public List<GameObject> PropsReferences { get; set; } = new List<GameObject>();
    public List<GameObject> EnemiesReferences { get; set; } = new List<GameObject>();
    public int RemainingEnemies { get; set; }

    public void EnableEdgeColliders()
    {
        foreach (var edgeCollider in EdgeColliders)
        {
            edgeCollider.SetActive(true);
        }
    }
    public void DisableEdgeColliders()
    {
        foreach (var edgeCollider in EdgeColliders)
        {
            edgeCollider.SetActive(false);
        }
    }

    public void EnableProps()
    {
        foreach (var prop in PropsReferences)
        {
            if (prop != null)
            {
                prop.SetActive(true);
            }
        }
    }

    public void DisableProps()
    {
        foreach (var prop in PropsReferences)
        {
            if (prop != null)
            {
                prop.SetActive(false);
            }
        }
    }

    public void EnableEnemies() { 
        foreach(var enemy in EnemiesReferences)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);

            }
        }
    }
    public void DisableEnemies()
    {
        foreach (var enemy in EnemiesReferences)
        {
            if (enemy != null)
            {
               enemy.SetActive(false);

            }
        }
    }

}
