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
    public List<GameObject> Cubes;
    Vector3 position;

    [Header("積木和題目的父物件")]
    public List<GameObject> Parents;
    float interval = 0.079f; //3.623-3.544 但好像間隔不平均(?

    [Header("桌子的位置")]
    public List<GameObject> Desks;

    [Header("積木的父物件")]
    public List<GameObject> CubeParents;


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

        for (int i = 0; i < 4; i++)
        {
            Question(i, Cube_Prefabs[i], Parents[i], CubeParents[i]);
        }

        PutCubeOnDesk(BlockGameTask._RandomQuestion - 1);
    }

    /// <summary>
    /// 生成題目和積木(其位置都是以放在user桌上為基準)，再依據不同題目包在對應的父物件底下(目的是可以改變父物件位置放在NPC桌上)
    /// </summary>
    /// <param name="PicNum"></param>
    /// <param name="PicPos"></param>
    /// <param name="CubePrefabs"></param>
    /// <param name="CubesInitPos"></param>
    /// <param name="Parent"></param>
    public void Question(int PicNum, List<GameObject> CubePrefabs, GameObject Parent, GameObject CubeParent)
    {
        Vector3 PicsPos = new Vector3((float)0.0, (float)0.0, (float)0.0);
        Vector3 CubesPos = new Vector3((float)0.0, (float)0.0, (float)0.0);

        GameObject picInstantiate = Instantiate(Question_Prefabs[PicNum], PicsPos, Quaternion.Euler(0, 180, 0)); // 因為場景X和Z方向不同，所以4張圖統一轉向
        picInstantiate.transform.SetParent(Parent.transform);

        foreach (GameObject cube in CubePrefabs)
        {
            CubesPos.z = CubesPos.z + interval;
            GameObject cubeInstantiate = Instantiate(cube, CubesPos, Quaternion.identity);
            cubeInstantiate.transform.SetParent(CubeParent.transform);
            /*
            if (PicNum+1 == BlockGameTask.RandomQuestion)
            {
                // user的積木放在MainSceneRes的Cubes裡面才能判斷順序
                BlockGameTask.Cubes.Add(cubeInstantiate.GetComponent<BlockEntity>());
            }
            */
        }
    }

    /// <summary>
    /// 根據user的選擇把其他組的積木放在桌上
    /// </summary>
    /// <param name="PicNum"> user選擇的圖片編號 </param>
    public void PutCubeOnDesk(int PicNum)
    {
        int otherDesk = 1;
        for (int i = 0; i < 4; i++)
        {
            if (i == PicNum)
            {
                //整個搬過去
                Parents[i].transform.position = Desks[0].transform.position;
                //設定積木的位置，題目是GetChild(0)
                Parents[i].transform.GetChild(0).transform.position = Desks[0].transform.GetChild(0).transform.position;
            }
            else
            {
                Parents[i].transform.position = Desks[otherDesk].transform.position;
                Parents[i].transform.GetChild(0).transform.position = Desks[otherDesk].transform.GetChild(0).transform.position;
                otherDesk++;
            }
        }
    }
}