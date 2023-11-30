using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static bool IsPositionInBounds(Vector2Int pos, int left, int right, int down, int up)
    {
        return pos.x >= left && pos.x <= right && pos.y >= down && pos.y <= up;
    }
    public static (int, int) GetLeastThenMax(int v1, int v2)
    {
        if (v1 > v2)
        {
            return (v2, v1);
        }

        return (v1, v2);
    }

}

public struct Margins
{
    public int Left;
    public int Right;
    public int Upper;
    public int Lower;

    public Margins(int left, int right, int upper, int lower)
    {
        Left = left;
        Right = right;
        Upper = upper;
        Lower = lower;
    }
}
