using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator:AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;
   
    protected override void RunProceduralGeneration()
    {  
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        // tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        if (randomWalkParameters.useRandomSeed)
            randomWalkParameters.seed = Random.Range(int.MinValue, int.MaxValue);
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            HashSet<Vector2Int> visited = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength, randomWalkParameters.seed);
            floorPosition.UnionWith(visited);
            if (randomWalkParameters.startRandomlyEachIteration)
            {
                currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
            }
            
        }
        return floorPosition;
    }
}
