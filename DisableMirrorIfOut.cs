using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMirrorIfOut : MonoBehaviour
{
    public Collider DissableIfOut;
    public GameObject MirrorToDisable;
    public GameObject VisibilityTeller;
    public Camera CameraThatLooks;
    Bounds bounds;
    void Start()
    {
        bounds = VisibilityTeller.GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(CameraThatLooks);
        if (GeometryUtility.TestPlanesAABB(planes, bounds))
            return true;
        else
            return false;
    }
    void Update()
    {
        //Debug.Log(ChangeItNow+""+IsThatClose+""+VisibilityBackUp);
        if (VisibilityBackUp!=IsVisible())
        {
            ChangeItNow = true;
        }
        VisibilityBackUp = IsVisible();
        if(ChangeItNow)
        {
            if(VisibilityBackUp &&IsThatClose)
            {
                MirrorToDisable.SetActive(true);
            }
            else
            {
                MirrorToDisable.SetActive(false);
            }
            ChangeItNow = false;
        }
    }
    bool VisibilityBackUp;
    bool IsThatClose;
    bool ChangeItNow = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other == DissableIfOut)
        {
            IsThatClose = true;
            ChangeItNow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other == DissableIfOut)
        {
            IsThatClose = false;
            ChangeItNow = true;
        }
    }
}
