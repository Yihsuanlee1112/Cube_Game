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
        Debug.Log("leftgroupstart");
        while (BlockGameTask._StartTobuild)
        {
            foreach (BlockEntity cube in cube_GC)
            {
                if (_RoundA)  //玩家回合
                {
                    Red.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    Red.SetBool("isTakeCube", false);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    _RoundA = false;
                }
                else
                {
                    Yoyo.SetBool("isTakeCube", true);
                    yield return new WaitForSeconds(7);
                    Yoyo.SetBool("isTakeCube", false);
                    GameEventCenter.DispatchEvent("OtherGroupCubeAns", cube);
                    _RoundA = true;
                }
            }
        }
        Debug.Log("Finished");
        yield return null;
    }
}
