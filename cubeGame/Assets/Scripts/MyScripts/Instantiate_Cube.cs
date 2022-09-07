using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Cube : MonoBehaviour
{
    //public GameObject InstantiateCube; //物件的生成點。
    [Header("Question Prefabs")]
    //[System.NonSerialized]
    public List<GameObject> Question_Prefabs;

    [Header("Cube Prefabs")]
    public List<GameObject> Q1_Cube_Prefabs;
    public List<GameObject> Q2_Cube_Prefabs;
    public List<GameObject> Q3_Cube_Prefabs;
    public List<GameObject> Q4_Cube_Prefabs;
    public List<List<GameObject>> Cube_Prefabs = new List<List<GameObject>>();

    [Header("Cubes")]
    public List<GameObject> AllCubePrefabs;
    public List<BlockEntity> AllCubes;
    Vector3 position;

    [Header("積木和題目的父物件")]
    public List<GameObject> Parents;
    float interval = 0.079f; //3.623-3.544 但好像間隔不平均(?
    float interval2 = 0.6f;
    
    [Header("桌子的位置")]
    public List<GameObject> Tables;

    [Header("積木的父物件")]
    public List<GameObject> CubeParents;

    [Header("CubeAns")]
    public List<GameObject> Q1_Ans;
    public List<GameObject> Q2_Ans;
    public List<GameObject> Q3_Ans;
    public List<GameObject> Q4_Ans;
    public List<List<GameObject>> Cube_Ans = new List<List<GameObject>>();


    //public int RandomQuestion = Random.Range(1, 4);
    //public int RandomQuestion = 2;

    //Quaternion rotation ;
    //Vector3 Question_position = Vector3.zero;

    // Start is called before the first frame update

    //public override void EntityDispose()
    //{

    //}
    private void Awake()
    {
        GameEventCenter.AddEvent("InstatiateCube", InstatiateCube);
        GameEventCenter.AddEvent("InstatiateCube_Mono", InstatiateCube_Mono);
        GameEventCenter.AddEvent("InstatiateCubeLv2", InstatiateCubeLv2);
        GameEventCenter.AddEvent("CubeOnDesk", CubeOnDesk);
        GameEventCenter.AddEvent("InstantiateQuestion", InstantiateQuestion);
        GameEventCenter.AddEvent("InstantiateQuestion_Mono", InstantiateQuestion_Mono);
        GameEventCenter.AddEvent("InstantiateQuestionLv2", InstantiateQuestionLv2);
    }
    void Start()
    {

    }
    public void InstatiateCube()
    {
        Cube_Prefabs.Add(Q1_Cube_Prefabs);
        Cube_Prefabs.Add(Q2_Cube_Prefabs);
        Cube_Prefabs.Add(Q3_Cube_Prefabs);
        Cube_Prefabs.Add(Q4_Cube_Prefabs);

        Cube_Ans.Add(Q1_Ans);
        Cube_Ans.Add(Q2_Ans);
        Cube_Ans.Add(Q3_Ans);
        Cube_Ans.Add(Q4_Ans);

        for (int i = 0; i < Cube_Prefabs.Count; i++)
        {
            // 生成積木和題目
            //          Q1_CubePrefaabs, Q1_Parent, Q1_CubeParent, Q1_Ans
            Question(i, Cube_Prefabs[i], Parents[i], CubeParents[i], Cube_Ans[i]);
        }

        // 題目和積木放到桌上
        PutCubeOnTables(BlockGameTask._RandomQuestion - 1);
    }
    public void InstatiateCubeLv2()
    {
        Cube_Prefabs.Add(Q1_Cube_Prefabs);
        Cube_Prefabs.Add(Q2_Cube_Prefabs);
        Cube_Prefabs.Add(Q3_Cube_Prefabs);
        Cube_Prefabs.Add(Q4_Cube_Prefabs);

        Cube_Ans.Add(Q1_Ans);//***********
        Cube_Ans.Add(Q2_Ans);
        Cube_Ans.Add(Q3_Ans);
        Cube_Ans.Add(Q4_Ans);

        for (int i = 0; i < 4; i++)
        {
            Question(i, Cube_Prefabs[i], Parents[i], CubeParents[i], Cube_Ans[i]);
        }

        //PutCubeOnTables(BlockGameTaskLv2._RandomQuestion - 1);
    }
    public void CubeOnDesk()
    {
        PutCubeOnTables(BlockGameTaskLv2._RandomQuestion - 1);
    }
    public void InstatiateCube_Mono()
    {
        Cube_Prefabs.Add(Q1_Cube_Prefabs);
        Cube_Prefabs.Add(Q2_Cube_Prefabs);
        Cube_Prefabs.Add(Q3_Cube_Prefabs);
        Cube_Prefabs.Add(Q4_Cube_Prefabs);

        Cube_Ans.Add(Q1_Ans);
        Cube_Ans.Add(Q2_Ans);
        Cube_Ans.Add(Q3_Ans);
        Cube_Ans.Add(Q4_Ans);

        for (int i = 0; i < Cube_Prefabs.Count; i++)
        {
            // 生成積木和題目
            //          Q1_CubePrefaabs, Q1_Parent, Q1_CubeParent, Q1_Ans
            Question(i, Cube_Prefabs[i], Parents[i], CubeParents[i], Cube_Ans[i]);
        }

        // 題目和積木放到桌上
        PutCubeOnTables(BlockGameTask_Mono._RandomQuestion - 1);
    }
    /// <summary>
    /// 生成題目和積木(其位置都是以放在user桌上為基準)，再依據不同題目包在對應的父物件底下(目的是可以改變父物件位置放在NPC桌上)
    /// </summary>
    /// <param name="PicNum"></param>
    /// <param name="PicPos"></param>
    /// <param name="CubePrefabs"></param>
    /// <param name="CubesInitPos"></param>
    /// <param name="Parent"></param>
    public void Question(int PicNum, List<GameObject> CubePrefabs, GameObject Parent, GameObject CubeParent, List<GameObject> CubeAns)
    {
        AllCubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().AllCubes;
        Vector3 PicsPos = new Vector3((float)0.0, (float)0.0, (float)0.0);
        Vector3 CubesPos = new Vector3((float)0.0, (float)0.0, (float)0.0);
        int cubeAnsIndex = 0;

        // 生成題目
        GameObject picInstantiate = Instantiate(Question_Prefabs[PicNum], PicsPos, Quaternion.Euler(0, 180, 0)); // 因為場景X和Z方向不同，所以4張圖統一轉向
        picInstantiate.transform.SetParent(Parent.transform);

        // 生成積木
        foreach (GameObject cube in CubePrefabs)
        {
            // 先把CubeAns放進Cube
            cube.GetComponent<BlockEntity>().ansTransform = CubeAns[cubeAnsIndex];
            cubeAnsIndex++;

            // 然後在生成
            CubesPos.z = CubesPos.z + interval;
            //CubesPos.y = CubesPos.y + interval2;
            GameObject cubeInstantiate = Instantiate(cube, CubesPos, Quaternion.identity);
            cubeInstantiate.transform.SetParent(CubeParent.transform);

            AllCubes.Add(cubeInstantiate.GetComponent<BlockEntity>());
            /*
            if (PicNum+1 == BlockGameTask._RandomQuestion)
            {
                // user的積木放在MainSceneRes的Cubes裡面才能判斷順序
                Cubes.Add(cubeInstantiate.GetComponent<BlockEntity>());
            }
            */
        }
    }
    public void QuestionLv2(int PicNum, List<GameObject> CubePrefabs, GameObject Parent, GameObject CubeParent)//, List<GameObject> CubeAns)
    {
        AllCubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().AllCubes;
        Vector3 PicsPos = new Vector3((float)0.0, (float)0.0, (float)0.0);
        Vector3 CubesPos = new Vector3((float)0.0, (float)0.0, (float)0.0);
        int cubeAnsIndex = 0;

        // 生成題目
        GameObject picInstantiate = Instantiate(Question_Prefabs[PicNum], PicsPos, Quaternion.Euler(0, 180, 0)); // 因為場景X和Z方向不同，所以4張圖統一轉向
        picInstantiate.transform.SetParent(Parent.transform);

        // 生成積木
        foreach (GameObject cube in CubePrefabs)
        {
            // 先把CubeAns放進Cube
            //cube.GetComponent<BlockEntity>().ansTransform = CubeAns[cubeAnsIndex];
            //cubeAnsIndex++;

            // 然後在生成
            CubesPos.z = CubesPos.z + interval;
            //CubesPos.y = CubesPos.y + interval2;
            GameObject cubeInstantiate = Instantiate(cube, CubesPos, Quaternion.identity);
            cubeInstantiate.transform.SetParent(CubeParent.transform);

            AllCubes.Add(cubeInstantiate.GetComponent<BlockEntity>());
            /*
            if (PicNum+1 == BlockGameTask._RandomQuestion)
            {
                // user的積木放在MainSceneRes的Cubes裡面才能判斷順序
                Cubes.Add(cubeInstantiate.GetComponent<BlockEntity>());
            }
            */
        }
    }

    /// <summary>
    /// 根據user的選擇把其他組的題目和積木放在桌上
    /// </summary>
    /// <param name="PicNum"> user選擇的圖片編號 </param>
    public void PutCubeOnTables(int PicNum)
    {
        GameObject CubeAns = GameObject.Find("CubeAns");
        int otherTable = 1;
        for (int i = 0; i < 4; i++)
        {
            if (i == PicNum)
            {
                //題目的位置等於Table底下的PicPos
                Parents[i].transform.GetChild(2).position = Tables[0].transform.GetChild(1).position;

                //積木的位置等於Table底下的CubePos
                Parents[i].transform.GetChild(1).transform.position = Tables[0].transform.GetChild(2).transform.position;

                Debug.Log("把積木的答案挪到桌子上");
                CubeAns.transform.GetChild(i).position = Tables[0].transform.GetChild(1).position;

                //Debug.Log("把積木答案的孩子們座標加上他爸的座標");
                //foreach (GameObject cubeans in Cube_Ans[i])
                //{
                //cubeans.transform.position += CubeAns.transform.GetChild(i).transform.position;
                //}
            }
            else
            {
                Debug.Log("in else");
                Parents[i].transform.GetChild(2).position = Tables[otherTable].transform.GetChild(1).position;
                Parents[i].transform.GetChild(1).transform.position = Tables[otherTable].transform.GetChild(2).transform.position;
                Debug.Log(Parents[i].transform.GetChild(2).position);
                Debug.Log(Parents[i].transform.GetChild(1).transform.position);
                Debug.Log("把積木的答案挪到桌子上");
                CubeAns.transform.GetChild(i).position = Tables[otherTable].transform.GetChild(1).position;

                Debug.Log("把積木答案的孩子們座標加上他爸的座標");
                foreach (GameObject cubeans in Cube_Ans[i])
                {
                    cubeans.transform.position += CubeAns.transform.GetChild(i).transform.position;
                }

                otherTable++;
            }
        }
    }

    public void InstantiateQuestion()
    {
        //GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        GameObject QuestionPicWithNum = Instantiate(GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/Question(Clone)"));
        //QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform);
        //QuestionPicWithNum.transform.position = GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.position;
        //GameObject QuestionPicWithNum = Instantiate(Question_Prefabs[BlockGameTask._RandomQuestion-1]);
        QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.GetChild(0).transform);
        //QuestionPicWithNum.transform.position = new Vector3(-1.5f, 0.2f, -0.3f);
        QuestionPicWithNum.transform.position = GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.GetChild(0).transform.position;
        QuestionPicWithNum.transform.rotation = Quaternion.Euler(-90, 0, -180);
        QuestionPicWithNum.transform.localScale = new Vector3(30, 30, 30);
    }
    public void InstantiateQuestionLv2()
    {
        //GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        GameObject QuestionPicWithNum = Instantiate(GameObject.Find("Parents/Q" + BlockGameTaskLv2._RandomQuestion + "_Parent/Question(Clone)"));
        //QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform);
        //QuestionPicWithNum.transform.position = GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.position;
        //GameObject QuestionPicWithNum = Instantiate(Question_Prefabs[BlockGameTask._RandomQuestion-1]);
        QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.GetChild(0).transform);
        //QuestionPicWithNum.transform.position = new Vector3(-1.5f, 0.2f, -0.3f);
        QuestionPicWithNum.transform.position = GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.GetChild(0).transform.position;
        QuestionPicWithNum.transform.rotation = Quaternion.Euler(-90, 0, -180);
        QuestionPicWithNum.transform.localScale = new Vector3(30, 30, 30);
    }
    public void InstantiateQuestion_Mono()
    {
        //GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").GetComponent<RawImage>().enabled = true;
        GameObject QuestionPicWithNum = Instantiate(GameObject.Find("Parents/Q" + BlockGameTask_Mono._RandomQuestion + "_Parent/Question(Clone)"));
        //QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform);
        //QuestionPicWithNum.transform.position = GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.position;
        //GameObject QuestionPicWithNum = Instantiate(Question_Prefabs[BlockGameTask._RandomQuestion-1]);
        QuestionPicWithNum.transform.SetParent(GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.GetChild(0).transform);
        //QuestionPicWithNum.transform.position = new Vector3(-1.5f, 0.2f, -0.3f);
        QuestionPicWithNum.transform.position = GameObject.Find("UserLeftSightCanvas/QuestionPicsWithNum/UserQuestionPic").transform.GetChild(0).transform.position;
        QuestionPicWithNum.transform.rotation = Quaternion.Euler(-90, 0, -180);
        QuestionPicWithNum.transform.localScale = new Vector3(30, 30, 30);
    }
}