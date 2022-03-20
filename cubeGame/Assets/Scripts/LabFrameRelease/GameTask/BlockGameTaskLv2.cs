using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGameTaskLv2 : TaskBase
{
    private PlayerEntity player;
    private NPCEntity npc;
    private List<BlockEntity> Q1_cube;
    private List<BlockEntity> Q2_cube;
    private List<BlockEntity> Q3_cube;
    private List<BlockEntity> Q4_cube;
    private List<BlockEntity> Cubes;
    //private List<GameObject> objectlist;
    private CameraEntity eyecamera;
    private Canvas mainSceneUI;
    private AudioClip clip;

    private Animator TeacherAnimator;
    //private Animator FourP1Ani, FourP2Ani, FourP3Ani, TwoPAni;
    //private Animator RPS_Animator;

    //private HandsTrigger PlayerHand;
    //private RecognizerEntity recognizerEntity;
    private string focusName;
    public static int RandomQuestion = 0;
    public static bool _userChooseRPS = false;
    public static bool _playerRound = true;//原本是false
    public bool _BlockFinished = false;
    public static bool _usersayhello = false;
    public static bool _talking = false;
    public static bool _npcremind = false;
    public static bool _eyetimerfinish = false;
    public static bool _waitforwatch = false;
    public static int answerindex = 0;
    //public static bool _NPCRemind = false;
    public int TalkScore = 0;//recognizerEntity

    public override IEnumerator TaskInit()
    {
        GameEventCenter.AddEvent("CheckCube", CheckCube);
        GameEventCenter.AddEvent<BlockEntity>("CubeAns", CubeAns);
        GameEventCenter.AddEvent<string>("GetFocusName", GetFocusName);
        GameEventCenter.AddEvent("AddCubesToList", AddCubesToList);
        GameEventCenter.AddEvent("FindQ1Cubes", FindQ1Cubes);
        GameEventCenter.AddEvent("FindQ2Cubes", FindQ2Cubes);
        GameEventCenter.AddEvent("FindQ3Cubes", FindQ3Cubes);
        GameEventCenter.AddEvent("FindQ4Cubes", FindQ4Cubes);

        //GameEventCenter.AddEvent("NPC_Remind", NPC_Remind);
        //GameEventCenter.AddEvent("NPC_Remind2", NPC_Remind2);
        //載入MainSceneRes
        player = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().player;
        npc = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().npc;
        Q1_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q1_cube;
        Q2_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q2_cube;
        Q3_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q3_cube;
        Q4_cube = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Q4_cube;
        Cubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes;
        //objectlist = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().ObjectList;
        npc.npchand = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPCHand;
        npc.speechList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().speechClip;
        npc.animator = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().NPC_animator;
        //mainSceneUI = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().MainSceneUI;
        //recognizerEntity = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().recognizerEntity;


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
        yield return null;
    }

    public override IEnumerator TaskStart()
    {
        TeacherAnimator = GameObject.Find("teacher").GetComponent<Animator>();

        //邀請小朋友一起堆積木
        //npc.animator.Play("Talk");
        //GameAudioController.Instance.PlayOneShot(npc.speechList[0]);
        //yield return new WaitForSeconds(1.5f);

        //打招呼
        yield return SayHello();

        //老師開場
        yield return Teacher_Opening();

        //老師說明遊戲規則
        yield return Teacher_Introduction();

        //老師問選圖
        yield return Teacher_Ask();

        RandomQuestion = 2;//user選的題目

        //User跟其他三組猜拳
        //第一次 小紅慢出(小綠生氣)
        GameEventCenter.DispatchEvent("FourPlayerRPS");//猜拳動畫
        GameObject.Find("FourPlayerChoose(Clone)/Canvas2").SetActive(true);
        GameObject.Find("FourPlayerChoose(Clone)/Canvas").SetActive(false);


        while (!_userChooseRPS)
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        }
        yield return new WaitForSeconds(1);
        GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP3");//小花慢出
        GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP3");


        yield return new WaitForSeconds(3);
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>("AudioClip/NPC_TooSlow");
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
        yield return new WaitForSeconds(5);

        //第二次 User贏。小紅說沒關係，我也喜歡另一個
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>("AudioClip/Host_PlayRPS_Again");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("主持人說再玩一次");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
        GameEventCenter.DispatchEvent("FourPlayerRPS");
        GameObject.Find("FourPlayerChoose(Clone)/Canvas").SetActive(true);
        GameObject.Find("FourPlayerChoose(Clone)/Canvas2").SetActive(false);

        while (!_userChooseRPS)
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        }
        yield return new WaitForSeconds(5);

        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("Result").SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(5);

        //User跟對面NPC猜拳
        GameEventCenter.DispatchEvent("InstatiateCube");
        npc.animator.SetBool("isTalk", true);
        clip = Resources.Load<AudioClip>("AudioClip/NPC_WinnerFirst");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        npc.animator.SetBool("isTalk", false);
        GameEventCenter.DispatchEvent("TwoPlayerRPS");
        while (!_userChooseRPS)
        {
            Debug.Log("User choose RPS");
            yield return new WaitUntil(() => _userChooseRPS);
        }
        yield return new WaitForSeconds(5);

        GameObject.FindWithTag("Result").SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            GameObject.FindWithTag("RPS").SetActive(false);
        }
        yield return new WaitForSeconds(5);

        // 框架
        //規則說明要輪流拼
        yield return NPC_Rule();

        //開始堆積木
        GameEventCenter.DispatchEvent("AddCubesToList");

        while (!_BlockFinished)
        {
            if (_playerRound)  //玩家回合
            {
                Debug.Log("User touch block");
                yield return new WaitUntil(() => !_playerRound);
            }
            else  //NPC回合
            {
                _npcremind = false;
                foreach (BlockEntity cube in Cubes)
                {
                    if (!cube._isChose)
                    {
                        npc.animator.Play("Puzzle"); //npc拿積木
                        yield return new WaitForSeconds(7);
                        if (!_npcremind)
                        {
                            Debug.Log("NPC putting block");
                            GameEventCenter.DispatchEvent("CubeAns", cube);
                            _playerRound = true;
                        }
                        else
                        {
                            Debug.Log("wait for remind");
                            //clip = Resources.Load<AudioClip>("AudioClip/NPC_Remind");
                            //yield return new WaitForSeconds(clip.length);
                        }
                        break;

                    }
                }

            }

            GameEventCenter.DispatchEvent("CheckCube");
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
        TeacherAnimator.SetBool("isSayingHello", true);
        clip = Resources.Load<AudioClip>("AudioClip/Teacher_SayHi");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("AudioClip/Teacher_SayHi" + clip.length);
        Debug.Log("打完招呼");
        TeacherAnimator.SetBool("isSayingHello", false);
    }
    IEnumerator Teacher_Opening()
    {
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>("AudioClip/Teacher_Opening");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("AudioClip/Teacher_Opening" + clip.length);
        Debug.Log("老師開完場");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
    }
    IEnumerator Teacher_Introduction()
    {
        TeacherAnimator.SetBool("isStandingAndTalking", true);
        clip = Resources.Load<AudioClip>("AudioClip/Teacher_Introduction");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("AudioClip/Teacher_Introduction" + clip.length);
        Debug.Log("老師說完遊戲規則");
        TeacherAnimator.SetBool("isStandingAndTalking", false);
    }
    IEnumerator Teacher_Ask()
    {
        TeacherAnimator.SetBool("isAsking", true);
        clip = Resources.Load<AudioClip>("AudioClip/Teacher_Ask");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("AudioClip/Teacher_Ask" + clip.length);
        Debug.Log("老師問完選圖");
        TeacherAnimator.SetBool("isAsking", false);
    }
    IEnumerator NPC_Rule()
    {
        npc.animator.SetBool("isTalk2", true);
        clip = Resources.Load<AudioClip>("AudioClip/NPC_Rule");
        GameAudioController.Instance.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Debug.Log("AudioClip/NPC_Rule" + clip.length);
        //GameAudioController.Instance.PlayOneShot(npc.speechList[5]);//NPC_Rule
        //yield return new WaitForSeconds(8.5f);
        npc.animator.SetBool("isTalk2", false);
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
        if (RandomQuestion == 1)
        {
            GameEventCenter.DispatchEvent("FindQ1Cubes");
        }
        else if (RandomQuestion == 2)
        {
            GameEventCenter.DispatchEvent("FindQ2Cubes");
        }
        else if (RandomQuestion == 3)
        {
            GameEventCenter.DispatchEvent("FindQ3Cubes");
        }
        else if (RandomQuestion == 4)
        {
            GameEventCenter.DispatchEvent("FindQ4Cubes");
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
        Cubes.AddRange(Q1_cube);
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
        Cubes.AddRange(Q2_cube);
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
        Q3_cube.Add(GameObject.Find("Q3GreenCube_1(Clone)").GetComponent<BlockEntity>());
        Cubes.AddRange(Q3_cube);
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
        Cubes.AddRange(Q4_cube);
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
        cube.ToAns();
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
