using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectObject : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector3 objectPosition;
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

    private void FetchObjectPosition()
    {   /*    
        objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        objectPosition = ExtensionMethods.getFlooredWorldPosition(objectPosition);

        Debug.Log("My click position is " + objectPosition);
        */
        RaycastHit2D hit = Physics2D.Raycast(transform.position, ExtensionMethods.getFlooredWorldPosition(Input.mousePosition));

        if (hit.collider != null)
        {
            targetedObject = hit.collider.gameObject;
            Debug.Log("I have hit a mothufucka named " + targetedObject.tag);

            targetedObject.AddComponent<MoveObject>();
        }
    }
}