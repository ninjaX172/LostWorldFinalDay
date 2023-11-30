using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public static class WallGenerator
{
    public static void GenerateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirections);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirections);
        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TileMapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighborsBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition))
                {
                    neighborsBinaryType += "1";
                }
                else
                {
                    neighborsBinaryType += "0";
                }
            }

            tilemapVisualizer.PaintSingleCornerWall(position, neighborsBinaryType, floorPositions);
        }
    }

    private static void CreateBasicWalls(TileMapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighborsBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirections)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition))
                {
                    neighborsBinaryType += "1";
                }
                else
                {
                    neighborsBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighborsBinaryType, floorPositions);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> cardinalDirections)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var floorPosition in floorPositions)
        {
            foreach (var direction in cardinalDirections)
            {
                var neighbourPosition = floorPosition + direction;
                if (!floorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        
        }
        return wallPositions;
    }
}
