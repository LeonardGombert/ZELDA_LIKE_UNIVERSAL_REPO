using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExtensionMethods
{
    //Get World Position
    public static Vector3 getCenteredWorldPosition(float horizontal, float vertical, float depth)
    {
        horizontal = Mathf.Floor(horizontal);
        vertical = Mathf.Floor(vertical);
        depth = Mathf.Floor(depth);

        Vector3 centeredWorldPosition = new Vector3(horizontal, vertical, depth);

        return centeredWorldPosition;
    }

    //??????????????????????????????????????????????????????????????????????????????????
    public static TileBase getTilemapCellPosition(Tilemap tilemap, Vector3 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    //Round Value to first int smaller than F
    public static Vector3 FloorVect3Values(Vector3 vector)
    {
        vector.x = Mathf.Floor(vector.x);
        vector.y = Mathf.Floor(vector.y);
        vector.z = Mathf.Floor(vector.z);

        return vector;
    }

    //Round value to the nearest int (larger or smaller)
    public static Vector3 RoundVect3(Vector3 vector)
    {
        vector.x = Mathf.Round(vector.x * 10) / 10;
        vector.y = Mathf.Round(vector.y * 10) / 10;
        vector.z = Mathf.Round(vector.z * 10) / 10;

        return vector;
    }
}
