using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //choose color
        if (Input.GetKeyDown(KeyCode.R))//red
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "紅色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        // 選擇生氣
        if (Input.GetKeyDown(KeyCode.B))//blue
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "藍色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        // 選擇開心
        if (Input.GetKeyDown(KeyCode.G))//green
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "綠色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        // user表達生氣
        if (Input.GetKeyDown(KeyCode.Y))//yellow
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "黃色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
    }
}
