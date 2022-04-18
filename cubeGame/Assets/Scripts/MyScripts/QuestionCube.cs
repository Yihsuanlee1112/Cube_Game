using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCube : MonoBehaviour
{
    public static bool _isCheck = false;
    public int CubeOrder = 0;
    public string TxtOrder = " ";
    private void Awake()
    {
        GameEventCenter.AddEvent("CheckOrder", CheckOrder);
    }
    public void CheckOrder()
    {
        TxtOrder = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
        CubeOrder = int.Parse(TxtOrder);
    }
}
