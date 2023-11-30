using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class TileMapVisualizer:MonoBehaviour
    {
        
        [SerializeField] private Tilemap floorTilemap;
        [SerializeField] private Tilemap wallTilemap;
        [SerializeField] private List<TileBase> floorTiles;
        [SerializeField] private List<int> floorTilesWeights;
        // [SerializeField] private List<int> wallTopsWeights;
        [SerializeField] private TileBase wallTop;
        [SerializeField] private TileBase wallTopLevel1;
        [SerializeField] private TileBase wallTopLevel2;


        [SerializeField] private TileBase wallSideRight;
        [SerializeField] private TileBase wallSideLeft;
        [SerializeField] private TileBase wallBottom;
        [SerializeField] private TileBase wallFull;
        [SerializeField] private TileBase wallInnerCornerDownLeft;
        [SerializeField] private TileBase wallInnerCornerDownRight;
        [SerializeField] private TileBase wallDiagonalCornerDownLeft;
        [SerializeField] private TileBase wallDiagonalCornerDownRight;
        [SerializeField] private TileBase wallDiagonalCornerUpLeft;
        [SerializeField] private TileBase wallDiagonalCornerUpRight;


        
        

        public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
        {
            WeightedRandomSelector<TileBase> weightedRandomSelector = new WeightedRandomSelector<TileBase>();
            for (int i = 0; i < floorTiles.Count; i++)
            {
                weightedRandomSelector.Add(floorTiles[i], floorTilesWeights[i]);
            }
            
            PaintTiles(floorPositions, floorTilemap, weightedRandomSelector);
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap,  WeightedRandomSelector<TileBase> tiles)
        {
            foreach (var position in positions)
            {
                PaintSingleTile(position, tilemap, tiles.GetRandomWeightedItem());
            }
        }

        private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
        {
            var tilePosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(tilePosition, tile);
        }

        //public void Clear()
        //{
        //    floorTilemap.ClearAllTiles();
        //    wallTilemap.ClearAllTiles();
        //}

        public void PaintSingleBasicWall(Vector2Int position, string binaryType, HashSet<Vector2Int> floorPositions)
        {
            if (wallTilemap.GetTile((Vector3Int)position) != null)
                return;
            int typesAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;
            if (WallTypesHelper.wallTop.Contains(typesAsInt))
            {
                tile = wallTop;
            }
            else if (WallTypesHelper.wallBottom.Contains(typesAsInt))
            {
                tile = wallBottom;
            }
            else if (WallTypesHelper.wallSideLeft.Contains(typesAsInt))
            {
                tile = wallSideLeft;
            }
            else if (WallTypesHelper.wallSideRight.Contains(typesAsInt))
            {
                tile = wallSideRight;
            }
            else if (WallTypesHelper.wallFull.Contains(typesAsInt))
            {
                tile = wallFull;
            }
            if (tile != null)
            {
                if (WallTypesHelper.wallTop.Contains(typesAsInt))
                {
                    PaintSingleTile(position, wallTilemap, wallTopLevel1);
                    PaintSingleTile(position + Vector2Int.up, wallTilemap, wallTopLevel2);
                }
                else
                {
                    PaintSingleTile(position, wallTilemap, tile);
                }
            }
        }

        public void PaintSingleCornerWall(Vector2Int position, string binaryType, HashSet<Vector2Int> floorPositions)
        {
            int typeAsInt = Convert.ToInt32(binaryType, 2);
            TileBase tile = null;
            if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerUpRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerUpLeft;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            {
                tile = wallDiagonalCornerDownLeft;
            }
            else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            {
                tile = wallInnerCornerDownRight;
            }
            else if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            {
                tile = wallInnerCornerDownLeft;
            }
            else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            {
                tile = wallFull;
            }
            else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
            {
                tile = wallBottom;
            }
            
            if (tile != null)
            {
                if (tile == wallDiagonalCornerUpLeft&&
                    !floorPositions.Contains(position + Vector2Int.up))
                {
                    PaintSingleTile(position, wallTilemap, wallDiagonalCornerUpLeft);
                    PaintSingleTile(position+Vector2Int.up, wallTilemap, wallDiagonalCornerUpLeft);
                }
                else if (tile == wallDiagonalCornerUpRight&&
                         !floorPositions.Contains(position + Vector2Int.up))
                {
                    PaintSingleTile(position, wallTilemap, wallDiagonalCornerUpRight);
                    PaintSingleTile(position+Vector2Int.up, wallTilemap, wallDiagonalCornerUpRight);
                }
                else
                {
                    PaintSingleTile(position, wallTilemap, tile);
                }
            }
            
        }
    }
}