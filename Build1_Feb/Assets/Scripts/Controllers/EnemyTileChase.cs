using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileChase : MonoBehaviour
{
    Vector3 originTile;
    Vector3 destinationTile;

    public float speed;
    public Transform target;
    public static bool playerHasMoved = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0) vertical = 0;

        if (playerHasMoved == true)
        {
            EnemyMoveCalculations();
        }
        else return;
    }

    private void EnemyMoveCalculations()
    {
        target = GameObject.FindWithTag("Player").transform;

        Debug.DrawLine(transform.position, target.position);
        
        /*
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.back);
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        */

        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 1f)     //move if distance from target is greater than 1
        {

            //transform.position + 1
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        originTile = transform.position;
        destinationTile = originTile + new Vector3((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"), 0);

        StartCoroutine(ExecuteEnemyMoveTowardsPlayer());
    }

    private IEnumerator ExecuteEnemyMoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, target.position) > 1f)     //move if distance from target is greater than 1
        {
            //transform.position + 1
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        yield return null;
    }
}
