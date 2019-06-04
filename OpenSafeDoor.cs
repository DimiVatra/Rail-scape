using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSafeDoor : MonoBehaviour
{
    public string Password;
    public bool Locked = true;
    public Pickable[] inside;
    public Canvas inputKey;
    InputField inputField;
    bool Opening = false;
    bool Closing = false;
    bool Opened = false;
    int DetectDistance = 4;
    float InitialYRotation;
    Transform tr;
    BoxCollider bc;
    public Collider otherCollider;
    UnlockSafeDoor Hand;
    void Start()
    {
        inputField = inputKey.GetComponentInChildren<InputField>();
        inputField.onEndEdit.AddListener(delegate { LookInput(inputField); });
        tr = GetComponent<Transform>();
        bc = GetComponent<BoxCollider>();
        Vector3 originalTriggerCollider = bc.center;
        originalTriggerCollider.z = -1f;
        bc.center = originalTriggerCollider;
        InitialYRotation = tr.rotation.eulerAngles.y;
        Hand = tr.GetComponentInChildren<UnlockSafeDoor>();
    }
    void LookInput(InputField input)
    {
        if(input.text==Password)
        {
            Locked = false;
            Open();
            inputKey.gameObject.SetActive(false);
            Vector3 originalTriggerCollider = bc.center;
            originalTriggerCollider.z = 1f;
            bc.center = originalTriggerCollider;
        }
        else
        {
            input.text = "";
            input.Select();
            inputField.ActivateInputField();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Opening)
        {
            if(Hand.Open())
            {
                Vector3 rotation = tr.rotation.eulerAngles;
                if (rotation.y > InitialYRotation - 90f)
                {
                    rotation.y -= 30*Time.deltaTime;
                    tr.rotation = Quaternion.Euler(rotation);
                }
                else
                {
                    Opening = false;
                    Opened = true;
                    foreach(Pickable pickable in inside)
                    {
                        pickable.IsPickable = true;
                        pickable.CheckAgain = true;
                    }
                }
            }
        }
        else if (Closing)
        {
            Vector3 rotation = tr.rotation.eulerAngles;
            if (rotation.y < InitialYRotation)
            {
                rotation.y += 1f;
                tr.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                rotation.y = InitialYRotation;
                tr.rotation = Quaternion.Euler(rotation);
                if(Hand.Close())
                {
                    Closing = false;
                    Opened = false;
                    foreach (Pickable pickable in inside)
                    {
                        pickable.IsPickable = false;
                        pickable.CheckAgain = false;
                    }
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Locked && other==otherCollider)
        {
            Open();
        }
        else if(other==otherCollider)
        {
            inputKey.gameObject.SetActive(true);
            
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!Locked && other == otherCollider)
        {
            Close();
        }
        else if (other == otherCollider)
        {
            inputKey.gameObject.SetActive(false);
        }
    }
    public void Open()
    {
        Opening = true;
        Closing = false;
        bc.size = new Vector3(bc.size.x * DetectDistance, bc.size.y * DetectDistance, bc.size.z * DetectDistance);
    }
    public void Close()
    {
        Opening = false;
        Closing = true;
        bc.size = new Vector3(bc.size.x / DetectDistance, bc.size.y / DetectDistance, bc.size.z / DetectDistance);
    }
}
