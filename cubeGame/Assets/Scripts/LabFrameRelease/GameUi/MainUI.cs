using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameFlowData gameData = new GameFlowData();
    //public GameObject DropDown;
    public Button Start_Btn;
    public InputField Userid, Username;
    public Dropdown LanguageDropdown; // 0: 中文, 1: 英文
    private void Start()
    {
        LanguageDropdown.onValueChanged.AddListener(delegate
        {
            Debug.Log("language:"+LanguageDropdown.value);
            
            if (LanguageDropdown.value == 0) // 中文
            {
                gameData.Language = Language.中文;
            }
            else if (LanguageDropdown.value == 1) // 英文
            {
                gameData.Language = Language.English;
            }
        });
        Start_Btn.onClick.AddListener(delegate
        {
            Debug.Log("start");
            // GameFlowData也要宣告
            //var gamedata = new GameFlowData()
            //{
            gameData.UserId = Userid.text;
            gameData.UserName = Username.text;
            //gameData.Language = Language.中文;
            //};
            GameDataManager.FlowData = gameData;
            GameDataManager.LabDataManager.LabDataCollectInit(() => GameDataManager.FlowData.UserId);
            GameSceneManager.Instance.Change2MainScene();
        });
       
    }
}
