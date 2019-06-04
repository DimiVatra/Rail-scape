using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSphere : MonoBehaviour
{
    // Start is called before the first frame update
    public bool RemoveIt;
    public Transform MakeNewChild;
    void Start()
    {
        
    }
    bool done = false;
    // Update is called once per frame
    void Update()
    {
        if(Time.time>10f && !done)
        {
            done = true;
            if(RemoveIt)
            {
                transform.GetChild(0).DetachChildren();
                MakeNewChild.SetAsLastSibling();
            }
        }
    }
}
