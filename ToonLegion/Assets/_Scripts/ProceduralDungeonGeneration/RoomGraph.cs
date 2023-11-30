using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGraph
{
    
    Dictionary<Vector2Int, List<Vector2Int>> graph = new Dictionary<Vector2Int, List<Vector2Int>>();
    public RoomGraph(HashSet<Vector2Int> roomFloor)
    {
        foreach (var position in roomFloor)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            foreach (var direction in Direction2D.cardinalDirections)
            {
                var neighborPosition = position + direction;
                if (roomFloor.Contains(neighborPosition))
                {
                    neighbors.Add(neighborPosition);
                }
            }
            graph.Add(position, neighbors);
        }
    }
    
    public Dictionary<Vector2Int, Vector2Int> GetReachableTilesByBFS(Vector2Int startPosition, Room room )
    {
        HashSet<Vector2Int> occupiedNodes = room.PropPositions;
        Dictionary<Vector2Int, Vector2Int> reachableTiles = new Dictionary<Vector2Int, Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(startPosition);
        reachableTiles.Add(startPosition, startPosition);
        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            foreach (var neighbor in graph[current])
            {
                if (reachableTiles.ContainsKey(neighbor) == false && 
                    occupiedNodes.Contains(neighbor) == false &&
                    IsNearWall(neighbor, room) == false)
                {
                    reachableTiles.Add(neighbor, current);
                    queue.Enqueue(neighbor);
                }
            }
        }
        return reachableTiles;
    }

    private bool IsNearWall(Vector2Int neighbor, Room room)
    {
        if (room.NearWallTilesUp.Contains(neighbor) ||
            room.NearWallTilesDown.Contains(neighbor) ||
            room.NearWallTilesLeft.Contains(neighbor) ||
            room.NearWallTilesRight.Contains(neighbor))
        {
            return true;
        }
        return false;
    }
}
