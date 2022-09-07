using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using SpeechLib;
using UnityEngine.UI;


public class BlockGameTask_Mono : TaskBase
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
        GameEventCenter.AddEvent<BlockEntity>("OtherGroupCubeAns_Mono", OtherGroupCubeAns_Mono);
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
        //GameEventCenter.AddEvent("User_MissingOneCube", User_MissingOneCube);
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
        GameObject.Find("-- MainSceneUI --/UserCanSee").SetActive(false);
        GameObject ChooseQuestionCanvas = GameObject.Find("ChooseQuestionCanvas");
        //GameObject XiaoMeiRaiseHandPic = GameObject.Find("Schematics/UserRightSightCanvas/XiaoMeiRaiseHandPic");
        GameObject Question1PicWithNum = GameObject.Find("Schematics/UserLeftSightCanvas/QuestionPicsWithNum/Question1PicWithNum");

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

        GameEventCenter.DispatchEvent("InstatiateCube_Mono");
        GameEventCenter.DispatchEvent("Find_QuestionCubes");
        GameEventCenter.DispatchEvent("AddCubesToList");
        GameEventCenter.DispatchEvent("NumOnQuestion");
        GameEventCenter.DispatchEvent("CheckOrder");//QuestionCube
        GameEventCenter.DispatchEvent("PutInRightOrder");
        GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        GameEventCenter.DispatchEvent("InstantiateQuestion_Mono");

        foreach (BlockEntity cube in AllCubes)
        {
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;//*****
            //cube.GetComponent<MeshRenderer>().enabled = false;//*******
            cube.GetComponent<BoxCollider>().isTrigger = true;

        }

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

        foreach (BlockEntity cube in AllCubes)
        {
            //cube.getcomponent<meshrenderer>().enabled = true;//***********
            cube.GetComponent<BoxCollider>().isTrigger = false;
            cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

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
                //if (Final_Order[RanNum - 1]._isChose && Final_Order[RanNum].GetComponent<BoxCollider>().enabled == false)//檢查前一個積木，
                //{
                //    if (Final_Order[RanNum]._isChose) //不見的也放過了
                //    {
                //        Debug.Log(Final_Order[RanNum] + "was put.");
                //        Debug.Log("User turn");
                //        yield return new WaitUntil(() => !_playerRound);
                //    }

                //    else
                //    {
                //        //user要跟老師說少一個，wait until 叫老師
                //        //yield return UserNeedsACube(XiaoMeiRaiseHandPic);
                //        Debug.Log("User need " + MissingCube);
                //        yield return new WaitForSeconds(2);
                //        //GameObject.Find(MissingCube).GetComponent<BoxCollider>().enabled = true;
                //        //GameObject.Find(MissingCube).GetComponent<MeshRenderer>().enabled = true;
                //    }
                //}

                //else
                //{
                    //GameEventCenter.DispatchEvent("KidsShouldPut");
                    Debug.Log("User touch Block");
                    yield return new WaitUntil(() => !_playerRound);
                //}
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
        npc.animator.SetBool("isHappy", true);
        yield return new WaitForSeconds(1);
        clip = Resources.Load<AudioClip>(audioClipRootPath + "NPC_Hooray");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isHappy", false);
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
    public void CubeToAns(BlockEntity cube)
    {
        cube.ToAnsLv1_Mono();
    }
    public void CubeOnAns(BlockEntity cube)
    {
        cube.OnAnsLv1_Mono();
    }
    public void OtherGroupCubeAns_Mono(BlockEntity cube)
    {
        cube.OtherGroupToAns();
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
    public void GetFocusName(string name)
    {
        focusName = name;
    }
}
