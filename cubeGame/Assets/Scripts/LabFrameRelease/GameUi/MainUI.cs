using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Button Start_Btn;
    public InputField Userid, Username;

    private void Start()
    {
        Start_Btn.onClick.AddListener(delegate
        {
            // GameFlowData也要宣告
            var gamedata = new GameFlowData()
            {
                UserId = Userid.text,
                UserName = Username.text
            };
            GameDataManager.FlowData = gamedata;
            GameDataManager.LabDataManager.LabDataCollectInit(() => GameDataManager.FlowData.UserId);
            GameSceneManager.Instance.Change2MainScene();
        });
    }
}
