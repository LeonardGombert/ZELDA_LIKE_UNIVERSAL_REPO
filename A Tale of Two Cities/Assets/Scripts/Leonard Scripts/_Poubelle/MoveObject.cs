using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveObject : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 originalObjectPosition;
    Vector3 targetObjectPosition;

    public Tilemap highlightTilemap;

    public float moveTime = 0.3f;

    public static bool hasBeenMoved = false;

    void Awake()
    {
        highlightTilemap = GameObject.Find("HighlightTMP").GetComponent<Tilemap>();
        originalObjectPosition = transform.position;
        Debug.Log("The Object's original position is " + originalObjectPosition);
    }

    // Update is called once per frame
    void Update()
    {
        FetchTilePosition();
    }

    private void FetchTilePosition()        //Get tile X, Y, Z positions of first position tile and second position tile
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Get mousePosition and floor it to align with grid            
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition = ExtensionMethods.getFlooredWorldPosition(mousePosition);

                //Get mousePosition in Grivd coord
                Vector3Int currentMousePositionInGrid = highlightTilemap.WorldToCell(mousePosition);

                targetObjectPosition = currentMousePositionInGrid;

                Vector3 offsetedTargetPosition = targetObjectPosition + new Vector3(0.5f, 0.5f, 0);

                StartCoroutine(MoveObjectTowards(offsetedTargetPosition));
            }
        }
            
        else return;
    }

    private IEnumerator MoveObjectTowards (Vector3 destinationPosition)
    {
        transform.position = destinationPosition;
        hasBeenMoved = true;
        yield return null;
    }
}
