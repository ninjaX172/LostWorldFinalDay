using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class RoomGenerator : MonoBehaviour
{
    private GridTileVisualizer gridTileVisualizer;
    public GameObject edgeColliderContainer;
    private PropSpawner propSpawner;



    void Awake()
    {
        gridTileVisualizer = GetComponent<GridTileVisualizer>();
        propSpawner = GetComponent<PropSpawner>();
    }

    public Room2 GenerateRoomByType(Vector2Int roomId, RoomType roomType, Dictionary<Vector2Int, Room2> adjacentRooms,
        Vector2Int baseDimension, Vector2Int doorDimension)
    {
        var room = GenerateBasicRoom(roomId, adjacentRooms, baseDimension, doorDimension);
        room.RoomType = roomType;
        switch (roomType)
        {
            case RoomType.Monster:
                room = GenerateMonsterRoom(room);
                break;
            case RoomType.Shrine:
                room = GenerateShrineRoom(room);
                break;
            case RoomType.Portal:
                room = GeneratePortalRoom(room);
                break;
            case RoomType.BulletHell:
                room = GenerateBulletHellRoom(room);
                break;
            case RoomType.Chill:
                room = GenerateChillRoom(room);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null);
        }

        return room;
    }

    private Room2 GenerateBulletHellRoom(Room2 room)
    {
        var possiblePropPlacementPositions = PossiblePropPlacementPositions(room.FloorPositions,
            room.RoomCenterPos,
            room.BaseDimension,
            new Margins(1, -1, -1, 2));

        var props1 = propSpawner.SpawnBulletPropsRandomly(possiblePropPlacementPositions, 2, 3);
        var props2 = propSpawner.PlaceAPropByPropType(new Vector2Int(room.RoomCenterPos.x, room.RoomCenterPos.y), PropType.Bullet);

        // room.PropsPositions = new HashSet<Vector2Int>(a1.Concat(a2));

        room.PropsReferences.AddRange(props1);
        room.PropsReferences.Add(props2);

        return room;
    }

    private Room2 GenerateChillRoom(Room2 room)
    {
        room.ValidEnemySpawnPositions = new HashSet<Vector2Int>(room.FloorPositions.Except(room.PropsPositions));

        return room;
    }

    private Room2 GenerateMonsterRoom(Room2 room)
    {
        // var room = GenerateBasicRoom(roomId, adjacentRooms, baseDimension, doorDimension);
        var possiblePropPlacementPositions = PossiblePropPlacementPositions(room.FloorPositions,
            room.RoomCenterPos,
            room.BaseDimension,
            new Margins(1, -1, -1, 2));
        room.PropsPositions = propSpawner.SpawnObstacleDecorationPropsRandomly(possiblePropPlacementPositions, 1, 3);
        room.ValidEnemySpawnPositions = new HashSet<Vector2Int>(room.FloorPositions.Except(room.PropsPositions));
        return room;
    }


    private Room2 GenerateShrineRoom(Room2 room)
    {
        // var room = GenerateBasicRoom(roomId, adjacentRooms, baseDimension, doorDimension);
        // room.PropsPositions = propSpawner.PlaceAPropByPropType(new Vector2Int(room.RoomCenterPos.x + 1, room.RoomCenterPos.y+3), PropType.Shrine);
        // room.ValidEnemySpawnPositions = new HashSet<Vector2Int>(room.FloorPositions.Except(room.PropsPositions));

        var prop = propSpawner.PlaceAPropByPropType(new Vector2Int(room.RoomCenterPos.x + 1, room.RoomCenterPos.y + 3), PropType.Shrine);
        room.PropsReferences.Add(prop);
        return room;
    }

    private Room2 GeneratePortalRoom(Room2 room)
    {
        // room.PropsPositions = propSpawner.Place
        // room.PropsPositions = propSpawner.PlaceAPropByPropType(new Vector2Int(room.RoomCenterPos.x + 1, room.RoomCenterPos.y+3), PropType.Portal);
        // room.ValidEnemySpawnPositions = new HashSet<Vector2Int>(room.FloorPositions.Except(room.PropsPositions));
        var prop = propSpawner.PlaceAPropByPropType(new Vector2Int(room.RoomCenterPos.x + 1, room.RoomCenterPos.y + 3), PropType.Portal);
        room.PropsReferences.Add(prop);
        return room;
    }

    private Room2 GenerateBasicRoom(Vector2Int roomId, Dictionary<Vector2Int, Room2> adjacentRooms, Vector2Int baseDimension, Vector2Int doorDimension)
    {
        var room = new Room2(roomId, baseDimension, doorDimension);
        var doorDirections = GetDoorDirections(roomId, adjacentRooms);

        foreach (var direction in doorDirections)
        {
            var door = new Door
            {
                Direction = direction,
                Center = room.RoomCenterPos + direction * (baseDimension),
                DistanceFromDoorCenter = Vector2Int.zero
            };
            room.Doors.Add(door);
        }

        RandomlyShiftDoors(room, new Margins(3, 3, 2, 2));

        var doorEntrances = GetStartAndEndPositionOfDoorEntrances(room, doorDirections);
        room.EdgeColliders = Create2dEdgeColliders(room, doorEntrances);

        // var floorPositions = GenerateFloorPositions(room.RoomCenterPos, baseDimension, doorDimension, doorDirections);
        var floorPositions = GenerateFloorPositions(room.RoomCenterPos, room);
        var wallPositions = GenerateWallPositions(floorPositions);

        gridTileVisualizer.PaintFloorTiles(floorPositions, room);
        gridTileVisualizer.PaintWallTiles(wallPositions);

        room.FloorPositions = floorPositions;
        room.WallPositions = wallPositions;
        return room;
    }

    // public Room2 GenerateRoom(Vector2Int roomId, Dictionary<Vector2Int, Room2> adjacentRooms, Vector2Int baseDimension, Vector2Int doorDimension)
    // {
    //     var room = new Room2(roomId, baseDimension, doorDimension);
    //     var doorDirections = GetDoorDirections(roomId, adjacentRooms);
    //     room.DoorDirections = doorDirections.ToList();
    //     
    //     var doorEntrances = GetStartAndEndPositionOfDoorEntrances(room, doorDirections);
    //     room.EdgeColliders = Create2dEdgeColliders(room, doorEntrances);
    //
    //     var floorPositions = GenerateFloorPositions(room.RoomCenterPos, baseDimension, doorDimension, doorDirections);
    //     var wallPositions = GenerateWallPositions(floorPositions);
    //     
    //     gridTileVisualizer.PaintFloorTiles(floorPositions, room);
    //     gridTileVisualizer.PaintWallTiles(wallPositions);
    //     
    //     var possiblePropPlacementPositions = PossiblePropPlacementPositions(floorPositions, 
    //         room.RoomCenterPos, 
    //         baseDimension, 
    //         new Margins(1,-1,-1,1));
    //     var positionsUsedForProps = propSpawner.SpawnProps(possiblePropPlacementPositions, 1, 3);
    //     room.ValidEnemySpawnPositions = floorPositions;
    //     room.ValidEnemySpawnPositions.ExceptWith(positionsUsedForProps);
    //     return room;
    // }

    private void RandomlyShiftDoors(Room2 room, Margins maxMargins)
    {
        foreach (var door in room.Doors)
        {
            var randomShift = Vector2Int.zero;
            if (door.Direction == Vector2Int.up || door.Direction == Vector2Int.down)
            {
                randomShift.x = Random.Range(-maxMargins.Left, maxMargins.Right);
            }
            else
            {
                randomShift.y = Random.Range(-maxMargins.Lower, maxMargins.Upper);
            }
            door.DistanceFromDoorCenter = randomShift;
        }
    }

    private List<(Vector2Int, Vector2Int, Vector2Int)> GetStartAndEndPositionOfDoorEntrances(Room2 room,
        HashSet<Vector2Int> doorDirections)
    {
        List<(Vector2Int, Vector2Int, Vector2Int)> doorEntrances = new List<(Vector2Int, Vector2Int, Vector2Int)>();
        foreach (var door in doorDirections)
        {
            var startPosition = Vector2Int.zero;
            var endPosition = Vector2Int.zero;

            if (door == Vector2Int.down || door == Vector2Int.up)
            {
                startPosition = new Vector2Int(room.RoomCenterPos.x - (room.DoorDimension.x),
                    room.RoomCenterPos.y + (room.BaseDimension.y) * door.y);
                endPosition = new Vector2Int(room.RoomCenterPos.x + (room.DoorDimension.x * 2), room.RoomCenterPos.y +
                    (room.BaseDimension.y) * door.y);
            }
            else
            {
                startPosition = new Vector2Int(room.RoomCenterPos.x + (room.BaseDimension.x) * door.x,
                    room.RoomCenterPos.y - (room.DoorDimension.y));
                endPosition = new Vector2Int(room.RoomCenterPos.x + (room.BaseDimension.x) * door.x, room.RoomCenterPos.y +
                    (room.DoorDimension.y * 2));
            }

            var adjustment = new Dictionary<Vector2Int, Vector2Int>
            {
                {Vector2Int.up, new Vector2Int(0, 3)},
                {Vector2Int.down, new Vector2Int(0, -2)},
                {Vector2Int.left, new Vector2Int(-2, 0)},
                {Vector2Int.right, new Vector2Int(3, 0)}
            };

            startPosition += adjustment[door];
            endPosition += adjustment[door];

            doorEntrances.Add((door, startPosition, endPosition));
        }

        return doorEntrances;
    }

    private List<GameObject> Create2dEdgeColliders(Room2 room, List<(Vector2Int, Vector2Int, Vector2Int)> positions)
    {
        List<GameObject> edgeColliders = new List<GameObject>();
        foreach (var position in positions)
        {
            var (direction, startPosition, endPosition) = position;
            var edgeColliderObj = new GameObject("Room Id: " + room.RoomId + " Direction: " + direction);
            edgeColliderObj.transform.parent = edgeColliderContainer.transform;
            edgeColliderObj.transform.position = new Vector3(0f, 0f, 0f);
            var collider = edgeColliderObj.AddComponent<EdgeCollider2D>();
            collider.points = new[]
            {
                new Vector2(startPosition.x, startPosition.y),
                new Vector2(endPosition.x, endPosition.y)
            };

            collider.isTrigger = true;
            var triggerHandler = edgeColliderObj.AddComponent<TriggerHandler>();
            triggerHandler.RoomId = room.RoomId;
            triggerHandler.DoorDirection = direction;
            edgeColliderObj.SetActive(false);
            edgeColliders.Add(edgeColliderObj);
        }
        return edgeColliders;
    }


    private HashSet<Vector2Int> GetDoorDirections(Vector2Int roomId, Dictionary<Vector2Int, Room2> adjacentRooms)
    {
        var doorDirections = GetAllRequiredDoorDirection(roomId, adjacentRooms);
        var blacklistDirections = GetAllBlacklistDoorDirections(roomId, adjacentRooms);
        var directions = new List<Vector2Int>
            { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        directions.RemoveAll(x => blacklistDirections.Contains(x));
        directions.RemoveAll(x => doorDirections.Contains(x));

        var numberOfDoors = Random.Range(1, directions.Count + 1);
        for (var i = 0; i < numberOfDoors; i++)
        {
            if (directions.Count == 0) break;
            var index = Random.Range(0, directions.Count);
            doorDirections.Add(directions[index]);
            directions.RemoveAt(index); // To avoid selecting the same element more than once
        }

        return doorDirections;
    }

    private HashSet<Vector2Int> GetAllBlacklistDoorDirections(Vector2Int roomId, Dictionary<Vector2Int, Room2> adjacentRooms)
    {
        var blacklist = new HashSet<Vector2Int>();

        foreach (var direction in Direction2D.cardinalDirections)
        {
            var neighborPosition = roomId + direction;
            if (!adjacentRooms.ContainsKey(neighborPosition)) continue;

            if (
                // !adjacentRooms[neighborPosition].DoorDirections.Contains(new Vector2Int(-direction.x, -direction.y))
                adjacentRooms[neighborPosition].Doors.All(x => x.Direction != new Vector2Int(-direction.x, -direction.y)))

                blacklist.Add(direction);

        }

        return blacklist;
    }

    private HashSet<Vector2Int> PossiblePropPlacementPositions(HashSet<Vector2Int> floorPositions, Vector2Int origin, Vector2Int baseDimension, Margins margins)
    {
        var possiblePropPlacementPositions = new HashSet<Vector2Int>(floorPositions);
        var leftBound = origin.x - baseDimension.x + margins.Left;
        var rightBound = origin.x + baseDimension.x + margins.Right;
        var upperBound = origin.y + baseDimension.y + margins.Upper;
        var lowerBound = origin.y - baseDimension.y + margins.Lower;

        foreach (var position in floorPositions)
        {
            if (position.x < leftBound || position.x > rightBound || position.y < lowerBound || position.y > upperBound)
            {
                possiblePropPlacementPositions.Remove(position);
            }
        }

        return possiblePropPlacementPositions;

    }

    // private HashSet<Vector2Int> GenerateFloorPositions(Vector2Int origin, Vector2Int baseDimension, Vector2Int doorDimension, HashSet<Vector2Int> doorDirections)
    private HashSet<Vector2Int> GenerateFloorPositions(Vector2Int origin, Room2 room)

    {
        var floorPositions = new HashSet<Vector2Int>();
        var upperVerticalBound = origin.y + room.BaseDimension.y;
        var rightHorizontalBound = origin.x + room.BaseDimension.x;
        var leftHorizontalBound = origin.x - room.BaseDimension.x;
        var lowerVerticalBound = origin.y - room.BaseDimension.y;

        var positionsToCheck = new Queue<Vector2Int>();
        positionsToCheck.Enqueue(new Vector2Int(origin.x, origin.y));

        // Base floor
        while (positionsToCheck.Count > 0)
        {
            var position = positionsToCheck.Dequeue();
            if (floorPositions.Contains(position) ||
                !Helpers.IsPositionInBounds(position,
                    leftHorizontalBound,
                    rightHorizontalBound,
                    lowerVerticalBound,
                    upperVerticalBound))
                continue;

            floorPositions.Add(position);
            foreach (var direction in Direction2D.cardinalDirections)
            {
                positionsToCheck.Enqueue(position + direction);
            }
        }

        // Add doors
        foreach (var direction in room.Doors.Select(x => x.Direction))
        {
            positionsToCheck.Enqueue(new Vector2Int(origin.x + (room.BaseDimension.x + 1) * direction.x, origin.y +
                (room.BaseDimension.y + 1) * direction.y));

            if (direction == Vector2Int.up || direction == Vector2Int.down)
            {
                upperVerticalBound = origin.y + (room.BaseDimension.y + room.DoorDimension.y * 2) * direction.y;
                lowerVerticalBound = origin.y + (room.BaseDimension.y) * direction.y;
                leftHorizontalBound = origin.x - room.DoorDimension.x;
                rightHorizontalBound = origin.x + room.DoorDimension.x;

                (lowerVerticalBound, upperVerticalBound) = Helpers.GetLeastThenMax(lowerVerticalBound, upperVerticalBound);

            }
            else
            {
                upperVerticalBound = origin.y + room.DoorDimension.y;
                lowerVerticalBound = origin.y - room.DoorDimension.y;
                leftHorizontalBound = origin.x + (room.BaseDimension.x + room.DoorDimension.x * 2) * direction.x;
                rightHorizontalBound = origin.x + (room.BaseDimension.x) * direction.x;

                (leftHorizontalBound, rightHorizontalBound) = Helpers.GetLeastThenMax(leftHorizontalBound, rightHorizontalBound);
            }


            while (positionsToCheck.Count > 0)
            {
                var position = positionsToCheck.Dequeue();
                if (floorPositions.Contains(position) ||
                    !Helpers.IsPositionInBounds(position,
                        leftHorizontalBound,
                        rightHorizontalBound,
                        lowerVerticalBound,
                        upperVerticalBound))
                    continue;

                floorPositions.Add(position);
                foreach (var dir in Direction2D.cardinalDirections)
                {
                    positionsToCheck.Enqueue(position + dir);
                }
            }
        }
        return floorPositions;
    }


    private HashSet<Vector2Int> GenerateWallPositions(HashSet<Vector2Int> floorPositions)
    {
        var wallPositions = new HashSet<Vector2Int>();
        foreach (var floorPosition in floorPositions)
        {
            foreach (var direction in Direction2D.eightDirectionsList)
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

    private HashSet<Vector2Int> GetAllRequiredDoorDirection(Vector2Int roomId, Dictionary<Vector2Int, Room2> adjacentRooms)
    {
        var requiredDoorDirection = new HashSet<Vector2Int>();

        foreach (var direction in Direction2D.cardinalDirections)
        {
            var neighborPosition = roomId + direction;
            if (!adjacentRooms.ContainsKey(neighborPosition)) continue;

            if (
                // adjacentRooms[neighborPosition].DoorDirections.Contains(new Vector2Int(-direction.x, -direction.y)))
                adjacentRooms[neighborPosition].Doors
                .Any((x) => x.Direction == new Vector2Int(-direction.x, -direction.y)))


                requiredDoorDirection.Add(direction);

        }

        return requiredDoorDirection;
    }


}

