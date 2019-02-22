using UnityEngine;

[System.Serializable]
public struct CellCoords
{

    [SerializeField]
    private int x, z;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    public CellCoords(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static CellCoords FromPosition (Vector3 position)
    {
        float x = position.x / (SquareMetrics.bisectSegment * 2f);
        float z = position.z / (SquareMetrics.bisectSegment * 2f);

        int iX = Mathf.RoundToInt(x);
        int iZ = Mathf.RoundToInt(z);

        return new CellCoords(iX, iZ);
    }

    public static CellCoords FromCoordinates(int x, int z)
    {
        return new CellCoords(x, z);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Z.ToString();
    }
}
