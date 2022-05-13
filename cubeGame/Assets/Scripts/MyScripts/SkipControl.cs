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
        
        if (Input.GetKeyDown(KeyCode.B))//blue
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "藍色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        
        if (Input.GetKeyDown(KeyCode.G))//green
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "綠色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        
        if (Input.GetKeyDown(KeyCode.Y))//yellow
        {
            BlockGameTaskLv2._userChooseColor = true;
            GameDataManager.FlowData.UserColor = "黃色";
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))//chooseRPS
        {
            BlockGameTaskLv2._userChooseRPS = true;
            BlockGameTask._userChooseRPS = true;
            Debug.Log("BlockGameTask._userChooseRPS: " + BlockGameTask._userChooseRPS);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))//choosePic1
        {
            BlockGameTaskLv2._RandomQuestion = 1;
            BlockGameTask._RandomQuestion = 1;
            BlockGameTaskLv2._userChooseQuestion = true;
            BlockGameTask._userChooseQuestion = true;
            Debug.Log("BlockGameTask._RandomQuestion: " + BlockGameTask._RandomQuestion);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))//choosePic2
        {
            BlockGameTaskLv2._RandomQuestion = 2;
            BlockGameTask._RandomQuestion = 2;
            BlockGameTaskLv2._userChooseQuestion = true;
            BlockGameTask._userChooseQuestion = true;
            Debug.Log("BlockGameTask._RandomQuestion: " + BlockGameTask._RandomQuestion);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))//choosePic3
        {
            BlockGameTaskLv2._RandomQuestion = 3;
            BlockGameTask._RandomQuestion = 3;
            BlockGameTaskLv2._userChooseQuestion = true;
            BlockGameTask._userChooseQuestion = true;
            Debug.Log("BlockGameTask._RandomQuestion: " + BlockGameTask._RandomQuestion);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))//choosePic4
        {
            BlockGameTaskLv2._RandomQuestion = 4;
            BlockGameTask._RandomQuestion = 4;
            BlockGameTaskLv2._userChooseQuestion = true;
            BlockGameTask._userChooseQuestion = true;
            Debug.Log("BlockGameTask._RandomQuestion: " + BlockGameTask._RandomQuestion);
        }
    }
}
