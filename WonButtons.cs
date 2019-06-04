using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonButtons : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Share()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
