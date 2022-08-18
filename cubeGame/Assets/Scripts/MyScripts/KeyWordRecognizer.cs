using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyWordRecognizer : MonoBehaviour
{
    private string UserColor;
    public static string MissingColor;
    public Text ShowTextOnUI;
    public Text ShowAllTextOnUI;

    //private KeywordRecognizer keywordRecognizer;
    //private string[] Keywords_array = new string[] { "小花", "你也丟得很好", "接的好", "我有點生氣", "我很生氣", "好"};

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("啟動辨識");
        //keywordRecognizer = new KeywordRecognizer(Keywords_array);
        //keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        //keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getAllResult(string result)
    {
        ShowAllTextOnUI.text = result;
        Debug.Log("bbbb");
    }
    public void GetResult(string result)
    {
        ShowTextOnUI.text = result;
        Debug.Log("Keyword: " + result);
        if (result.Contains("紅色") || result.Contains("藍色")|| result.Contains("綠色")|| result.Contains("黃色"))
        {
            BlockGameTaskLv2._userChooseColor = true;
            switch (result)
            {
                case "紅色":
                    BlockGameTaskLv2._userChooseColor = true;
                    UserColor = "紅色";
                    MissingColor = "紅色";
                    break;
                case "藍色":
                    BlockGameTaskLv2._userChooseColor = true;
                    UserColor = "藍色";
                    MissingColor = "藍色";
                    break;
                case "綠色":
                    BlockGameTaskLv2._userChooseColor = true;
                    UserColor = "綠色";
                    MissingColor = "綠色";
                    break;
                case "黃色":
                    BlockGameTaskLv2._userChooseColor = true;
                    UserColor = "黃色";
                    MissingColor = "黃色";
                    break;
            }
            GameDataManager.FlowData.UserColor = UserColor;
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor + " Result: " + UserColor);
        }
        else if (result.Contains("老師"))
        {
            BlockGameTask._userSpeekToTeacher = true;
            BlockGameTaskLv2._userSpeekToTeacher = true;
            Debug.Log("BlockGameTask._userSpeekToTeacher: " + BlockGameTask._userSpeekToTeacher);
            Debug.Log("BlockGameTaskLv2._userSpeekToTeacher: " + BlockGameTaskLv2._userSpeekToTeacher);
        }
        else if (result.Contains("少") && result.Contains("積木") &&
            result.Contains("紅色") || result.Contains("藍色") || result.Contains("綠色") || result.Contains("黃色"))
        {
            BlockGameTask._userSpeekToTeacher = true;
            BlockGameTaskLv2._userSpeekToTeacher = true;
            Debug.Log("BlockGameTask._userSpeekToTeacher: " + BlockGameTask._userSpeekToTeacher);
            Debug.Log("BlockGameTaskLv2._userSpeekToTeacher: " + BlockGameTaskLv2._userSpeekToTeacher);
        }
        //else if (result.Contains("接到球") && result.Contains("好棒"))
        //{
        //    BlockGameTask._isUserPraisePenguin = true;
        //}
        //else if (result.Contains("亂丟") && result.Contains("生氣"))
        //{
        //    BlockGameTask._isUserExpressAngry = true;
        //}
        //else if (result.Contains("手") && result.Contains("伸") && result.Contains("說好"))
        //{
        //    BlockGameTask._isAskPeguineReady = true;
        //}
        //else if (result.Contains("好") || result.Contains("豪"))
        //{
        //    BlockGameTask._isUserSayYes = true;
        //}
    }
}
