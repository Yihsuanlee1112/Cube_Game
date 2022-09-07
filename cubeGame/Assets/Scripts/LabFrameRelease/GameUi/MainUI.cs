using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;
using lab317;

public class MainUI : MonoBehaviour
{
    public GameFlowData gameData = new GameFlowData();
    //public GameObject DropDown;
    public Button Start_Btn;
    public InputField Userid, Username;
    public Dropdown LanguageDropdown, LevelDropdown; // 0: 中文, 1: 英文
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

        LevelDropdown.onValueChanged.AddListener(delegate
        {
            Debug.Log("Level:" + LevelDropdown.value);

            if (LevelDropdown.value == 0) // LV1
            {
                gameData.Level = Level.Level1;
            }
            else if (LevelDropdown.value == 1) // LV2
            {
                gameData.Level = Level.Level2;
            }
            else if (LevelDropdown.value == 2) // Mono_LV1
            {
                gameData.Level = Level.Level3;
            }
            else if (LevelDropdown.value == 3) // Mono_LV2
            {
                gameData.Level = Level.Level4;
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
            //上傳startPage
            GameDataManager.LabDataManager.SendData(new StartPage(GameDataManager.FlowData.UserId));
            Debug.Log("Send");
            GameSceneManager.Instance.Change2MainScene();
        });
       
    }
}
