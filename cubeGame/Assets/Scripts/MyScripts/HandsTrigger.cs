using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsTrigger : MonoBehaviour
{
    private NPCEntity NPCEntity;
    private BlockEntity MyPreCube;
    private int MyPreCubeIndex;
    public List<BlockEntity> FirstBlock;
    private RockPaperScissors RockPaperScissors;
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
        var MyCube = other.GetComponent<BlockEntity>();//BLockEntity
        var index = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes.IndexOf(MyCube);//int
        QuestionPicked();
        Debug.Log(FirstCube);
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

            
                //判斷是否輪到User
                if (!other.gameObject.GetComponent<Rigidbody>().isKinematic)
            {
                PlayerEntity._take = false;
            }
            else
            {
                PlayerEntity._take = true;
            }

            //堆積木
            if (!BlockGameTask._playerRound)
            {
                //BlockGameTask._NPCRemind = true;
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
            //var cube = gameObject.transform.GetChild(5).gameObject.GetComponent<BlockEntity>();//hand底下的第6個
            var cube = gameObject.transform.GetChild(0).gameObject.GetComponent<BlockEntity>();//FakeHand
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

        //猜拳選題目
        //第一輪: 小花贏了，但是慢出(小綠生氣)
        else if (other.gameObject.tag == "FirstRoundRock4P")
        {
            GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Rock collider false");
            GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P2");
            GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowScissorsResultP1P2");
            GameObject.FindGameObjectWithTag("FirstRoundPaper4P").SetActive(false);
            Debug.Log("Found paper");
            GameObject.FindGameObjectWithTag("FirstRoundScissors4P").SetActive(false);
            Debug.Log("Found scissors");
           
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose rock");
        }
        else if(other.gameObject.tag == "FirstRoundScissors4P")
        {
            GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Scissors collider false");
            GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P2");
            GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowPaperResultP1P2");
            GameObject.FindGameObjectWithTag("FirstRoundPaper4P").SetActive(false);
            GameObject.FindGameObjectWithTag("FirstRoundRock4P").SetActive(false);
           
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose Scissors");
        }
        else if(other.gameObject.tag == "FirstRoundPaper4P")
        {
            GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Paper collider false");
            GameEventCenter.DispatchEvent("FirstRoundCloseAnimatorP1P2");
            GameEventCenter.DispatchEvent("FirstRoundFourPlayerShowRockResultP1P2");
            GameObject.FindGameObjectWithTag("FirstRoundRock4P").SetActive(false);
            GameObject.FindGameObjectWithTag("FirstRoundScissors4P").SetActive(false);
            
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose paper");
        }
        //第二輪:User贏
        else if(other.gameObject.tag == "Rock4P")
        {
            GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Rock collider false");
            GameEventCenter.DispatchEvent("CloseAnimator4P");
            GameEventCenter.DispatchEvent("FourPlayerShowScissorsResult");
            GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
            Debug.Log("Found paper");
            GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);
            Debug.Log("Found scissors");
           
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose rock");
        }
        else if(other.gameObject.tag == "Scissors4P")
        {
            GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Scissors collider false");
            GameEventCenter.DispatchEvent("CloseAnimator4P");
            GameEventCenter.DispatchEvent("FourPlayerShowPaperResult");
            GameObject.FindGameObjectWithTag("Paper4P").SetActive(false);
            GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);
           
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose Scissors");
        }
        else if(other.gameObject.tag == "Paper4P")
        {
            GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Paper collider false");
            GameEventCenter.DispatchEvent("CloseAnimator4P");
            GameEventCenter.DispatchEvent("FourPlayerShowRockResult");
            GameObject.FindGameObjectWithTag("Rock4P").SetActive(false);
            GameObject.FindGameObjectWithTag("Scissors4P").SetActive(false);
            
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose paper");
        }
        //第三輪: 小組內部猜拳，決定順序
        else if(other.gameObject.tag == "Rock2P")
        {
            GameObject.Find("Rock").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Rock collider false");
            GameEventCenter.DispatchEvent("CloseAnimator2P");
            GameEventCenter.DispatchEvent("TwoPlayerShowScissorsResult");
            GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
            GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);
            
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose rock");
        }
        else if(other.gameObject.tag == "Scissors2P")
        {
            GameObject.Find("Scissors").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Scissors collider false");
            GameEventCenter.DispatchEvent("CloseAnimator2P");
            GameEventCenter.DispatchEvent("TwoPlayerShowPaperResult");
            GameObject.FindGameObjectWithTag("Paper2P").SetActive(false);
            GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
            
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose scissors");
        }
        else if(other.gameObject.tag == "Paper2P")
        {
            GameEventCenter.DispatchEvent("CloseAnimator2P");
            GameEventCenter.DispatchEvent("TwoPlayerShowRockResult");
            GameObject.FindGameObjectWithTag("Rock2P").SetActive(false);
            GameObject.FindGameObjectWithTag("Scissors2P").SetActive(false);
            GameObject.Find("Paper").GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Paper collider false");
            BlockGameTask._userChooseRPS = true;
            Debug.Log("User choose paper");
        }
    }
    private void QuestionPicked()
    {
        //判斷哪個題目
        if (BlockGameTask.RandomQuestion == 1)
        {
            FirstCube = FirstBlock[0].name;
        }
        else if (BlockGameTask.RandomQuestion == 2)
        {
            FirstCube = FirstBlock[1].name;
        }
        else if (BlockGameTask.RandomQuestion == 3)
        {
            FirstCube = FirstBlock[2].name;
        }
        else if (BlockGameTask.RandomQuestion == 4)
        {
            FirstCube = FirstBlock[3].name;
        }
    }
}

 
    
