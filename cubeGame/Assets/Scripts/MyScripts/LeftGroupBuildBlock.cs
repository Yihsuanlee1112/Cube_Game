using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeftGroupBuildBlock : MonoBehaviour
{
    private bool _RoundA = true;
    private Animator Green, Hat;
    private List<BlockEntity> cube_GA;
    private void Awake()
    {


    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LeftGroup());
    }
    IEnumerator LeftGroup()
    {
        cube_GA = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GA;
        Green = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Green;
        Hat = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Hat;
        //Debug.Log("leftgroupstart");
        if (BlockGameTask._StartTobuild)
        {
            Debug.Log("Left RoundA: " + _RoundA);
            if (_RoundA)  //玩家回合
            {
                foreach (BlockEntity cube in cube_GA)
                {
                    Hat.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    Hat.SetBool("isTakeCube", false);
                    //GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    //GameEventCenter.DispatchEvent("OtherGroupCubeAnsLv2", cube);
                    _RoundA = false;
                    yield return new WaitForSeconds(7);
                }
            }
            if (!_RoundA)
            {
                foreach (BlockEntity cube in cube_GA)
                {
                    Green.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    Green.SetBool("isTakeCube", false);
                    //GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    //GameEventCenter.DispatchEvent("OtherGroupCubeAnsLv2", cube);
                    _RoundA = true;
                    yield return new WaitForSeconds(7);
                }
            }
        }
        else
        {
            Hat.SetBool("isTakeCube", false);
            Green.SetBool("isTakeCube", false);
        }

            yield return null;
    }

}

