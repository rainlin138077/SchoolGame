using System;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors; // 存放所有門的陣列
    public GameObject player;
    public Vector3[] mapPositions; // 定義每個畫面對應的地圖座標
    public Vector3[][] mapDoors=new Vector3[][]
    {
        new Vector3[] { new Vector3((float)-196.16, (float)-50.51,2), new Vector3((float)-179.33,(float)-44.56,2), new Vector3((float)-165.8,(float)-38.57,2), new Vector3((float)-174.3,(float)-53.36,2)},
        new Vector3[] { new Vector3((float)-114.49, (float)-55.2,2), new Vector3((float)-113.25,(float)-46.08,2), new Vector3((float)-76.5,(float)-43.0,2), new Vector3((float)-91.8,(float)-45.9,2)},
        new Vector3[] { new Vector3((float)-216.7, (float)-90.91,2), new Vector3((float)-205.4,(float)-102.88,2), new Vector3((float)-185.7,(float)-85.01,2), new Vector3((float)-161.3,(float)-103.08,2)},
        new Vector3[] { new Vector3((float)-110.0, (float)-96.89,2), new Vector3((float)-86.28,(float)-87.88,2), new Vector3((float)-121.25,(float)-87.94,2), new Vector3((float)-97.1,(float)-93.95,2)},
        new Vector3[] { new Vector3((float)-162.72, (float)-147.89,2), new Vector3((float)-219.17,(float)-132.96,2), new Vector3((float)-203.28,(float)-141.95,2), new Vector3((float)-176.23,(float)-145.02,2)}
    }; //
    public Vector3[] mapflags;
    public GameObject Flags;
    public Vector3 resetPosition; // 錯誤時的重置位置（最前面地圖）

    private QuestionDisplay questionDisplay;
    private int currentNearDoorIndex = -1;
    public int currentMapIndex = 0; // 當前畫面的索引
    public static DoorManager Instance { get; private set; }

    void Start()
    {
        // 獲取 QuestionDisplay 腳本引用
        questionDisplay = QuestionDisplay.Instance;
        if (questionDisplay == null)
        {
            Debug.LogError("找不到 QuestionDisplay！");
            return;
        }

        // 初始化門和地圖
        SetupDoors();
        Instance = this;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 初始化單例
        }
        else
        {
            Destroy(gameObject); // 確保只有一個 DoorManager 存在
        }
    }

    void SetupDoors()
    {
        player.transform.position = new Vector3(mapPositions[currentMapIndex].x,mapPositions[currentMapIndex].y,0);
        Flags.transform.position =new Vector3(mapflags[currentMapIndex].x, mapflags[currentMapIndex].y, mapflags[currentMapIndex].z);
        doors[0].transform.position = new Vector3(mapDoors[currentMapIndex][0].x,mapDoors[currentMapIndex][0].y,2);
        doors[1].transform.position = new Vector3(mapDoors[currentMapIndex][1].x,mapDoors[currentMapIndex][1].y,2);
        doors[2].transform.position = new Vector3(mapDoors[currentMapIndex][2].x,mapDoors[currentMapIndex][2].y,2);
        doors[3].transform.position = new Vector3(mapDoors[currentMapIndex][3].x,mapDoors[currentMapIndex][3].y,2);
        if (doors == null || doors.Length == 0)
        {
            Debug.LogError("沒有設置門物件！");
            return;
        }

        // 為每個門設置觸發器
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i] == null)
            {
                Debug.LogError($"門 {i} 未設置！");
                continue;
            }

            BoxCollider2D boxCollider = doors[i].GetComponent<BoxCollider2D>();
            if (boxCollider == null)
            {
                boxCollider = doors[i].AddComponent<BoxCollider2D>();
            }
            boxCollider.isTrigger = true;

            DoorTrigger trigger = doors[i].GetComponent<DoorTrigger>();
            if (trigger == null)
            {
                trigger = doors[i].AddComponent<DoorTrigger>();
            }
            trigger.doorIndex = i;
            trigger.doorManager = this;
        }
    }

    void Update()
    {
        // 玩家進入門範圍並按下 E 鍵
        if (currentNearDoorIndex != -1 && Input.GetKeyDown(KeyCode.E))
        {
            CheckAnswer(currentNearDoorIndex);
        }
    }

    public void PlayerEnteredDoor(int doorIndex)
    {
        currentNearDoorIndex = doorIndex;
        Debug.Log($"玩家進入門 {doorIndex} 的範圍");
    }

    public void PlayerExitedDoor(int doorIndex)
    {
        if (currentNearDoorIndex == doorIndex)
        {
            currentNearDoorIndex = -1;
        }
        Debug.Log($"玩家離開門 {doorIndex} 的範圍");
    }

    void CheckAnswer(int selectedDoorIndex)
    {
        if (questionDisplay == null) return;

        int correctAnswer = questionDisplay.getA();

        if (selectedDoorIndex == correctAnswer)
        {
            Debug.Log("答案正確！傳送到下一畫面"+currentMapIndex);
            if (currentMapIndex + 1 < mapflags.Length)
            {
                Flags.transform.position =new Vector3(mapflags[currentMapIndex+1].x, mapflags[currentMapIndex+1].y, mapflags[currentMapIndex+1].z);
                doors[0].transform.position = new Vector3(mapDoors[currentMapIndex+1][0].x,mapDoors[currentMapIndex+1][0].y,2);
                doors[1].transform.position = new Vector3(mapDoors[currentMapIndex+1][1].x,mapDoors[currentMapIndex+1][1].y,2);
                doors[2].transform.position = new Vector3(mapDoors[currentMapIndex+1][2].x,mapDoors[currentMapIndex+1][2].y,2);
                doors[3].transform.position = new Vector3(mapDoors[currentMapIndex+1][3].x,mapDoors[currentMapIndex+1][3].y,2);
                questionDisplay.LoadRandomQuestion();
            }
            MoveToNextMap();
        }
        else
        {
            Debug.Log("答案錯誤！返回上一畫面或重置位置"+currentMapIndex);
            if(currentMapIndex>0) currentMapIndex-=1;
            Flags.transform.position =new Vector3(mapflags[currentMapIndex].x, mapflags[currentMapIndex].y, mapflags[currentMapIndex].z);
            doors[0].transform.position = new Vector3(mapDoors[currentMapIndex][0].x,mapDoors[currentMapIndex][0].y,2);
            doors[1].transform.position = new Vector3(mapDoors[currentMapIndex][1].x,mapDoors[currentMapIndex][1].y,2);
            doors[2].transform.position = new Vector3(mapDoors[currentMapIndex][2].x,mapDoors[currentMapIndex][2].y,2);
            doors[3].transform.position = new Vector3(mapDoors[currentMapIndex][3].x,mapDoors[currentMapIndex][3].y,2);
            questionDisplay.LoadRandomQuestion();
            currentMapIndex+=1;
            MoveToPreviousMapOrReset();
        }
    }



    void MoveToNextMap()
{
    if (currentMapIndex < mapPositions.Length - 1)
    {
        currentMapIndex++;
        UpdatePlayerPosition(mapPositions[currentMapIndex]);

        // 通知相機跳轉到新位置
        playerCamera cameraScript = Camera.main.GetComponent<playerCamera>();
        if (cameraScript != null)
        {
            cameraScript.JumpToTarget();
        }
        
    }
    else
    {
        Debug.Log("已是最後一個畫面！");
        currentMapIndex=5;
        UpdatePlayerPosition(mapPositions[currentMapIndex]);
        playerCamera cameraScript = Camera.main.GetComponent<playerCamera>();
        if (cameraScript != null)
        {
            cameraScript.JumpToTarget();
        }
    }
}

void MoveToPreviousMapOrReset()
{
    if (currentMapIndex > 0)
    {
        currentMapIndex--;
        UpdatePlayerPosition(mapPositions[currentMapIndex]);

        // 通知相機跳轉到新位置
        playerCamera cameraScript = Camera.main.GetComponent<playerCamera>();
        if (cameraScript != null)
        {
            cameraScript.JumpToTarget();
        }
    }
    else
    {
        UpdatePlayerPosition(resetPosition);

        // 通知相機跳轉到初始位置
        playerCamera cameraScript = Camera.main.GetComponent<playerCamera>();
        if (cameraScript != null)
        {
            cameraScript.JumpToTarget();
        }
    }
}


    void UpdatePlayerPosition(Vector3 newPosition)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = newPosition;
        }
    }

    
}

// 門的觸發器腳本
public class DoorTrigger : MonoBehaviour
{
    public int doorIndex;
    public DoorManager doorManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorManager.PlayerEnteredDoor(doorIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorManager.PlayerExitedDoor(doorIndex);
        }
    }
}
