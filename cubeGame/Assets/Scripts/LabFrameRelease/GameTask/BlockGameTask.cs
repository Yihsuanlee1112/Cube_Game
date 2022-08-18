using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using SpeechLib;
using UnityEngine.UI;

public class BlockGameTask : TaskBase
{
    private GameObject VRCamera;
    private PlayerEntity player;
    private GameObject userLeftHandTrigger;
    private GameObject userRightHandTrigger;
    private List<GameObject> Question_Cube;
    private NPCEntity npc;
    private Animator HostAnimator, TeacherAnimator;
    private Animator XiaoHua, XiaoMei, Green, Yoyo, Red, Hat;
    private Animator TeacherAnimator2, npc2, XiaoHua2, XiaoMei2, Green2, Yoyo2, Hat2, Red2;
    private GameObject GreenTriggerBall;
    public static GameObject RedTriggerBall;
    private List<BlockEntity> Q1_cube;
    private List<BlockEntity> Q2_cube;
    private List<BlockEntity> Q3_cube;
    private List<BlockEntity> Q4_cube;
    private List<BlockEntity> Cubes;
    private List<BlockEntity> AllCubes;
    private List<BlockEntity> Final_Order;
    private List<BlockEntity> cube_GA;
    private List<BlockEntity> cube_GB;
    private List<BlockEntity> cube_GC;
    private List<Color> Colors;
    //private List<GameObject> objectlist;
    private EyeCameraEntity eyecamera;                  // 專門看眼動泡泡的camera
    public static GameObject eyebubble { get; set; }

    private GameObject Coin;
    private GameObject Ruby;
    private GameObject Heart;

    private List<int> textOnQuestion;
    private List<GameObject> QuestionOrder;
    //private Instantiate_Cube instantiate_Cube;
    //private RockPaperScissors RockPaperScissors;
    private AudioClip clip, XiaoMeiColor;
    private string MissingCube;
    private int RanNum;
    private string audioClipRootPath;
    private string focusName;
    private string XiaoMeiMissingCube;
    public static GameObject KidShouldPut;
    public static int RecentOrder = 1;
    public static int RemindCelebrate = 0;
    public static int RemindRaiseHand = 0;
    public static bool _RoundA = true;
    public static int _ShowResult = 0;
    public static int _RandomQuestion = 0;
    public static bool _StartTobuild = false;
    public static bool _userChooseRPS = false;
    public static bool _userChooseQuestion = false;
    public static bool _userRaiseHand = false;
    public static bool _userCelebrate = false;
    public static bool _userSpeekToTeacher = false;
    public static bool _playerRound = true;//原本是false
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
    public override IEnumerator TaskInit()
    {
        GameEventCenter.AddEvent("CheckCube", CheckCube);
        GameEventCenter.AddEvent<BlockEntity>("CubeToAns", CubeToAns);
        GameEventCenter.AddEvent<BlockEntity>("CubeOnAns", CubeOnAns);
        GameEventCenter.AddEvent<BlockEntity>("OtherGroupCubeAns", OtherGroupCubeAns);
        GameEventCenter.AddEvent<string>("GetFocusName", GetFocusName);
        GameEventCenter.AddEvent("AddCubesToList", AddCubesToList);
        GameEventCenter.AddEvent("FindQ1Cubes", FindQ1Cubes);
        GameEventCenter.AddEvent("FindQ2Cubes", FindQ2Cubes);
        GameEventCenter.AddEvent("FindQ3Cubes", FindQ3Cubes);
        GameEventCenter.AddEvent("FindQ4Cubes", FindQ4Cubes);
        GameEventCenter.AddEvent("Find_QuestionCubes", Find_QuestionCubes);
        GameEventCenter.AddEvent("KidsShouldPut", KidsShouldPut);
        GameEventCenter.AddEvent("NumOnQuestion", NumOnQuestion);
        GameEventCenter.AddEvent("PutInRightOrder", PutInRightOrder);
        GameEventCenter.AddEvent("User_MissingOneCube", User_MissingOneCube);
        //GameEventCenter.AddEvent("TeacherGiveCube", TeacherGiveCube);
        //GameEventCenter.AddEvent("NPC_Remind", NPC_Remind);
        //GameEventCenter.AddEvent("NPC_Remind2", NPC_Remind2);
        //載入MainSceneRes
        player = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().player;
        userLeftHandTrigger = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().userLeftHandTrigger;
        userRightHandTrigger = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().userRightHandTrigger;
        npc = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().npc;
        GreenTriggerBall = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().GreenTriggerBall;
        RedTriggerBall = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().RedTriggerBall;
        Question_Cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Question_Cube;
        Q1_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q1_cube;
        Q2_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q2_cube;
        Q3_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q3_cube;
        Q4_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q4_cube;
        Cubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes;
        AllCubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().AllCubes;
        Final_Order = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Final_Order;
        QuestionOrder = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().QuestionOrder;
        textOnQuestion = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().textOnQuestion;
        cube_GA = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GA;
        cube_GB = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GB;
        cube_GC = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GC;
        //objectlist = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ObjectList;
        npc.npc_hand = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC_Hand;
        npc.ChineseSpeechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ChineseSpeechClip;
        npc.EnglishSpeechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().EnglishSpeechClip;
        npc.animator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC_animator;
        HostAnimator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().HostAnimator;
        TeacherAnimator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().TeacherAnimator;
        XiaoHua = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoHua;
        XiaoMei = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoMei;
        Green = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Green;
        Yoyo = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Yoyo;
        Red = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Red;
        Hat = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Hat;
        Colors = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Colors;
        //TeacherAnimator2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().TeacherAnimator2;
        //npc2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().npc2;
        //XiaoHua2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoHua2;
        //XiaoMei2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoMei2;
        //Hat2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Hat2;
        //Yoyo2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Yoyo2;
        //Green2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Green2;
        //Red2 = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Red2;
        Coin = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Coin;
        Ruby = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Ruby;
        Heart = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Heart;

        VRCamera = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().VRCamera;
        //mainSceneUI = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().MainSceneUI;


        //VRIK初始化
        player.GetComponent<PlayerEntity>().Init(VRCamera);

        //NPC初始化
        npc.EntityInit();

        //設定VR中可看到UI
        //yield return new WaitForSeconds(0.5f);
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

        yield return null;
    }

    public override IEnumerator TaskStart()
    {
        GameObject ChooseQuestionCanvas = GameObject.Find("ChooseQuestionCanvas");
        GameObject XiaoMeiRaiseHandPic = GameObject.Find("Schematics/UserRightSightCanvas/XiaoMeiRaiseHandPic");
        GameObject Question1PicWithNum = GameObject.Find("Schematics/UserLeftSightCanvas/QuestionPicsWithNum/Question1PicWithNum");
        
        ChooseQuestionCanvas.SetActive(true);
        XiaoMeiRaiseHandPic.SetActive(true);
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
        yield return new WaitForSeconds(1.5f);
        //老師說明遊戲規則
        yield return Teacher_Introduction();
        yield return new WaitForSeconds(1.5f);
        //主持人請大家一起說剪刀石頭布
        yield return Host_RemindAllToSayRPS();
        yield return new WaitForSeconds(0.5f);

        //語音辨識

        //User跟其他三組猜拳
        //第一次 小美慢出(NPC生氣)
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1);
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
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP2");//小美慢出
        GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP2");
        yield return new WaitForSeconds(1);

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
        yield return new WaitForSeconds(1);

        //第二次 User贏
        HostAnimator.SetBool("isStandingAndTalking", true);
        //記得聽到布的時候，要一起出拳喔，不然贏了會不算喔，要再重猜一次
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RemindRPSOnTime");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("主持人說再玩一次");
        HostAnimator.SetBool("isStandingAndTalking", false);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1);
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
        yield return new WaitForSeconds(2);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("Result").SetActive(false);
        }
        for (int i = 0; i < 4; i++)
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

        GameEventCenter.DispatchEvent("InstatiateCube");
        GameEventCenter.DispatchEvent("Find_QuestionCubes");
        GameEventCenter.DispatchEvent("AddCubesToList");
        GameEventCenter.DispatchEvent("NumOnQuestion");
        GameEventCenter.DispatchEvent("CheckOrder");//QuestionCube
        GameEventCenter.DispatchEvent("PutInRightOrder");
        GameEventCenter.DispatchEvent("User_MissingOneCube");

        GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        GameEventCenter.DispatchEvent("InstantiateQuestion");
        
        //GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        //GameObject QuestionPicWithNum = Instantiate(Question(Clone));
        //QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform);

        cube_GB[0].GetComponent<MeshRenderer>().enabled = false;//小美少第一顆積木

        //Debug.Log(GameObject.Find("Parents/Q1_Parent").transform.GetChild(1));
        //Debug.Log(GameObject.Find("Parents/Q1_Parent").transform.GetChild(1).position);
        foreach (BlockEntity cube in AllCubes)
        {
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;//*****
            //cube.GetComponent<MeshRenderer>().enabled = false;//*******
            cube.GetComponent<BoxCollider>().isTrigger = true;

        }

        //小美: 沒贏也沒關係，每一張圖我都喜歡
        //KidA.Play("坐在椅子上說話");
        XiaoMei.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_ItsOkTolose");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        XiaoMei.SetBool("isTalk2", false);
        //KidA.SetBool("isDefault", true);
        //小朋友，你們要記得按照數字順序輪流完成作品喔
        yield return Teacher_LV1Remind();
        yield return new WaitForSeconds(2);
        //NPC說贏的先
        //npc.animator.SetBool("isDefault", false);
        yield return NPC_WinnerFirst();
        yield return new WaitForSeconds(2);
        _userChooseRPS = false;
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1);
        GameEventCenter.DispatchEvent("TwoPlayerRPS");
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas").SetActive(true);
        GameObject.Find("TwoPlayerChoose(Clone)/Canvas2").SetActive(false);
        //NPC說剪刀石頭布
        npc.animator.SetBool("isPlayingRPS", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_SayRPS");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isPlayingRPS", false);

        //Debug.Log(clip.length);
        //_userChooseRPS = false;
        do
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        } while (!_userChooseRPS);
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        //npc.animator.SetBool("isDefault", true);
        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(1);
        //npc.animator.SetBool("isDefault", true);
        //框架
        //猜拳之後，小星說你贏了你先
        yield return NPC_YouWin();
        yield return new WaitForSeconds(2);

        //小美跟老師拿積木
        yield return XiaoMeiNeedsCube(XiaoMeiRaiseHandPic);
        //cube_GB[0].GetComponent<MeshRenderer>().enabled = true;//小美少第一顆積木
        
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_LetsStart");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        
        foreach (BlockEntity cube in AllCubes)
        {
            //cube.getcomponent<meshrenderer>().enabled = true;//***********
            cube.GetComponent<BoxCollider>().isTrigger = false;
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        //missingcube 
        //gameobject.find(missingcube).getcomponent<rigidbody>().constraints = rigidbodyconstraints.freezeall;
        //gameobject.find(missingcube).getcomponent<meshrenderer>().enabled = false;
        //gameobject.find(missingcube).getcomponent<boxcollider>().enabled = false;


        //開始堆積木
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = true;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = true;

        if (Final_Order[0]._isUserColor)
        {
            _playerRound = true;
        }

        while (!_BlockFinished)
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
                        yield return UserNeedsACube(XiaoMeiRaiseHandPic);
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
                //_npcremind = false;
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

        //while (_StartTobuild)
        //{
        //    yield return OtherGroupBuildBlock();
        //}
        userRightHandTrigger.GetComponent<BoxCollider>().enabled = false;
        userLeftHandTrigger.GetComponent<BoxCollider>().enabled = false;

        // 結束後語音
        //小星: 耶!我們完成了!（小星雙手舉高）等待2秒
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

        //if didn't raise hand, RemindCelebrate =1
        //主持人（教學）: 你和小星一起合作完成了積木圖案，很棒喔！小星很開心，想跟你擊掌! （等待2秒）
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_CelebrateLv1");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        
        //wait2Sec
        _userCelebrate = false;
        while (RemindCelebrate < 2)
        {
            Debug.Log(RemindCelebrate);
            GameEventCenter.DispatchEvent("Text2sec_isEnabled", true); // 2秒計時器打開
            GameEventCenter.DispatchEvent("Timer2secReset");
            yield return new WaitUntil(() =>
            {
                Debug.Log(RemindCelebrate);
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
                Debug.Log(RemindCelebrate);
                GameEventCenter.DispatchEvent("Text2sec_isEnabled", false); // 2秒計時器關閉
                GameEventCenter.DispatchEvent("Timer2secReset");
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
                GameEventCenter.DispatchEvent("Text2sec_isEnabled", false); // 2秒計時器關閉
                GameEventCenter.DispatchEvent("Timer2secReset");
                if (RemindCelebrate < 1)
                {
                    Debug.Log(RemindCelebrate);
                    //主持人（提醒1）
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_CelebrateRemind");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isClapHandInSpace", true);
                    yield return new WaitForSeconds(3);
                    npc.animator.SetBool("isClapHandInSpace", false);
                }
                RemindCelebrate++;
                Debug.Log(RemindCelebrate);

            }
            RedTriggerBall.SetActive(false);
            GameEventCenter.DispatchEvent("Text2sec_isEnabled", false); // 2秒計時器關閉
            GameEventCenter.DispatchEvent("Timer2secReset");
        }
        Debug.Log(RemindCelebrate);
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
        TeacherAnimator.SetBool("isSayHi", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(clip);
        Debug.Log("打完招呼");
        HostAnimator.SetBool("isSayingHello", false);
        TeacherAnimator.SetBool("isSayHi", false);
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
    IEnumerator Teacher_LV1Remind()
    {
        TeacherAnimator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_LV1Remind");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log(audioClipRootPath + "Teacher_LV1Remind" + clip.length);
        Debug.Log("老師提醒規則");
        TeacherAnimator.SetBool("isTalk", false);
        yield return null;
    }
    IEnumerator NPC_YouWin()
    {
        //npc.animator.Play("坐在椅子上說話");
        npc.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouWin");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk2", false);
        //npc.animator.SetBool("isDefault", true);
        yield return null;
    }
    IEnumerator NPC_WinnerFirst()
    {
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_WinnerFirst");
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
        yield return new WaitForSeconds(2);
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
    //IEnumerator TeacherMoveToUser()
    //{
    //    TeacherAnimator.SetBool("isTakeCube", true);
    //    TeacherAnimator.SetBool("isTakeCubeWalking", true);
    //    yield return new WaitForSeconds(3.5f);
    //    testAni.TeacherMoveToUser = true;
    //    TeacherAnimator.SetBool("isPutingCube", true);
    //}
    IEnumerator UserNeedsACube(GameObject XiaoMeiRaiseHandPic)
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

        HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTellLikeXiaoMei");
        GameAudioController.Instance.PlayOneShot(clip);
        XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
        yield return new WaitForSeconds(clip.length);
        HostAnimator.SetBool("isStandingAndTalking", false);
        Debug.Log("主持人說明要跟小美一樣，舉手之後跟老師說少一個");
        //wait5sec
        while (RemindRaiseHand < 3)
        {
            //Debug.Log(RemindRaiseHand);
            GameEventCenter.DispatchEvent("Text5sec_isEnabled", true); // 5秒計時器打開
            GameEventCenter.DispatchEvent("Timer5secReset");
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

            //if (_userRaiseHand && _userSpeekToTeacher && !_is5secTimeUp) // user有舉手 有說話
            //{
            //    //Debug.Log(RemindRaiseHand);
            //    GameEventCenter.DispatchEvent("Text5sec_isEnabled", false); // 5秒計時器關閉
            //    GameEventCenter.DispatchEvent("Timer5secReset");
            //    _userRaiseHand = false;
            //    npc.animator.SetBool("isTalk2", true);
            //    clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher2");
            //    GameAudioController.Instance.PlayOneShot(clip);
            //    yield return new WaitForSeconds(clip.length);
            //    npc.animator.SetBool("isTalk2", false);
            //    Debug.Log("NPC叫user要跟老師說少一個");
            //    yield return new WaitForSeconds(2);
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
            //else if (!_is5secTimeUp) // 5秒內
            if (!_is5secTimeUp) // 5秒內
            {
                //Debug.Log(RemindRaiseHand);
                if (_userRaiseHand && _userSpeekToTeacher) // user有舉手 有說話
                {
                    //Debug.Log(RemindRaiseHand);
                    GameEventCenter.DispatchEvent("Text5sec_isEnabled", false); // 5秒計時器關閉
                    GameEventCenter.DispatchEvent("Timer5secReset");
                    _userRaiseHand = false;
                    _userSpeekToTeacher = false;

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
                    GameEventCenter.DispatchEvent("Text5sec_isEnabled", false); // 5秒計時器關閉
                    GameEventCenter.DispatchEvent("Timer5secReset");
                    //小星提示:你可以舉手，然後跟老師說你少了什麼顏色的積木
                    npc.animator.SetBool("isTalk", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher2");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isTalk", false);
                    Debug.Log("NPC叫user要跟老師說少一個");
                    yield return new WaitForSeconds(2);
                    //_userSpeekToTeacher = false;                    
                    RemindRaiseHand++;
                    Debug.Log(RemindRaiseHand);
                }
                else if (_userRaiseHand && !_userSpeekToTeacher)// 有舉手沒說話
                //else if(RemindRaiseHand == 1)
                {
                    Debug.Log("主持人（提醒）");
                    GameEventCenter.DispatchEvent("Text5sec_isEnabled", false); // 5秒計時器關閉
                    GameEventCenter.DispatchEvent("Timer5secReset");
                    //老師問USER什麼事?
                    TeacherAnimator.SetBool("isTalkingToXiaoMei", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_AskUser");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length + 2);
                    TeacherAnimator.SetBool("isTalkingToXiaoMei", false);
                    //主持人（提醒1）:我們在上課的時候，遇到問題就可以像小美一樣，先舉手等待老師，然後跟老師說
                    HostAnimator.SetBool("isPointing", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTell");
                    GameAudioController.Instance.PlayOneShot(clip);
                    XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
                    yield return new WaitForSeconds(clip.length);
                    HostAnimator.SetBool("isPointing", false);
                    Debug.Log("主持人說明要跟小美一樣，舉手之後跟老師說少一個");
                    //_userRaiseHand = false;
                    RemindRaiseHand++;
                    Debug.Log(RemindRaiseHand);
                }

            }
            else if (!_userRaiseHand && !_userSpeekToTeacher && _is5secTimeUp) //過了5秒 沒舉手 沒說話
            {
                //Debug.Log(RemindRaiseHand);
                GameEventCenter.DispatchEvent("Text5sec_isEnabled", false); // 5秒計時器關閉
                GameEventCenter.DispatchEvent("Timer5secReset");
                if (RemindRaiseHand == 0)
                {
                    //小星提示:你可以舉手，然後跟老師說你少了什麼顏色的積木
                    npc.animator.SetBool("isTalk", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_YouCanTellTheTeacher2");
                    GameAudioController.Instance.PlayOneShot(clip);
                    yield return new WaitForSeconds(clip.length);
                    npc.animator.SetBool("isTalk", false);
                    Debug.Log("NPC叫user要跟老師說少一個");
                    yield return new WaitForSeconds(2);
                }
                else if (RemindRaiseHand == 1)
                {
                    Debug.Log("主持人（提醒）");
                    //主持人（提醒1）:我們在上課的時候，遇到問題就可以像小美一樣，先舉手等待老師，然後跟老師說
                    HostAnimator.SetBool("isPointing", true);
                    clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTell");
                    GameAudioController.Instance.PlayOneShot(clip);
                    XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
                    yield return new WaitForSeconds(clip.length);
                    HostAnimator.SetBool("isPointing", false);
                    Debug.Log("主持人說明要跟小美一樣，舉手之後跟老師說少一個");

                }
                RemindRaiseHand++;
                Debug.Log(RemindRaiseHand);
            }
            GameEventCenter.DispatchEvent("Text5sec_isEnabled", false); // 5秒計時器關閉
            GameEventCenter.DispatchEvent("Timer5secReset");
        }

        RemindRaiseHand = 0;
        XiaoMeiRaiseHandPic.SetActive(false);
        GreenTriggerBall.SetActive(false);//Close GreenTriggerBall
        //yield return TeacherGiveCube(UserNewCube);
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
        TeacherAnimator.SetBool("isTakeCubeToUser", true);
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
        //testAni.TeacherMoveBackFromUser = true;
        //TeacherAnimator.SetBool("isTakeCube", false);
        //TeacherAnimator.SetBool("isTakeCubeWalking", false);
        yield return new WaitForSeconds(7);
        
        UserNewCube.GetComponent<Animator>().SetBool("isToUser", false);
        TeacherAnimator.SetBool("isTakeCubeToUser", false);
        yield return new WaitForSeconds(2);
        UserNewCube.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("老師走回去");
        //yield return new WaitForSeconds(5);
        //testAni.TeacherMoveBackFromUser = false;
        yield return null;
    }
    IEnumerator XiaoMeiNeedsCube(GameObject XiaoMeiRaiseHandPic)
    {
        GameObject TeacherGiveXiaoMeiCube = null;
        if (_RandomQuestion == 1)
        {
            XiaoMeiMissingCube = "藍色";//Q3
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_BlueCube");
        }
        else if (_RandomQuestion == 2)
        {
            XiaoMeiMissingCube = "紅色";//Q4
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/RedCuboid");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_RedCube");
        }
        else if (_RandomQuestion == 3)
        {
            XiaoMeiMissingCube = "藍色";//Q1
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid3");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_BlueCube");
        }
        else if (_RandomQuestion == 4)
        {
            XiaoMeiMissingCube = "藍色";//Q2
            TeacherGiveXiaoMeiCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid3");
            XiaoMeiColor = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_BlueCube");
        }
        XiaoMei.SetBool("isRaiseHand", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_CallTeacher");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        XiaoMei.SetBool("isRaiseHand", false);
        yield return new WaitForSeconds(0.5f);
        TeacherAnimator.SetBool("isTalkingToXiaoMei", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Teacher_AskXiaoMei");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length + 2);
        TeacherAnimator.SetBool("isTalkingToXiaoMei", false);
        yield return new WaitForSeconds(2);
        XiaoMei.SetBool("isRaiseHandAndTalkToTeacher", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "XiaoMei_INeedACube");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(XiaoMeiColor);
        yield return new WaitForSeconds(XiaoMeiColor.length);
        XiaoMei.SetBool("isRaiseHandAndTalkToTeacher", false);
        yield return new WaitForSeconds(2);

        HostAnimator.SetBool("isPointing", true);
        //HostAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "Host_RaiseHandThenTellLikeXiaoMei");
        XiaoMeiRaiseHandPic.GetComponent<RawImage>().enabled = true;
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        HostAnimator.SetBool("isPointing", false);
        //HostAnimator.SetBool("isStandingAndTalking", false);
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
        TeacherGiveXiaoMeiCube.GetComponent<MeshRenderer>().enabled = false;
        cube_GB[0].GetComponent<MeshRenderer>().enabled = true;//小美少第一顆積木
        yield return new WaitForSeconds(7);
        TeacherAnimator.SetBool("isTakeCubeToXiaoMei", false);
        TeacherGiveXiaoMeiCube.GetComponent<Animator>().SetBool("isToXiaoMei", false);
        yield return new WaitForSeconds(2);
        TeacherGiveXiaoMeiCube.GetComponent<MeshRenderer>().enabled = true;
        //testAni.TeacherMoveToXiaoMei = false;
        Debug.Log("老師走到小美");
        //testAni.TeacherMoveBackFromXiaoMei = true;
        //TeacherAnimator.SetBool("isTakeCube", false);
        //TeacherAnimator.SetBool("isTakeCubeWalking", false);
        //TeacherAnimator.SetBool("isPutingCube", false);
        //Debug.Log("老師走回去");
        //yield return new WaitForSeconds(5);
        //testAni.TeacherMoveBackFromXiaoMei = false;
        yield return new WaitForSeconds(2);
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
                Debug.Log("cube.name: " + cube.name);
                int delStr = cubeName.IndexOf("(Clone)");
                if (delStr >= 0)
                {
                    cubeName = cubeName.Remove(delStr);
                }

                if (questionOrder.name == cubeName)
                {
                    Final_Order.Add(cube);
                    cube._isOnUserTable = true;
                    //Debug.Log("questionOrder.name: " + questionOrder.name);
                    //Debug.Log("cube.name: " + cube.name);
                }
            }
        }
        for (int userCube = 0; userCube < 10; userCube++)//users cube -> is user color
        {
            if (userCube % 2 == 0)
                Cubes[userCube]._isUserColor = true;
        }
    }
    public void NumOnQuestion()
    {
        for (int i = 0; i < 10; i++)
        {
            textOnQuestion.Add(int.Parse(GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)").transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text));
            //textOnQuestion.Sort();
            QuestionOrder.Add(GameObject.Find("Parents/Q" + _RandomQuestion + "_Parent/Question(Clone)").transform.GetChild(i).gameObject);

            QuestionOrder.Sort((x, y) => {
                return int.Parse(x.transform.GetChild(0).GetChild(0).GetComponent<Text>().text).CompareTo(int.Parse(y.transform.GetChild(0).GetChild(0).GetComponent<Text>().text));
            });
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
    public void FindQ1Cubes()
    {
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
    public void User_MissingOneCube()//odd numbers
    {
        RanNum = Random.Range(6, 8);
        if (RanNum % 2 == 0)
        {
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
            RanNum += 1;
            Debug.Log(RanNum);
            Debug.Log(Final_Order[RanNum]);
            //Cubes[RanNum].gameObject.tag = MissingCube;
            MissingCube = Final_Order[RanNum].name;
            Debug.Log("Q" + _RandomQuestion + "_CubeParent/" + MissingCube + " is missing....");
            GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Q" + _RandomQuestion + "_CubeParent/" + MissingCube).GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void KidsShouldPut()
    {
        foreach (GameObject cube in Question_Cube)
        {
            if (cube.GetComponent<QuestionCube>().CubeOrder == RecentOrder)
            {
                KidShouldPut = cube;
                Debug.Log("Kid should put: " + KidShouldPut);
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
        cube.ToAnsLv1();
    }
    public void CubeOnAns(BlockEntity cube)
    {
        cube.OnAnsLv1();
    }
    public void OtherGroupCubeAns(BlockEntity cube)
    {
        cube.OtherGroupToAns();
    }
    IEnumerator TeacherGiveCube(GameObject UserNewCube)
    {
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
        else if(GameObject.Find(MissingCube).transform.localScale == Cube2Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCube2");
        }
        else if(GameObject.Find(MissingCube).transform.localScale == CuboidScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCuboid");
        }
        else if(GameObject.Find(MissingCube).transform.localScale == Cuboid3Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[0])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/RedCuboid3");
        }
        //blue
        else if(GameObject.Find(MissingCube).transform.localScale == CubeScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[1])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/BlueCube");
        }
        else if(GameObject.Find(MissingCube).transform.localScale == CuboidScale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[1])
        {
            UserNewCube = GameObject.Find("TeacherWithCubes/teacher/BlueCuboid");
        }
        else if(GameObject.Find(MissingCube).transform.localScale == Cuboid3Scale && GameObject.Find(MissingCube).GetComponent<MeshRenderer>().material.color == Colors[1])
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
        yield return UserNewCube;
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
