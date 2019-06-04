using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsSoundsController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rail;
    float firstOne;
    float railOnFirstOne;
    void Start()
    {
        firstOne = transform.GetChild(0).position.z;
        railOnFirstOne = rail.position.z;
    }
    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            if(passed(transform.GetChild(i).position.z) &&! transform.GetChild(i).GetComponent<AudioSource>().isPlaying)
            {
                transform.GetChild(i).GetComponent<AudioSource>().Play();
                transform.GetChild(i).GetComponent<AudioSource>().volume = rail.GetComponent<Velocity>().Speed / 80;
            }
        }
    }
    bool passed(float point)
    {
        return (Mathf.Abs(rail.position.z - railOnFirstOne)%100 - (point - firstOne))<3;
    }
}
