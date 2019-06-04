using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocked : MonoBehaviour
{
    public bool AllowFurther;
    public bool block;
    public bool HideUntilUnlock;
    public bool Block {
        get {
            if (Condition != null && Condition.AllowFurther)
            {
                block = false;
                if(HideUntilUnlock)
                    gameObject.GetComponent<Renderer>().enabled = true;
            }
            return block;
        }
        set
        {
            if(Condition==null || Condition.Block==false)
                block = value;
            if (!block &&HideUntilUnlock)
                gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
    public Blocked Condition;
    // Start is called before the first frame update
    void Start()
    {
        if(HideUntilUnlock)
            gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
