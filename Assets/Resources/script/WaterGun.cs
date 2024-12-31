using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaterGun : MonoBehaviour
{   
    public float speed;
    public float Timer;
    public float radian;
    public delegate void Recyle(WaterGun bullet);
    public Recyle recyle;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnEnable()
    {
        // 當子彈被啟用時，計算其方向
        direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0).normalized;
    }
    void Update()
    {
        float movement = speed * Time.deltaTime;
        Timer -=Time.deltaTime;

        if(Timer <= 0)
        {
            //Destroy(this.gameObject);
            recyle(this);
        }
        this.gameObject.transform.position += direction  * Time.deltaTime * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if(other.gameObject.tag == "Monster" )
        {
            recyle(this);
        }
    }
}
