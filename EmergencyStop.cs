using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmergencyStop : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform railToStop;
    public string SecurityCode;
    public Collider otherCollider;
    public Canvas inputKey;
    InputField inputField;
    Transform tr;
    bool NiggaDidIt = false;
    bool TurningIt = false;
    Vector3 InitialRotation;
    public AudioSource AudioSource;
    public AudioSource[] SoundsToDisable;
    float WinMoment;
    bool StartCounting;
    public Canvas NiggaWon;
    void Start()
    {
        tr = GetComponent<Transform>();
        InitialRotation = tr.rotation.eulerAngles;
        inputField = inputKey.GetComponentInChildren<InputField>();
        inputField.onEndEdit.AddListener(delegate { LookInput(inputField); });
    }
    private void Update()
    {
        if(NiggaDidIt)
        {
            WinMoment = Time.time;
            AudioSource.volume += 0.05f;
            foreach(AudioSource audioSource in SoundsToDisable)
            {
                audioSource.volume -= Time.deltaTime;
            }
            NiggaDidIt = false;
            StartCounting = true;
        }
        if(StartCounting && Time.time - WinMoment > 40f)
        {
                NiggaWon.gameObject.SetActive(true);
         
            StartCounting = false;
        }
    }
    void LookInput(InputField input)
    {
        if (input.text == SecurityCode)
        {   
            if(!NiggaDidIt)
            {
                StopTheRails();
                inputKey.gameObject.SetActive(false);
            }
        }
        else
        {
            input.text = "";
            input.Select();
            inputField.ActivateInputField();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other==otherCollider && !NiggaDidIt)
        {
            inputKey.gameObject.SetActive(true);
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == otherCollider && !NiggaDidIt)
        {
            inputKey.gameObject.SetActive(false);
        }
    }
    void StopTheRails()
    {
        TurningIt = true;
        NiggaDidIt = true;
        AudioSource.Play();
        AudioSource.volume = 0;
            railToStop.gameObject.GetComponent<Velocity>().Stop = true;
    }
}
