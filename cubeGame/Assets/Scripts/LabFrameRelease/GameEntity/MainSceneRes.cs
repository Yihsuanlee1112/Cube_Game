using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneRes : GameSceneEntityRes
{
    [Header("Entities")]
    public PlayerEntity player;
    public NPCEntity npc;
    public CameraEntity eyeCamera;


    [Header("GameObjects")]
    public GameObject GreenTriggerBall;
    public GameObject RedTriggerBall;
    public GameObject NPC_Hand;
    public GameObject vrCamera;
    public GameObject userLeftHandTrigger;
    public GameObject userRightHandTrigger;

    [Header("Question_BlockLists")]
    //public List<GameObject> Q1_QuestionCube;
    //public List<GameObject> Q2_QuestionCube;
    //public List<GameObject> Q3_QuestionCube;
    //public List<GameObject> Q4_QuestionCube;
    public List<GameObject> Question_Cube;

    [Header("BlockLists")]
    public List<BlockEntity> Q1_cube;
    public List<BlockEntity> Q2_cube;
    public List<BlockEntity> Q3_cube;
    public List<BlockEntity> Q4_cube;
    public List<BlockEntity> AllCubes;
    public List<BlockEntity> Final_Order;
    //public List<GameObject> Lv2_Order;

    [Header("Each_Groups_BlockLists")]
    public List<BlockEntity> cube_GA;
    public List<BlockEntity> cube_GB;
    public List<BlockEntity> cube_GC;
    public List<BlockEntity> Cubes;

    [Header("ColorList")]
    public List<Color> Colors;

    [Header("NumList")]
    public List<int> textOnQuestion;
    public List<GameObject> QuestionOrder;

    [Header("TimerList")]
    public List<TimerEntity> TimerList;

    [Header("Animator")]
    public Animator HostAnimator;
    public Animator NPC_animator;
    public Animator TeacherAnimator;
    public Animator XiaoHua, XiaoMei, Green, Yoyo, Red, Hat;
    //public Animator TeacherAnimator2, npc2, XiaoHua2, XiaoMei2, Green2, Yoyo2, Hat2, Red2; 

    [Header("Instantiate")]
    public Instantiate_Cube Instantiate_Cube;
    public RockPaperScissors RockPaperScissors;
    //public otherGroupBiuldBlock OtherKids;
    

    [Header("AudioClips")]
    public List<AudioClip> ChineseSpeechClip;
    public List<AudioClip> EnglishSpeechClip;

    [Header("Award")]
    public GameObject Coin;
    public GameObject Ruby;
    public GameObject Heart;
    //public List<GameObject> ObjectList;
    public Transform PutPosition;
    //public List<TimerEntity> TimerList;

    //public Canvas MainSceneUI;


}
