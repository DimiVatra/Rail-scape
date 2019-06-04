using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseToBook : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public GameObject SimpleMan;
    public GameObject JeffTheKiller;
    public Pickable MirrorToActivate;
    // Start is called before the first frame update
    void Start()
    {
        OnTriggerExit(sphereCollider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other==sphereCollider)
        {
            SimpleMan.SetActive(false);
            JeffTheKiller.SetActive(true);
            MirrorToActivate.IsPickable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == sphereCollider)
        {
            SimpleMan.SetActive(true);
            JeffTheKiller.SetActive(false);
            MirrorToActivate.IsPickable = true;
        }
    }
}
