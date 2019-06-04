using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : MonoBehaviour
{
    public bool IsPickable;
    public bool CheckAgain;
    public bool StayAfterGetToFloor;
    public string nameToDisplay;
    public bool ViewImage;
    public RawImage ShowImage;
    Rigidbody temporaryRigidbody;
    BoxCollider boxCollider;
    Vector3 lastPosition;
    bool dropped;
    private void Start()
    {
        if(StayAfterGetToFloor)
        {
            boxCollider = GetComponent<BoxCollider>();
            temporaryRigidbody = GetComponent<Rigidbody>();
            lastPosition = transform.position;
            dropped = false;
        }
    }
    public void GoToFloor()
    {
        if(temporaryRigidbody == null && StayAfterGetToFloor)
        {
            temporaryRigidbody = gameObject.AddComponent<Rigidbody>();
            boxCollider.isTrigger = false;
            temporaryRigidbody.useGravity = true;
            dropped = true;
        }
    }
    private void Update()
    {
        if(dropped )
        {
            if(transform.position==lastPosition)
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
                boxCollider.isTrigger = true;
                dropped = false;
            }
            lastPosition = transform.position;
        }
    }
    public bool IsVisible;
}
