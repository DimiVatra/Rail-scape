using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenDrawer : MonoBehaviour
{
    float InitialXCoordinate;
    public OpenDrawer Left;
    public OpenDrawer Right;
    bool Opening = false;
    bool Closing = false;
    bool Opened = false;
    static int NumberOfTriggered = 0;
    bool ShouldFreeze;
    public int respondToKey;
    public bool CanInitiate;
    Transform tr;
    public bool Focused;
    public GameObject MarkOfFocus;
    FirstPersonController personToHold;
    
    void Start()
    {
        tr = GetComponent<Transform>();
        InitialXCoordinate = tr.position.x;
        personToHold = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Opening)
        {
            Vector3 position = tr.position;
            if (position.x > InitialXCoordinate - 0.70f)
            {
                position.x -= Time.deltaTime;
                tr.position = position;
            }
            else
            {
                Opening = false;
                Opened = true;
            }
        }
        else if (Closing)
        {
            Vector3 position = tr.position;
            if (position.x < InitialXCoordinate)
            {
                position.x += Time.deltaTime;
                tr.position = position;
            }
            else
            {
                position.x = InitialXCoordinate;
                tr.position = position;
                Closing = false;
                Opened = false;
            }
        }
        else if (Input.GetKeyDown((KeyCode)(48 + respondToKey)))
        {
            Focus();
            OpenDrawer left = Left, right = Right;
            while (left != null)
            {
                left.UnFocus();
                left = left.Left;
            }
            while (right != null)
            {
                right.UnFocus();
                right = right.Right;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.O)||Input.GetKeyDown(KeyCode.Return))
            {
                if (Focused && CheckAvaiability())
                {
                    if (Opened) Close();
                    else Open();
                }
            }
            else if(Input.GetMouseButtonUp(1))
            {
                if (Focused && Right != null && !ShouldFreeze)
                {
                    Right.Focus();
                    UnFocus();
                }
            }
            else if (Input.GetMouseButtonDown(1) )
            {
                AnnounceFreeze();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (Focused && Left != null && !ShouldFreeze)
                {
                    Left.Focus();
                    UnFocus();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                AnnounceFreeze();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController" && CanInitiate)
        {
            Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController" && CanInitiate)
        {
            Close();
        }
    }
    void AnnounceFreeze()
    {
        if (!Focused) ShouldFreeze = true;
        else ShouldFreeze = false;
    }
    public void Open()
    {
        Opening = true;
        Closing = false;
    }
    public void Close()
    {
        Opening = false;
        Closing = true;
    }
    public void Focus()
    {
        Focused = true;
        MarkOfFocus.SetActive(true);
    }
    public void UnFocus()
    {
        Focused = false;
        MarkOfFocus.SetActive(false);
    }
    public bool CheckAvaiability()
    {
        if (CanInitiate) return true;
        return Left.IsTheFirstLocked();
    }
    public bool IsTheFirstLocked()
    {
        if (Opened) return false;
        if (CanInitiate) return true;
        return Left.AllOpenToThis();
    }
    public bool AllOpenToThis()
    {
        if (!Opened) return false;
        if (CanInitiate) return true;
        return Left.AllOpenToThis();
    }
}
