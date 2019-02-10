using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbilitySelectObjectToMove : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 firstTilePosition;
    public Vector3 targetObjectPosition;

    private GameObject targetedObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) FetchObjectPosition();
        else return;
    }

    private void FetchObjectPosition()        //Get tile X, Y, Z positions of first position tile and second position tile
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstTilePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            firstTilePosition = ExtensionMethods.getCenteredWorldPosition(firstTilePosition.x, firstTilePosition.y, 0);

            Debug.Log("My click position is " + firstTilePosition);

            //firstTilePosition = ExtensionMethods.RoundVect3(firstTilePosition);



            RaycastHit2D hit = Physics2D.Raycast(transform.position, firstTilePosition);

            if (hit.collider != null)
            {
                targetedObject = hit.collider.gameObject;
                Debug.Log("I have hit MOTHAFUCKA" + targetedObject.tag);

                targetObjectPosition = targetedObject.transform.position;
                Debug.Log("MOTHAFUCKA'S POSITION" + targetObjectPosition);

                targetedObject.AddComponent<AbilityMoveSelectedObject>();
            }
        }
    }
}