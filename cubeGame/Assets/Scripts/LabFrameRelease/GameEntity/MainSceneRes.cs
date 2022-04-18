﻿using System.Collections;
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
    public GameObject userLeftHandTrigger;
    public GameObject userRightHandTrigger;

    [Header("Question_BlockLists")]
    public List<GameObject> Q1_QuestionCube;
    public List<GameObject> Q2_QuestionCube;
    public List<GameObject> Q3_QuestionCube;
    public List<GameObject> Q4_QuestionCube;
    public List<GameObject> QuestionCube;

    [Header("BlockLists")]
    public List<BlockEntity> Q1_cube;
    public List<BlockEntity> Q2_cube;
    public List<BlockEntity> Q3_cube;
    public List<BlockEntity> Q4_cube;
    public List<BlockEntity> AllCubes;
    public List<BlockEntity> Lv2_Order;
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

    [Header("Animator")]
    public Animator HostAnimator;
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
