using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newhippo : MonoBehaviour
{

    public int hippoBlood;
    public BoxCollider2D boxCollider2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hippoBlood == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hippoBlood -= 1;  // 減少血量
            //Debug.Log("Hippo hit by bullet. Health: " + hippoBlood);    
        }
    // 假設 CircleCollider 用來偵測玩家是否在範圍內
    }



}
