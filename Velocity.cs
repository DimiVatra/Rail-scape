using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float Speed=1f;
    public bool Stop;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.velocity = new Vector3(0, 0, Speed);
    }

    void FixedUpdate()
    {
        //Speed = rb.velocity.z;
        Vector3 position = transform.position;
        position.z += Time.deltaTime * Speed;
        transform.position = position;
        if (Stop && Speed > 0)
        {
            Speed -= Time.deltaTime* 9f;
        }
        if (Speed < 0)
        { Speed = 0;
            GetComponent<Rigidbody>().drag = 1;
        }

    }
}