using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewRails : MonoBehaviour
{
    public AllowTrainFall[] ElementsToAllowFall;
    float FallDelay = 0;
    int NrOfRails = 4;
    public float TimeOutAfter;
    float FallingMoment;
    bool Once,Once2;
    public Transform LoseCanvas;
    // Start is called before the first frame update
    void Start()
    {
    }
    static bool TimeOut = false;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 200f)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = -600f- FallDelay;
            transform.position = newPosition;
            if(TimeOut)
            {
                    foreach (AllowTrainFall allowTrainFall in ElementsToAllowFall)
                    {
                        allowTrainFall.AllowFall();
                    }
            }
        }
        if(Time.time>TimeOutAfter && !Once)
        {
            Once = Once2 = true;
            FallDelay = 300f;
            TimeOut = true;
            FallingMoment = Time.time;
        }
        if(Once2 && Time.time - FallingMoment >10f)
        {
            Once2 = false;
            LoseCanvas.gameObject.SetActive(true);
        }
    }
}
