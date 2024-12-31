using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("initial");
        }
    }

    [System.Obsolete]
    public void GameStart()
    {
        Application.LoadLevel("start");
    }
    
    public void GameExit()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

}