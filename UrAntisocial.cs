using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UrAntisocial : MonoBehaviour
{
    public Transform Antisocial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Share()
    {
        Antisocial.gameObject.SetActive(true);
    }
}
