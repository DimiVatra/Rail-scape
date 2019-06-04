using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public GameObject Fire;
    public GameObject Caller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - (FireStartingMoment+20) < 0.1 && Time.time - (FireStartingMoment + 20)>0)
        {
            foreach(ParticleSystem particle in Fire.GetComponents<ParticleSystem>())
            {
                particle.Stop();
            }
            Caller.GetComponent<SendToDestination>().DoneBurning();
        }
        if (Time.time - (FireStartingMoment + 10) < 0.1 && Time.time - (FireStartingMoment + 10) > 0)
            GetComponent<Renderer>().enabled = false;
    }
    float FireStartingMoment=-300;
    public void Burn(GameObject Caller)
    {
        this.Caller = Caller;
        FireStartingMoment = Time.time;
        foreach (ParticleSystem particle in Fire.GetComponents<ParticleSystem>())
        {
            particle.Play();
        }

    }
}
