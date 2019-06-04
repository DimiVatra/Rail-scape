using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowTrainFall : MonoBehaviour
{
    // Start is called before the first frame update
    public CapsuleCollider ColliderToDisable;
    public MeshCollider ColliderToDisable2;
    public Rigidbody RigidbodyToDisable;
    bool DisableCharacterController = false;
    public void AllowFall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        if (ColliderToDisable != null)
            ColliderToDisable.isTrigger = true;
        if (ColliderToDisable2 != null)
        {
            if (ColliderToDisable2.convex == false)
                ColliderToDisable2.convex = true;
            ColliderToDisable2.isTrigger = true;
        }
        if (RigidbodyToDisable != null)
            RigidbodyToDisable.isKinematic = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-200f)

        {
            Application.Quit();
        }
    }
}
