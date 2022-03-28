using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneRes : GameSceneEntityRes
{
    public PlayerEntity player;
    public GameObject NPC1_Hand;
    public List<BlockEntity> Q1_cube;
    public List<BlockEntity> Q2_cube;
    public List<BlockEntity> Q3_cube;
    public List<BlockEntity> Q4_cube;
    public List<BlockEntity> Cubes;
    //public HandsTrigger MyPreCube;
    //public HandsTrigger MyPreCubeIndex;
    public NPCEntity npc1;
    public Animator NPC1_animator;
    public GameObject GreenTriggerBall;
    public Animator TeacherAnimator;
    public Instantiate_Cube Instantiate_Cube;
    public RockPaperScissors RockPaperScissors;
    public CameraEntity eyeCamera;
    public List<AudioClip> speechClip;
    //public RecognizerEntity recognizerEntity;
    //public List<GameObject> ObjectList;
    public Transform PutPosition;
    //public List<TimerEntity> TimerList;
    public GameObject vrCamera;
    //public Canvas MainSceneUI;

    
}
