using UnityEngine;

public class playerCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 velocity = Vector3.zero;

    public GameObject End;


    [Range(0, 1)]
    public float smoothTime;

    public Vector3 positionOffset;

    private DoorManager doorManager;

    [Header("Axis Limitation")]
    private Vector2 xLimit;
    private Vector2 yLimit;

    void Start()
    {
        End.gameObject.SetActive(false);
    }
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        doorManager = DoorManager.Instance; // 獲取 DoorManager 的實例
        if (doorManager == null)
        {
            Debug.LogError("找不到 DoorManager！");
        }
    }

    private void Update()
    {
        UpdateMapLimits(); // 根據地圖更新相機限制範圍
    }

    private void LateUpdate()
    {
        // 根據玩家位置平滑移動相機
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y),
            Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y),
            -10 // 固定 Z 軸
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // 更新地圖範圍
    private void UpdateMapLimits()
    {
        int currentMapIndex = doorManager != null ? doorManager.currentMapIndex : 0;

        switch (currentMapIndex)
        {
            case 0:
                xLimit = new Vector2(-202, -179);
                yLimit = new Vector2(-49, -47);
                Debug.Log("第一關範圍更新");
                break;
            case 1:
                xLimit = new Vector2(-113, -89);
                yLimit = new Vector2(-49, -47);
                Debug.Log("第二關範圍更新");
                break;
            case 2:
                xLimit = new Vector2(-202, -179);
                yLimit = new Vector2(-94, -92);
                Debug.Log("第三關範圍更新");
                break;
            case 3:
                xLimit = new Vector2(-113, -89);
                yLimit = new Vector2(-94, -92);
                Debug.Log("第四關範圍更新");
                break;
            case 4:
                xLimit = new Vector2(-202, -179);
                yLimit = new Vector2(-139, -137);
                Debug.Log("第五關範圍更新");
                break;
            default:
                xLimit = new Vector2((float)-2.3, (float)89.5);
                yLimit = new Vector2((float)-77.8,-68);

                End.gameObject.SetActive(true);
                Debug.Log("End");
                break;
        }
    }

    // 地圖切換時直接跳轉相機
    public void JumpToTarget()
    {
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y),
            Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y),
            -10 // 固定 Z 軸
        );

        // 直接設置相機位置到目標位置
        transform.position = targetPosition;
        Debug.Log("相機已跳轉到新位置");
    }
}
