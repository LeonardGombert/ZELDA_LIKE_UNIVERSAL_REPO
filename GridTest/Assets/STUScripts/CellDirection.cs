using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellDirection
{
    N, E, S, W
}

public static class HexDirectionExtensions
{

    public static CellDirection Opposite(this CellDirection direction)
    {
        return (int)direction < 2 ? (direction + 2) : (direction - 2);
    }
}
