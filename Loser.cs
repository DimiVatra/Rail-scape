using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loser : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform LoseCanvas;
    void Start()
    {
        
    }
    bool Done;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-100f && ! Done)
        {
            Done = true;
            LoseCanvas.gameObject.SetActive(true);
        }
    }
}
