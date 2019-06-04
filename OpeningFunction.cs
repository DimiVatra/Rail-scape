using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningFunction : MonoBehaviour
{
    // Start is called before the first frame update
    float InitialXCoordinate;
    bool Opening = false;
    bool Closing = false;
    bool Opened = false;
    int DetectDistance=2;
    Transform tr;
    BoxCollider bc;
    public Blocked Condition;

    void Start()
    {
        tr = GetComponent<Transform>();
        bc = GetComponent<BoxCollider>();
        InitialXCoordinate = tr.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Opening)
        {
            Vector3 position = tr.position;
            if(position.x<InitialXCoordinate+1.2f)
            {
                position.x += Time.deltaTime;
                tr.position = position;
            }
            else
            {
                Opening = false;
                Opened = true;
            }
        }
        else if(Closing)
        {
            Vector3 position = tr.position;
            if (position.x > InitialXCoordinate)
            {
                position.x -= Time.deltaTime;
                tr.position = position;
            }
            else
            {
                position.x = InitialXCoordinate;
                tr.position = position;
                Closing = false;
                Opened = false;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController")
        {
            if (Condition==null || Condition.AllowFurther)
                Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController")
        {
            if (Condition == null || Condition.AllowFurther)
                Close();
        }
    }
    private void Open()
    {
        Opening = true;
        Closing = false;
        bc.size = new Vector3(bc.size.x * DetectDistance, bc.size.y * DetectDistance, bc.size.z * DetectDistance);
    }
    private void Close()
    {
        Opening = false;
        Closing = true;
        bc.size = new Vector3(bc.size.x / DetectDistance, bc.size.y / DetectDistance, bc.size.z / DetectDistance);
    }
}
