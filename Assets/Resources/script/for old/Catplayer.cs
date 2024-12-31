using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Catplayer : MonoBehaviour
{

    public GameObject waterPerfab;
    public ObjectPool<WaterGun> BulletPool;

    public int playerBlood = 0;
    public float speed ;
    public int addBlood ;
    public int d=1;
    // Start is called before the first frame update
    void Start()
    {
        //設定初始血量
        playerBlood = 30 + addBlood;
        //創建odject pool指令
        BulletPool = new ObjectPool<WaterGun>
        (
            ()=>
            {
                //物件池創建子彈物件的時候,執行的動作
                WaterGun b = Instantiate(waterPerfab, this.transform.position, Quaternion.identity).GetComponent<WaterGun>();
                b.recyle = (bullet)=>{
                    BulletPool.Release(bullet);
                };
                return b;
            },
            (bullet)=>{
                //從物件池拿取子彈時，執行的動作
                bullet.transform.position = this.transform.position;
                bullet.Timer = 2;
                bullet.gameObject.SetActive(true);
            },
            (bullet)=>{
                // 子彈物件回收時,執行的動作
                bullet.gameObject.SetActive(false);
            },
            (bullet)=>{
                Destroy(bullet.gameObject);
            }, true , 1, 5
        );

    }

    // Update is called once per frame
    void Update()
    {
        //判斷血量
        if(playerBlood == 0)
        {
            print("game over");
        }
        else
        {
            //上下左右限制
            if(Input.GetKey(KeyCode.A))
            {
                if(Input.GetKey(KeyCode.W))
                {
                    this.gameObject.transform.position += new Vector3(-0.035f*speed,0.035f*speed,0);
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    this.gameObject.transform.position += new Vector3(-0.035f*speed,-0.035f*speed,0);        
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(-0.055f*speed,0,0);
                }
            }
            if(Input.GetKey(KeyCode.D))
            {
                if(Input.GetKey(KeyCode.W))
                {
                    this.gameObject.transform.position += new Vector3(0.035f*speed,0.035f*speed,0);
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    this.gameObject.transform.position += new Vector3(0.035f*speed,-0.035f*speed,0);
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(0.055f*speed,0,0);
                }
            }
            if(Input.GetKey(KeyCode.W)  && Input.GetKey(KeyCode.A)!=true && Input.GetKey(KeyCode.D) !=true)
            {
                this.gameObject.transform.position += new Vector3(0,0.055f*speed,0);
            }
            if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)!=true && Input.GetKey(KeyCode.D) !=true)
            {
                this.gameObject.transform.position += new Vector3(0,-0.055f*speed,0);
            }

            //開火限制
            //https://docs.unity3d.com/ScriptReference/Pool.ObjectPool_1.html
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //Instantiate(waterPerfab, this.transform.position, Quaternion.identity);
            
                float d_angle = 360/d;
                float d_radian = 360/d * Mathf.PI /180;

                for(int i=0;i <d;i++)
                {
                    WaterGun w = BulletPool.Get();
                    w.radian = d_radian * i;
                }
            }
        }
    }

}
