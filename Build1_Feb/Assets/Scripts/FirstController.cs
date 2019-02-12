using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour {

    [SerializeField]
    float maxSpeed = 1.2f;

    Rigidbody2D rB;
    bool bFacingRight = true;
    bool bKinematic;


    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();

        if(rB.bodyType == RigidbodyType2D.Kinematic)
        {
            bKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Use FixedUpdate for Physics
    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //Debug.Log("moveX : " + moveX.ToString());
        //Debug.Log("moveY : " + moveY.ToString());

        //GetComponent<Rigidbody2D>().velocity = new Vector2(moveX*maxSpeed, moveY*maxSpeed);

        if (bKinematic)
        {
            rB.velocity = new Vector2 (moveX * maxSpeed, moveY*maxSpeed);
        }

        else
        {
            rB.velocity = new Vector2 (moveX * maxSpeed, rB.velocity.y);
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
}

