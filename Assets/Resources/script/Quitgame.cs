using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Quitgame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
