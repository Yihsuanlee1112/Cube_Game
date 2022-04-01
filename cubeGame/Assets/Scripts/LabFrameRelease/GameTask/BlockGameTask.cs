using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using SpeechLib;

public class BlockGameTask : TaskBase
{
    private PlayerEntity player;
    private NPCEntity npc1;
    private Animator TeacherAnimator;
    private Animator KidA, KidB, KidC, KidD, KidE, KidF;
    private GameObject GreenTriggerBall;
    private List<BlockEntity> Q1_cube;
    private List<BlockEntity> Q2_cube;
    private List<BlockEntity> Q3_cube;
    private List<BlockEntity> Q4_cube;
    private List<BlockEntity> Cubes;
    private List<BlockEntity> cube_GA;
    private List<BlockEntity> cube_GB;
    private List<BlockEntity> cube_GC;
    //private List<GameObject> objectlist;
    private CameraEntity eyecamera;
    private Canvas mainSceneUI;
    //private Instantiate_Cube instantiate_Cube;
    //private RockPaperScissors RockPaperScissors;
    private AudioClip clip;
    private string MissingCube;
    private int RanNum;
    private string audioClipRootPath;
    private string focusName;

    public static bool _RoundA = true;
    public static int _ShowResult = 0;
    public static int _RandomQuestion = 0;
    public static bool _StartTobuild = false;
    public static bool _userChooseRPS = false;
    public static bool _userChooseQuestion = false;
    public static bool _userRaiseHand = false;
    public static bool _playerRound = true;//原本是false
    public bool _BlockFinished = false;
    public static bool _usersayhello = false;
    public static bool _talking = false;
    public static bool _npcremind = false;
    public static bool _eyetimerfinish = false;
    public static bool _waitforwatch = false;
    public static int answerindex = 0;
    public int TalkScore = 0;//recognizerEntity

    public override IEnumerator TaskInit()
    {
        GameEventCenter.AddEvent("CheckCube", CheckCube);
        GameEventCenter.AddEvent<BlockEntity>("CubeAns", CubeAns);
        GameEventCenter.AddEvent<BlockEntity>("OtherGroupCubeAns", OtherGroupCubeAns);
        GameEventCenter.AddEvent<string>("GetFocusName", GetFocusName);
        GameEventCenter.AddEvent("AddCubesToList", AddCubesToList);
        GameEventCenter.AddEvent("FindQ1Cubes", FindQ1Cubes);
        GameEventCenter.AddEvent("FindQ2Cubes", FindQ2Cubes);
        GameEventCenter.AddEvent("FindQ3Cubes", FindQ3Cubes);
        GameEventCenter.AddEvent("FindQ4Cubes", FindQ4Cubes);
        GameEventCenter.AddEvent("User_MissingOneCube", User_MissingOneCube);
        //GameEventCenter.AddEvent("NPC_Remind", NPC_Remind);
        //GameEventCenter.AddEvent("NPC_Remind2", NPC_Remind2);
        //載入MainSceneRes
        player = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().player;
        npc1 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().npc1;
        GreenTriggerBall = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().GreenTriggerBall;
        Q1_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q1_cube;
        Q2_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q2_cube;
        Q3_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q3_cube;
        Q4_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q4_cube;
        Cubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes;
        cube_GA = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GA;
        cube_GB= GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GB;
        cube_GC = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GC;
        //objectlist = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ObjectList;
        npc1.npchand = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC1_Hand;
        npc1.ChineseSpeechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ChineseSpeechClip;
        npc1.EnglishSpeechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().EnglishSpeechClip;
        npc1.animator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC1_animator;
        TeacherAnimator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().TeacherAnimator;
        KidA = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidA;
        KidB = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidB;
        KidC = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidC;
        KidD = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidD;
        KidE = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidE;
        KidF = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().KidF;
        //mainSceneUI = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().MainSceneUI;


        //VRIK初始化
        player.Init(GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().vrCamera);

        //NPC初始化
        npc1.EntityInit();

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
        
        //TeacherAnimator = GameObject.Find("teacher").GetComponent<Animator>();
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        GameObject.Find("ChooseQuestionCanvas").GetComponent<Canvas>().enabled = false;
        for (int i = 1; i < 5; i++)
        {
            GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = false;
        }
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
        
        //RandomQuestion = 4;//user選的題目

        //User跟其他三組猜拳
        //第一次 小花慢出(NPC生氣)

        GameEventCenter.DispatchEvent("FourPlayerRPS");//猜拳動畫
        GameObject.Find("FourPlayerChoose(Clone)/Canvas2").SetActive(true);
        GameObject.Find("FourPlayerChoose(Clone)/Canvas").SetActive(false);

        //大家一起說剪刀石頭布
        yield return All_NPC_SayRPS();
        _userChooseRPS = false;
        do
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        } while (!_userChooseRPS);

        yield return new WaitForSeconds(1);
        GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP2");//小花慢出
        GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP2");
       
        
        yield return new WaitForSeconds(3);
        npc1.animator.SetBool("isTalk", true);
        //不算～，你慢出，再猜一次！
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_TooSlow");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc1.animator.SetBool("isTalk", false);
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
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        //記得聽到布的時候，要一起出拳喔，不然贏了會不算喔，要再重猜一次
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_RemindRPSOnTime");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("主持人說再玩一次");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
        GameEventCenter.DispatchEvent("FourPlayerRPS");
        GameObject.Find("FourPlayerChoose(Clone)/Canvas").SetActive(true);
        GameObject.Find("FourPlayerChoose(Clone)/Canvas2").SetActive(false);
        
        //大家一起說剪刀石頭布
        yield return All_NPC_SayRPS();
        _userChooseRPS = false;
        do
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        } while (!_userChooseRPS);
        yield return new WaitForSeconds(3);
        
        for (int i=0; i<3; i++)
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
        yield return UserChooseQuestion();
        yield return new WaitForSeconds(1);
        //其他沒有贏的組，老師一組發一張圖案
        TeacherAnimator.SetBool("isSlouchStandErect", false);
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_GiveQuestionToOtherGroups");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        TeacherAnimator.SetBool("isStandingAndTalking", false);
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        yield return new WaitForSeconds(2);
        
        //User跟對面NPC猜拳
        GameEventCenter.DispatchEvent("InstatiateCube");
        GameEventCenter.DispatchEvent("AddCubesToList");
        GameEventCenter.DispatchEvent("User_MissingOneCube"); 
        //小花: 沒贏也沒關係，每一張圖我都喜歡
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Flower_ItsOkTolose");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        //小朋友，你們要記得按照數字順序輪流完成作品喔
        yield return Teacher_LV1Remind();
        //NPC說贏的先
        yield return NPC_WinnerFirst();
        GameEventCenter.DispatchEvent("TwoPlayerRPS");
        //NPC說剪刀石頭布
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        Debug.Log(clip.length);
        _userChooseRPS = false;
        do
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        } while (!_userChooseRPS);
        yield return new WaitForSeconds(2);
        
        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(2);

        //框架
        //猜拳之後，小星說你贏了你先
        yield return NPC_YouWin();
        //開始堆積木
       
        Debug.Log("Start:" + _StartTobuild);
       
        while (!_BlockFinished)
        { 
            _StartTobuild = true;
           // while (_StartTobuild) 
            //{
            //    yield return OtherGroupBuildBlock();
            //}
            if (_playerRound)  //玩家回合
            {
                if (Cubes[RanNum-1]._isChose)
                {
                    if(Cubes[RanNum - 1]._isChose && Cubes[RanNum]._isChose )
                    {
                    Debug.Log(Cubes[RanNum] + "was put.");
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
                Debug.Log("User touch Block"); 
                yield return new WaitUntil(() => !_playerRound);
            }
            else  //NPC回合
            {
                _npcremind = false;
                foreach (BlockEntity cube in Cubes)
                {
                    if (!cube._isChose)
                    {
                        npc1.animator.Play("Puzzle"); //npc拿積木
                        yield return new WaitForSeconds(7);
                        if (!_npcremind)
                        {   
                            Debug.Log("NPC putting Block");
                            GameEventCenter.DispatchEvent("CubeAns", cube);
                            _playerRound = true;
                        }
                        else
                        {
                            Debug.Log("wait for remind");
                            //clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_Remind");
                            //yield return new WaitForSeconds(clip.length);
                        }
                        break;
                    }
                }
            }
            GameEventCenter.DispatchEvent("CheckCube");
            _StartTobuild = false;
            yield return new WaitForSeconds(2);
        }
        
        yield return null;
    }

    public override IEnumerator TaskStop()
    {
        yield return null;
    }
    IEnumerator SayHello()
    {
        TeacherAnimator.SetBool("isSlouchStandErect", false);
        TeacherAnimator.SetBool("isSayingHello", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(clip);
        Debug.Log("打完招呼");
        TeacherAnimator.SetBool("isSayingHello", false);
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        yield return null;
    }
    IEnumerator Teacher_Opening()
    {
        TeacherAnimator.SetBool("isSlouchStandErect", false);
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_Opening");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_Opening" + clip.length);
        Debug.Log("老師開完場");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        yield return null;
    }
    IEnumerator Teacher_Introduction()
    {
        TeacherAnimator.SetBool("isSlouchStandErect", false);
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_Introduction");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_Introduction" + clip.length);
        Debug.Log("老師說完遊戲規則");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        yield return null;
    }
    IEnumerator Host_RemindAllToSayRPS()
    {
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_RemindAllToSayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Host_RemindAllToSayRPS" + clip.length);
        yield return null;
    }
    IEnumerator Teacher_AskUserWhichPic()
    {
        TeacherAnimator.SetBool("isSlouchStandErect", false);
        TeacherAnimator.SetBool("isAsking", true);
        SpVoice npcsay = new SpVoice();
        npcsay.Speak(GameDataManager.FlowData.UserName, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        yield return new WaitForSeconds(1.5f);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_AskUserWhichPic");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_AskUserWhichPic" + clip.length);
        Debug.Log("老師問完選圖");
        TeacherAnimator.SetBool("isAsking", false);
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        yield return null;
    }
    IEnumerator Teacher_LV1Remind()
    {
        TeacherAnimator.SetBool("isSlouchStandErect", false);
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Teacher_LV1Remind");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"Teacher_LV1Remind" + clip.length);
        Debug.Log("老師提醒規則");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
        TeacherAnimator.SetBool("isSlouchStandErect", true);
        yield return null;
    }
    IEnumerator NPC_YouWin()
    {
        npc1.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_YouWin");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath+"NPC_Rule" + clip.length);
        npc1.animator.SetBool("isTalk2", false);
        yield return null;
    }
    IEnumerator NPC_WinnerFirst()
    {
        npc1.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_WinnerFirst");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc1.animator.SetBool("isTalk", false);
        yield return null;
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
        yield return null;
    }
    IEnumerator UserChooseQuestion()
    {
        GameObject.Find("ChooseQuestionCanvas").GetComponent<Canvas>().enabled = true;
        for(int i = 1; i<5; i++)
        {
            GameObject.FindGameObjectWithTag("Q" + i).GetComponent<BoxCollider>().enabled = true;
        }
        
        while (!_userChooseQuestion)
        {
            Debug.Log("User choose Question");
            yield return new WaitUntil(() => _userChooseQuestion);
        }
        yield return new WaitForSeconds(5);
        GameObject.Find("ChooseQuestionCanvas").GetComponent<Canvas>().enabled = false;
        yield return null;
    }
    IEnumerator UserNeedsACube()
    {
        yield return new WaitForSeconds(2);
        //開起綠球
        GreenTriggerBall.SetActive(true);
        //clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_YouCanTellTheTeacher1");//你可以跟老師說
        npc1.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"NPC_YouCanTellTheTeacher2");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc1.animator.SetBool("isTalk2", false);
        Debug.Log("NPC叫user要跟老師說少一個");
        yield return new WaitForSeconds(2);
        clip = Resources.Load<AudioClip>(audioClipRootPath+"Host_RaiseHandThenTell");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("主持人說明要跟小花一樣，舉手之後跟老師說少一個");
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
        yield return null;
    }
    IEnumerator OtherGroupBuildBlock()
    {
        yield return LeftGroupBuildBlock();
        yield return MiddleGroupBuildBlock();
        yield return RightGroupBuildBlock();
        yield return null;
    }
    IEnumerator LeftGroupBuildBlock()
    {
        Debug.Log("Leftgroupstart");
        foreach (BlockEntity cube in cube_GA)//LeftDesk
        {
            if (_RoundA)  //玩家回合
            {
                //KidA.Play("Puzzle");
                KidB.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                //KidC.Play("Puzzle");
                yield return new WaitForSeconds(7);
                _RoundA = false;
            }
            else
            {
                //KidD.Play("Puzzle");
                KidE.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                //KidF.Play("Puzzle");
                yield return new WaitForSeconds(7);
                _RoundA = true;
            }
        }
        yield return null;
    }
    IEnumerator MiddleGroupBuildBlock()
    {
        Debug.Log("Middlegroupstart");
        foreach (BlockEntity cube in cube_GA)//MiddleDesk
        {
            if (_RoundA)  //玩家回合
            {
                //KidA.Play("Puzzle");
                //KidB.Play("Puzzle");
                KidC.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                yield return new WaitForSeconds(7);
                _RoundA = false;
            }
            else
            {
                //KidD.Play("Puzzle");
                //KidE.Play("Puzzle");
                KidF.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                yield return new WaitForSeconds(7);
                _RoundA = true;
            }
        }
        yield return null;
    }
    IEnumerator RightGroupBuildBlock()
    {
        Debug.Log("Rightgroupstart");
        foreach (BlockEntity cube in cube_GA)//RightDesk
        {
            if (_RoundA)  //玩家回合
            {
                KidA.Play("Puzzle");
                //KidB.Play("Puzzle");
                //KidC.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                yield return new WaitForSeconds(7);
                _RoundA = false;
            }
            else
            {
                KidD.Play("Puzzle");
                //KidE.Play("Puzzle");
                //KidF.Play("Puzzle");
                GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                yield return new WaitForSeconds(7);
                _RoundA = true;
            }
        }
        yield return null;
    }
    public void CheckCube()
    {
        foreach (BlockEntity item in Cubes)
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
    public void AddCubesToList()
    {
        if(_RandomQuestion == 1)
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
        else if(_RandomQuestion == 2)
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
        Q1_cube.Add(GameObject.Find("Q1BlueCube(Clone)").GetComponent<BlockEntity>());//missing cube
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
        Q2_cube.Add(GameObject.Find("Q2RedCuboid_2(Clone)").GetComponent<BlockEntity>());//missing cube
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
        Q3_cube.Add(GameObject.Find("Q3YellowCuboid3(Clone)").GetComponent<BlockEntity>());//missing cube
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
        Q4_cube.Add(GameObject.Find("Q4BlueCuboid_2(Clone)").GetComponent<BlockEntity>());//missing cube
        Q4_cube.Add(GameObject.Find("Q4YellowCuboid_2(Clone)").GetComponent<BlockEntity>());
        //Cubes.AddRange(Q4_cube);
    }
    public void User_MissingOneCube()//odd numbers
    {
        RanNum = Random.Range(4, 7);
        if (RanNum % 2 == 0)
        {
            Debug.Log(RanNum);
            Debug.Log(Cubes[RanNum]);
            MissingCube = Cubes[RanNum].name;
            Debug.Log(MissingCube+ " is missing....");
            GameObject.Find(MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = false;
            
        }
        else
        {
            RanNum += 1;
            Debug.Log(RanNum);
            Debug.Log(Cubes[RanNum]);
            //Cubes[RanNum].gameObject.tag = MissingCube;
            MissingCube = Cubes[RanNum].name;
            Debug.Log(MissingCube +" is missing....");
            GameObject.Find(MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find(MissingCube).GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    public void TakeObject()//拼圖的氣球
    {
        var index = 2;/*Random.Range(0, 3);*/
        //npc.NPCTakeObject(objectlist[index]);
    }
    public void PutObject()
    {
        npc1.NPCPutObject(GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().PutPosition);
    }
    public void CubeAns(BlockEntity cube)
    {
        cube.ToAns();
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
