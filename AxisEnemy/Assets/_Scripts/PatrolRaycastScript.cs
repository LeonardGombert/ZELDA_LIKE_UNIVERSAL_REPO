using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRaycastScript : MonoBehaviour
{
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, 0.1f);

        if(groundInfo.collider == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                StartCoroutine(microMovementCooldown());
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                StartCoroutine(microMovementCooldown());
            }
        }
    }

        IEnumerator microMovementCooldown()
        {
        Debug.Log("Working");

        /*while (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }*/
        yield return new WaitForSeconds(2);

        yield return null;
        }
 
}
