using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStairs : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Person;
    public CharacterController PersonCollider;
    bool HeIsInside;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) && HeIsInside)
        {
            Vector3 position = Person.transform.position;
            position.x += Time.deltaTime;
            Person.transform.position = position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == PersonCollider)
            HeIsInside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == PersonCollider)
            HeIsInside = true;
    }
}
