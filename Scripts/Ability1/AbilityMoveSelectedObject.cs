using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbilityMoveSelectedObject : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 originalObjectPosition;
    public Vector3 targetObjectPosition;
    public Vector3 NewObjectPosition;

    public Tilemap tilemap;
    public TileBase tileBase;

    public float moveTime = 0.3f;

    public static AbilityMoveSelectedObject instance = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null) instance = this;
        else if (instance != this)Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        originalObjectPosition = ExtensionMethods.RoundVect3(transform.position);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) FetchTilePosition();
        else return;
    }

    private void FetchTilePosition()        //Get tile X, Y, Z positions of first position tile and second position tile
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetObjectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // Vector3 targetPosition = mouse position
            tileBase = ExtensionMethods.getTilemapCellPosition(tilemap, targetObjectPosition);

            //Debug.Log("My TileBase Position is " + tileBase);

            /*targetObjectPosition = ExtensionMethods.RoundVect3(targetObjectPosition);       //Round or floor Vector3 targetPosition values

            Debug.Log("Tile Position 1 is " + targetObjectPosition);

            StartCoroutine(ExecuteTileSwitchPositions(targetObjectPosition));*/
        }
        else return;
    }

    private IEnumerator ExecuteTileSwitchPositions(Vector3 destination)
    {
        float sqrRemainingDistanceToDestination = (originalObjectPosition - destination).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistanceToDestination > 0)
        {
            transform.position = Vector3.MoveTowards(originalObjectPosition, destination, 20 * Time.deltaTime);
            NewObjectPosition = transform.position;

            yield return null;
        }
    }
}
