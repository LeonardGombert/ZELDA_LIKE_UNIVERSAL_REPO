using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed;
    public GameObject player;
    bool playerHasMoved;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.back);
            transform.rotation = rotation;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
}
