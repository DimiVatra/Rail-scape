using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    public Transform Destination;
    public bool occupied;
    SendToDestination Pickable;
    Collider focusedObject;
    Collider takenObject;
    public Canvas canvas;
    Transform PickUpText;
    Transform ThrowText;
    public bool reSearch;
    bool LookingAtDestination;
    // Start is called before the first frame update
    void Start()
    {
        Pickable = null;
        focusedObject = null;
        PickUpText = canvas.transform.Find("PickUpText");
        ThrowText = canvas.transform.Find("ThrowText");
        reSearch = false;
        //ThrowText.GetComponent<TMPro.TextMeshProUGUI>().text = "F - Mount";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (occupied && LookingAtDestination)
            {
                SendIt();
            }
            else if (focusedObject != null)
            {
                PickItUp();
            }
        }
        if (occupied)
        {
            takenObject.gameObject.transform.position = Destination.position;
            takenObject.gameObject.transform.rotation = Destination.rotation;
        }
    }
    void AllowThisPickUp(Collider other)
    {
        focusedObject = other;
        PickUpText.GetComponent<TMPro.TextMeshProUGUI>().text = "F-pick up " + other.gameObject.GetComponent<SendToDestination>().nameToDisplay;
        PickUpText.gameObject.SetActive(true);
    }
    void DenyThisPickUp()
    {
        focusedObject = null;
        PickUpText.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(takenObject!=null)
        {
            if (takenObject.GetComponent<SendToDestination>().Destination.GetComponent<Collider>() == other)
            {
                LookingAtDestination = true;
                if(takenObject.GetComponent<SendToDestination>()._BlowTorch)
                {
                    ThrowText.GetComponent<TMPro.TextMeshProUGUI>().text = "F - Burn it";
                }
                else
                {
                    ThrowText.GetComponent<TMPro.TextMeshProUGUI>().text = "F - Mount it";
                }
                ThrowText.gameObject.SetActive(true);
                return;
            }
        }
        SendToDestination pickable = other.GetComponent<SendToDestination>();
        if (pickable != null && !pickable.IsBlocked() && !pickable.IsOnDestination && focusedObject == null && !occupied)
        {
            Pickable = pickable;
            AllowThisPickUp(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        SendToDestination pickable = other.GetComponent<SendToDestination>();
        if (pickable != null && !pickable.IsBlocked() && !pickable.IsOnDestination && (focusedObject == null) && !occupied && (reSearch || pickable.CheckAgain))
        {
            Pickable = pickable;
            AllowThisPickUp(other);
            reSearch = false;
            Pickable.CheckAgain = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (takenObject != null)
        {
            if (takenObject.GetComponent<SendToDestination>().Destination.GetComponent<Collider>() == other)
            {
                LookingAtDestination = false;
                ThrowText.gameObject.SetActive(false);
                return;
            }
        }
        
        SendToDestination pickable = other.GetComponent<SendToDestination>();
        if (pickable != null && !pickable.IsBlocked() && focusedObject == other)
        {
            Pickable = pickable;
            DenyThisPickUp();
        }
    }
    private void PickItUp()
    {
        occupied = true;
        takenObject = focusedObject;
        DenyThisPickUp();
        //ThrowText.GetComponent<TMPro.TextMeshProUGUI>().text = "F - Mount it";
        //ThrowText.gameObject.SetActive(true);
    }
    private void SendIt()
    {
        occupied = false;
        takenObject.GetComponent<SendToDestination>().GoToDestination();
        takenObject.transform.position = takenObject.GetComponent<SendToDestination>().Destination.transform.position;
        takenObject = null;
        ThrowText.gameObject.SetActive(false);
        reSearch = true;
    }
}
