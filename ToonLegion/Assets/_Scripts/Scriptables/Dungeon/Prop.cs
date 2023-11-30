using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Prop : ScriptableObject
{
    [Header("Prop data:")] public Sprite PropSprite;
    public Vector2Int PropSize = Vector2Int.one;

    [Space, Header("Placement type:")] 
    public bool Corner = true;
    public bool NearWallUp = true;
    public bool NearWallDown = true;
    public bool NearWallRight = true;
    public bool NearWallLeft = true;
    public bool Inner = true;

    [Min(1)] public int PlacementQuantityMin = 1;
    [Min(1)] public int PlacementQuantityMax = 1;

    [Space, Header("Group placement:")] public bool PlaceAsGroup = false;
    [Min(1)] public int GroupMinCount = 1;
    [Min(1)] public int GroupMaxCount = 1;
    
    
    [Space, Header("Shadow casting:")]
    public bool CastShadows = false;
    public List<Vector2> ShadowCastingPoints = new List<Vector2>();
    
    [Space, Header("Collusion:")]
    public bool HasCollusion = true;
    public Vector2 CollusionSize = Vector2.one;
    public Vector2 CollusionOffset = Vector2.zero;
    public CapsuleDirection2D CollusionDirection = CapsuleDirection2D.Vertical;
}

