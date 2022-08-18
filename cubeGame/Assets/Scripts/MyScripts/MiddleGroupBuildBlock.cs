using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleGroupBuildBlock : MonoBehaviour
{
    private bool _RoundA = true;
    private Animator XiaoHua, XiaoMei;
    private List<BlockEntity> cube_GB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MiddleGroup());
    }
    IEnumerator MiddleGroup()
    {
        cube_GB = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GB;
        XiaoHua = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoHua;
        XiaoMei = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().XiaoMei;
        //Debug.Log("Middlegroupstart");
        if (BlockGameTask._StartTobuild)
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Mid RoundA: " + _RoundA);
                if (_RoundA)  //小美
                {
                    foreach (BlockEntity cube in cube_GB)
                    {
                        XiaoHua.SetBool("isTakeCube", true);
                        //XiaoHua.Play("堆積木");
                        yield return new WaitForSeconds(7);
                        XiaoHua.SetBool("isTakeCube", false);
                        GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                        GameEventCenter.DispatchEvent("OtherGroupCubeAnsLv2", cube);
                        _RoundA = false;
                        yield return new WaitForSeconds(7);
                    }
                }
                if (!_RoundA)
                {
                    foreach (BlockEntity cube in cube_GB)
                    {
                        XiaoMei.SetBool("isTakeCube", true);
                        //XiaoMei.Play("堆積木");
                        yield return new WaitForSeconds(7);
                        XiaoMei.SetBool("isTakeCube", false);
                        GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                        GameEventCenter.DispatchEvent("OtherGroupCubeAnsLv2", cube);
                        _RoundA = true;
                        yield return new WaitForSeconds(7);
                    }
                }
            }
        }
        //else
        //{
        //    XiaoHua.SetBool("isTakeCube", false);
        //    XiaoMei.SetBool("isTakeCube", false);
        //    //XiaoHua.SetBool("isClapHand", true);
        //    //XiaoMei.SetBool("isClapHand", true);
        //    //yield return new WaitForSeconds(3.5f);
        //    //XiaoHua.SetBool("isClapHand", false);
        //    //XiaoMei.SetBool("isClapHand", false);
        //}
        yield return null;
    }
}
