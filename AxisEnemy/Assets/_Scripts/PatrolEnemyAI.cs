using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyAI : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;



    // Start is called before the first frame update
    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, currentPatrolPoint.position) <.1f)
        {
            if(currentPatrolIndex +1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }
        Vector2 patrolPointDir = currentPatrolPoint.position - transform.position;
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x)*Mathf.Rad2Deg -90f;

        Quaternion q = Quaternion.AngleAxis (angle, Vector2.right);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f);
    }
}
