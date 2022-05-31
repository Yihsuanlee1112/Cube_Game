using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGroupBuildBlock : MonoBehaviour
{
    private bool _RoundA = true;
    private Animator Yoyo, Red;
    private List<BlockEntity> cube_GC;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RightGroup());
    }
    IEnumerator RightGroup()
    {
        cube_GC = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().cube_GC;
        Yoyo = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Yoyo;
        Red = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().Red;
        //Debug.Log("Rightgroupstart");
        if (BlockGameTask._StartTobuild)
        {
            Debug.Log("Right RoundA: " + _RoundA);
            if (_RoundA)  //玩家回合
            {
                foreach (BlockEntity cube in cube_GC)
                {
                    Yoyo.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    Yoyo.SetBool("isTakeCube", false);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAnsLv2", cube);
                    _RoundA = false;

                    yield return new WaitForSeconds(3);
                }
            }
            if (!_RoundA)
            {
                foreach (BlockEntity cube in cube_GC)
                {
                    Red.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    Red.SetBool("isTakeCube", false);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAnsLv2", cube);
                    _RoundA = true;
                    yield return new WaitForSeconds(3);
                }
            }
        }
        yield return null;
    }
}
