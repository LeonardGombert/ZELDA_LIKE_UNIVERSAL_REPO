using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveObject : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 originalObjectPosition;
    public Vector3 targetObjectPosition;
    public Vector3 NewObjectPosition;

    public float moveTime = 0.3f;

    void Awake()
    {
        originalObjectPosition = transform.position;
        Debug.Log("My original position is " + originalObjectPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //originalObjectPosition = transform.position;
        //Debug.Log("My original position is " + originalObjectPosition);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) FetchTilePosition();
        else return;
    }

    private void FetchTilePosition()        //Get tile X, Y, Z positions of first position tile and second position tile
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetObjectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // Vector3 targetPosition = mouse position

            targetObjectPosition = ExtensionMethods.getFlooredWorldPosition(targetObjectPosition);       //Floor Vector3 targetPosition values
           
            StartCoroutine(ExecuteTileSwitchPositions(targetObjectPosition));

            Debug.Log("My Destination is " + NewObjectPosition);
        }
        else return;
    }

    private IEnumerator ExecuteTileSwitchPositions(Vector3 destination)
    {
        float sqrRemainingDistanceToDestination = (originalObjectPosition - destination).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistanceToDestination > 0)
        {
            NewObjectPosition = Vector3.MoveTowards(originalObjectPosition, destination, 20f);
            NewObjectPosition = ExtensionMethods.getFlooredWorldPosition(NewObjectPosition);
            transform.position = NewObjectPosition;

            this.transform.position = ExtensionMethods.FloorVect3Values(this.transform.position) + new Vector2(0.5f, 0.5f);

            yield return null;
        }
    }
}
