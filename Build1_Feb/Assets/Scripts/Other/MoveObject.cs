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
        Debug.Log("The Object's original position is " + originalObjectPosition);
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
           
            StartCoroutine(MoveObjectTo(targetObjectPosition));

            Debug.Log("The Object's destination position is " + NewObjectPosition);
        }
        else return;
    }

    private IEnumerator MoveObjectTo(Vector3 destinationPosition)
    {
        float sqrRemainingDistanceToDestination = (originalObjectPosition - destinationPosition).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistanceToDestination > 0)
        {
            NewObjectPosition = Vector3.MoveTowards(originalObjectPosition, destinationPosition, 20f);
            NewObjectPosition = ExtensionMethods.getFlooredWorldPosition(NewObjectPosition);
            transform.position = NewObjectPosition;

            this.transform.position = ExtensionMethods.FloorVect3Values(this.transform.position) + new Vector2(0.5f, 0.5f);

            yield return null;
        }
    }
}
