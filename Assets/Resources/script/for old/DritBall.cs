using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DritBall : MonoBehaviour
{   
    public float speed;
    public float Timer;
    public float radian;
    public delegate void Recyle(DritBall bullet);
    public Recyle recyle;
    private Vector3 direction;


    void Update()
    {
        //float movement = speed * Time.deltaTime;
        Timer -=Time.deltaTime;

        if(Timer <= 0)
        {
            Destroy(this.gameObject);
            //recyle(this);
        }
        //this.gameObject.transform.position += direction  * Time.deltaTime * speed;
    }
}
