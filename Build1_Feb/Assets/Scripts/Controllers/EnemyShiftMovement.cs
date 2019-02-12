    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShiftMovement : MonoBehaviour
{
    Vector3 centerTile;
    Vector3 leftTile;
    Vector3 rightTile;

    Vector3 currentPosition;
    Vector3 destinationTile;

    Vector3 vel;

    private bool patrolIsCoolingDown = false;
    private float patrolCooldownValue = 1f;
    private float speed = 25f;

    private Rigidbody2D rb;
    private Animator anim;

    public bool movingRight = false;
    public bool movingLeft = false;
    

    // Start is called before the first frame update
    void Awake()
    {
        centerTile = transform.position;
        leftTile = Vector3.left*2;
        rightTile = Vector3.right*2;

        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        vel = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        GatherMovementCalculations();
    }

    private void GatherMovementCalculations()
    {
        if (transform.position == centerTile)
        {
            destinationTile = centerTile + leftTile;
        }

        if (transform.position == centerTile + leftTile)
        {
            movingLeft = false;
            destinationTile = currentPosition + (rightTile * 2);
            movingRight = true;
        }

        if (transform.position == centerTile + rightTile)
        {
            movingRight = false;
            destinationTile = currentPosition + (leftTile * 2);
            movingLeft = true;
        }
            
        StartCoroutine(ExecutePatrolMovement(destinationTile, patrolCooldownValue));
    }

    private IEnumerator ExecutePatrolMovement(Vector3 destination, float cooldown)
    {
        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        if (movingRight == true)
        {
            anim.SetBool("moveRight", true);
            anim.SetBool("moveLeft", false);
        }

        //if (currentPosition != rightTile ||  currentPosition != leftTile) anim.SetBool("isSliding", true);
        if (movingLeft == true)
        {
            anim.SetBool("moveRight", false);
            anim.SetBool("moveLeft", true);
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        currentPosition = transform.position;

        yield return null;
    }

    private IEnumerator PatrolArrivalOnPositionCooldown(float cooldown)
    {
        patrolIsCoolingDown = true;

        while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        patrolIsCoolingDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "kill")
        {
            //destroy Object
        }
    }
}
