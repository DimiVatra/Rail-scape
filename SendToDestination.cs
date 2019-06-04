using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToDestination : MonoBehaviour
{
    public GameObject Destination;
    Blocked Blocked;
    public bool IsOnDestination;
    public string nameToDisplay;
    public bool CheckAgain;
    public bool _BlowTorch;
    // Start is called before the first frame update
    void Start()
    {
        Blocked = GetComponent<Blocked>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsBlocked()
    {
        return Blocked.Block;
    }
    public void GoToDestination()
    {
        if(_BlowTorch)
        {
            Destination.GetComponent<Burnable>().Burn(gameObject);
        }
        else
        {
            transform.position = Destination.transform.position; IsOnDestination = true;
            Blocked.AllowFurther = true;
        }
    }
    public void DoneBurning()
    {
        IsOnDestination = true;
        Blocked.AllowFurther = true;
    }
}
