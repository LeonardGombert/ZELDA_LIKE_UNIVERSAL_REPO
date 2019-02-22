using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public SquareCellScript cellPrefab;

    public Text cellLabelPrefab;

    Canvas gridCanvas;

    SquareCellScript[] cells;

    //BoxCollider boxCollider;

    void Awake()
    {
        //boxCollider = gameObject.AddComponent<BoxCollider>();

        gridCanvas = GetComponentInChildren<Canvas>();

        cells = new SquareCellScript[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = x * 10f;
        position.y = 0f;
        position.z = z * 10f;

        SquareCellScript cell = cells[i] = Instantiate<SquareCellScript>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = CellCoords.FromCoordinates (x, z);

        if (x > 0)
        {
            cell.SetNeighbor(CellDirection.W, cells[i - 1]);
        }

        if (z > 0)
        {
            //if ((z & 1) == 0)
            
                cell.SetNeighbor(CellDirection.S, cells[i - width]);
            
        }

            Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();

        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        CellCoords coordinates = CellCoords.FromPosition(position);
        Debug.Log("daddy touched me at " + coordinates.ToString());
    }

}
