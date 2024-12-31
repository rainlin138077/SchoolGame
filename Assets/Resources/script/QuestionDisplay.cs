using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class Question
{
    public string questionText;       // 題目
    public string[] options;          // 選項
    public int correctOption;         // 正確答案索引
}

public class QuestionDisplay : MonoBehaviour
{
    public TextMeshProUGUI questionText;    // 顯示題目
    public TextMeshProUGUI questionCount;   // 顯示題數
    public TextMeshProUGUI[] questionA;     // 顯示答案

    private List<Question> questions;       // 題目列表
    private List<int> shuffledIndices; // 打亂後的題目索引列表
    private int currentQuestionIndex = 0;   // 當前題目索引
    public static QuestionDisplay Instance;

    void Start()
    {
        LoadQuestionsFromResources();
        // 打亂題目索引
        shuffledIndices = Enumerable.Range(0, questions.Count).OrderBy(x => Random.value).ToList();
        DisplayQuestion();
        Instance = this;
    }

    void Update()
    {

    }

    void LoadQuestionsFromResources()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions");
        if (jsonFile != null)
        {
            string jsonContent = jsonFile.text;
            questions = JsonUtility.FromJson<QuestionList>("{\"questions\":" + jsonContent + "}").questions;
        }
        else
        {
            Debug.LogError("JSON file not found in Resources!");
        }
    }

    void DisplayQuestion()
    {
        if (questions == null || questions.Count == 0) return;

        // 檢查是否還有題目
        if (currentQuestionIndex >= shuffledIndices.Count)
        {
            // 顯示所有題目都已顯示完畢的訊息
            questionText.text = "所有題目都已顯示完畢！";
            questionCount.text = "";
            return;
        }

        // 获取当前题目的索引
        int index = shuffledIndices[currentQuestionIndex];

        Question currentQuestion = questions[index];

        // 顯示題目文字
        questionText.text = currentQuestion.questionText;

        // 顯示題數文字
        questionCount.text = $" {currentQuestionIndex + 1}";

        // 設置門上的文字
        for (int i = 0; i < questionA.Length; i++)
        {
            if (i < currentQuestion.options.Length)
            {
                questionA[i].text = $"{i+1+"."+currentQuestion.options[i]}";
            }
        }

        // 递增当前题目的索引
        currentQuestionIndex++;
    }

    public int getA()
    {
        // 获取当前题目的索引
        int index = shuffledIndices[currentQuestionIndex - 1];

        // 返回当前题目的答案
        return questions[index].correctOption;
    }

    public int getcurrentQuestionIndex()
    {
        return currentQuestionIndex;
    }

    public void LoadRandomQuestion()
    {
        // 隨機選擇題目索引
        currentQuestionIndex = Random.Range(0, shuffledIndices.Count);
        

        // 顯示新題目
        DisplayQuestion();

        Debug.Log($"隨機選擇了新題目，索引：{currentQuestionIndex}");
    }
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}
