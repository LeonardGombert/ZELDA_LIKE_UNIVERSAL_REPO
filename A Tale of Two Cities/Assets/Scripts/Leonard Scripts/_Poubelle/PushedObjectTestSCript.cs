using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedObjectTestSCript : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.right * 50);
    }
}
