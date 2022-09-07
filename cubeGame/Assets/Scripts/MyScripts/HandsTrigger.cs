using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandsTrigger : MonoBehaviour
{
    public static BlockEntity MyPreCube;
    private int MyPreCubeIndex;
    //public List<BlockEntity> FirstBlock;
    private string FirstCube;

    //private bool _NPCRemind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)//other=>cube
    {
        //Debug.Log(FirstBlock[0].name);
        //var MyCube = other.GetComponent<BlockEntity>();//BLockEntity
        //var index = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyCube);//int

        //拿積木
        if (GameTaskManager.task == 0)
        {

            //QuestionPicked();
            //拿積木
            if (other.gameObject.tag == "cube" && !PlayerEntity._take && !other.GetComponent<BlockEntity>()._isChose && other.gameObject)
            //GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.Contains(MyCube))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;

                //堆積木
                if (!BlockGameTask._playerRound)
                {
                    BlockGameTask._npcremind = true;
                    Debug.Log("NPCs turn");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    GameEventCenter.DispatchEvent("NPCRemind");//輪流拼
                }
                else if (other.gameObject.transform.localScale.x == BlockGameTask.KidShouldPut.transform.localScale.x &&
                    other.gameObject.transform.localScale.y == BlockGameTask.KidShouldPut.transform.localScale.y &&
                    other.gameObject.GetComponent<MeshRenderer>().material.color == BlockGameTask.KidShouldPut.GetComponent<MeshRenderer>().material.color &&
                    other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose)
                //成功: same color, same scale(x,y), _isUserColor, !_isChose
                {
                    Debug.Log("Sucdeed");
                    Debug.Log("Catch " + other.name);
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                    //PlayerEntity._take = true;
                    //Debug.Log(PlayerEntity._take);
                    Debug.Log("I put: " + BlockGameTask.KidShouldPut);
                }
                else if (other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose &&
                    other.gameObject.transform.localScale.x != BlockGameTask.KidShouldPut.transform.localScale.x &&
                    other.gameObject.transform.localScale.y != BlockGameTask.KidShouldPut.transform.localScale.y ||
                    other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose &&
                    other.gameObject.GetComponent<MeshRenderer>().material.color != BlockGameTask.KidShouldPut.GetComponent<MeshRenderer>().material.color)
                //fail. color not match || fail. scale not match 
                //_isUserColor, !_isChose
                {
                    BlockGameTask._npcremind = true;
                    Debug.Log("player round. fail. right color, wrong scale || fail. right color, wrong scale");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    //PlayerEntity._take = false;
                    //Debug.Log(PlayerEntity._take);
                }
                else if (!other.GetComponent<BlockEntity>()._isUserColor)
                {//!_isUserColor
                    BlockGameTask._npcremind = true;
                    Debug.Log("Wrong Cube, its NPCs cube. take it again");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

                }

                //判斷是否輪到User
                if (!other.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    PlayerEntity._take = false;
                }
                else
                {
                    PlayerEntity._take = true;
                }
            }
            //放積木
            else if (other.gameObject.tag == "q" && PlayerEntity._take)
            {
                Debug.Log(PlayerEntity._take);
                Debug.Log("Put toAns");
                var parent = GameObject.Find("Answer");
                //var cube = gameObject.transform.GetChild(5).gameObject.GetComponent<BlockEntity>();//hand底下的第6個
                var cube = gameObject.transform.GetChild(0).gameObject.GetComponent<BlockEntity>();//FakeHand
                cube.GetComponent<Rigidbody>().useGravity = true;
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                cube.transform.SetParent(parent.transform);
                GameEventCenter.DispatchEvent("CubeToAns", cube);
                GameEventCenter.DispatchEvent("CubeOnAns", cube);
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log(cube.transform.position);
                Debug.Log("In Hands Trigger:" + BlockGameTask.RecentOrder);
                QuestionCube._isCheck = true;
                BlockGameTask._playerRound = false;
                PlayerEntity._take = false;
            }
            else if (other.gameObject.tag == "Q1")
            {
                Debug.Log("Q1");
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q1").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask._RandomQuestion = 1;
                BlockGameTask._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q2")
            {
                Debug.Log("Q2");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask._RandomQuestion = 2;
                BlockGameTask._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q3")
            {
                Debug.Log("Q3");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask._RandomQuestion = 3;
                BlockGameTask._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q4")
            {
                Debug.Log("Q4");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask._RandomQuestion = 4;
                BlockGameTask._userChooseQuestion = true;
            }
            //舉手碰綠球
            else if (other.gameObject.tag == "greenTriggerBall")
            {
                Debug.Log("User Raise Hand");
                BlockGameTask._userRaiseHand = true;
            }
            //擊掌
            else if (other.gameObject.tag == "redTriggerBall")
            {
                Debug.Log("User Celebrate");
                BlockGameTask._userCelebrate = true;
                BlockGameTask.RedTriggerBall.SetActive(false);
            }

            //猜拳選題目
            //第一輪: 小花贏了，但是慢出(小綠生氣)
            else if (other.gameObject.tag == "FirstRoundRock4P")//Paper
            {
                Debug.Log("LV1!!!!");
                BlockGameTask._ShowResult = 1;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P3");
                GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP1P3");
                GameObject.FindGameObjectWithTag("FirstRoundPaper4P").SetActive(false);
                Debug.Log("Found paper");
                GameObject.FindGameObjectWithTag("FirstRoundScissors4P").SetActive(false);
                Debug.Log("Found scissors");

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "FirstRoundScissors4P")//Rock
            {
                BlockGameTask._ShowResult = 0;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P3");
                GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP1P3");
                GameObject.FindGameObjectWithTag("FirstRoundPaper4P").SetActive(false);
                GameObject.FindGameObjectWithTag("FirstRoundRock4P").SetActive(false);

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose Scissors");
            }
            else if (other.gameObject.tag == "FirstRoundPaper4P")//Scissors
            {
                BlockGameTask._ShowResult = 2;
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");
                GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P3");
                GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP1P3");
                GameObject.FindGameObjectWithTag("FirstRoundRock4P").SetActive(false);
                GameObject.FindGameObjectWithTag("FirstRoundScissors4P").SetActive(false);

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
            //第二輪:User贏
            else if (other.gameObject.tag == "Rock4P")//Scissors
            {
                BlockGameTask._ShowResult = 2;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResult");
                GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
                Debug.Log("Found paper");
                GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);
                Debug.Log("Found scissors");

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "Scissors4P")//Paper
            {
                BlockGameTask._ShowResult = 1;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResult");
                GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
                GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose Scissors");
            }
            else if (other.gameObject.tag == "Paper4P")//Rock
            {
                BlockGameTask._ShowResult = 0;
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResult");
                GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
            //第三輪: 小組內部猜拳，決定順序。User wins
            else if (other.gameObject.tag == "Rock2P")//Scissors
            {
                BlockGameTask._ShowResult = 2;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResult");
                GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "Scissors2P")//Paper
            {
                BlockGameTask._ShowResult = 1;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResult");
                GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose scissors");
            }
            else if (other.gameObject.tag == "Paper2P")//Rock
            {
                BlockGameTask._ShowResult = 0;
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResult");
                GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");

                BlockGameTask._userChooseRPS = true;
                Debug.Log("User choose paper");
            }

        }
        else if (GameTaskManager.task == 1)
        {
            //QuestionPickedLv2();
            //拿積木
            if (other.gameObject.tag == "cube" && !PlayerEntity._take && !other.GetComponent<BlockEntity>()._isChose && other.gameObject)
            {
                other.GetComponent<BoxCollider>().isTrigger = false;

                //堆積木
                if (!BlockGameTaskLv2._playerRound)
                {
                    BlockGameTaskLv2._npcremind = true;
                    Debug.Log("NPCs turn");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    GameEventCenter.DispatchEvent("NPCRemindLv2");//輪流拼
                }
                else if (other.gameObject.transform.localScale.x == BlockGameTaskLv2.KidShouldPut.transform.localScale.x &&
                    other.gameObject.transform.localScale.y == BlockGameTaskLv2.KidShouldPut.transform.localScale.y &&
                    other.gameObject.GetComponent<MeshRenderer>().material.color == BlockGameTaskLv2.KidShouldPut.GetComponent<MeshRenderer>().material.color &&
                    other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose)
                //成功: same color, same scale(x,y), _isUserColor, !_isChose
                {
                    Debug.Log("Sucdeed");
                    Debug.Log("Catch " + other.name);
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                    //PlayerEntity._take = true;
                    //Debug.Log(PlayerEntity._take);
                    Debug.Log("I put: " + BlockGameTaskLv2.KidShouldPut);
                }
                else if (other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose &&
                    other.gameObject.transform.localScale.x != BlockGameTaskLv2.KidShouldPut.transform.localScale.x &&
                    other.gameObject.transform.localScale.y != BlockGameTaskLv2.KidShouldPut.transform.localScale.y ||
                    other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose &&
                    other.gameObject.GetComponent<MeshRenderer>().material.color != BlockGameTaskLv2.KidShouldPut.GetComponent<MeshRenderer>().material.color)
                //fail. color not match || fail. scale not match 
                //_isUserColor, !_isChose
                {
                    BlockGameTaskLv2._npcremind = true;
                    Debug.Log("player round. fail. right color, wrong scale || fail. right color, wrong scale");
                    GameEventCenter.DispatchEvent("NPCRemind_OrderLv2");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    //PlayerEntity._take = false;
                    //Debug.Log(PlayerEntity._take);
                }
                else if (!other.GetComponent<BlockEntity>()._isUserColor)
                {//!_isUserColor
                    BlockGameTaskLv2._npcremind = true;
                    Debug.Log("Wrong Cube, its NPCs cube. take it again");
                    GameEventCenter.DispatchEvent("NPCRemind_OrderLv2");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

                }

                //判斷是否輪到User
                if (!other.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    PlayerEntity._take = false;
                }
                else
                {
                    PlayerEntity._take = true;
                }
            }
            //放積木
            else if (other.gameObject.tag == "q" && PlayerEntity._take)
            {


                Debug.Log(PlayerEntity._take);
                Debug.Log("Put toAns");
                var parent = GameObject.Find("Answer");
                //var cube = gameObject.transform.GetChild(5).gameObject.GetComponent<BlockEntity>();//hand底下的第6個
                var cube = gameObject.transform.GetChild(0).gameObject.GetComponent<BlockEntity>();//FakeHand

                cube.GetComponent<Rigidbody>().useGravity = true;
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                cube.transform.SetParent(parent.transform);

                SameCube(cube);

                GameEventCenter.DispatchEvent("CubeToAns", cube);
                GameEventCenter.DispatchEvent("CubeOnAns", cube);
                cube.ansTransform.GetComponent<CheckCubeAns>()._isUsed = true;//CubeAns isUsed ******
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log(cube.transform.position);
                Debug.Log("In Hands Trigger:" + BlockGameTaskLv2.RecentOrder);
                QuestionCube._isCheck = true;
                BlockGameTaskLv2._playerRound = false;
                PlayerEntity._take = false;
            }


            else if (other.gameObject.tag == "Q1")
            {
                Debug.Log("Q1");
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q1").GetComponent<BoxCollider>().enabled = false;
                BlockGameTaskLv2._RandomQuestion = 1;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q2")
            {
                Debug.Log("Q2");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").GetComponent<BoxCollider>().enabled = false;
                BlockGameTaskLv2._RandomQuestion = 2;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q3")
            {
                Debug.Log("Q3");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").GetComponent<BoxCollider>().enabled = false;
                BlockGameTaskLv2._RandomQuestion = 3;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q4")
            {
                Debug.Log("Q4");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").GetComponent<BoxCollider>().enabled = false;
                BlockGameTaskLv2._RandomQuestion = 4;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            //舉手碰綠球
            else if (other.gameObject.tag == "greenTriggerBall")
            {
                Debug.Log("User Raise Hand");
                //BlockGameTask._userRaiseHand = true;
                BlockGameTaskLv2._userRaiseHand = true;
            }

            //舉手碰紅球
            else if (other.gameObject.tag == "redTriggerBall")
            {
                Debug.Log("User Celebrate");
                //BlockGameTask._userCelebrate = true;
                BlockGameTaskLv2._userCelebrate = true;
                BlockGameTaskLv2.RedTriggerBall.SetActive(false);
            }

            //猜拳選題目
            //第一輪: 小花贏了，但是慢出(小綠生氣)
            else if (other.gameObject.tag == "FirstRoundRock4P")//Paper
            {
                Debug.Log("LV2!!!!");
                BlockGameTaskLv2._ShowResult = 1;
                Debug.Log(BlockGameTaskLv2._ShowResult);
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P3");
                GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP1P3Lv2");
                GameObject.FindGameObjectWithTag("FirstRoundPaper4P").SetActive(false);
                Debug.Log("Found paper");
                GameObject.FindGameObjectWithTag("FirstRoundScissors4P").SetActive(false);
                Debug.Log("Found scissors");

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "FirstRoundScissors4P")//Rock
            {
                BlockGameTaskLv2._ShowResult = 0;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P3Lv2");
                GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP1P3Lv2");
                GameObject.FindGameObjectWithTag("FirstRoundPaper4P").SetActive(false);
                GameObject.FindGameObjectWithTag("FirstRoundRock4P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose Scissors");
            }
            else if (other.gameObject.tag == "FirstRoundPaper4P")//Scissors
            {
                BlockGameTaskLv2._ShowResult = 2;
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");
                GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P3");
                GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowResultP1P3Lv2");
                GameObject.FindGameObjectWithTag("FirstRoundRock4P").SetActive(false);
                GameObject.FindGameObjectWithTag("FirstRoundScissors4P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
            //第二輪:User贏
            else if (other.gameObject.tag == "Rock4P")//Scissors
            {
                BlockGameTaskLv2._ShowResult = 2;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResultLv2");
                GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
                Debug.Log("Found paper");
                GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);
                Debug.Log("Found scissors");

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "Scissors4P")//Paper
            {
                BlockGameTaskLv2._ShowResult = 1;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResultLv2");
                GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
                GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose Scissors");
            }
            else if (other.gameObject.tag == "Paper4P")//Rock
            {
                BlockGameTaskLv2._ShowResult = 0;
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResultLv2");
                GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
            //Lv1 第三輪 : 小組內部猜拳，決定順序。User wins
            //Lv2 第三輪 : 小組內部猜拳，決定第一個顏色。User wins
            else if (other.gameObject.tag == "Rock2P")//Scissors
            {
                BlockGameTaskLv2._ShowResult = 2;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResultLv2");
                GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "Scissors2P")//Paper
            {
                BlockGameTaskLv2._ShowResult = 1;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResultLv2");
                GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose scissors");
            }
            else if (other.gameObject.tag == "Paper2P")//Rock
            {
                BlockGameTaskLv2._ShowResult = 0;
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResultLv2");
                GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
            //Lv2 第四輪 : 小組內部猜拳，決定第二個顏色。User Lose
            else if (other.gameObject.tag == "SecondRock2P")//Paper
            {
                BlockGameTaskLv2._ShowResult = 1;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResultLv2");//*********
                GameObject.FindGameObjectWithTag("SecondPaper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("SecondScissors2P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "SecondScissors2P")//Rock
            {
                BlockGameTaskLv2._ShowResult = 0;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResultLv2");//*********
                GameObject.FindGameObjectWithTag("SecondPaper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("SecondRock2P").SetActive(false);

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose scissors");
            }
            else if (other.gameObject.tag == "SecondPaper2P")//Scissors
            {
                BlockGameTaskLv2._ShowResult = 2;
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResultLv2");//*********
                GameObject.FindGameObjectWithTag("SecondRock2P").SetActive(false);
                GameObject.FindGameObjectWithTag("SecondScissors2P").SetActive(false);
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");

                BlockGameTaskLv2._userChooseRPS = true;
                Debug.Log("User choose paper");
            }

        }
        else if (GameTaskManager.task == 2)//Lv1_Mono
        {
            //QuestionPicked();
            //拿積木
            if (other.gameObject.tag == "cube" && !PlayerEntity._take && !other.GetComponent<BlockEntity>()._isChose && other.gameObject)
            //GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.Contains(MyCube))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;

                //堆積木
                if (!BlockGameTask_Mono._playerRound)
                {
                    BlockGameTask_Mono._npcremind = true;
                    Debug.Log("NPCs turn");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    GameEventCenter.DispatchEvent("NPCRemind");//輪流拼
                }
                else if (other.gameObject.transform.localScale.x == BlockGameTask_Mono.KidShouldPut.transform.localScale.x &&
                    other.gameObject.transform.localScale.y == BlockGameTask_Mono.KidShouldPut.transform.localScale.y &&
                    other.gameObject.GetComponent<MeshRenderer>().material.color == BlockGameTask_Mono.KidShouldPut.GetComponent<MeshRenderer>().material.color &&
                    other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose)
                //成功: same color, same scale(x,y), _isUserColor, !_isChose
                {
                    Debug.Log("Sucdeed");
                    Debug.Log("Catch " + other.name);
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                    //PlayerEntity._take = true;
                    //Debug.Log(PlayerEntity._take);
                    Debug.Log("I put: " + BlockGameTask_Mono.KidShouldPut);
                }
                else if (other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose &&
                    other.gameObject.transform.localScale.x != BlockGameTask_Mono.KidShouldPut.transform.localScale.x &&
                    other.gameObject.transform.localScale.y != BlockGameTask_Mono.KidShouldPut.transform.localScale.y ||
                    other.GetComponent<BlockEntity>()._isUserColor && !other.GetComponent<BlockEntity>()._isChose &&
                    other.gameObject.GetComponent<MeshRenderer>().material.color != BlockGameTask_Mono.KidShouldPut.GetComponent<MeshRenderer>().material.color)
                //fail. color not match || fail. scale not match 
                //_isUserColor, !_isChose
                {
                    BlockGameTask_Mono._npcremind = true;
                    Debug.Log("player round. fail. right color, wrong scale || fail. right color, wrong scale");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    //PlayerEntity._take = false;
                    //Debug.Log(PlayerEntity._take);
                }
                else if (!other.GetComponent<BlockEntity>()._isUserColor)
                {//!_isUserColor
                    BlockGameTask_Mono._npcremind = true;
                    Debug.Log("Wrong Cube, its NPCs cube. take it again");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

                }

                //判斷是否輪到User
                if (!other.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    PlayerEntity._take = false;
                }
                else
                {
                    PlayerEntity._take = true;
                }
            }
            //放積木
            else if (other.gameObject.tag == "q" && PlayerEntity._take)
            {
                Debug.Log(PlayerEntity._take);
                Debug.Log("Put toAns");
                var parent = GameObject.Find("Answer");
                //var cube = gameObject.transform.GetChild(5).gameObject.GetComponent<BlockEntity>();//hand底下的第6個
                var cube = gameObject.transform.GetChild(0).gameObject.GetComponent<BlockEntity>();//FakeHand
                cube.GetComponent<Rigidbody>().useGravity = true;
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                cube.transform.SetParent(parent.transform);
                GameEventCenter.DispatchEvent("CubeToAns", cube);
                GameEventCenter.DispatchEvent("CubeOnAns", cube);
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log(cube.transform.position);
                Debug.Log("In Hands Trigger:" + BlockGameTask_Mono.RecentOrder);
                QuestionCube._isCheck = true;
                BlockGameTask_Mono._playerRound = false;
                PlayerEntity._take = false;
            }
            else if (other.gameObject.tag == "Q1")
            {
                Debug.Log("Q1");
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q1").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask_Mono._RandomQuestion = 1;
                BlockGameTask_Mono._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q2")
            {
                Debug.Log("Q2");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask_Mono._RandomQuestion = 2;
                BlockGameTask_Mono._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q3")
            {
                Debug.Log("Q3");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask_Mono._RandomQuestion = 3;
                BlockGameTask_Mono._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q4")
            {
                Debug.Log("Q4");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").GetComponent<BoxCollider>().enabled = false;
                BlockGameTask_Mono._RandomQuestion = 4;
                BlockGameTask_Mono._userChooseQuestion = true;
            }


            //猜拳選題目
            //第一輪: 小花贏了，但是慢出(小綠生氣)

            //第二輪:User贏
            else if (other.gameObject.tag == "Rock4P")//Scissors
            {
                BlockGameTask_Mono._ShowResult = 2;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResult_Mono");
                GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
                Debug.Log("Found paper");
                GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);
                Debug.Log("Found scissors");

                BlockGameTask_Mono._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "Scissors4P")//Paper
            {
                BlockGameTask_Mono._ShowResult = 1;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResult_Mono");
                GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
                GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);

                BlockGameTask_Mono._userChooseRPS = true;
                Debug.Log("User choose Scissors");
            }
            else if (other.gameObject.tag == "Paper4P")//Rock
            {
                BlockGameTask_Mono._ShowResult = 0;
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");
                GameEventCenter.DispatchEvent("CloseAnimator4P");
                GameEventCenter.DispatchEvent("FourPlayerShowResult_Mono");
                GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);

                BlockGameTask_Mono._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
            //第三輪: 小組內部猜拳，決定順序。User wins
            else if (other.gameObject.tag == "Rock2P")//Scissors
            {
                BlockGameTask_Mono._ShowResult = 2;
                GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Rock collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResult_Mono");
                GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);

                BlockGameTask_Mono._userChooseRPS = true;
                Debug.Log("User choose rock");
            }
            else if (other.gameObject.tag == "Scissors2P")//Paper
            {
                BlockGameTask_Mono._ShowResult = 1;
                GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Scissors collider false");
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResult_Mono");
                GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
                BlockGameTask_Mono._userChooseRPS = true;
                Debug.Log("User choose scissors");
            }
            else if (other.gameObject.tag == "Paper2P")//Rock
            {
                BlockGameTask_Mono._ShowResult = 0;
                GameEventCenter.DispatchEvent("CloseAnimator2P");
                GameEventCenter.DispatchEvent("TwoPlayerShowResult_Mono");
                GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
                GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);
                GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Paper collider false");

                BlockGameTask_Mono._userChooseRPS = true;
                Debug.Log("User choose paper");
            }
        }
    }


    public void SameCube(BlockEntity cube)
    {
        if (BlockGameTaskLv2._RandomQuestion == 2)
        {
            if(cube.name == "Q2BlueCuboid3_2(Clone)" && GameObject.Find("Q2_Ans/Q2BlueCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false 
                && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2BlueCuboid3_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2BlueCuboid3_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆藍色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2BlueCuboid3_1");//第二顆的答案跟第一顆藍色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2BlueCuboid3_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2BlueCuboid3_2");
            }
            else if(cube.name == "Q2GreenCuboid_2(Clone)" && GameObject.Find("Q2_Ans/Q2GreenCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2GreenCuboid_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2GreenCuboid_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆綠色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2GreenCuboid_1");//第二顆的答案跟第一顆綠色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2GreenCuboid_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2GreenCuboid_2");
            }
            else if(cube.name == "Q2RedCuboid_2(Clone)" && GameObject.Find("Q2_Ans/Q2RedCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2RedCuboid_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2RedCuboid_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆紅色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2RedCuboid_1");//第二顆的答案跟第一顆紅色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2RedCuboid_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2RedCuboid_2");
            }
            else if(cube.name == "Q2YellowCube_2(Clone)" && GameObject.Find("Q2_Ans/Q2RedCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                 && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2YellowCube_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2YellowCube_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆紅色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2YellowCube_1");//第二顆的答案跟第一顆紅色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2YellowCube_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2YellowCube_2");
            }
        }
    }
    public void SameCube_Mono(BlockEntity cube)
    {
        if (BlockGameTaskLv2._RandomQuestion == 2)
        {
            if (cube.name == "Q2BlueCuboid3_2(Clone)" && GameObject.Find("Q2_Ans/Q2BlueCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2BlueCuboid3_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2BlueCuboid3_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆藍色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2BlueCuboid3_1");//第二顆的答案跟第一顆藍色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2BlueCuboid3_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2BlueCuboid3_2");
            }
            else if (cube.name == "Q2GreenCuboid_2(Clone)" && GameObject.Find("Q2_Ans/Q2GreenCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2GreenCuboid_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2GreenCuboid_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆綠色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2GreenCuboid_1");//第二顆的答案跟第一顆綠色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2GreenCuboid_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2GreenCuboid_2");
            }
            else if (cube.name == "Q2RedCuboid_2(Clone)" && GameObject.Find("Q2_Ans/Q2RedCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2RedCuboid_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2RedCuboid_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆紅色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2RedCuboid_1");//第二顆的答案跟第一顆紅色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2RedCuboid_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2RedCuboid_2");
            }
            else if (cube.name == "Q2YellowCube_2(Clone)" && GameObject.Find("Q2_Ans/Q2RedCuboid3_1").GetComponent<CheckCubeAns>()._isUsed == false
                 && GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2YellowCube_2").GetComponent<QuestionCube>().CubeOrder > GameObject.Find("Parents/Q2_Parent/Question(Clone)/Q2YellowCube_1").GetComponent<QuestionCube>().CubeOrder)//先拿第二顆紅色長方體
            {
                cube.ansTransform = GameObject.Find("CubeAns/Q2_Ans/Q2YellowCube_1");//第二顆的答案跟第一顆紅色長方體的答案交換
                GameObject.Find("Parents/Q2_Parent/Q2_CubeParent/Q2YellowCube_1(Clone)").GetComponent<BlockEntity>().ansTransform = GameObject.Find("Q2_Ans/Q2YellowCube_2");
            }
        }
    }
    //private void QuestionPicked()
    //{
    //    //判斷哪個題目
    //    if (BlockGameTask._RandomQuestion == 1)
    //    {
    //        FirstCube = FirstBlock[0].name;
    //    }
    //    else if (BlockGameTask._RandomQuestion == 2)
    //    {
    //        FirstCube = FirstBlock[1].name;
    //    }
    //    else if (BlockGameTask._RandomQuestion == 3)
    //    {
    //        FirstCube = FirstBlock[2].name;
    //    }
    //    else if (BlockGameTask._RandomQuestion == 4)
    //    {
    //        FirstCube = FirstBlock[3].name;
    //    }
    //}
    //private void QuestionPickedLv2()
    //{
    //    //判斷哪個題目
    //    if (BlockGameTaskLv2._RandomQuestion == 1)
    //    {
    //        FirstCube = FirstBlock[0].name;
    //    }
    //    else if (BlockGameTaskLv2._RandomQuestion == 2)
    //    {
    //        FirstCube = FirstBlock[1].name;
    //    }
    //    else if (BlockGameTaskLv2._RandomQuestion == 3)
    //    {
    //        FirstCube = FirstBlock[2].name;
    //    }
    //    else if (BlockGameTaskLv2._RandomQuestion == 4)
    //    {
    //        FirstCube = FirstBlock[3].name;
    //    }
    //}
}