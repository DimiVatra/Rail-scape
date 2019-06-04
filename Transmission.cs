using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour
{
    // Start is called before the first frame update
    public Velocity FollowThatSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
         rotation.z -= FollowThatSpeed.Speed * 3 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
