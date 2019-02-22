using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareCellScript : MonoBehaviour
{
    public CellCoords coordinates;

    [SerializeField]
    SquareCellScript[] neighbors;

    public SquareCellScript GetNeighbor(CellDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(CellDirection direction, SquareCellScript cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}
