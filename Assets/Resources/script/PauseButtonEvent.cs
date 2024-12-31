using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PauseCanvas;
    button button;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClick()
    {
        //Time.timeScale=0f;
        Instantiate (PauseCanvas, Vector2.zero, Quaternion.identity);
    }
}
