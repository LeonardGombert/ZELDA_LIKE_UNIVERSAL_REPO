using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExtensionMethods
{
    //Get World Position
    public static Vector2 getFlooredWorldPosition(Vector2 worldPosition)
    {
        worldPosition.x = Mathf.Floor(worldPosition.x);       //horizontal = Mathf.Floor(horizontal * 2f) * 0.5f;
        worldPosition.y = Mathf.Floor(worldPosition.y);           //vertical = Mathf.Floor(vertical * 2f) * 0.5f;
        
        return worldPosition;
    }

    public static Vector2 floorWithHalfOffset(Vector2 worldPosition)
    {
        worldPosition.x = Mathf.Floor(worldPosition.x * 2f) * 0.5f;
        worldPosition.y = Mathf.Floor(worldPosition.y * 2f) * 0.5f;

        return worldPosition;
    }

    /*??????????????????????????????????????????????????????????????????????????????????
    public static TileBase getTilemapCellPosition(Tilemap tilemap, Vector3 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }*/

    //Round Value to first int smaller than F
    public static Vector2 FloorVect3Values(Vector2 vector)
    {
        vector.x = Mathf.Floor(vector.x);
        vector.y = Mathf.Floor(vector.y);

        return vector;
    }

    //Round value to the nearest int (larger or smaller)
    public static Vector2 RoundVect3(Vector2 vector)
    {
        vector.x = Mathf.Round(vector.x * 10) / 10;
        vector.y = Mathf.Round(vector.y * 10) / 10;

        return vector;
    }
}
