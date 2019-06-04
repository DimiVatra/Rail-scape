using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform TheMaster;
    public float Delay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Follow = TheMaster.position;
        Follow.z += Delay - 200f;
        Follow.z %= 800;
        Follow.z += 200f;
        transform.position = Follow;
    }
}
