using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherGroupBiuldBlock : MonoBehaviour
{
    private bool _RoundA = true;
    private Animator KidA, KidB, KidC, KidD, KidE, KidF;
    private List<BlockEntity> Cubes;
     private void Awake()
    {
        Debug.Log("othergroupstart");
        StartCoroutine(LeftGroup());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator OtherGroupBuildBlock()
    {
        Debug.Log("otherGroupBiuldBlock=>Cubes.Count: " + Cubes.Count);
        StartCoroutine(LeftGroup());
        yield return null;
    }
    IEnumerator LeftGroup()
    {
        Cubes = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Cubes;
       
        Debug.Log("othergroupstart");
        while (BlockGameTask._StartTobuild)
        {
            if (_RoundA)  //玩家回合
            {
                KidA.Play("Puzzle");
                KidB.Play("Puzzle");
                KidC.Play("Puzzle");
                yield return new WaitForSeconds(3);
            }
            else  
            {
                KidD.Play("Puzzle");
                KidE.Play("Puzzle");
                KidF.Play("Puzzle");
                yield return new WaitForSeconds(3);
            }
        }
        yield return null;
    }
}
