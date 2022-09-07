using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;
using GameData;
using LabData;

public class BlockGameTaskLv2_Mono : TaskBase
{
    private GameObject VRCamera;
    private EyeCameraEntity eyecamera;                  // 專門看眼動泡泡的camera
    public static GameObject eyebubble { get; set; }
    private PlayerEntity player;
    private GameObject userLeftHandTrigger;
    private GameObject userRightHandTrigger;
    private List<GameObject> Question_Cube;
    private NPCEntity npc;
    private Animator HostAnimator, TeacherAnimator;
    private Animator XiaoHua, XiaoMei, Green, Yoyo, Red, Hat;
    private GameObject GreenTriggerBall;
    public static GameObject RedTriggerBall;
    private List<BlockEntity> Q1_cube;
    private List<BlockEntity> Q2_cube;
    private List<BlockEntity> Q3_cube;
    private List<BlockEntity> Q4_cube;
    private List<BlockEntity> AllCubes;
    private List<BlockEntity> Cubes;
    private List<BlockEntity> Final_Order;
    //private List<GameObject> Lv2_Order;
    private List<BlockEntity> cube_GA;
    private List<BlockEntity> cube_GB;
    private List<BlockEntity> cube_GC;
    private List<Color> Colors;
    private Color UserChoice1;
    private Color UserChoice2;

    private GameObject Coin;
    private GameObject Ruby;
    private GameObject Heart;

    private List<int> textOnQuestion;
    private List<GameObject> QuestionOrder;
    //private List<GameObject> objectlist;
    private AudioClip clip, XiaoMeiColor, colorClip, colorClip2;
    private string MissingCube;
    private int RanNum;
    private string focusName;
    private string XiaoMeiMissingCube;
    public static GameObject KidShouldPut;
    public static int RecentOrder = 1;
    public static int RemindCelebrate = 0;
    public static bool _RoundA = true;
    public static int _ShowResult = 0;
    public static int _RandomQuestion = 0;
    public static int RemindRaiseHand = 0;
    public static bool _StartTobuild = false;
    public static bool _userChooseRPS = false;
    public static bool _userAskTeacher = false;
    public static bool _userChooseColor = false;
    public static bool _userChooseQuestion = false;
    public static bool _userSpeekToTeacher = false;
    public static bool _userRaiseHand = false;
    public static bool _userCelebrate = false;
    public static bool _playerRound = false;
    public bool _BlockFinished = false;
    public static bool _usersayhello = false;
    public static bool _talking = false;
    public static bool _npcremind = false;
    public static bool _eyetimerfinish = false;
    public static bool _waitforwatch = false;
    public static int answerindex = 0;
    public int TalkScore = 0;//recognizerEntity
    public static bool _is5secTimeUp = false;
    public static bool _is2secTimeUp = false;
    public static bool _isEyeFinish = false;
    private string audioClipRootPath;
    public override IEnumerator TaskInit()
    {
        GameEventCenter.AddEvent("CheckCube", CheckCube);
        GameEventCenter.AddEvent<BlockEntity>("CubeToAns", CubeToAns);
        GameEventCenter.AddEvent<BlockEntity>("CubeOnAns", CubeOnAns);
        GameEventCenter.AddEvent<string>("GetFocusName", GetFocusName);
        GameEventCenter.AddEvent<BlockEntity>("OtherGroupCubeAnsLv2_Mono", OtherGroupCubeAnsLv2_Mono);
        GameEventCenter.AddEvent("AddCubesToList", AddCubesToList);
        GameEventCenter.AddEvent("FindQ1Cubes", FindQ1Cubes);
        GameEventCenter.AddEvent("FindQ2Cubes", FindQ2Cubes);
        GameEventCenter.AddEvent("FindQ3Cubes", FindQ3Cubes);
        GameEventCenter.AddEvent("FindQ4Cubes", FindQ4Cubes);
        GameEventCenter.AddEvent("Find_QuestionCubes", Find_QuestionCubes);
        //GameEventCenter.AddEvent("User_MissingOneCubeLv2", User_MissingOneCubeLv2);
        GameEventCenter.AddEvent("RandomNumOnQuestion", RandomNumOnQuestion);
        GameEventCenter.AddEvent("KidsShouldPut", KidsShouldPut);
        GameEventCenter.AddEvent("PutInRightOrder", PutInRightOrder);
        //GameEventCenter.AddEvent("NPC_Remind", NPC_Remind);
        //GameEventCenter.AddEvent("NPC_Remind2", NPC_Remind2);
        //載入MainSceneRes
        userLeftHandTrigger = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().userLeftHandTrigger;
        userRightHandTrigger = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().userRightHandTrigger;
        player = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().player;
        npc = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().npc;
        GreenTriggerBall = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().GreenTriggerBall;
        RedTriggerBall = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().RedTriggerBall;
        Question_Cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Question_Cube;
        Q1_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q1_cube;
        Q2_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q2_cube;
        Q3_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q3_cube;
        Q4_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q4_cube;
        Cubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes;
        Final_Order = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Final_Order;
        cube_GA = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GA;
        cube_GB = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GB;
        cube_GC = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GC;
        AllCubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().AllCubes;
        Colors = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Colors;
        textOnQuestion = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().textOnQuestion;
        //objectlist = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ObjectList;
        npc.npc_hand = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC_Hand;
        npc.ChineseSpeechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ChineseSpeechClip;
        npc.EnglishSpeechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().EnglishSpeechClip;
        npc.animator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC_animator;
        HostAnimator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().HostAnimator;
        TeacherAnimator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().TeacherAnimator;
        QuestionOrder = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().QuestionOrder;
        //mainSceneUI = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().MainSceneUI;
        XiaoHua = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoHua;
        XiaoMei = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoMei;
        Green = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Green;
        Yoyo = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Yoyo;
        Red = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Red;
        Hat = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Hat;
        Coin = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Coin;
        Ruby = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Ruby;
        Heart = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Heart;
        VRCamera = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().VRCamera;
        //VRIK初始化
        player.GetComponent<PlayerEntity>().Init(VRCamera);

        //NPC初始化
        npc.EntityInit();

        //設定VR中可看到UI
        //mainSceneUI.worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //mainSceneUI.planeDistance = 1;

        //啟動眼球追蹤
        //LabVisualization.VisualizationManager.Instance.VisulizationInit();
        //LabVisualization.VisualizationManager.Instance.StartDataVisualization();

        // 宣告泡泡為全域 在EyeTrackEquipment.cs設定泡泡的localPosition
        eyebubble = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().eyeBubble;

        // 只看眼動泡泡的camera宣告和初始化
        eyecamera = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().eyeCamera;
        eyecamera.Init();

        //answerindex = 0;   //初始化

        //語音Recognizer 初始化
        //recognizerEntity.Init();

        // 開啟遊戲計時器
        GameEventCenter.DispatchEvent("GameTimerText_isEnabled", true);
        //語言選擇
        if (GameDataManager.FlowData.Language == Language.中文)
        {
            audioClipRootPath = "AudioClip/Chinese/";
        }
        else
        {
            audioClipRootPath = "AudioClip/English/";
        }
        yield return null;
    }

    public override IEnumerator TaskStart()
    {
        GameObject.Find("-- MainSceneUI --/UserCanSee").SetActive(false);
        GameObject ChooseQuestionCanvas = GameObject.Find("ChooseQuestionCanvas");
        //GameObject XiaoMeiRaiseHandPic = GameObject.Find("Schematics/UserRightSightCanvas/XiaoMeiRaiseHandPic");
        GameObject Question1PicWithNum = GameObject.Find("Schematics/UserLeftSightCanvas/QuestionPicsWithNum/Question1PicWithNum");
        GameObject Question1Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question1Pic");
        GameObject Question2Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question2Pic");
        GameObject Question3Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question3Pic");
        GameObject Question4Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question4Pic");
        ChooseQuestionCanvas.SetActive(true);
        //XiaoMeiRaiseHandPic.SetActive(true);
        //語言選擇
        if (GameDataManager.FlowData.Language == Language.中文)
        {
            audioClipRootPath = "AudioClip/Chinese/";
        }
        else
        {
            audioClipRootPath = "AudioClip/English/";
        }

        HostAnimator.SetBool("isSlouchStandErect", true);

        //GameObject.Find("ChooseQuestionCanvas").GetComponent<Canvas>().enabled = false;
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        for (int i = 1; i < 5; i++)
        {
            GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = false;
        }
        yield return Teacher_Introduction();
        yield return new WaitForSeconds(1f);
        GameEventCenter.DispatchEvent("FourPlayerRPS");//猜拳動畫
        GameObject.Find("FourPlayerChoose(Clone)/Canvas").SetActive(true);
        GameObject.Find("FourPlayerChoose(Clone)/Canvas2").SetActive(false);
        //大家一起說剪刀石頭布
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        yield return All_NPC_SayRPS();
        _userChooseRPS = false;
        do
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        } while (!_userChooseRPS);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("Result").SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }

        //XXX那一組猜拳贏了，你可以先選一張圖。你要選哪一張圖?
        yield return Teacher_AskUserWhichPic();
        yield return new WaitForSeconds(1.5f);

        //讓user選題目
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        yield return UserChooseQuestion(ChooseQuestionCanvas);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        //其他沒有贏的組，老師一組發一張圖案
        TeacherAnimator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveQuestionToOtherGroups");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        TeacherAnimator.SetBool("isTalk", false);
        yield return new WaitForSeconds(1);
        //小朋友，你們可以分配顏色，按照題目上的數字順序輪流完成作品
        yield return Teacher_RemindLv2();
        yield return new WaitForSeconds(2);

        //顯示user選的題目在右邊
        if (_RandomQuestion == 1)
        {
            Question1Pic.GetComponent<RawImage>().enabled = true;
        }
        else if (_RandomQuestion == 2)
        {
            Question2Pic.GetComponent<RawImage>().enabled = true;
        }
        else if (_RandomQuestion == 3)
        {
            Question3Pic.GetComponent<RawImage>().enabled = true;
        }
        else if (_RandomQuestion == 4)
        {
            Question4Pic.GetComponent<RawImage>().enabled = true;
        }

        //NPC說 總共有四種顏色耶，你可以選兩種顏色。第一個顏色，你想要什麼呢?
        yield return NPC_AskUserFirstColor();
        yield return new WaitForSeconds(1);
        //Recognizer*************************************************************
        yield return NPC_SameColor1();
        yield return new WaitForSeconds(1);
        //user win
        _userChooseRPS = false;
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        GameEventCenter.DispatchEvent("TwoPlayerRPS");
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas").SetActive(true);
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas2").SetActive(false);
        //NPC說剪刀石頭布
        npc.animator.SetBool("isPlayingRPS", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isPlayingRPS", false);

        do
        {
            Debug.Log("User choose RPS");
            Debug.Log(_userChooseRPS);
            yield return new WaitUntil(() => _userChooseRPS);
            Debug.Log(_userChooseRPS);
        } while (!_userChooseRPS);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);

        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(1);

        //小星說: 喔不!我輸了，你可以拿XX
        yield return NPC_YouWinLv2();
        yield return new WaitForSeconds(1);
        yield return NPC_AskUserSecondColor();
        yield return new WaitForSeconds(1);
        yield return NPC_IfUserChoseSameColor();
        yield return new WaitForSeconds(1);
        yield return UserFinalColor();

        GameObject.Find("QuestionPics").SetActive(false);
        //框架
        //User choose two colors
        //yield return UserChooseColor();//**************************

        //出現題目跟積木
        GameEventCenter.DispatchEvent("InstatiateCubeLv2");
        GameEventCenter.DispatchEvent("CubeOnDesk");
        GameEventCenter.DispatchEvent("AddCubesToList");
        foreach (BlockEntity cube in AllCubes)
        {
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            //cube.GetComponent<MeshRenderer>().enabled = false;
            cube.GetComponent<BoxCollider>().isTrigger = true;

        }
        for (int i = 0; i < 10; i++)
        {
            GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)").transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = null;
        }
        //GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");

        GameEventCenter.DispatchEvent("Find_QuestionCubes");
        //題目出現數字順序
        GameEventCenter.DispatchEvent("RandomNumOnQuestion");
        GameEventCenter.DispatchEvent("CheckOrder");//QuestionCube
        //GameEventCenter.DispatchEvent("AddCubesToList");
        GameEventCenter.DispatchEvent("PutInRightOrder");

        GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        GameEventCenter.DispatchEvent("InstantiateQuestionLv2");

        //GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");
        foreach (BlockEntity cube in AllCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = true;
            //cube.GetComponent<BoxCollider>().isTrigger = false;
            //cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    public override IEnumerator TaskStop()
    {
        // 關閉遊戲計時器
        GameEventCenter.DispatchEvent("GameTimerText_isEnabled", false);

        // 遊戲正常結束才會儲存數據
        GameEventCenter.DispatchEvent("StoreEndData");

        GameApplication.Instance.GameApplicationDispose(GameApplication.DisposeOptions.Back2Ui);
        yield return null;
    }
    IEnumerator Teacher_Introduction()
    {
        TeacherAnimator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_Introduction");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath + "Teacher_Introduction" + clip.length);
        Debug.Log("老師說完遊戲規則");
        TeacherAnimator.SetBool("isTalk", false);
        yield return null;
    }
    IEnumerator Teacher_RemindLv2()
    {
        TeacherAnimator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_LV2Remind");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath + "Teacher_LV2Remind" + clip.length);
        Debug.Log("老師提醒規則");
        TeacherAnimator.SetBool("isTalk", false);
        yield return null;
    }
    IEnumerator Teacher_AskUserWhichPic()
    {
        TeacherAnimator.SetBool("isPoint", true);
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserName, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_AskUserWhichPic");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath + "Teacher_AskUserWhichPic" + clip.length);
        Debug.Log("老師問完選圖");
        TeacherAnimator.SetBool("isPoint", false);
        yield return null;
    }
    IEnumerator UserChooseQuestion(GameObject ChooseQuestionCanvas)
    {
        //ChooseQuestionCanvas.SetActive(true);
        ChooseQuestionCanvas.GetComponent<Canvas>().enabled = true;
        for (int i = 1; i < 5; i++)
        {
            GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = true;
        }

        while (!_userChooseQuestion)
        {
            Debug.Log("User choose Question");
            yield return new WaitUntil(() => _userChooseQuestion);
        }
        yield return new WaitForSeconds(3);
        ChooseQuestionCanvas.SetActive(false);
        yield return null;
    }
    IEnumerator NPC_AskUserFirstColor()
    {
        _userChooseColor = false;
        //yield return new WaitForSeconds(2);
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_AskUserFirstColor");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
        do
        {
            Debug.Log("User choose first color");
            Debug.Log(_userChooseColor);
            yield return new WaitUntil(() => _userChooseColor);
            Debug.Log(_userChooseColor);
        } while (!_userChooseColor);
        yield return null;
    }
    IEnumerator NPC_AskUserSecondColor()
    {
        _userChooseColor = false;
        GameDataManager.FlowData.UserColor = " ";
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_AskUserSecondColor");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
        do
        {
            Debug.Log("User choose first color");
            Debug.Log(_userChooseColor);
            yield return new WaitUntil(() => _userChooseColor);
            Debug.Log(_userChooseColor);
        } while (!_userChooseColor);
        yield return null;
    }
    IEnumerator NPC_SameColor1()
    {
        Debug.Log(GameDataManager.FlowData.UserColor);
        if (GameDataManager.FlowData.UserColor == "紅色")
        {
            UserChoice1 = Colors[0];//red
            colorClip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Red");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        else if (GameDataManager.FlowData.UserColor == "藍色")
        {
            UserChoice1 = Colors[1];//blue
            colorClip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Blue");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        else if (GameDataManager.FlowData.UserColor == "綠色")
        {
            UserChoice1 = Colors[2];//green
            colorClip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Green");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        else if (GameDataManager.FlowData.UserColor == "黃色")
        {
            UserChoice1 = Colors[3];//yellow
            colorClip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Yellow");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        npc.animator.SetBool("isSad", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SameColor1");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(colorClip);
        yield return new WaitForSeconds(colorClip.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_WinnerCanHaveColor1");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(colorClip);
        yield return new WaitForSeconds(colorClip.length);
        npc.animator.SetBool("isSad", false);
        GameDataManager.FlowData.UserFirstColor = GameDataManager.FlowData.UserColor;
        Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        Debug.Log("UserColor1: " + GameDataManager.FlowData.UserFirstColor);
        yield return null;
    }
    IEnumerator NPC_YouWinLv2()
    {
        Debug.Log(GameDataManager.FlowData.UserColor);
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouWinLv2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(colorClip);
        yield return new WaitForSeconds(colorClip.length);
        npc.animator.SetBool("isTalk", false);
        yield return new WaitForSeconds(2);
        yield return null;
    }
    IEnumerator NPC_IfUserChoseSameColor()
    {
        if (GameDataManager.FlowData.UserColor == GameDataManager.FlowData.UserFirstColor)
        {
            if (GameDataManager.FlowData.UserFirstColor == "紅色")
            {
                GameDataManager.FlowData.UserColor = "藍色";
                Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
            }
            else if (GameDataManager.FlowData.UserFirstColor == "藍色")
            {
                GameDataManager.FlowData.UserColor = "綠色";
                Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
            }
            else if (GameDataManager.FlowData.UserFirstColor == "綠色")
            {
                GameDataManager.FlowData.UserColor = "黃色";
                Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
            }
            else if (GameDataManager.FlowData.UserFirstColor == "黃色")
            {
                GameDataManager.FlowData.UserColor = "紅色";
                Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
            }
            npc.animator.SetBool("isSad2", true);
            //可是你剛剛已經選了這個顏色，這樣子我拚太多了!我們要分工合作，一人兩個顏色!
            clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouChoseTheSameColor");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            npc.animator.SetBool("isSad2", false);
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return NPC_SameColor2();
            //user lose
            _userChooseRPS = false;
            userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
            userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

            GameEventCenter.DispatchEvent("TwoPlayerRPS");
            GameObject.Find("TwoPlayerChoose(Clone)/Canvas").SetActive(false);
            GameObject.Find("TwoPlayerChoose(Clone)/Canvas2").SetActive(true);
            //NPC說剪刀石頭布
            npc.animator.SetBool("isPlayingRPS", true);
            clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            npc.animator.SetBool("isPlayingRPS", false);

            do
            {
                Debug.Log("User choose RPS");
                Debug.Log(_userChooseRPS);
                yield return new WaitUntil(() => _userChooseRPS);
                Debug.Log(_userChooseRPS);
            } while (!_userChooseRPS);
            userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
            userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(2);

            GameObject.FindWithTag("Result").SetActive(false);
            for (int i = 0; i < 2; i++)
            {
                GameObject.FindWithTag("RPS").SetActive(false);
            }
            yield return new WaitForSeconds(2);
            //小星說: 我贏了! XX是我的積木
            yield return NPC_YouLoseLv2();
        }
    }
    IEnumerator NPC_SameColor2()
    {
        Debug.Log(GameDataManager.FlowData.UserColor);
        if (GameDataManager.FlowData.UserColor == "紅色")
        {
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Red");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        else if (GameDataManager.FlowData.UserColor == "藍色")
        {
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Blue");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        else if (GameDataManager.FlowData.UserColor == "綠色")
        {
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Green");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        else if (GameDataManager.FlowData.UserColor == "黃色")
        {
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Yellow");
            Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        }
        npc.animator.SetBool("isSad2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SameColor2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(colorClip2);
        yield return new WaitForSeconds(colorClip2.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_WinnerCanHaveColor2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(colorClip2);
        yield return new WaitForSeconds(colorClip2.length);
        npc.animator.SetBool("isSad2", false);
        yield return new WaitForSeconds(2);
        Debug.Log("UserColor1: " + GameDataManager.FlowData.UserFirstColor);
        Debug.Log("GameDataManager.FlowData.UserColor: " + GameDataManager.FlowData.UserColor);
        yield return null;
    }
    IEnumerator NPC_YouLoseLv2()
    {
        ////決定usercolor2
        ////int RanColor = Random.Range(0, 3);//在0到2之間隨機取值
        ////UserChoice2 = Colors[RanColor];

        npc.animator.SetBool("isExciting", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_UserLoseLv2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(colorClip2);
        yield return new WaitForSeconds(colorClip2.length);
        //yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_MyColor");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isExciting", false);
        yield return new WaitForSeconds(2);
        yield return null;
    }
    IEnumerator UserFinalColor()
    {
        Color red, blue, green, yellow;
        red = new Color(1, (float)0.23, (float)0.23, 1);
        blue = new Color((float)0.203, (float)0.203, (float)0.87, 1);
        green = new Color(0, (float)0.706, 0, 1);
        yellow = new Color(1, 1, 0, 1);

        //決定usercolor2
        if (GameDataManager.FlowData.UserColor == "紅色" && GameDataManager.FlowData.UserFirstColor == "藍色" || GameDataManager.FlowData.UserColor == "藍色" && GameDataManager.FlowData.UserFirstColor == "紅色")
        {
            UserChoice2 = yellow;
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Yellow");
            GameDataManager.FlowData.UserColor = "黃色";
            Debug.Log("UserChoice2: " + UserChoice2);
            Debug.Log("GameDataManager.FlowData.UserColor(UserChoice2): " + GameDataManager.FlowData.UserColor);
            Debug.Log("GameDataManager.FlowData.UserFirstColor(UserChoice1): " + GameDataManager.FlowData.UserFirstColor);
        }
        else if (GameDataManager.FlowData.UserColor == "紅色" && GameDataManager.FlowData.UserFirstColor == "黃色" || GameDataManager.FlowData.UserColor == "黃色" && GameDataManager.FlowData.UserFirstColor == "紅色")
        {
            UserChoice2 = green;
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Green");
            GameDataManager.FlowData.UserColor = "綠色";
            Debug.Log("UserChoice2: " + UserChoice2);
            Debug.Log("GameDataManager.FlowData.UserColor(UserChoice2): " + GameDataManager.FlowData.UserColor);
            Debug.Log("GameDataManager.FlowData.UserFirstColor(UserChoice1): " + GameDataManager.FlowData.UserFirstColor);
        }
        else if (GameDataManager.FlowData.UserColor == "紅色" && GameDataManager.FlowData.UserFirstColor == "綠色" || GameDataManager.FlowData.UserColor == "綠色" && GameDataManager.FlowData.UserFirstColor == "紅色")
        {
            UserChoice2 = yellow;
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Yellow");
            GameDataManager.FlowData.UserColor = "黃色";
            Debug.Log("UserChoice2: " + UserChoice2);
            Debug.Log("GameDataManager.FlowData.UserColor(UserChoice2): " + GameDataManager.FlowData.UserColor);
            Debug.Log("GameDataManager.FlowData.UserFirstColor(UserChoice1): " + GameDataManager.FlowData.UserFirstColor);
        }
        else if (GameDataManager.FlowData.UserColor == "黃色" && GameDataManager.FlowData.UserFirstColor == "藍色" || GameDataManager.FlowData.UserColor == "藍色" || GameDataManager.FlowData.UserFirstColor == "黃色")
        {
            UserChoice2 = red;
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Red");
            GameDataManager.FlowData.UserColor = "紅色";
            Debug.Log("UserChoice2: " + UserChoice2);
            Debug.Log("GameDataManager.FlowData.UserColor(UserChoice2): " + GameDataManager.FlowData.UserColor);
            Debug.Log("GameDataManager.FlowData.UserFirstColor(UserChoice1): " + GameDataManager.FlowData.UserFirstColor);
        }
        else if (GameDataManager.FlowData.UserColor == "黃色" && GameDataManager.FlowData.UserFirstColor == "綠色" || GameDataManager.FlowData.UserColor == "綠色" || GameDataManager.FlowData.UserFirstColor == "黃色")
        {
            UserChoice2 = red;
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Red");
            GameDataManager.FlowData.UserColor = "紅色";
            Debug.Log("UserChoice2: " + UserChoice2);
            Debug.Log("GameDataManager.FlowData.UserColor(UserChoice2): " + GameDataManager.FlowData.UserColor);
            Debug.Log("GameDataManager.FlowData.UserFirstColor(UserChoice1): " + GameDataManager.FlowData.UserFirstColor);
        }
        else if (GameDataManager.FlowData.UserColor == "綠色" && GameDataManager.FlowData.UserFirstColor == "藍色" || GameDataManager.FlowData.UserColor == "藍色" || GameDataManager.FlowData.UserFirstColor == "綠色")
        {
            UserChoice2 = red;
            colorClip2 = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Red");
            GameDataManager.FlowData.UserColor = "紅色";
            Debug.Log("UserChoice2: " + UserChoice2);
            Debug.Log("GameDataManager.FlowData.UserColor(UserChoice2): " + GameDataManager.FlowData.UserColor);
            Debug.Log("GameDataManager.FlowData.UserFirstColor(UserChoice1): " + GameDataManager.FlowData.UserFirstColor);
        }

        //XX跟XX是你的顏色喔，不要拿錯囉!
        npc.animator.SetBool("isTalk", true);
        GameAudioController.Instance.PlayOneShot(colorClip);
        yield return new WaitForSeconds(colorClip.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_And");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);

        GameAudioController.Instance.PlayOneShot(colorClip2);
        yield return new WaitForSeconds(colorClip2.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_DontTakeThemWrong");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
        yield return null;
    }
    IEnumerator All_NPC_SayRPS()
    {
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        //Debug.Log(clip.length);
        XiaoMei.SetBool("isPlayingRPS", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        //Debug.Log(clip.length);

        Hat.SetBool("isPlayingRPS", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Red_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        //Debug.Log(clip.length);
        Yoyo.SetBool("isPlayingRPS", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Green_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        //Debug.Log(clip.length);
        yield return new WaitForSeconds(clip.length);
        Hat.SetBool("isPlayingRPS", false);
        XiaoMei.SetBool("isPlayingRPS", false);
        Yoyo.SetBool("isPlayingRPS", false);
        yield return new WaitForSeconds(1);
        yield return null;
    }
    IEnumerator UserChooseQuestion()
    {
        GameObject.Find("ChooseQuestionCanvas").GetComponent<Canvas>().enabled = true;
        for (int i = 1; i < 5; i++)
        {
            GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = true;
        }

        while (!_userChooseQuestion)
        {
            Debug.Log("User choose Question");
            yield return new WaitUntil(() => _userChooseQuestion);
        }
        yield return new WaitForSeconds(2);
        GameObject.Find("ChooseQuestionCanvas").SetActive(false);
        yield return null;
    }
    public void CheckCube()
    {
        foreach (BlockEntity item in Final_Order)
        {
            if (!item._isChose)
            {
                _BlockFinished = false;
                break;
            }
            else
            {
                _BlockFinished = true;
            }
        }
    }
    public void RandomNumOnQuestion()
    {
        int[] randomArray = new int[10];
        GameObject TextOnQuestion = GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)");
        for (int i = 0; i < randomArray.Length;)
        {
            bool x = true;

            int RanNum = Random.Range(1, 11);//在1到10之間隨機取值
            for (int j = 0; j < i; ++j)
            {
                if (RanNum == randomArray[j])
                {
                    x = false;
                    break;
                }
            }
            if (x)
            {
                randomArray[i] = RanNum;
                i++;
            }
        }

        for (int i = 0; i < randomArray.Length; i++)//randomArray.Length = 10
        {
            GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)").transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = randomArray[i].ToString();
            textOnQuestion.Add(int.Parse(GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)").transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text));
            //textOnQuestion.Sort();
            QuestionOrder.Add(GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)").transform.GetChild(i).gameObject);

            QuestionOrder.Sort((x, y) => {
                return int.Parse(x.transform.GetChild(0).GetChild(0).GetComponent<Text>().text).CompareTo(int.Parse(y.transform.GetChild(0).GetChild(0).GetComponent<Text>().text));
            });
        }
    }
    public void PutInRightOrder()
    {
        string cubeName;
        foreach (GameObject questionOrder in QuestionOrder)
        {
            for (int i = 0; i < 10; i++)
            {
                BlockEntity cube = GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Q" + _RandomQuestion + "_CubeParent").transform.GetChild(i).GetComponent<BlockEntity>();
                //Get Cube name without "(Clone)"
                cubeName = cube.name;
                int delStr = cubeName.IndexOf("(Clone)");
                if (delStr >= 0)
                {
                    cubeName = cubeName.Remove(delStr);
                }

                if (questionOrder.name == cubeName)
                {
                    Final_Order.Add(cube);
                    Debug.Log("questionOrder.name: " + questionOrder.name);
                    Debug.Log("cube.name: " + cube.name);
                }
            }
        }
        foreach (BlockEntity cube in Final_Order)
        {
            cube._isOnUserTable = true;
            if (UserChoice1 == cube.GetComponent<MeshRenderer>().material.color || UserChoice2 == cube.GetComponent<MeshRenderer>().material.color)
            {
                Debug.Log(UserChoice1 + "UserChoice1!!!!!!!!!");
                Debug.Log(UserChoice2 + " + UserChoice2!!!!!!!!!!");
                Debug.Log(cube.GetComponent<MeshRenderer>().material.color);
                cube._isUserColor = true;
            }
        }
    }
    public void AddCubesToList()
    {
        if (_RandomQuestion == 1)
        {
            GameEventCenter.DispatchEvent("FindQ1Cubes");
            Cubes.AddRange(Q1_cube);

            GameEventCenter.DispatchEvent("FindQ2Cubes");
            cube_GA.AddRange(Q2_cube);

            GameEventCenter.DispatchEvent("FindQ3Cubes");
            cube_GB.AddRange(Q3_cube);

            GameEventCenter.DispatchEvent("FindQ4Cubes");
            cube_GC.AddRange(Q4_cube);
        }
        else if (_RandomQuestion == 2)
        {
            GameEventCenter.DispatchEvent("FindQ2Cubes");
            Cubes.AddRange(Q2_cube);

            GameEventCenter.DispatchEvent("FindQ1Cubes");
            cube_GA.AddRange(Q1_cube);

            GameEventCenter.DispatchEvent("FindQ3Cubes");
            cube_GB.AddRange(Q3_cube);

            GameEventCenter.DispatchEvent("FindQ4Cubes");
            cube_GC.AddRange(Q4_cube);
        }
        else if (_RandomQuestion == 3)
        {
            GameEventCenter.DispatchEvent("FindQ3Cubes");
            Cubes.AddRange(Q3_cube);

            GameEventCenter.DispatchEvent("FindQ1Cubes");
            cube_GA.AddRange(Q1_cube);

            GameEventCenter.DispatchEvent("FindQ2Cubes");
            cube_GB.AddRange(Q2_cube);

            GameEventCenter.DispatchEvent("FindQ4Cubes");
            cube_GC.AddRange(Q4_cube);
        }
        else if (_RandomQuestion == 4)
        {
            GameEventCenter.DispatchEvent("FindQ4Cubes");
            Cubes.AddRange(Q4_cube);

            GameEventCenter.DispatchEvent("FindQ1Cubes");
            cube_GA.AddRange(Q1_cube);

            GameEventCenter.DispatchEvent("FindQ2Cubes");
            cube_GB.AddRange(Q2_cube);

            GameEventCenter.DispatchEvent("FindQ3Cubes");
            cube_GC.AddRange(Q3_cube);
        }
    }
    public void FindQ1Cubes()
    {
        Debug.Log("find Q1_cube!!!");
        Q1_cube.Add(GameObject.Find("Q1BlueCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1RedCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1GreenCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1YellowCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1BlueCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1RedCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1GreenCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1YellowCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1BlueCube(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("Q1RedCube(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q1_cube);
    }
    public void FindQ2Cubes()
    {
        Debug.Log("find Q2_cube!!!");
        Q2_cube.Add(GameObject.Find("Q2BlueCuboid3_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2BlueCuboid3_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2YellowCube_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2GreenCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2RedCube2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2GreenCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2YellowCube_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2RedCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2RedCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("Q2BlueCuboid(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q2_cube);
    }
    public void FindQ3Cubes()
    {
        Debug.Log("find Q3_cube!!!");
        Q3_cube.Add(GameObject.Find("Q3BlueCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3YellowCube(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3BlueCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3RedCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3RedCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3BlueCuboid_3(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3GreenCube_1(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3BlueCuboid_4(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3YellowCuboid3(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("Q3GreenCube_2(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q3_cube);
    }
    public void FindQ4Cubes()
    {
        Debug.Log("find Q1_cube!!!");
        Q4_cube.Add(GameObject.Find("Q4RedCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4GreenCuboid(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4RedCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4RedCuboid_3(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4YellowCube(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4YellowCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4BlueCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4GreenCube(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4BlueCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("Q4YellowCuboid_2(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q4_cube);
    }
    public void Find_QuestionCubes()
    {
        if (_RandomQuestion == 1)
        {
            Debug.Log("find Users_Q1cube!!!");
            Question_Cube.AddRange(GameObject.FindGameObjectsWithTag("Q1question"));
        }
        else if (_RandomQuestion == 2)
        {
            Debug.Log("find Users_Q2cube!!!");
            Question_Cube.AddRange(GameObject.FindGameObjectsWithTag("Q2question"));
        }
        else if (_RandomQuestion == 3)
        {
            Debug.Log("find Users_Q3cube!!!");
            Question_Cube.AddRange(GameObject.FindGameObjectsWithTag("Q3question"));
        }
        else if (_RandomQuestion == 4)
        {
            Debug.Log("find Users_Q4cube!!!");
            Question_Cube.AddRange(GameObject.FindGameObjectsWithTag("Q4question"));
        }
    }
    public void KidsShouldPut()
    {
        foreach (GameObject cube in Question_Cube)
        {
            if (cube.GetComponent<QuestionCube>().CubeOrder == RecentOrder)
            {
                Debug.Log("CubeOrder: " + cube.GetComponent<QuestionCube>().CubeOrder + "RecentOrder: " + RecentOrder);
                KidShouldPut = cube;
                Debug.Log("Kid should put: " + KidShouldPut);
                Debug.Log("Kid should put: " + KidShouldPut.name);
            }
        }
    }
    public void CubeToAns(BlockEntity cube)
    {
        cube.ToAnsLv2_Mono();
    }
    public void CubeOnAns(BlockEntity cube)
    {
        cube.OnAnsLv2_Mono();
    }
    public void OtherGroupCubeAnsLv2_Mono(BlockEntity cube)
    {
        cube.OtherGroupToAns();
    }
    public void GetFocusName(string name)
    {
        focusName = name;
    }
}
