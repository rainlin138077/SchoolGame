using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public CircleCollider2D CircleCollider;
    public GameObject bulletPrefab; // 子彈預製體
    public float bulletSpeed = 100f; // 子彈速度
     public float detectionInterval = 0.3f; // 偵測間隔時間
    // Start is called before the first frame update
    private void Start()
    {
        // 開始範圍偵測的 Coroutine
        StartCoroutine(DetectAndShoot());
    }

    private IEnumerator DetectAndShoot()
    {
        // 不斷重複偵測敵人
        while (true)
        {
            // 在範圍內檢查是否有敵人
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(CircleCollider.bounds.center, CircleCollider.radius);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Player"))
                {
                    Vector2 targetPosition = enemy.transform.position;
                    Debug.Log("Detected Target at position: " + targetPosition);
                    
                    // 發射子彈
                    FireBullet(targetPosition);

                    // 等待一段時間再發射
                    yield return new WaitForSeconds(detectionInterval);
                    break;
                }
            }

            // 等待一段時間後再進行下一次偵測
            yield return new WaitForSeconds(detectionInterval);
        }
    }

    private void FireBullet(Vector2 targetPosition)
    {
        // 計算從當前位置到目標位置的方向
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // 生成子彈並設置位置與方向
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 設定子彈的速度和方向
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
        Debug.Log("發射成功");
    }
}

