using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondController : MonoBehaviour {

    [SerializeField]
    float maxSpeed = 1.2f;
    [SerializeField]
    float jumpForce = 200f;
    [SerializeField]
    bool bGrounded;


    //Private
    Rigidbody2D rB;
    bool bFacingRight = true;
    bool bKinematic = false;
    bool bDoubleJump = false;

    //To Manage Ground
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundMask;


    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();

        if (rB.bodyType == RigidbodyType2D.Kinematic)
        {
            bKinematic = true;
        }
    }

    // Use FixedUpdate for Physics
    private void FixedUpdate()
    {
        bGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

        //Reset DoubleJump
        if (bGrounded)
        {
            bDoubleJump = false;
        }


        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //Debug.Log("moveX : " + moveX.ToString());
        //Debug.Log("moveY : " + moveY.ToString());

        //GetComponent<Rigidbody2D>().velocity = new Vector2(moveX*maxSpeed, moveY*maxSpeed);

        if (bKinematic)
        {
            rB.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
        }

        else
        {
            rB.velocity = new Vector2(moveX * maxSpeed, rB.velocity.y);
        }

        if ((moveX > 0 && !bFacingRight) || (moveX < 0 && bFacingRight))
        {
            FlipSprite();
        }

    }

    void FlipSprite()
    {
        bFacingRight = !bFacingRight;
        Vector3 spriteLocalScale = transform.localScale;
        spriteLocalScale.x *= -1;
        transform.localScale = spriteLocalScale;
    }

    // Update is called once per frame
    private void Update()
    {
        /* Standard Jump
        if (bGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rB.AddForce(new Vector2(0, jumpForce));
        }*/

        //Double Jump
        if ((bGrounded || !bDoubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            rB.AddForce(new Vector2(0, jumpForce));

            if (!bDoubleJump && !bGrounded)
            {
                bDoubleJump = true;
            }
        }
    }

}
