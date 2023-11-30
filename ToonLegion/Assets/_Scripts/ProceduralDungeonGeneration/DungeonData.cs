using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData : MonoBehaviour
{
    public List<Room> Rooms { get; set; } = new List<Room>();
    public HashSet<Vector2Int> Path { get; set; } = new HashSet<Vector2Int>();
    public GameObject PlayerReference { get; set; }

    public void Reset()
    {
        foreach (Room room in Rooms)
        {
            foreach (var item in room.PropObjectReferences)
            {
                DestroyImmediate(item);
            }

            foreach (var item in room.EnemiesInTheRoom)
            {
                DestroyImmediate(item);
            }
        }

        Rooms = new List<Room>();
        Path = new HashSet<Vector2Int>();
        DestroyImmediate(PlayerReference);
    }
}
