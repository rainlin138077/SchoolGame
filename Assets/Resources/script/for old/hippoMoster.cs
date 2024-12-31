using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Pool;

public class hippoMostor : MonoBehaviour
{
    //
    public GameObject DritBall;

    //設定圓形框架觸碰器
    public CircleCollider2D CircleCollider;
    //設定方形框架觸碰器
    public BoxCollider2D SquareCollider;
    public int hippoBlood;
    public int d = 1;

    public Vector3 space;


    // Start is called before the first frame update
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

    //判斷主角是否在範圍裡
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 假設 SquareCollider 用來判斷攻擊的碰撞
        if (other != null)
        {
            //被子彈打到
            if (other == SquareCollider && other.CompareTag("Bullet"))
            {
                hippoBlood -= 1;  // 減少血量
                Debug.Log("Hippo hit by bullet. Health: " + hippoBlood);
            }

            //相碰的狀態
            if (other == SquareCollider && other.CompareTag("Player"))
            {
                hippoBlood -= 1;  // 減少血量
                Catplayer player = new Catplayer();
                player.playerBlood-=1;
                Debug.Log("Hippo hit by bullet. Health: " + hippoBlood);
            }

        // 假設 CircleCollider 用來偵測玩家是否在範圍內
            //if (other == CircleCollider && other.CompareTag("Player"))
            //{
            //    Vector2 targetPosition = other.transform.position;
            //    Debug.Log("玩家位置: " + targetPosition);
                
                //if(other)
            //}
        }
    }



}
