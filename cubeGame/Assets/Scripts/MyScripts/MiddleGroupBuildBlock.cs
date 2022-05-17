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
        Debug.Log("Middlegroupstart");
        if (BlockGameTask._StartTobuild)
        {
            foreach (BlockEntity cube in cube_GB)
            {
                if (_RoundA)  //玩家回合
                {
                    XiaoHua.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    XiaoHua.SetBool("isTakeCube", false);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    _RoundA = false;
                }
                else
                {
                    XiaoMei.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    XiaoMei.SetBool("isTakeCube", false);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    _RoundA = true;
                }
            }
        }
        Debug.Log("Finished");
        yield return null;
    }
}
