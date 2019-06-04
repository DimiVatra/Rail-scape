using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockNextStep : MonoBehaviour
{
    // Start is called before the first frame update
    public int Step = 0;
    public Transform HandleToRotate;
    public CharacterController characterController;
    public Canvas canvas;
    Transform ThrowText;
    Blocked[] Conditions;
    bool ActTheHandler;
    bool RotateDown, RotateStop;
    float InitialRotation;
    void Start()
    {
        Conditions = GetComponents<Blocked>();
        InitialRotation = HandleToRotate.rotation.eulerAngles.x;
        ThrowText = canvas.transform.Find("ThrowText");
        //avem trei Block
        //Tentatva de rotatie a handleurlui=>
        //Verificam la ce pas suntem, if !.Block => AllowFurther + Animatie rotatie;
        //consuma o rotatie => abia la urmatoarea deblocare se redeschide
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ActTheHandler)
                ActIt();
        }
        if(RotateDown)
        {
            Vector3 rotation = HandleToRotate.rotation.eulerAngles;
            if (rotation.x < InitialRotation + 20f)
            {
                rotation.x += 30 * Time.deltaTime;
                HandleToRotate.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                RotateDown = false;
                RotateStop = true;
            }
        }
        else if(RotateStop)
        {

            InitialRotation = HandleToRotate.rotation.eulerAngles.x;
            RotateDown = false;
            RotateStop = false;
        }
    }
    void ActIt()
    {
        BlockThisAct();
        RotateDown = true;
        foreach(Blocked blocked in Conditions)
        {
            if(!blocked.Block && !blocked.AllowFurther)
            {
                blocked.AllowFurther = true;
            }
        }
    }
    void AllowThisAct(Collider other)
    {
        ThrowText.GetComponent<TMPro.TextMeshProUGUI>().text = "F-Act the handler";
        ThrowText.gameObject.SetActive(true);
        ActTheHandler = true;
    }
    void BlockThisAct()
    {
        ThrowText.gameObject.SetActive(false);
        ActTheHandler = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == characterController && !ActTheHandler)
        {
            foreach(Blocked blocked in Conditions)
            {
                if(blocked.Condition.AllowFurther &&!blocked.AllowFurther)
                {
                    AllowThisAct(other);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other == characterController && !ActTheHandler)
        {
            foreach (Blocked blocked in Conditions)
            {
                if (blocked.Condition.AllowFurther && !blocked.AllowFurther)
                {
                    AllowThisAct(other);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == characterController && ActTheHandler)
        {
            BlockThisAct();
        }
    }
}
