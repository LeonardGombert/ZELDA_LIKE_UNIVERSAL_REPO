using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    Vector3 centerTile;
    Vector3 leftTile;
    Vector3 rightTile;

    Vector3 currentPosition;
    Vector3 destinationTile;

    public static bool canPatrol = true;
    private bool patrolIsCoolingDown = false;
    private float patrolCooldownValue = 1f;
    private float speed = 25f;

    MoveObject moveObjectScript;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        centerTile = transform.position;
        currentPosition = transform.position;
        leftTile = Vector3.left;
        rightTile = Vector3.right;
        rb = GetComponent<Rigidbody2D>();
        moveObjectScript = GetComponent<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculations();
    }

    private void MovementCalculations()
    {
        if(MoveObject.hasBeenMoved == true)
        {
            centerTile = transform.position;
            MoveObject.hasBeenMoved = false;
            Object.Destroy(moveObjectScript);
        }

        if (canPatrol)
        {
            if (transform.position == centerTile) destinationTile = centerTile + leftTile;      //starting position
            if (transform.position == centerTile + leftTile) destinationTile = currentPosition + (rightTile * 2);
            if (transform.position == centerTile + rightTile) destinationTile = currentPosition + (leftTile * 2);

            StartCoroutine(ExecutePatrolMovement(destinationTile, patrolCooldownValue));
        }

        else return;
    }

    private IEnumerator ExecutePatrolMovement(Vector3 destination, float cooldown)
    {
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        currentPosition = transform.position;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        Debug.Log("I have collided with " + collidedObject.gameObject.name);
        //Destroy(collidedObject.gameObject);
        //Destroy(gameObject);
    }
}
