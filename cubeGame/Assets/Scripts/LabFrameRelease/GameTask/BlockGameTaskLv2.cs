using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class BlockGameTaskLv2 : TaskBase
{
    private CameraEntity eyecamera;
    private PlayerEntity player;
    private GameObject userLeftHandTrigger;
    private GameObject userRightHandTrigger;
    private List<GameObject> Question_Cube;
    private NPCEntity npc;
    private Animator HostAnimator;
    private Animator KidA, KidB, KidC, KidD, KidE, KidF;
    private GameObject GreenTriggerBall;
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

    Color UserChoice1;
    Color UserChoice2;

    private List<int> textOnQuestion;
    private List<GameObject> QuestionOrder;
    //private List<GameObject> objectlist;
    private Canvas mainSceneUI;
    private AudioClip clip;
    private string MissingCube;
    private int RanNum;
    private string focusName;

    public static GameObject KidShouldPut;
    public static int RecentOrder = 1;
    public static bool _RoundA = true;
    public static int _ShowResult = 0;
    public static int _RandomQuestion = 0;
    public static bool _StartTobuild = false;
    public static bool _userChooseRPS = false;
    public static bool _userChooseQuestion = false;
    public static bool _userRaiseHand = false;
    public static bool _playerRound = false;
    public bool _BlockFinished = false;
    public static bool _usersayhello = false;
    public static bool _talking = false;
    public static bool _npcremind = false;
    public static bool _eyetimerfinish = false;
    public static bool _waitforwatch = false;
    public static int answerindex = 0;
    public int TalkScore = 0;//recognizerEntity
    private string audioClipRootPath;
    public override IEnumerator TaskInit()
    {
        GameEventCenter.AddEvent("CheckCube", CheckCube);
        GameEventCenter.AddEvent<BlockEntity>("CubeAns", CubeAns);
        GameEventCenter.AddEvent<string>("GetFocusName", GetFocusName);
        GameEventCenter.AddEvent<BlockEntity>("OtherGroupCubeAns", OtherGroupCubeAns);
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
        QuestionOrder = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().QuestionOrder;
        //mainSceneUI = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().MainSceneUI;
        KidA = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidA;
        KidB = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidB;
        KidC = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidC;
        KidD = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidD;
        KidE = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidE;
        KidF = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidF;

        //VRIK初始化
        player.Init(GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().vrCamera);

        //NPC初始化
        npc.EntityInit();

        //設定VR中可看到UI
        yield return new WaitForSeconds(0.5f);
        //mainSceneUI.worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //mainSceneUI.planeDistance = 1;

        //啟動眼球追蹤
        yield return new WaitForSeconds(0.5f);
        //LabVisualization.VisualizationManager.Instance.VisulizationInit();
        //LabVisualization.VisualizationManager.Instance.StartDataVisualization();

        eyecamera = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().eyeCamera;
        eyecamera.Init();
        answerindex = 0;   //初始化

        //語音Recognizer 初始化
        //recognizerEntity.Init();
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
        //for (int i = 1; i < 5; i++)
        //{
        //    GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = false;
        //}
        //邀請小朋友一起堆積木
        //npc.animator.Play("Talk");
        //GameAudioController.Instance.PlayOneShot(npc.speechList[0]);
        //yield return new WaitForSeconds(1.5f);
        //yield return new WaitForSeconds(2);
        /*
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
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_TooSlow");
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
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_RemindRPSOnTime");
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
        */
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
        HostAnimator.SetBool("isSlouchStandErect", false);
        HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_GiveQuestionToOtherGroups");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        HostAnimator.SetBool("isStandingAndTalking", false);
        HostAnimator.SetBool("isSlouchStandErect", true);
        yield return new WaitForSeconds(2);

        //User跟對面NPC猜拳**********************************************************************************
        GameEventCenter.DispatchEvent("InstatiateCubeLv2");
        //GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");
        GameEventCenter.DispatchEvent("CubeOnDesk");
        GameEventCenter.DispatchEvent("Find_QuestionCubes");//*****
        GameEventCenter.DispatchEvent("AddCubesToList");
        foreach (BlockEntity cube in AllCubes)
        {
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //cube.GetComponent<MeshRenderer>().enabled = false;
            cube.GetComponent<BoxCollider>().isTrigger = true;
            
        }
        for(int i = 0; i<10; i++)
        {
            GameObject.Find("Parents/Q"+ _RandomQuestion+"_Parent/Question(Clone)").transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = null;
        }
        //小花: 沒贏也沒關係，每一張圖我都喜歡
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Flower_ItsOkTolose");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        //小朋友，你們可以分配顏色，按照題目上的數字順序輪流完成作品
        yield return Teacher_RemindLv2();
        //NPC說 總共有四種顏色耶，你可以選兩種顏色。第一個顏色，你想要什麼呢?
        yield return NPC_AskUserFirstColor();
        //Recognizer*************************************************************
        yield return NPC_SameColor1();
        //user win
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        GameEventCenter.DispatchEvent("TwoPlayerRPS");
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas").SetActive(true);
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas2").SetActive(false);
        //NPC說剪刀石頭布
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
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
        yield return new WaitForSeconds(2);

        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(2);

        //小星說: 你贏了，你可以拿XX
        yield return NPC_YouWinLv2();
        yield return NPC_AskUserSecondColor();

        yield return NPC_SameColor2();
        //user lose
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        GameEventCenter.DispatchEvent("TwoPlayerRPS");
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas").SetActive(false);
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas2").SetActive(true);
        //NPC說剪刀石頭布
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
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
        yield return new WaitForSeconds(2);

        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(2);
        //小星說: 你輸了，XX是我的積木
        yield return NPC_YouLoseLv2();
        //框架
        //User choose two colors
        yield return UserChooseColor();//**************************

        //題目出現數字順序
        GameEventCenter.DispatchEvent("RandomNumOnQuestion");
        GameEventCenter.DispatchEvent("CheckOrder");//QuestionCube
        //GameEventCenter.DispatchEvent("AddCubesToList");
        GameEventCenter.DispatchEvent("PutInRightOrder");
        GameEventCenter.DispatchEvent("User_MissingOneCubeLv2");
        foreach (BlockEntity cube in AllCubes)
        {
            //cube.GetComponent<MeshRenderer>().enabled = true;
            cube.GetComponent<BoxCollider>().isTrigger = false;
            //cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        //MissingCube
        //GameObject.Find(MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = false;
        //GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = false;
        //開始堆積木
        //_StartTobuild = true;
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        if(Final_Order[0].GetComponent<BlockEntity>()._isUserColor)
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
                        yield return new WaitForSeconds(5);
                        GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = true;
                        GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = true;
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
                    if (!cube._isChose && !cube._isUserColor)
                    {
                        Debug.Log("NPC touch block");
                        npc.animator.Play("坐在椅子上尋找積木");
                        Debug.Log("find Block");
                        npc.animator.Play("坐在椅子上放積木(2D圖片) NPC用左手拿取桌上的積木，然後放在中間的圖片上");
                        //npc.animator.SetBool("isTakeCube", true);
                        yield return new WaitForSeconds(1f);
                        Debug.Log("put Block");
                        npc.transform.rotation = Quaternion.Euler(0, 0, 0);
                        //npc.animator.SetBool("findCube", true); 
                        //npc.animator.SetBool("takeCube", true);
                        //yield return new WaitForSeconds(3);
                        //Debug.Log("NPC putting Block");
                        //npc.animator.SetBool("findCube",false);
                        //npc.animator.SetBool("isTakeCube", false);
                        yield return new WaitForSeconds(8);
                        if (!_npcremind)
                        {
                            Debug.Log("NPC putting Block");
                            GameEventCenter.DispatchEvent("KidsShouldPut");
                            GameEventCenter.DispatchEvent("CubeAns", cube);
                            
                        }
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
        yield return null;
    }

    public override IEnumerator TaskStop()
    {
        yield return null;
    }
    IEnumerator SayHello()
    {
        HostAnimator.SetBool("isSlouchStandErect", false);
        HostAnimator.SetBool("isSayingHello", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(clip);
        Debug.Log("打完招呼");
        HostAnimator.SetBool("isSayingHello", false);
        HostAnimator.SetBool("isSlouchStandErect", true);
    }
    IEnumerator Teacher_Opening()
    {
        HostAnimator.SetBool("isSlouchStandErect", false);
        HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_Opening");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_Opening" + clip.length);
        Debug.Log("老師開完場");
        HostAnimator.SetBool("isStandingAndTalking", false);
        HostAnimator.SetBool("isSlouchStandErect", true);
    }
    IEnumerator Teacher_Introduction()
    {
        HostAnimator.SetBool("isSlouchStandErect", false);
        HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_Introduction");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_Introduction" + clip.length);
        Debug.Log("老師說完遊戲規則");
        HostAnimator.SetBool("isStandingAndTalking", false);
        HostAnimator.SetBool("isSlouchStandErect", true);
    }
    IEnumerator Host_RemindAllToSayRPS()
    {
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_RemindAllToSayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Host_RemindAllToSayRPS" + clip.length);
    }
    IEnumerator Teacher_AskUserWhichPic()
    {
        HostAnimator.SetBool("isSlouchStandErect", false);
        HostAnimator.SetBool("isAsking", true);

        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserName, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_AskUserWhichPic");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_AskUserWhichPic" + clip.length);
        Debug.Log("老師問完選圖");
        HostAnimator.SetBool("isAsking", false);
        HostAnimator.SetBool("isSlouchStandErect", true);
    }
    IEnumerator Teacher_RemindLv2()
    {
        HostAnimator.SetBool("isSlouchStandErect", false);
        HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_LV2Remind");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_LV2Remind" + clip.length);
        Debug.Log("老師提醒規則");
        HostAnimator.SetBool("isStandingAndTalking", false);
        HostAnimator.SetBool("isSlouchStandErect", true);
    }
    IEnumerator NPC_AskUserFirstColor()
    {
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+ "NPC_AskUserFirstColor");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
    }
    IEnumerator NPC_AskUserSecondColor()
    {
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_AskUserSecondColor");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
    }
    IEnumerator NPC_SameColor1()
    {
        GameDataManager.FlowData.UserColor = "紅色";
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SameColor1");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserColor, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_WinnerCanHaveColor1");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
    }
    IEnumerator NPC_SameColor2()
    {
        GameDataManager.FlowData.UserColor = "黃色";
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SameColor2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserColor, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_WinnerCanHaveColor2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
    }
    IEnumerator NPC_YouWinLv2()
    {
        npc.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_YouWinLv2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserColor, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        npc.animator.SetBool("isTalk2", false);
    }
    IEnumerator NPC_YouLoseLv2()
    {
        npc.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouLoseLv2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserColor, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_MyColor");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk2", false);
    }
    IEnumerator All_NPC_SayRPS()
    {
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Flower_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Red_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Green_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
        yield return new WaitForSeconds(1);
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
    IEnumerator UserNeedsACube()
    {
        yield return new WaitForSeconds(2);
        //開起綠球
        GreenTriggerBall.SetActive(true);
        //clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_YouCanTellTheTeacher1");//你可以跟老師說
        npc.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_YouCanTellTheTeacher2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk2", false);
        Debug.Log("NPC叫user要跟老師說少一個");
        yield return new WaitForSeconds(2);
        //clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_RaiseHandThenTell");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        //Debug.Log("主持人說明舉手之後跟老師說少一個");
        yield return new WaitUntil(() => _userRaiseHand);//*******************
        yield return new WaitForSeconds(3);
        GreenTriggerBall.SetActive(false);//Close GreenTriggerBall
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_YouWaitedToTalk");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("主持人說user有等老師，還有說自己少一個積木很棒");
        yield return new WaitForSeconds(3);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_GiveUserCube");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
    }
    IEnumerator UserChooseColor()
    {
        Color red, blue, green, yellow;
        //Color UserChoice1;
        //Color UserChoice2;
        red = new Color(1, (float)0.23, (float)0.23, 1);
        blue = new Color((float)0.203, (float)0.203, (float)0.87, 1);
        green = new Color(0, (float)0.706, 0, 1);
        yellow = new Color(1, 1, 0, 1);
        //User : 我想要藍色和紅色 [語音辨識] 
        //NPC: 蛤~我也想要紅色[故意跟user一樣]，那我們來猜拳
        UserChoice1 = Colors[0];//red
        UserChoice2 = Colors[3];//yellow
                                //UserChoice1 = Colors[1];
                                //UserChoice1 = Colors[2];
                                //UserChoice1 = Colors[3];
                                //UserChoice1 = green;
                                //UserChoice2 = yellow;
        GameDataManager.FlowData.UserColor = UserChoice2.ToString();
        //NPC: 蛤~我也想要紅色[故意跟user一樣]，那我們來猜拳
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserName, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);

        Debug.Log(UserChoice1 + "UserChoice1");
        Debug.Log(UserChoice2 + " + UserChoice2");
        Debug.Log(GameDataManager.FlowData.UserColor);
        yield return new WaitForSeconds(2);
    }
    IEnumerator OtherGroupBuildBlock()
    {
        //yield return LeftGroupBuildBlock();
        //yield return MiddleGroupBuildBlock();
        //yield return RightGroupBuildBlock();
        foreach (BlockEntity cube in cube_GA)
        {
            if (_RoundA)  //玩家回合
            {
                KidA.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                KidB.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                KidC.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                yield return new WaitForSeconds(7);
                _RoundA = false;
            }
            else
            {
                KidD.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                KidE.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                KidF.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                yield return new WaitForSeconds(7);
                _RoundA = true;
            }
        }
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

        for (int i = 0; i <randomArray.Length; i++)//randomArray.Length = 10
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
            if (UserChoice1 == cube.GetComponent<MeshRenderer>().material.color || UserChoice2 == cube.GetComponent<MeshRenderer>().material.color)
            {
                //Debug.Log(UserChoice1 + "UserChoice1");
                //Debug.Log(UserChoice2 + " + UserChoice2");
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
        Q1_cube.Add(GameObject.Find("BlueCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("RedCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("GreenCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("YellowCuboid3(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("BlueCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("RedCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("GreenCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("YellowCuboid(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("BlueCube(Clone)").GetComponent<BlockEntity>());
        Q1_cube.Add(GameObject.Find("RedCube(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q1_cube);
    }
    public void FindQ2Cubes()
    {
        Debug.Log("find Q2_cube!!!");
        Q2_cube.Add(GameObject.Find("BlueCuboid3_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("BlueCuboid3_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("YellowCube_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("GreenCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("RedCube2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("GreenCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("YellowCube_2(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("RedCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q2_cube.Add(GameObject.Find("RedCuboid_2(Clone)").GetComponent<BlockEntity>());//missing cube
        Q2_cube.Add(GameObject.Find("BlueCuboid(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q2_cube);
    }
    public void FindQ3Cubes()
    {
        Debug.Log("find Q3_cube!!!");
        Q3_cube.Add(GameObject.Find("BlueCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("YellowCube(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("BlueCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("RedCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("RedCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("BlueCuboid_3(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("GreenCube_1(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("BlueCuboid_4(Clone)").GetComponent<BlockEntity>());
        Q3_cube.Add(GameObject.Find("YellowCuboid3(Clone)").GetComponent<BlockEntity>());//missing cube
        Q3_cube.Add(GameObject.Find("GreenCube_2(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q3_cube);
    }
    public void FindQ4Cubes()
    {
        Debug.Log("find Q1_cube!!!");
        Q4_cube.Add(GameObject.Find("RedCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("GreenCuboid(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("RedCuboid_2(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("RedCuboid_3(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("YellowCube(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("YellowCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("BlueCuboid_1(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("GreenCube(Clone)").GetComponent<BlockEntity>());
        Q4_cube.Add(GameObject.Find("BlueCuboid_2(Clone)").GetComponent<BlockEntity>());//missing cube
        Q4_cube.Add(GameObject.Find("YellowCuboid_2(Clone)").GetComponent<BlockEntity>());
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
        else if(_RandomQuestion == 4)
        {
            Debug.Log("find Users_Q4cube!!!");
            Question_Cube.AddRange(GameObject.FindGameObjectsWithTag("Q4question"));
        }
    }
    public void KidsShouldPut()
    {
        foreach(GameObject cube in Question_Cube)
        {
            if(cube.GetComponent<QuestionCube>().CubeOrder == RecentOrder)
            {
                Debug.Log("CubeOrder: "+cube.GetComponent<QuestionCube>().CubeOrder+"RecentOrder: "+RecentOrder);
                KidShouldPut = cube;
                Debug.Log("Kid should put: "+ KidShouldPut);
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
                Debug.Log(MissingCube + " is missing....");
                GameObject.Find(MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = false;
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
                    Debug.Log(MissingCube + " is missing....");
                    GameObject.Find(MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = false;
                    
                }
                else
                {
                    RanNum++;
                    Debug.Log(RanNum);
                    Debug.Log(Final_Order[RanNum]);
                    MissingCube = Final_Order[RanNum].name;
                    Debug.Log(MissingCube + " is missing....");
                    GameObject.Find(MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = false;
                    
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
    public void CubeAns(BlockEntity cube)
    {
        cube.ToAnsLv2();
    }
    public void OtherGroupCubeAns(BlockEntity cube)
    {
        cube.OtherSroupToAns();
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
