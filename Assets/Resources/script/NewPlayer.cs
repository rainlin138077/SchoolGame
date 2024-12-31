using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour
{

    private bool nearFlag = false; // 是否靠近旗子

    public GameObject popupWindow; // 彈窗 UI

    public Button closeButton; // 關閉按鈕
    public float speed = 1.0f;

    //製作角色的物理性能
    private new Rigidbody2D rigidbody2D;

    
    public SpriteRenderer sprite;
    void Start()
    {
        rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseWindow); // 添加點擊事件
            popupWindow.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("CloseButton 未設置！");
        }
    }
    void Update()
    {


        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            filp("A");
            this.gameObject.transform.position -= new Vector3(speed,0,0);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            filp("D");
            this.gameObject.transform.position += new Vector3(speed,0,0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(new Vector2(0,50),ForceMode2D.Impulse);
            print("Space clicked!");
        }

        //旗子的功能
        if (nearFlag && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("視窗彈出！");
            ShowWindow();
        }

        if(Input.GetKey(KeyCode.Escape) && closeButton != null)
            {
                CloseWindow();
            }
    }

    private void filp(string word)
    {
        if(word == "A" && sprite.flipX ==true)
        {
            sprite.flipX = false;
        }

        if(word == "D" && sprite.flipX ==false)
        {
            sprite.flipX = true;
        }
    }

    void ShowWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(true); // 顯示彈窗
        }

        
    }


    void CloseWindow()
    {
        if (popupWindow != null)
        {
            popupWindow.SetActive(false); // 隱藏彈窗
        }

        Debug.Log("視窗已關閉");
    }

    //旗子判斷
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Flag")
        {
            nearFlag = true; // 靠近旗子
            Debug.Log("靠近旗子！");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Flag")
        {
            nearFlag = false; // 離開旗子
            Debug.Log("離開旗子！");
        }
    }

}
