using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdController : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    float maxSpeed = 2f;
    [SerializeField]
    float dashVelocity = 100f;

    Rigidbody2D rigidBody;

    bool bKinematic = true;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        InvokeRepeating("SpawnTrailPart", 0, 0.2f); // replace 0.2f with needed repeatRate

        if (bKinematic == true)
        {
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Use FixedUpdate for Physics
    void FixedUpdate()
    {

        //Basic Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Debug.Log("moveX : " + moveX.ToString());
        Debug.Log("moveY : " + moveY.ToString());

        if (bKinematic)
        {
            rigidBody.velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);
        }
        
        //Dash Ability

        if (Input.GetKeyDown(KeyCode.LeftShift) && bKinematic)
        {
            //rigidBody.velocity = new Vector2(moveX * dashVelocity, moveY * dashVelocity); //makes player "teleport"
            rigidBody.AddForce(new Vector2(dashVelocity, 0), ForceMode2D.Force);   //make player "dash" and not "teleport"
            SpawnTrailPart();
        }


        //Sprite Direction
        if (moveX > 0)
        {
            anim.SetBool("bMoveRight", true);
            anim.SetBool("bMoveLeft", false);
            anim.SetBool("bMoveUp", false);
            anim.SetBool("bMoveDown", false);
            anim.SetBool("bIdle", false);
        }

        else if (moveX < 0)
        {
            anim.SetBool("bMoveLeft", true);
            anim.SetBool("bMoveRight", false);
            anim.SetBool("bMoveUp", false);
            anim.SetBool("bMoveDown", false);
            anim.SetBool("bIdle", false);
        }

        if (moveY > 0)
        {
            anim.SetBool("bMoveUp", true);
            anim.SetBool("bMoveRight", false);
            anim.SetBool("bMoveLeft", false);
            anim.SetBool("bMoveDown", false);
            anim.SetBool("bIdle", false);
        }

        else if (moveY < 0)
        {
            anim.SetBool("bMoveDown", true);
            anim.SetBool("bMoveRight", false);
            anim.SetBool("bMoveLeft", false);
            anim.SetBool("bMoveUp", false);
            anim.SetBool("bIdle", false);
        }

        if(moveX == 0 && moveY == 0)
        {
            anim.SetBool("bMoveRight", false);
            anim.SetBool("bMoveLeft", false);
            anim.SetBool("bMoveUp", false);
            anim.SetBool("bMoveDown", false);
            anim.SetBool("bIdle", true);
        }
    }

    private void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        trailPart.transform.localScale = transform.localScale;
        //trailParts.Add(trailPart);

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        Destroy(trailPart, 0.5f);
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f;
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }


}