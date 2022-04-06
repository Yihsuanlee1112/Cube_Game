using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsTrigger : MonoBehaviour
{
    public static BlockEntity MyPreCube;
    private int MyPreCubeIndex;
    public List<BlockEntity> FirstBlock;
    private string FirstCube;
    private List<BlockEntity> Cubes;
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
        var MyCube = other.GetComponent<BlockEntity>();//BLockEntity
        var index = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyCube);//int
        QuestionPicked();
        //拿積木
        if (GameTaskManager.task == 0)
        {
            Debug.Log("TaskLv1");
            //拿積木
            if (other.gameObject.tag == "cube" && !PlayerEntity._take &&
             !other.GetComponent<BlockEntity>()._isChose &&
             GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.Contains(MyCube))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("Old" + MyPreCube + MyPreCubeIndex);
                Debug.Log("New" + MyCube + index);
                Debug.Log(index);//int
                Debug.Log(index - MyPreCubeIndex);
                
                //堆積木
                if (!BlockGameTask._playerRound)
                {
                    //BLockGameTask._NPCRemind = true;
                    Debug.Log("Wrong Action");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    GameEventCenter.DispatchEvent("NPCRemind");//輪流拼
                }

                else if (other.gameObject.GetComponent<BlockEntity>() == GameObject.Find(FirstCube + "(Clone)").GetComponent<BlockEntity>())
                {
                    Debug.Log(other.gameObject.GetComponent<BlockEntity>());
                    //GameEventCenter.DispatchEvent("NPCRemind_Order"); 
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                }
                else if (index > 0 && index < 10 && index - MyPreCubeIndex != 2
                    || !GameObject.Find(FirstCube + "(Clone)").GetComponent<Rigidbody>().isKinematic)
                {
                    Debug.Log("Wrong Cube, put 1 first");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
                else if (GameObject.Find(FirstCube + "(Clone)").GetComponent<Rigidbody>().isKinematic)
                {
                    Debug.Log("Catch " + other.name);
                    Debug.Log("follow hand");
                    Debug.Log(gameObject.name);
                    Debug.Log(other.gameObject.name);
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                    MyPreCube = other.GetComponent<BlockEntity>();//BlockEntity
                    MyPreCubeIndex = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyPreCube);//int
                    Debug.Log(MyPreCube + " " + MyPreCubeIndex);
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
                MyCube = MyPreCube;
                index = MyPreCubeIndex;
                Debug.Log(MyCube + " " + MyPreCube);
                Debug.Log(index + " " + MyPreCubeIndex);
            }
            //放積木
            else if (other.gameObject.tag == "q" && PlayerEntity._take)
            {
                Debug.Log("Put toAns");
                var parent = GameObject.Find("Answer");
                var cube = gameObject.transform.GetChild(5).gameObject.GetComponent<BlockEntity>();//hand底下的第6個
                //var cube = gameObject.transform.GetChild(0).gameObject.GetComponent<BlockEntity>();//FakeHand
                Debug.Log(parent);
                Debug.Log(cube);
                cube.GetComponent<Rigidbody>().useGravity = true;
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                cube.transform.SetParent(parent.transform);
                GameEventCenter.DispatchEvent("CubeAns", cube);
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //Debug.Log(parent);
                Debug.Log(cube.transform.position);
                //GetComponent<BoxCollider>().enabled = false;
                BlockGameTask._playerRound = false;
                PlayerEntity._take = false;
            }
            else if (other.gameObject.tag == "Q1")
            {
                Debug.Log("Q1");
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                BlockGameTask._RandomQuestion = 1;
                BlockGameTask._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q2")
            {
                Debug.Log("Q2");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                BlockGameTask._RandomQuestion = 2;
                BlockGameTask._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q3")
            {
                Debug.Log("Q3");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                BlockGameTask._RandomQuestion = 3;
                BlockGameTask._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q4")
            {
                Debug.Log("Q4");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                BlockGameTaskLv2._RandomQuestion = 4;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            //舉手碰綠球
            else if (other.gameObject.tag == "greenTriggerBall")
            {
                Debug.Log("User Raise Hand");
                BlockGameTask._userRaiseHand = true;
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
        else if(GameTaskManager.task == 1)
        {
            Debug.Log("TaskLv2");
            Cubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes;
            //var MyCube = other.GetComponent<BlockEntity>();//BLockEntity
            //var index = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyCube);//int
            QuestionPickedLv2();
            //拿積木
            if (other.gameObject.tag == "cube" && !PlayerEntity._take &&
                !other.GetComponent<BlockEntity>()._isChose &&
                GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.Contains(MyCube))
            {
                other.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("Old" + MyPreCube + MyPreCubeIndex);
                Debug.Log("New" + MyCube + index);
                Debug.Log(index);//int
                Debug.Log(index - MyPreCubeIndex);


                //堆積木
                if (!BlockGameTaskLv2._playerRound)
                {
                    //BLockGameTask._NPCRemind = true;
                    Debug.Log("NPCs turn");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    GameEventCenter.DispatchEvent("NPCRemind");//輪流拼
                    PlayerEntity._take = false;
                    Debug.Log(PlayerEntity._take);
                    BlockGameTaskLv2._playerRound = false;
                    Debug.Log(BlockGameTaskLv2._playerRound);
                }
                else if (Cubes[index - 1]._isChose && MyCube._isUserColor && BlockGameTaskLv2._playerRound)//成功
                {
                    Debug.Log("Sucdeed");
                    Debug.Log("Catch " + other.name);
                    Debug.Log("follow hand");
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                    MyPreCube = other.GetComponent<BlockEntity>();//BlockEntity
                    MyPreCubeIndex = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyPreCube);//int
                    Debug.Log(MyPreCube + " " + MyPreCubeIndex);
                    PlayerEntity._take = true;
                    Debug.Log(PlayerEntity._take);
                    BlockGameTaskLv2._playerRound = false;
                    Debug.Log(BlockGameTaskLv2._playerRound);
                }
                else if (Cubes[index - 1]._isChose && MyCube._isUserColor && BlockGameTaskLv2._playerRound && Cubes[index + 1]._isUserColor)//成功, next isUserColor
                {
                    Debug.Log("成功, next isUserColor");
                    Debug.Log("Catch " + other.name);
                    Debug.Log("follow hand");
                    other.GetComponent<BoxCollider>().isTrigger = false;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    other.gameObject.transform.SetParent(gameObject.transform);
                    MyPreCube = other.GetComponent<BlockEntity>();//BlockEntity
                    MyPreCubeIndex = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyPreCube);//int
                    Debug.Log(MyPreCube + " " + MyPreCubeIndex);
                    PlayerEntity._take = true;
                    Debug.Log(PlayerEntity._take);
                    BlockGameTaskLv2._playerRound = true;
                    Debug.Log(BlockGameTaskLv2._playerRound);
                }
                else if (!Cubes[index - 1]._isChose && MyCube._isUserColor && Cubes[index + 1]._isUserColor &&  BlockGameTaskLv2._playerRound)//player round. wrong order, former first, next isUsercColor
                {
                    Debug.Log("player round. wrong order, former first, next isUsercColor");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    PlayerEntity._take = false;
                    Debug.Log(PlayerEntity._take);
                    BlockGameTaskLv2._playerRound = true;
                    Debug.Log(BlockGameTaskLv2._playerRound);
                }
                else if (!MyCube._isUserColor && BlockGameTaskLv2._playerRound)//player round. NPCs cube, take again,
                {
                    Debug.Log("Wrong Cube, its NPCs cube. , take again");
                    GameEventCenter.DispatchEvent("NPCRemind_Order");
                    //StartCoroutine(NPCEntity.NPCRemind());
                    other.gameObject.transform.parent = null;
                    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    PlayerEntity._take = false;
                    Debug.Log(PlayerEntity._take);
                    Debug.Log("Take again");
                    BlockGameTaskLv2._playerRound = true;
                    Debug.Log(BlockGameTaskLv2._playerRound);
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
                MyCube = MyPreCube;
                index = MyPreCubeIndex;
                Debug.Log(MyCube + " " + MyPreCube);
                Debug.Log(index + " " + MyPreCubeIndex);
            }
            //放積木
            else if (other.gameObject.tag == "q" && PlayerEntity._take)
            {
                Debug.Log(PlayerEntity._take);
                Debug.Log("Put toAns");
                var parent = GameObject.Find("Answer");
                //var cube = gameObject.transform.GetChild(5).gameObject.GetComponent<BlockEntity>();//hand底下的第6個
                var cube = gameObject.transform.GetChild(0).gameObject.GetComponent<BlockEntity>();//FakeHand
                Debug.Log(parent);
                Debug.Log(cube);
                cube.GetComponent<Rigidbody>().useGravity = true;
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                cube.transform.SetParent(parent.transform);
                GameEventCenter.DispatchEvent("CubeAns", cube);
                cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log(cube.transform.position);
                BlockGameTaskLv2._playerRound = false;
                PlayerEntity._take = false;
            }


            else if (other.gameObject.tag == "Q1")
            {
                Debug.Log("Q1");
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                BlockGameTaskLv2._RandomQuestion = 1;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q2")
            {
                Debug.Log("Q2");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                BlockGameTaskLv2._RandomQuestion = 2;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q3")
            {
                Debug.Log("Q3");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q4").SetActive(false);
                BlockGameTaskLv2._RandomQuestion = 3;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            else if (other.gameObject.tag == "Q4")
            {
                Debug.Log("Q4");
                GameObject.FindGameObjectWithTag("Q1").SetActive(false);
                GameObject.FindGameObjectWithTag("Q2").SetActive(false);
                GameObject.FindGameObjectWithTag("Q3").SetActive(false);
                BlockGameTaskLv2._RandomQuestion = 4;
                BlockGameTaskLv2._userChooseQuestion = true;
            }
            //舉手碰綠球
            else if (other.gameObject.tag == "greenTriggerBall")
            {
                Debug.Log("User Raise Hand");
                BlockGameTaskLv2._userRaiseHand = true;
            }

            //猜拳選題目
            //第一輪: 小花贏了，但是慢出(小綠生氣)
            else if (other.gameObject.tag == "FirstRoundRock4P")//Paper
            {
                Debug.Log("LV2!!!!");
                BlockGameTaskLv2._ShowResult = 1;
                Debug.Log(BlockGameTask._ShowResult);
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
                BlockGameTaskLv2._ShowResult = 0;
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
            //第三輪: 小組內部猜拳，決定順序。User wins
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

        }
    }

    private void QuestionPicked()
    {
        //判斷哪個題目
        if (BlockGameTask._RandomQuestion == 1)
        {
            FirstCube = FirstBlock[0].name;
        }
        else if (BlockGameTask._RandomQuestion == 2)
        {
            FirstCube = FirstBlock[1].name;
        }
        else if (BlockGameTask._RandomQuestion == 3)
        {
            FirstCube = FirstBlock[2].name;
        }
        else if (BlockGameTask._RandomQuestion == 4)
        {
            FirstCube = FirstBlock[3].name;
        }
    }
    private void QuestionPickedLv2()
    {
        //判斷哪個題目
        if (BlockGameTaskLv2._RandomQuestion == 1)
        {
            FirstCube = FirstBlock[0].name;
        }
        else if (BlockGameTaskLv2._RandomQuestion == 2)
        {
            FirstCube = FirstBlock[1].name;
        }
        else if (BlockGameTaskLv2._RandomQuestion == 3)
        {
            FirstCube = FirstBlock[2].name;
        }
        else if (BlockGameTaskLv2._RandomQuestion == 4)
        {
            FirstCube = FirstBlock[3].name;
        }
    }
}