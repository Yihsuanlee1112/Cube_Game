using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneRes : GameSceneEntityRes
{
    [Header("Entities")]
    public PlayerEntity player;
    public NPCEntity npc1;
    public CameraEntity eyeCamera;

    [Header("GameObjects")]
    public GameObject GreenTriggerBall;
    public GameObject NPC1_Hand;
    public GameObject vrCamera;

    [Header("User_BLockLists")]
    public List<BlockEntity> Q1_cube;
    public List<BlockEntity> Q2_cube;
    public List<BlockEntity> Q3_cube;
    public List<BlockEntity> Q4_cube;
    public List<BlockEntity> Cubes;
    public List<BlockEntity> AllCubes;
    [Header("Other_BLockLists")]
    public List<BlockEntity> cube_GA;
    public List<BlockEntity> cube_GB;
    public List<BlockEntity> cube_GC;
    [Header("ColorList")]
    public List<Color> Colors;

    [Header("Animator")]
    public Animator TeacherAnimator;
    public Animator NPC1_animator;
    public Animator KidA, KidB, KidC, KidD, KidE, KidF;

    [Header("Instantiate")]
    public Instantiate_Cube Instantiate_Cube;
    public RockPaperScissors RockPaperScissors;
    //public otherGroupBiuldBlock OtherKids;

    [Header("AudioClips")]
    public List<AudioClip> ChineseSpeechClip;
    public List<AudioClip> EnglishSpeechClip;
    //public List<GameObject> ObjectList;
    public Transform PutPosition;
    //public List<TimerEntity> TimerList;
    
    //public Canvas MainSceneUI;

    
}
