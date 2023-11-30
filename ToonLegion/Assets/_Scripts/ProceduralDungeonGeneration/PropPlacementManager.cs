using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEngine.Rendering.Universal;

public class PropPlacementManager : MonoBehaviour
{
    private DungeonData dungeonData;

    [SerializeField] private List<Prop> propsToPlace;
    [SerializeField] private GameObject propContainer;
    [SerializeField, Range(0,1)] private float cornerPropPlacementChance = 0.7f;
    [SerializeField] private GameObject propPrefab;
    public UnityEvent OnFinishedPropPlacement;

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
            List<Prop> cornerProps = propsToPlace.Where(x => x.Corner).ToList();
            PlaceCornerProps(room, cornerProps);
            
            List<Prop> leftWallProps = propsToPlace
                .Where(x => x.NearWallLeft)
                .OrderByDescending(x=>x.PropSize.x * x.PropSize.y)
                .ToList();
            PlaceProps(room, leftWallProps, room.NearWallTilesLeft, PlacementOriginCorner.BottomLeft);
            
            List<Prop> rightWallProps = propsToPlace
                .Where(x => x.NearWallRight)
                .OrderByDescending(x=>x.PropSize.x * x.PropSize.y)
                .ToList();
            PlaceProps(room, rightWallProps, room.NearWallTilesRight, PlacementOriginCorner.TopRight);
            
            List<Prop> upWallProps = propsToPlace
                .Where(x => x.NearWallUp)
                .OrderByDescending(x=>x.PropSize.x * x.PropSize.y)
                .ToList();
            PlaceProps(room, upWallProps, room.NearWallTilesUp, PlacementOriginCorner.TopLeft);
            
            List<Prop> downWallProps = propsToPlace
                .Where(x => x.NearWallDown)
                .OrderByDescending(x=>x.PropSize.x * x.PropSize.y)
                .ToList();
            PlaceProps(room, downWallProps, room.NearWallTilesDown, PlacementOriginCorner.BottomLeft);
            
            List<Prop> innerProps = propsToPlace
                .Where(x=>x.Inner)
                .OrderByDescending(x=>x.PropSize.x * x.PropSize.y)
                .ToList();
            PlaceProps(room, innerProps, room.InnerTiles, PlacementOriginCorner.BottomLeft);

        }
        RunEvent();
    }

    private void PlaceProps(Room room, List<Prop> propsToPlace, HashSet<Vector2Int> availableTiles, PlacementOriginCorner placement)
    {
        if (propsToPlace.Count == 0)
            return;
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>(availableTiles);
        positions.ExceptWith(dungeonData.Path);

        foreach (Prop propToPlace in propsToPlace)
        {
            int quantity = Random.Range(propToPlace.PlacementQuantityMin, propToPlace.PlacementQuantityMax);

            for (int i = 0; i < quantity; i++)
            {
                positions.ExceptWith(room.PropPositions);
                List<Vector2Int> availablePositions = positions
                    .OrderBy(x => Random.value)
                    .ToList();
                if (!TryPlacingPropBruteForce(room, propToPlace, availablePositions, placement))
                    break;
            }
        }
    }

    private bool TryPlacingPropBruteForce(Room room, Prop propToPlace, List<Vector2Int> availablePositions, PlacementOriginCorner placement)
    {
        foreach (Vector2Int position in availablePositions)
        {
            if (room.PropPositions.Contains(position))
                continue;

            List<Vector2Int> freePositionsAround = TryToFitProp(propToPlace, availablePositions, position, placement);

            if (freePositionsAround.Count == propToPlace.PropSize.x * propToPlace.PropSize.y)
            {
                PlacePropGameObjectAt(room, position, propToPlace);
                foreach (Vector2Int pos in freePositionsAround)
                {
                    room.PropPositions.Add(pos);
                }
                if (propToPlace.PlaceAsGroup)
                    PlaceGroupObject(room, position, propToPlace, 1);
                
                return true;
            }
        }
        return false;
    }

    private List<Vector2Int> TryToFitProp(Prop propToPlace, List<Vector2Int> availablePositions, Vector2Int originPosition, PlacementOriginCorner placement)
    {
        List<Vector2Int> freePositions = new();
        //Perform the specific loop depending on the PlacementOriginCorner
        if (placement == PlacementOriginCorner.BottomLeft)
        {
            for (int xOffset = 0; xOffset < propToPlace.PropSize.x; xOffset++)
            {
                for (int yOffset = 0; yOffset < propToPlace.PropSize.y; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else if (placement == PlacementOriginCorner.BottomRight)
        {
            for (int xOffset = -propToPlace.PropSize.x + 1; xOffset <= 0; xOffset++)
            {
                for (int yOffset = 0; yOffset < propToPlace.PropSize.y; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else if (placement == PlacementOriginCorner.TopLeft)
        {
            for (int xOffset = 0; xOffset < propToPlace.PropSize.x; xOffset++)
            {
                for (int yOffset = -propToPlace.PropSize.y + 1; yOffset <= 0; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }
        else
        {
            for (int xOffset = -propToPlace.PropSize.x + 1; xOffset <= 0; xOffset++)
            {
                for (int yOffset = -propToPlace.PropSize.y + 1; yOffset <= 0; yOffset++)
                {
                    Vector2Int tempPos = originPosition + new Vector2Int(xOffset, yOffset);
                    if (availablePositions.Contains(tempPos))
                        freePositions.Add(tempPos);
                }
            }
        }

        return freePositions;
    }

    private void PlaceCornerProps(Room room, List<Prop> cornerProps)
    {
        if (cornerProps.Count == 0)
            return;
        float chance = cornerPropPlacementChance;
        foreach (Vector2Int cornerTile in room.CornerTiles)
        {
            if (Random.value > chance)
            {
                chance = Mathf.Clamp01(chance + 0.1f);
                continue;
            }

            Prop propToPlace = cornerProps[Random.Range(0, cornerProps.Count)];
            PlacePropGameObjectAt(room, cornerTile, propToPlace);
            if (propToPlace.PlaceAsGroup)
                PlaceGroupObject(room, cornerTile, propToPlace, 2);
        }
    }

    private void PlaceGroupObject(Room room, Vector2Int groupOriginPosition, Prop propToPlace, int searchOffset)
    {
        int count = Random.Range(propToPlace.GroupMinCount, propToPlace.GroupMaxCount)-1;
        count = Mathf.Clamp(count, 0, 8);
        
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        for (int x = -searchOffset; x <= searchOffset; x++)
        {
            for (int y = -searchOffset; y <= searchOffset; y++)
            {
                Vector2Int position = groupOriginPosition + new Vector2Int(x, y);
                if (room.FloorTiles.Contains(position) && 
                    !room.PropPositions.Contains(position) && 
                    !dungeonData.Path.Contains(position))
                {
                    availablePositions.Add(position);
                }
            }
        }
        availablePositions.OrderBy(x => Random.value).ToList();
        int tempCount = Math.Min(availablePositions.Count, count);
        for (int i = 0; i < tempCount; i++)
        {
            PlacePropGameObjectAt(room, availablePositions[i], propToPlace);
        }
    }

    private GameObject PlacePropGameObjectAt(Room room, Vector2Int placementPosition, Prop propToPlace)
    {
        GameObject prop = Instantiate(propPrefab);
        SpriteRenderer propSpriteRenderer = prop.GetComponentInChildren<SpriteRenderer>();
        propSpriteRenderer.sprite = propToPlace.PropSprite;
        propSpriteRenderer.sortingOrder = 2;

        if (propToPlace.HasCollusion)
        {
            CapsuleCollider2D propCollider = propSpriteRenderer.gameObject.AddComponent<CapsuleCollider2D>();
            propCollider.size = propToPlace.CollusionSize;
            propCollider.offset = propToPlace.CollusionOffset;
            propCollider.direction = propToPlace.CollusionDirection;
        }
        
        

        if (propToPlace.CastShadows)
        {
            var shadowCasterPoints = propToPlace.ShadowCastingPoints;
            SpriteShadowCastingGenerator spriteShadowCastingGenerator = new SpriteShadowCastingGenerator();
            spriteShadowCastingGenerator.GenerateShadowCasting(propSpriteRenderer, shadowCasterPoints);
        }
        
        
        
        Vector3 newPosition = new Vector3(placementPosition.x, placementPosition.y, -2f);
        prop.transform.localPosition = newPosition;
        propSpriteRenderer.transform.localPosition = (Vector2)propToPlace.PropSize / 2f;
        prop.transform.parent = propContainer.transform;
        room.PropPositions.Add(placementPosition);
        room.PropObjectReferences.Add(prop);
        return prop;
    }

    public void RunEvent()
    {
        OnFinishedPropPlacement?.Invoke();
    }
}




public enum PlacementOriginCorner
{
    BottomLeft,
    BottomRight,
    TopLeft,
    TopRight
}




















