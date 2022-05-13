using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testAni : MonoBehaviour
{
    //public Animator NPC, BlueCube;
    public GameObject cube;//use list for each cubes in every Question

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAni());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator PlayAni()
   {
        string CubeName = cube.name;
        cube.GetComponent<BlockEntity>()._isChose = true;
        //cube.GetComponent<BlockEntity>()._isOnUserTable = true;
        yield return new WaitForSeconds(3);
        if(cube.GetComponent<BlockEntity>()._isChose && cube.GetComponent<BlockEntity>()._isOnUserTable && !cube.GetComponent<BlockEntity>()._isUserColor)
        {
            Debug.Log(cube.GetComponent<BlockEntity>()._isChose);
            Debug.Log(cube.GetComponent<BlockEntity>()._isOnUserTable);
            Debug.Log(cube.GetComponent<BlockEntity>()._isUserColor);
            //GameObject.Find("Tables/UserTable/block/" + CubeName).GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("desk/block/" + CubeName).GetComponent<Animator>().Play("Take Block");
        }
        ////GameObject BlueCube = GameObject.Find("BlueCube");
        //NPC.SetBool("isTakeCube", true);
        //yield return new WaitForSeconds(3);
        //BlueCube.Play("Take Block");
        //NPC.SetBool("isTakeCube", false);
        //Debug.Log("PlayAni");
        ////BlueCube.SetBool("isTake", true);
        //if (CubeName.contains("BlueCube"))
        //{
        //    for (int i = 0; i < 13; i++)
        //    {
        //        if (cubeName == GameObject.Find("Tables/UserTable/block").transform.GetChild(i).name)
        //        {
        //            Debug.Log(GameObject.Find("Tables/UserTable/block").transform.GetChild(i).name);
        //            GameObject.Find("Tables/UserTable/block").transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        //            GameObject.Find("Tables/UserTable/block").transform.GetChild(i).GetComponent<Animator>().Play("Take Block");
        //        }

        //    }
        //}

        yield return new WaitForSeconds(5);
    }
    

}
