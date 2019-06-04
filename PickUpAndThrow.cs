using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpAndThrow : MonoBehaviour
{
    public Transform Destination;
    public bool occupied;
    Pickable Pickable;
    Collider focusedObject;
    Collider takenObject;
    public Canvas canvas;
    Transform PickUpText;
    Transform ThrowText;
    public bool reSearch;
    public SphereCollider ColliderToIgnore1;

    void Start()
    {
        Pickable = null;
        focusedObject = null;
        PickUpText = canvas.transform.Find("PickUpText");
        ThrowText = canvas.transform.Find("ThrowText");
        reSearch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(occupied)
            {
                ThrowIt();
            }
            else if(focusedObject!=null)
            {
                PickItUp();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (occupied)
                ThrowIt();
        }
        if(occupied)
        {
            if(takenObject.GetComponent<Pickable>() != null && takenObject.GetComponent<Pickable>().ViewImage)
            {
                takenObject.GetComponent<Pickable>().ShowImage.gameObject.SetActive(true);
            }
            else
            {
                takenObject.gameObject.transform.position = Destination.position;
                takenObject.gameObject.transform.rotation = Destination.rotation;
            }
        }
    }
    void AllowThisPickUp(Collider other)
    {
        focusedObject = other;
        if(other.GetComponent<Pickable>() != null && other.GetComponent<Pickable>().ViewImage)
            PickUpText.GetComponent<TMPro.TextMeshProUGUI>().text = "F - "+other.gameObject.GetComponent<Pickable>().nameToDisplay;
        else
            PickUpText.GetComponent<TMPro.TextMeshProUGUI>().text="F-pick up "+other.gameObject.GetComponent<Pickable>().nameToDisplay;
        PickUpText.gameObject.SetActive(true);
    }
    void SetImageToScreen(Collider other)
    {
        focusedObject = other;
        focusedObject.gameObject.GetComponent<Pickable>().ShowImage.enabled = true;
    }
    void DenyThisPickUp()
    {
        focusedObject = null;
        PickUpText.gameObject.SetActive(false);
    }
    void HideImageFromScreen()
    {
        focusedObject.gameObject.GetComponent<Pickable>().ShowImage.enabled = false;
        focusedObject = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        Pickable pickable = other.GetComponent<Pickable>();
        if (other == ColliderToIgnore1) return;
        if (pickable!=null && pickable.IsPickable && (focusedObject==null || focusedObject.GetComponent<Pickable>().ViewImage) &&!occupied)
        {
            Pickable = pickable;
            AllowThisPickUp(other);
        }
        else if(pickable!=null && pickable.IsPickable && pickable.ViewImage &&(focusedObject==null)&&!occupied)
        {
            Pickable = pickable;
            AllowThisPickUp(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Pickable pickable = other.GetComponent<Pickable>();
        if (other == ColliderToIgnore1) return;
        if (pickable != null && pickable.IsPickable && (focusedObject == null) && !occupied && (reSearch || pickable.CheckAgain))
        {
            Pickable = pickable;
            AllowThisPickUp(other);
            reSearch = false;
            Pickable.CheckAgain = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Pickable pickable = other.GetComponent<Pickable>();
        if (other == ColliderToIgnore1) return;
        if (pickable != null && pickable.IsPickable && focusedObject == other)
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
        ThrowText.GetComponent<TMPro.TextMeshProUGUI>().text = "F - Throw it";
        ThrowText.gameObject.SetActive(true);
        if(!Pickable.StayAfterGetToFloor &&!(takenObject.GetComponent<Pickable>() != null && takenObject.GetComponent<Pickable>().ViewImage))
        {
            takenObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //takenObject.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    private void ThrowIt()
    {
        occupied = false;
        if (takenObject.GetComponent<Pickable>() != null && takenObject.GetComponent<Pickable>().ViewImage)
        {
            takenObject.GetComponent<Pickable>().ShowImage.gameObject.SetActive(false);
        }
        else if (!Pickable.StayAfterGetToFloor)
        {
            takenObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            takenObject.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
        else
        {
            Pickable.GoToFloor();
        }
        
        takenObject = null;
        ThrowText.gameObject.SetActive(false);
        reSearch = true;
    }
}
