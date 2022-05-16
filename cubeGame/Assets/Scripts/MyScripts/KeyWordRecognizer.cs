using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWordRecognizer : MonoBehaviour
{
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

    public void GetResult(string result)
    {
        Debug.Log("Keyword: " + result);
        if (result.Contains("紅色") || result.Contains("藍色")|| result.Contains("綠色")|| result.Contains("黃色"))
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = result;
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor + " Result: " + result);
        }
        else if (result.Contains("老師") && result.Contains("少") && result.Contains("積木"))
        {
            BlockGameTask._userSpeekToTeacher = true;
            BlockGameTaskLv2._userSpeekToTeacher = true;
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
