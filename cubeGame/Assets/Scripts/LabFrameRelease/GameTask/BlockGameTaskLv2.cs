using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;
using GameData;
using LabData;

public class BlockGameTaskLv2 : TaskBase
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
    private GameObject RedTriggerBall;
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
        GameEventCenter.AddEvent<BlockEntity>("OtherGroupCubeAnsLv2", OtherGroupCubeAnsLv2);
        GameEventCenter.AddEvent("AddCubesToList", AddCubesToList);
        GameEventCenter.AddEvent("FindQ1Cubes", FindQ1Cubes);
        GameEventCenter.AddEvent("FindQ2Cubes", FindQ2Cubes);
        GameEventCenter.AddEvent("FindQ3Cubes", FindQ3Cubes);
        GameEventCenter.AddEvent("FindQ4Cubes", FindQ4Cubes);
        GameEventCenter.AddEvent("Find_QuestionCubes", Find_QuestionCubes);
        GameEventCenter.AddEvent("User_MissingOneCubeLv2", User_MissingOneCubeLv2);
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
        GameObject ChooseQuestionCanvas = GameObject.Find("ChooseQuestionCanvas");
        GameObject XiaoMeiRaiseHandPic = GameObject.Find("Schematics/UserRightSightCanvas/XiaoMeiRaiseHandPic");
        GameObject Question1Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question1Pic");
        GameObject Question2Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question2Pic");
        GameObject Question3Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question3Pic");
        GameObject Question4Pic = GameObject.Find("Schematics/UserRightSightCanvas/QuestionPics/Question4Pic");
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
        GameObject.Find("ChooseQuestionCanvas").GetComponent<Canvas>().enabled = false;
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        for (int i = 1; i < 5; i++)
        {
            GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = false;
        }
        //邀請小朋友一起堆積木
        //npc.animator.Play("Talk");
        //GameAudioController.Instance.PlayOneShot(npc.speechList[0]);
        //yield return new WaitForSeconds(1.5f);
        //yield return new WaitForSeconds(2);
        
        //打招呼
        yield return SayHello();
        yield return new WaitForSeconds(1.5f);
        //老師開場
        yield return Teacher_Opening();
        yield return new WaitForSeconds(3f);
        //老師說明遊戲規則
        yield return Teacher_Introduction();
        yield return new WaitForSeconds(1.5f);
        //主持人請大家一起說剪刀石頭布
        yield return Host_RemindAllToSayRPS();
        yield return new WaitForSeconds(1.5f);

        //語音辨識

        //User跟其他三組猜拳
        //第一次 小花慢出(NPC生氣)
        GameEventCenter.DispatchEvent("FourPlayerRPS");//猜拳動畫
        GameObject.Find("FourPlayerChoose(Clone)/Canvas2").SetActive(true);
        GameObject.Find("FourPlayerChoose(Clone)/Canvas").SetActive(false);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        //大家一起說剪刀石頭布
        yield return All_NPC_SayRPS();
        _userChooseRPS = false;
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
        GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP2");//小花慢出
        GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP2Lv2");


        yield return new WaitForSeconds(3);
        npc.animator.SetBool("isTalk", true);
        //不算～，你慢出，再猜一次！
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_XiaoMeiTooSlow");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("Result").SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(3);

        //第二次 User贏
        HostAnimator.SetBool("isStandingAndTalking", true);
        //記得聽到布的時候，要一起出拳喔，不然贏了會不算喔，要再重猜一次
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RemindRPSOnTime");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("主持人說再玩一次");
        HostAnimator.SetBool("isStandingAndTalking", false);
        GameEventCenter.DispatchEvent("FourPlayerRPS");
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
            Debug.Log(_userChooseRPS);
            yield return new WaitUntil(() => _userChooseRPS);
            Debug.Log(_userChooseRPS);
        } while (!_userChooseRPS);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3);

        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("Result").SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }

        //XXX那一組猜拳贏了，你可以先選一張圖。你要選第幾張圖?
        yield return Teacher_AskUserWhichPic();
        yield return new WaitForSeconds(1.5f);
        //讓user選題目
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        yield return UserChooseQuestion();
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        //其他沒有贏的組，老師一組發一張圖案
        TeacherAnimator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveQuestionToOtherGroups");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        TeacherAnimator.SetBool("isTalk", false);
        yield return new WaitForSeconds(2);

        //User跟對面NPC猜拳**********************************************************************************
        //GameEventCenter.DispatchEvent("InstatiateCubeLv2");
        ////GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");
        //GameEventCenter.DispatchEvent("CubeOnDesk");
        //GameEventCenter.DispatchEvent("Find_QuestionCubes");//*****
        //GameEventCenter.DispatchEvent("AddCubesToList");
        //foreach (BlockEntity cube in AllCubes)
        //{
        //    cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //    cube.GetComponent<MeshRenderer>().enabled = false;
        //    cube.GetComponent<BoxCollider>().isTrigger = true;

        //}
        //for(int i = 0; i<10; i++)
        //{
        //    GameObject.Find("Parents/Q"+ _RandomQuestion+"_Parent/Question(Clone)").transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = null;
        //}
        //小花: 沒贏也沒關係，每一張圖我都喜歡
        XiaoHua.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_ItsOkTolose");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        XiaoHua.SetBool("isTalk", false);
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
        yield return new WaitForSeconds(2);
        //Recognizer*************************************************************
        yield return NPC_SameColor1();
        yield return new WaitForSeconds(3);
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
        yield return new WaitForSeconds(2);

        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(2);

        //小星說: 喔不!我輸了，你可以拿XX
        yield return NPC_YouWinLv2();
        yield return new WaitForSeconds(2);
        yield return NPC_AskUserSecondColor();
        yield return new WaitForSeconds(3);
        yield return NPC_IfUserChoseSameColor();
        //yield return NPC_SameColor2();
        ////user lose
        //_userChooseRPS = false;
        //userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        //userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        //GameEventCenter.DispatchEvent("TwoPlayerRPS");
        //GameObject.Find("TwoPlayerChoose(Clone)/Canvas").SetActive(false);
        //GameObject.Find("TwoPlayerChoose(Clone)/Canvas2").SetActive(true);
        ////NPC說剪刀石頭布
        //npc.animator.SetBool("isPlayingRPS", true);
        //clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        //npc.animator.SetBool("isPlayingRPS", false);
        
        //do
        //{
        //    Debug.Log("User choose RPS");
        //    Debug.Log(_userChooseRPS);
        //    yield return new WaitUntil(() => _userChooseRPS);
        //    Debug.Log(_userChooseRPS);
        //} while (!_userChooseRPS);
        //userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        //userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        //yield return new WaitForSeconds(2);

        //GameObject.FindWithTag("Result").SetActive(false);
        //for (int i = 0; i < 2; i++)
        //{
        //    GameObject.FindWithTag("RPS").SetActive(false);
        //}
        //yield return new WaitForSeconds(2);
        //小星說: 我贏了! XX是我的積木
        //yield return NPC_YouLoseLv2();
        yield return new WaitForSeconds(2);
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
        //GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");
        foreach (BlockEntity cube in AllCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = true;
            //cube.GetComponent<BoxCollider>().isTrigger = false;
            //cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");
        cube_GB[0].GetComponent<MeshRenderer>().enabled = false;//小花少第一顆積木
        yield return new WaitForSeconds(3);
        //開始堆積木
        //小花跟老師拿積木
        yield return XiaoMeiNeedsCube(XiaoMeiRaiseHandPic);
        //cube_GB[0].GetComponent<MeshRenderer>().enabled = true;//小花少第一顆積木
        foreach (BlockEntity cube in AllCubes)
        {
            //cube.GetComponent<MeshRenderer>().enabled = true;
            cube.GetComponent<BoxCollider>().isTrigger = false;
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        if (Final_Order[0].GetComponent<BlockEntity>()._isUserColor)
        {
            _playerRound = true;
        }
        while (!_BlockFinished)//LV2
        {
            _StartTobuild = true;
            GameEventCenter.DispatchEvent("KidsShouldPut");
            Debug.Log("Recent order is : " + RecentOrder);
            if (Final_Order[RecentOrder - 1]._isUserColor)
            {
                _playerRound = true;
            }
            else
            {
                _playerRound = false;
            }
            Debug.Log("PlayerRound: " + _playerRound);
            //while (_StartTobuild) 
            //{
            //    yield return OtherGroupBuildBlock();
            //}
            if (_playerRound)  //玩家回合
            {
                if (Final_Order[RanNum - 1]._isChose && Final_Order[RanNum].GetComponent<BoxCollider>().enabled == false)//檢查前一個積木，
                {
                    if (Final_Order[RanNum]._isChose) //不見的也放過了
                    {
                        Debug.Log(Final_Order[RanNum] + "was put.");
                        Debug.Log("User turn");
                        yield return new WaitUntil(() => !_playerRound);
                    }

                    else
                    {
                        //user要跟老師說少一個，wait until 叫老師
                        yield return UserNeedsACube();
                        Debug.Log("User need " + MissingCube);
                        yield return new WaitForSeconds(2);
                        //GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = true;
                        //GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = true;
                    }
                }

                else
                {
                    //GameEventCenter.DispatchEvent("KidsShouldPut");
                    Debug.Log("User touch Block");
                    yield return new WaitUntil(() => !_playerRound);
                }
            }
            else  //NPC回合
            {
                _npcremind = false;
                foreach (BlockEntity cube in Final_Order)
                {
                    string cubeName;
                    //Get Cube name without "(Clone)"
                    cubeName = cube.name;
                    int delStr = cubeName.IndexOf("(Clone)");
                    if (delStr >= 0)
                    {
                        cubeName = cubeName.Remove(delStr);
                    }
                    if (!cube._isChose && !cube._isUserColor)
                    {
                        Debug.Log("NPC touch block");
                        //npc.animator.Play("坐在椅子上放積木(2D圖片) NPC用左手拿取桌上的積木，然後放在中間的圖片上");
                        npc.animator.SetBool("isTakeCube", true);
                        yield return new WaitForSeconds(1.5f);
                        Debug.Log("put Block");
                        npc.transform.rotation = Quaternion.Euler(0, 0, 0);
                        _npcremind = false;
                        if (!_npcremind)
                        {
                            Debug.Log("NPC put block");
                            GameEventCenter.DispatchEvent("KidsShouldPut");
                            GameEventCenter.DispatchEvent("CubeToAns", cube);
                            yield return new WaitForSeconds(0.5f);
                            //GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<Animator>().SetBool("isTake", false);
                            GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/block/" + cubeName).GetComponent<MeshRenderer>().enabled = false;
                            yield return new WaitForSeconds(0.5f);
                            GameEventCenter.DispatchEvent("CubeOnAns", cube);
                        }
                        npc.animator.SetBool("isTakeCube", false);
                        //npc.animator.SetBool("isDefault", true);
                        break;
                    }
                }

            }
            GameEventCenter.DispatchEvent("CheckCube");
            _StartTobuild = false;
            yield return new WaitForSeconds(2);
        }

        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;

        //結束後語音
        //小星: 耶!我們完成了!（小星雙手舉高）等待2秒
        //npc.animator.Play("坐在椅子上開心 雙手舉高，眼睛跟嘴巴要笑(上揚)");
        npc.animator.SetBool("isHappy", true);
        yield return new WaitForSeconds(1);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Hooray");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isHappy", false);
        npc.animator.SetBool("isClapHand", true);
        yield return new WaitForSeconds(3.5f);
        npc.animator.SetBool("isClapHand", false);
        //RedTriggerBall
        RedTriggerBall.SetActive(true);
        yield return new WaitForSeconds(2);
        //wait2Sec
        _userCelebrate = false;
        while (RemindCelebrate < 3)
        {
            //Debug.Log(RemindCelebrate);
            GameEventCenter.DispatchEvent("Text2sec_isEnabledLv2", true); // 2秒計時器打開
            GameEventCenter.DispatchEvent("Timer2secResetLv2");
            yield return new WaitUntil(() =>
            {
                //Debug.Log(RemindCelebrate);
                if (_userCelebrate && !_is2secTimeUp) // user有擊掌 
                {
                    return true;
                }
                else if (!_userCelebrate && _is2secTimeUp) // 過了2秒user沒有擊掌
                {
                    return true;
                }
                return false;
            });

            if (_userCelebrate && !_is2secTimeUp) // user有擊掌
            {
                //Debug.Log(RemindCelebrate);
                GameEventCenter.DispatchEvent("Text2sec_isEnabledLv2", false); // 2秒計時器關閉
                GameEventCenter.DispatchEvent("Timer2secResetLv2");
                _userCelebrate = false;

                yield return new WaitForSeconds(1);
                //主持人：你有跟小星擊掌耶，可以獲得一個愛心喔
                clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_CelebrateGetHeart");
                GameAudioController.Instance.PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length);
                RedTriggerBall.SetActive(false);
                yield return new WaitForSeconds(2);
                npc.animator.SetBool("isExiting", true);
                Heart.SetActive(true);
                clip = Resources.Load<AudioClip>("AudioClip/Awards/ruby");
                GameAudioController.Instance.PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length);
                Heart.SetActive(false);
                npc.animator.SetBool("isExiting", false);
                GameEventCenter.DispatchEvent("GameHeartCounter");   // 愛心數量加一
                break;
            }
            else if (!_userCelebrate && _is2secTimeUp) // 過了2秒user沒有擊掌
            {
                //Debug.Log(RemindCelebrate);
                GameEventCenter.DispatchEvent("Text2sec_isEnabledLv2", false); // 5秒計時器關閉
                GameEventCenter.DispatchEvent("Timer2secResetLv2");
                if (RemindCelebrate == 1)
                {
                    Debug.Log("Host_CelebrateLv2");
                    //主持人（提醒1）
                    npc.animator.SetBool("isRaiseAgain", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_CelebrateLv2");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isClapHandInSpace", true);
                    yield return new WaitForSeconds(3);
                    npc.animator.SetBool("isClapHandInSpace", false);
                }
                else if (RemindCelebrate == 2)
                {
                    Debug.Log("Host_CelebrateRemind");
                    //主持人（提醒2）: 小星很開心，想跟你擊掌，你可以碰紅色的球跟小星擊個掌（等待2秒）
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_CelebrateRemind");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                }
                npc.animator.SetBool("isRaiseAgain", false);
                RemindCelebrate++;
                Debug.Log(RemindCelebrate);
            }
            GameEventCenter.DispatchEvent("Text2sec_isEnabledLv2", false); // 2秒計時器關閉
            GameEventCenter.DispatchEvent("Timer2secResetLv2");
        }
        Hat.SetBool("isClapHand", true);
        Red.SetBool("isClapHand", true);
        Green.SetBool("isClapHand", true);
        XiaoHua.SetBool("isClapHand", true);
        XiaoMei.SetBool("isClapHand", true);
        Yoyo.SetBool("isClapHand", true);
        
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_GetFiveCoins");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        for (int i = 0; i < 5; i++)
        {
            Coin.SetActive(true);                                    // 金幣動畫
            clip = Resources.Load<AudioClip>("AudioClip/Awards/coin");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Coin.SetActive(false);
            GameEventCenter.DispatchEvent("GameCoinCounter");        // 金幣數量加一(金幣動畫)
        }

        RemindCelebrate = 0;//歸零
        Debug.Log(RemindCelebrate);
        Hat.SetBool("isHappy", true);
        Red.SetBool("isHappy", true);
        Green.SetBool("isExiting", true);
        XiaoHua.SetBool("isHappy", true);
        XiaoMei.SetBool("isExiting", true);
        Yoyo.SetBool("isExiting", true);
        yield return new WaitForSeconds(5);
        Hat.SetBool("isHappy", false);
        Red.SetBool("isHappy", false);
        Green.SetBool("isExiting", false);
        XiaoHua.SetBool("isHappy", false);
        XiaoMei.SetBool("isExiting", false);
        Yoyo.SetBool("isExiting", false);
        yield return null;
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
    IEnumerator SayHello()
    {

        HostAnimator.SetBool("isSayingHello", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(clip);
        Debug.Log("打完招呼");
        HostAnimator.SetBool("isSayingHello", false);
        yield return null;
    }
    IEnumerator Teacher_Opening()
    {
        TeacherAnimator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_Opening");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath + "Teacher_Opening" + clip.length);
        Debug.Log("老師開完場");
        TeacherAnimator.SetBool("isTalk", false);
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
    IEnumerator Host_RemindAllToSayRPS()
    {
        HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RemindAllToSayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath + "Host_RemindAllToSayRPS" + clip.length);
        HostAnimator.SetBool("isStandingAndTalking", false);
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
        yield return new WaitForSeconds(3);
        yield return null;
    }
    IEnumerator NPC_IfUserChoseSameColor()
    {
        if(GameDataManager.FlowData.UserColor == GameDataManager.FlowData.UserFirstColor)
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
        yield return new WaitForSeconds(3);
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
        yield return new WaitForSeconds(3);
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
        XiaoHua.SetBool("isPlayingRPS", true);
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
        XiaoHua.SetBool("isPlayingRPS", false);
        Yoyo.SetBool("isPlayingRPS", false);
        yield return new WaitForSeconds(2);
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
        yield return new WaitForSeconds(3);
        GameObject.Find("ChooseQuestionCanvas").SetActive(false);
        yield return null;
    }
    IEnumerator XiaoMeiNeedsCube(GameObject XiaoMeiRaiseHandPic)
    {
        GameObject TeacherGiveXiaoMeiCube = null;
        if (_RandomQuestion == 1)
        {
            XiaoMeiMissingCube = "藍色";
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_BlueCube");
        }
        else if (_RandomQuestion == 2)
        {
            XiaoMeiMissingCube = "黃色";
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/RedCuboid");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_YellowCube");
        }
        else if (_RandomQuestion == 3)
        {
            XiaoMeiMissingCube = "紅色";
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid3");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_RedCube");
        }
        else if (_RandomQuestion == 4)
        {
            XiaoMeiMissingCube = "藍色";
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid3");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_BlueCube");
        }
        XiaoHua.SetBool("isRaiseHand", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_CallTeacher");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        XiaoHua.SetBool("isRaiseHand", false);
        yield return new WaitForSeconds(2);
        TeacherAnimator.SetBool("isTalkingToXiaoMei", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_AskXiaoMei");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length + 2);
        TeacherAnimator.SetBool("isTalkingToXiaoMei", false);
        yield return new WaitForSeconds(2);
        XiaoHua.SetBool("isRaiseHandAndTalkToTeacher", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_INeedACube");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(XiaoMeiColor);
        yield return new WaitForSeconds(XiaoMeiColor.length);
        XiaoHua.SetBool("isRaiseHandAndTalkToTeacher", false);
        yield return new WaitForSeconds(2);

        HostAnimator.SetBool("isPointing", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTellLikeXiaoMei");
        XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        HostAnimator.SetBool("isPointing", false);
        yield return new WaitForSeconds(2);
        XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = false;

        //TeacherAnimator.SetBool("isTakeCube", true);
        //yield return new WaitForSeconds(2f);
        //testAni.TeacherMoveToXiaoMei = true;
        //TeacherAnimator.SetBool("isTakeCubeWalking", true);
        //TeacherAnimator.SetBool("isPutingCube", true);
        TeacherAnimator.SetBool("isTakeCubeToXiaoMei", true);
        TeacherGiveXiaoMeiCube.GetComponent<Animator>().SetBool("isToXiaoMei", true);

        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveCube");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        if (XiaoMeiMissingCube == "藍色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_BlueCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (XiaoMeiMissingCube == "黃色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_YellowCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (XiaoMeiMissingCube == "紅色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_RedCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (XiaoMeiMissingCube == "綠色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GreenCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        yield return new WaitForSeconds(7);
        cube_GB[0].GetComponent<MeshRenderer>().enabled = true;//小美少第一顆積木
        yield return new WaitForSeconds(7);
        TeacherAnimator.SetBool("isTakeCubeToXiaoMei", false);
        TeacherGiveXiaoMeiCube.GetComponent<Animator>().SetBool("isToXiaoMei", false);
        yield return new WaitForSeconds(2);
        TeacherGiveXiaoMeiCube.GetComponent<MeshRenderer>().enabled = true;
        //testAni.TeacherMoveToXiaoMei = false;
        //Debug.Log("老師走到小花");
        //testAni.TeacherMoveBackFromXiaoMei = true;
        //TeacherAnimator.SetBool("isTakeCube", false);
        //TeacherAnimator.SetBool("isTakeCubeWalking", false);
        //TeacherAnimator.SetBool("isPutingCube", false);
        Debug.Log("老師走回去");
        //yield return new WaitForSeconds(5);
        //testAni.TeacherMoveBackFromXiaoMei = false;

        yield return new WaitForSeconds(2);
        yield return null;

    }
    IEnumerator UserNeedsACube()
    {
        yield return new WaitForSeconds(2);
        _userRaiseHand = false;
        _userSpeekToTeacher = false;
        KeyWordRecognizer.MissingColor = null;
        GameObject UserNewCube = null;
        //開起綠球
        GreenTriggerBall.SetActive(true);
        npc.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher4");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk2", false);
        Debug.Log("NPC:你少一塊積木，要舉手跟老師說喔!");
        yield return new WaitForSeconds(1);
        while (RemindRaiseHand < 3)
        {
            //Debug.Log(RemindRaiseHand);
            GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", true); // 5秒計時器打開
            GameEventCenter.DispatchEvent("Timer5secResetLv2");
            yield return new WaitUntil(() =>
            {
                //Debug.Log(RemindRaiseHand);
                if (_userRaiseHand && !_userSpeekToTeacher && !_is5secTimeUp) // user有舉手 沒說話
                {
                    return true;
                }
                else if (!_userRaiseHand && _userSpeekToTeacher && !_is5secTimeUp) //user沒舉手 有說話
                {
                    return true;
                }
                if (_userRaiseHand && _userSpeekToTeacher && !_is5secTimeUp) // user有舉手 有說話
                {
                    return true;
                }
                else if (!_userRaiseHand && !_userSpeekToTeacher && _is5secTimeUp) // 過了5秒user沒有舉手 也沒有說話
                {
                    return true;
                }
                //if (_userRaiseHand && !_is5secTimeUp) // user有舉手
                //{
                //    return true;
                //}
                //else if (!_userRaiseHand && _is5secTimeUp) // 過了5秒user沒有舉手
                //{
                //    return true;
                //}
                return false;
            });

            //if (_userRaiseHand && _userSpeekToTeacher && !_is5secTimeUp) // user有舉手 說話
            //{
            //    //Debug.Log(RemindRaiseHand);
            //    GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", false); // 5秒計時器關閉
            //    GameEventCenter.DispatchEvent("Timer5secResetLv2");
            //    _userRaiseHand = false;

            //    yield return new WaitForSeconds(1);
            //    //主持人: 你有舉手等待老師，很棒!可以獲得一個愛心
            //    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_YouRaiseHandToTalk");
            //    GameAudioController.Instance.PlayOneShot(clip);
            //    yield return new WaitForSeconds(clip.length);
            //    Debug.Log("主持人: 你有舉手等待老師，很棒!可以獲得一個愛心");
            //    yield return new WaitForSeconds(2);

            //    Heart.SetActive(true);
            //    clip = Resources.Load<AudioClip>("AudioClip/Awards/ruby");
            //    GameAudioController.Instance.PlayOneShot(clip);
            //    yield return new WaitForSeconds(clip.length);
            //    Heart.SetActive(false);
            //    GameEventCenter.DispatchEvent("GameHeartCounter");   // 愛心數量加一

            //    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_YouSaidYouMissCube");
            //    GameAudioController.Instance.PlayOneShot(clip);
            //    yield return new WaitForSeconds(clip.length);
            //    Debug.Log("主持人: 而且你有跟老師說你少了一個積木，很棒喔!可以獲得一個寶石");

            //    Ruby.SetActive(true);
            //    clip = Resources.Load<AudioClip>("AudioClip/Awards/ruby");
            //    GameAudioController.Instance.PlayOneShot(clip);
            //    yield return new WaitForSeconds(clip.length);
            //    Ruby.SetActive(false);
            //    GameEventCenter.DispatchEvent("GameRubyCounter");   // 寶石數量加一
            //    yield return new WaitForSeconds(2);
            //    break;
            //}
            if (!_is5secTimeUp) //5秒內 
            {
                if (_userRaiseHand && _userSpeekToTeacher) // user有舉手 說話
                {
                    GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", false); // 5秒計時器關閉
                    GameEventCenter.DispatchEvent("Timer5secResetLv2");
                    _userRaiseHand = false;
                    _userSpeekToTeacher = false;
                    yield return new WaitForSeconds(1);
                    //主持人: 你有舉手等待老師，很棒!可以獲得一個愛心
                    HostAnimator.SetBool("isClapping", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_YouRaiseHandToTalk");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    HostAnimator.SetBool("isClapping", false);
                    Debug.Log("主持人: 你有舉手等待老師，很棒!可以獲得一個愛心");
                    yield return new WaitForSeconds(2);

                    Heart.SetActive(true);
                    clip = Resources.Load<AudioClip>("AudioClip/Awards/ruby");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    Heart.SetActive(false);
                    GameEventCenter.DispatchEvent("GameHeartCounter");   // 愛心數量加一

                    HostAnimator.SetBool("isClapping", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_YouSaidYouMissCube");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    HostAnimator.SetBool("isClapping", false);
                    Debug.Log("主持人: 而且你有跟老師說你少了一個積木，很棒喔!可以獲得一個寶石");

                    Ruby.SetActive(true);
                    clip = Resources.Load<AudioClip>("AudioClip/Awards/ruby");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    Ruby.SetActive(false);
                    GameEventCenter.DispatchEvent("GameRubyCounter");   // 寶石數量加一
                    yield return new WaitForSeconds(2);
                    break;
                }
                else if (!_userRaiseHand && _userSpeekToTeacher)//user沒有舉手 有說話
                //if (RemindRaiseHand == 0)
                {
                    //小星提示:你可以舉手，然後跟老師說你少了什麼顏色的積木
                    GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", false); // 5秒計時器關閉
                    GameEventCenter.DispatchEvent("Timer5secResetLv2");
                    npc.animator.SetBool("isTalk2", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher2");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isTalk2", false);
                    Debug.Log("NPC:你可以舉手，然後跟老師說你少了什麼顏色的積木");
                    yield return new WaitForSeconds(2);
                    //_userSpeekToTeacher = false;
                    RemindRaiseHand++;
                    Debug.Log(RemindRaiseHand);
                }
                else if (_userRaiseHand && !_userSpeekToTeacher)// 有舉手 沒說話
                //else if(RemindRaiseHand == 1)
                {
                    Debug.Log("主持人（提醒）");
                    GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", false); // 5秒計時器關閉
                    GameEventCenter.DispatchEvent("Timer5secResetLv2");
                    //主持人（提醒1）:我們在上課的時候，遇到問題就可以像小美一樣，先舉手等待老師，再跟老師說
                    HostAnimator.SetBool("isPointing", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTellLikeXiaoMei");
                    GameAudioController.Instance.PlayOneShot(clip);
                    //XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
                    yield return new WaitForSeconds(clip.length);
                    HostAnimator.SetBool("isPointing", false);
                    Debug.Log("主持人:我們在上課的時候，遇到問題就可以像美一樣，先舉手等待老師，再跟老師說");
                    //_userRaiseHand = false;
                    RemindRaiseHand++;
                    Debug.Log(RemindRaiseHand);
                }
            }
            else if (!_userRaiseHand && !_userSpeekToTeacher && _is5secTimeUp) // 過了5秒user沒有舉手 沒說話
            {
                //Debug.Log(RemindRaiseHand);
                GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", false); // 5秒計時器關閉
                GameEventCenter.DispatchEvent("Timer5secResetLv2");
                if (RemindRaiseHand == 0)
                {
                    Debug.Log("小星提示1");
                    npc.animator.SetBool("isTalk", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher1");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isTalk", false);
                    Debug.Log("NPC叫user要跟老師說少一個");
                    yield return new WaitForSeconds(2);
                }
                else if (RemindRaiseHand == 1)
                {
                    Debug.Log("小星提示2");
                    //小星提示2:你可以舉手，然後跟老師說你少了什麼顏色的積木
                    npc.animator.SetBool("isTalk2", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher2");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isTalk2", false);
                    Debug.Log("NPC叫user要跟老師說少一個");
                    yield return new WaitForSeconds(2);

                }
                else if (RemindRaiseHand == 2)
                {
                    Debug.Log("主持人（提醒）");
                    //主持人（提醒1）:我們在上課的時候，遇到問題就可以像小美一樣，先舉手等待老師，然後跟老師說
                    HostAnimator.SetBool("isPointing", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTellLikeXiaoMei");
                    GameAudioController.Instance.PlayOneShot(clip);
                    //XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
                    yield return new WaitForSeconds(clip.length);
                    HostAnimator.SetBool("isPointing", false);
                    Debug.Log("我們在上課的時候，遇到問題就可以像小美一樣，先舉手等待老師，然後跟老師說");
                    yield return new WaitForSeconds(2);
                }
                RemindRaiseHand++;
                Debug.Log(RemindRaiseHand);
            }
            GameEventCenter.DispatchEvent("Text5sec_isEnabledLv2", false); // 5秒計時器關閉
            GameEventCenter.DispatchEvent("Timer5secResetLv2");
        }
        RemindRaiseHand = 0;
        GreenTriggerBall.SetActive(false);//Close GreenTriggerBall

        Vector3 CubeScale, Cube2Scale, CuboidScale, Cuboid3Scale;
        CubeScale = new Vector3(0.05f, 0.05f, 0.05f);
        Cube2Scale = new Vector3(0.1f, 0.1f, 0.1f);
        CuboidScale = new Vector3(0.1f, 0.05f, 0.05f);
        Cuboid3Scale = new Vector3(0.15f, 0.05f, 0.05f);
        //red
        if (GameObject.Find(MissingCube).transform.localScale == CubeScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])//Red
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCube");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == Cube2Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCube2");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == CuboidScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCuboid");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == Cuboid3Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCuboid3");
        }
        //blue
        else if (GameObject.Find(MissingCube).transform.localScale == CubeScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[1])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/BlueCube");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == CuboidScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[1])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == Cuboid3Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[1])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid3");
        }
        //green
        else if (GameObject.Find(MissingCube).transform.localScale == CubeScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[2])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/GreenCube");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == CuboidScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[2])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/GreenCuboid");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == Cuboid3Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[2])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/GreenCuboid3");
        }
        //yellow
        else if (GameObject.Find(MissingCube).transform.localScale == CubeScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[2])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/YellowCube");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == CuboidScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[2])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/YellowCuboid");
        }
        else if (GameObject.Find(MissingCube).transform.localScale == Cuboid3Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[2])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/YellowCuboid3");
        }
        //GameEventCenter.DispatchEvent("TeacherGiveCube");
        Debug.Log("UserNewCube: " + UserNewCube);
        //TeacherAni
        //TeacherAnimator.SetBool("isTakeCube", true);
        //yield return new WaitForSeconds(3f);
        //testAni.TeacherMoveToUser = true;
        //TeacherAnimator.SetBool("isTakeCubeWalking", true);
        //TeacherAnimator.SetBool("isPutingCube", true);
        UserNewCube.GetComponent<Animator>().SetBool("isToUser", true);
        TeacherAnimator.GetComponent<Animator>().SetBool("isTakeCubeToUser", true);

        if (KeyWordRecognizer.MissingColor == null)
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveUserCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (KeyWordRecognizer.MissingColor == "藍色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_BlueCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (KeyWordRecognizer.MissingColor == "黃色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_YellowCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (KeyWordRecognizer.MissingColor == "紅色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_RedCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else if (KeyWordRecognizer.MissingColor == "綠色")
        {
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GiveCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_GreenCube");
            GameAudioController.Instance.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        yield return new WaitForSeconds(7);
        UserNewCube.GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = true;
        GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = true;
        //testAni.TeacherMoveToUser = false;
        Debug.Log("老師走到USER");
        yield return new WaitForSeconds(7);
        UserNewCube.GetComponent<Animator>().SetBool("isToUser", false);
        TeacherAnimator.SetBool("isTakeCubeToUser", false);
        yield return new WaitForSeconds(2);
        UserNewCube.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("老師走回去");
        //testAni.TeacherMoveBackFromUser = true;
        //TeacherAnimator.SetBool("isTakeCube", false);
        //TeacherAnimator.SetBool("isTakeCubeWalking", false);
        //TeacherAnimator.SetBool("isPutingCube", false);
        //Debug.Log("老師走回去");
        //yield return new WaitForSeconds(5);
        //testAni.TeacherMoveBackFromUser = false;
        yield return null;
    }
    IEnumerator UserChooseColor()
    {
        UserChoice1 = Colors[0];//red
        UserChoice2 = Colors[3];//yellow
        GameDataManager.FlowData.UserColor = UserChoice1.ToString();
        //NPC: 蛤~我也想要紅色[故意跟user一樣]，那我們來猜拳
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserName, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);

        Debug.Log(UserChoice1 + "UserChoice1");
        Debug.Log(UserChoice2 + " + UserChoice2");
        Debug.Log(GameDataManager.FlowData.UserColor);
        yield return new WaitForSeconds(2);
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
            }
        }
    }
    public void User_MissingOneCubeLv2()
    {
        RanNum = Random.Range(4, 9);
        Debug.Log(RanNum);
        for (int i = 0; i < 9; i++)
        {
            if (Final_Order[RanNum]._isUserColor)
            {
                Debug.Log(RanNum);
                Debug.Log(Final_Order[RanNum]);
                MissingCube = Final_Order[RanNum].name;
                Debug.Log("Q" + _RandomQuestion + "_CubeParent/" + MissingCube + " is missing....");
                GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<BoxCollider>().enabled = false;
                break;
            }
            else if (!Final_Order[RanNum]._isUserColor)
            {
                if (RanNum > 9)
                {
                    RanNum--;
                    Debug.Log(RanNum);
                    Debug.Log(Final_Order[RanNum]);
                    MissingCube = Final_Order[RanNum].name;
                    Debug.Log("Q" + _RandomQuestion + "_CubeParent/" + MissingCube + " is missing....");
                    GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<BoxCollider>().enabled = false;

                }
                else
                {
                    RanNum++;
                    Debug.Log(RanNum);
                    Debug.Log(Final_Order[RanNum]);
                    MissingCube = Final_Order[RanNum].name;
                    Debug.Log("Q" + _RandomQuestion + "_CubeParent/" + MissingCube + " is missing....");
                    GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<BoxCollider>().enabled = false;

                }
                break;
            }
        }
    }
    public void TakeObject()//拼圖的氣球
    {
        var index = 2;/*Random.Range(0, 3);*/
        //npc.NPCTakeObject(objectlist[index]);
    }
    public void PutObject()
    {
        npc.NPCPutObject(GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().PutPosition);
    }
    public void CubeToAns(BlockEntity cube)
    {
        cube.ToAnsLv2();
    }
    public void CubeOnAns(BlockEntity cube)
    {
        cube.OnAnsLv2();
    }
    public void OtherGroupCubeAnsLv2(BlockEntity cube)
    {
        cube.OtherGroupToAns();
    }
    public void GetFocusName(string name)
    {
        focusName = name;
    }
    //public void NPC_Remind()
    //{
    //    StartCoroutine(npc.NPCRemind());
    //}
    //public void NPC_Remind2()
    //{
    //    npc.NPCRemind2();
    //}
}
