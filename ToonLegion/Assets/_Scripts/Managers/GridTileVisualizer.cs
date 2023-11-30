using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


[System.Serializable]
public class TileFloorParameters
{
    public TileBase floorTile;
    public int weight;
}



public class GridTileVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private Tilemap wallTilemap;
    [SerializeField]
    private List<TileFloorParameters> floorTileParameters;

    [SerializeField] private TileBase floorTop;
    [SerializeField] private TileBase floorLeft;
    [SerializeField] private TileBase floorRight;
    [SerializeField] private TileBase floorTopLeftCorner;
    [SerializeField] private TileBase floorTopRightCorner;
    [SerializeField] private TileBase floorBottomLeftCorner;
    [SerializeField] private TileBase floorBottomRightCorner;

    [SerializeField] private TileBase basicWallTopLevel1;
    [SerializeField] private TileBase basicWallTopLevel2;
    [SerializeField] private TileBase basicWallBottom;

    [SerializeField] private TileBase basicWallLeft;
    [SerializeField] private TileBase basicWallRight;



    [SerializeField] private TileBase innerWallBottomRightLevel1;
    [SerializeField] private TileBase innerWallBottomRightLevel2;
    [SerializeField] private TileBase innerWallBottomLeftLevel1;
    [SerializeField] private TileBase innerWallBottomLeftLevel2;
    [SerializeField] private TileBase innerWallTopRightCorner;
    [SerializeField] private TileBase innerWallTopLeftCorner;

    [SerializeField] private TileBase outerWallTopRightCorner;
    [SerializeField] private TileBase outerWallTopLeftCorner;

    [SerializeField] private TileBase outerWallBottomRightCorner;
    [SerializeField] private TileBase outerWallBottomLeftCorner;

    private WeightedRandomSelector<TileBase> floorTileSelector = new WeightedRandomSelector<TileBase>();

    private List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new(-1, 1),
        new(0, 1),
        new(1, 1),
        new(-1, 0),
        new(0, 0),
        new(1, 0),
        new(-1, -1),
        new(0, -1),
        new(1, -1),
    };

    void Start()
    {
        foreach (var tileFloorParameter in floorTileParameters)
        {
            floorTileSelector.Add(tileFloorParameter.floorTile, tileFloorParameter.weight);
        }
    }

    public void PaintWallTiles(IEnumerable<Vector2Int> wallPositions)
    {
        var wallPositionsByTileBase = AssignTileBaseToWalls(wallPositions);
        foreach (var position in wallPositionsByTileBase)
        {
            // PaintSingleTile(position, wallTilemap, basicWallTopLevel1);
            PaintSingleTile(position.Key, wallTilemap, position.Value);
        }
    }


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions, Room2 room)
    {
        var edgePositions = IdentifyEdgePositions(floorPositions);
        var floorPositionsByTileBase = AssignTileBaseToPositions(floorPositions, edgePositions, room);

        foreach (var position in floorPositionsByTileBase)
        {
            PaintSingleTile(position.Key, floorTilemap, position.Value);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        tilemap.SetTile(new Vector3Int(position.x, position.y, 0), tile);
    }


    private IEnumerable<Vector2Int> IdentifyEdgePositions(IEnumerable<Vector2Int> floorPositions)
    {
        int randomIndex = UnityEngine.Random.Range(0, floorPositions.Count());
        var startingPoint = floorPositions.ElementAt(randomIndex);
        // var startingPoint = floorPositions.GetEnumerator().Current;
        while (floorPositions.Contains(startingPoint))
            startingPoint += Vector2Int.up;
        startingPoint -= Vector2Int.up;

        var positionsToCheck = new Queue<Vector2Int>();
        positionsToCheck.Enqueue(startingPoint);

        var maxCount = 100000;
        var edgePositions = new HashSet<Vector2Int>();
        var visitedPositions = new HashSet<Vector2Int>();
        while (positionsToCheck.Count > 0)
        {
            if (maxCount-- < 0)
            {
                Debug.Log("ERROR: maxCount reached");
                break;
            }

            var position = positionsToCheck.Dequeue();

            if (visitedPositions.Contains(position) || !floorPositions.Contains(position))
                continue;

            visitedPositions.Add(position);

            var isEdge = false;
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition)) continue;
                isEdge = true;
                break;
            }

            if (!isEdge) continue;
            {
                edgePositions.Add(position);
                foreach (var direction in Direction2D.cardinalDirections)
                {
                    var neighborPosition = position + direction;
                    positionsToCheck.Enqueue(neighborPosition);
                }
            }
        }

        return edgePositions;
    }


    private Dictionary<Vector2Int, TileBase> AssignTileBaseToWalls(IEnumerable<Vector2Int> wallPositions)
    {
        var tileBaseByPosition = new Dictionary<Vector2Int, TileBase>();
        /*
         * Toggle a bit if position in that direction does exist
         *      0 1 2
         *      3 4 5       0 1 2 3 4 5 6 7 8
         *      6 7 8
         */
        var wallTable = new Dictionary<string, List<TileBase>>()
        {
            { "000000111", new List<TileBase>() { basicWallTopLevel1, basicWallTopLevel2 } },
            { "001001111",  new List<TileBase>() { innerWallBottomRightLevel1, innerWallBottomRightLevel2 }},
            { "001001001",  new List<TileBase>() {  basicWallLeft}},
            { "100100111",  new List<TileBase>() {  innerWallBottomLeftLevel1, innerWallBottomLeftLevel2}},
            { "000001001",  new List<TileBase>() {  basicWallLeft}},
            { "000000001",  new List<TileBase>() {  basicWallLeft, outerWallTopRightCorner}},
            { "000000011",  new List<TileBase>() {  basicWallTopLevel1, basicWallTopLevel2 }},
            { "000000110",  new List<TileBase>() {  basicWallTopLevel1, basicWallTopLevel2 }},
            { "000100100",  new List<TileBase>() {  basicWallRight }},
            { "100100100",  new List<TileBase>() {  basicWallRight}},
            { "111000000",  new List<TileBase>() {  basicWallBottom}},
            { "111001001",  new List<TileBase>() {  innerWallTopRightCorner}},
            { "111100100",  new List<TileBase>() {  innerWallTopLeftCorner}},
            { "001000000",  new List<TileBase>() {  outerWallBottomLeftCorner}},
            { "001001000",  new List<TileBase>() {  basicWallLeft}},
            { "100100000",  new List<TileBase>() {  basicWallRight}},
            { "110000000",  new List<TileBase>() {  basicWallBottom}},
            { "011000000",  new List<TileBase>() {  basicWallBottom}},
            { "100000000",  new List<TileBase>() {  outerWallBottomRightCorner}},
            { "000000100",  new List<TileBase>() {  basicWallRight, outerWallTopLeftCorner}},
        };

        foreach (var position in wallPositions)
        {
            var binaryType = "";
            foreach (var direction in eightDirectionsList)
            {
                if (floorTilemap.GetTile((Vector3Int)(position + direction)) != null)
                    binaryType += "1";
                else
                    binaryType += "0";
            }

            if (!wallTable.ContainsKey(binaryType))
            {
                // tileBaseByPosition.Add(position, basicWallTopLevel1);
                continue;
            }
            if (wallTable[binaryType].Count > 1)
            {
                if (tileBaseByPosition.ContainsKey(position))
                {
                    tileBaseByPosition.Remove(position);
                }
                tileBaseByPosition.Add(position, wallTable[binaryType][0]);
                tileBaseByPosition.Add(position + Vector2Int.up, wallTable[binaryType][1]);
                continue;
            }
            if (tileBaseByPosition.ContainsKey(position))
            {
                continue;
            }
            tileBaseByPosition.Add(position, wallTable[binaryType][0]);

        }

        return tileBaseByPosition;

    }

    private Dictionary<Vector2Int, TileBase> AssignTileBaseToPositions(IEnumerable<Vector2Int> floorPositions,
        IEnumerable<Vector2Int> edgePositions, Room2 room)
    {
        var nonEdgePositions = floorPositions.Except(edgePositions);

        var tileBaseByPosition = new Dictionary<Vector2Int, TileBase>();
        foreach (var position in nonEdgePositions)
        {
            // tileBaseByPosition.Add(position, floorTiles[Random.Range(0, floorTiles.Count)]);
            var tileFloor = floorTileSelector.GetRandomWeightedItem();
            tileBaseByPosition.Add(position, tileFloor);
        }


        /*
         * Toggle a bit if position in that direction does not exist
         *      0 1 2
         *      3 4 5       0 1 2 3 4 5 6 7 8
         *      6 7 8
         */
        var floorTable = new Dictionary<string, TileBase>()
        {
            {"111000000", floorTop },
            {"110000000", floorTop },
            {"011000000", floorTop },
            {"100100100", floorLeft},
            {"100100000", floorLeft},
            {"000100100", floorLeft},
            {"000100110", floorLeft},
            {"100100111", floorLeft},
            {"100100110", floorLeft},
            {"001001011", floorRight},
            {"001001111", floorRight},
            {"001001001", floorRight},
            {"000001001", floorRight},
            {"001001000", floorRight},
            {"000001011", floorRight},
            {"111100100", floorTopLeftCorner},
            {"111001001", floorTopRightCorner},
            {"001000000", floorBottomLeftCorner},
            {"100000000", floorBottomRightCorner}
        };

        var quadrantsToSkip = new List<int>();
        // if (room.DoorDirections.Contains(Vector2Int.left))
        // {  
        //     quadrantsToSkip.Add(3);
        // }
        if (room.Doors.Any(x => x.Direction == Vector2Int.left))
        {
            quadrantsToSkip.Add(3);
        }
        // if (room.DoorDirections.Contains(Vector2Int.right))
        // {
        //     quadrantsToSkip.Add(4);
        // }
        if (room.Doors.Any(x => x.Direction == Vector2Int.right))
        {
            quadrantsToSkip.Add(4);
        }

        // var specialCase = room.DoorDirections.Contains(Vector2Int.down) ? new List<int> {3,4} : new List<int>();
        var specialCase = room.Doors.Any(x => x.Direction == Vector2Int.down) ? new List<int> { 3, 4 } : new List<int>();

        foreach (var position in edgePositions)
        {
            var binaryType = "";
            foreach (var direction in eightDirectionsList)
            {
                if (!floorPositions.Contains(position + direction))
                    binaryType += "1";
                else
                    binaryType += "0";
            }

            if (!floorTable.ContainsKey(binaryType) ||
                quadrantsToSkip.Any(x => isInQuadrant(x, new Vector2Int(room.RoomCenterPos.x, room.RoomCenterPos.y - room.DoorDimension.y), position)) ||
                specialCase.Any(x => isInQuadrant(x, new Vector2Int(room.RoomCenterPos.x, room.RoomCenterPos.y - room.BaseDimension.y), position)))

            {
                // tileBaseByPosition.Add(position, floorTiles[Random.Range(0, floorTiles.Count)]);
                var tileFloor = floorTileSelector.GetRandomWeightedItem();
                tileBaseByPosition.Add(position, tileFloor);
                continue;
            }
            tileBaseByPosition.Add(position, floorTable[binaryType]);
        }


        return tileBaseByPosition;
    }


    private bool isInQuadrant(int quadrant, Vector2Int origin, Vector2Int coord)
    {
        return quadrant switch
        {
            1 => coord.x > origin.x && coord.y > origin.y,
            2 => coord.x < origin.x && coord.y > origin.y,
            3 => coord.x < origin.x && coord.y < origin.y,
            4 => coord.x > origin.x && coord.y < origin.y,
            _ => false
        };
    }

}
