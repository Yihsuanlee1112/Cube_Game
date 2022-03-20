using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockPaperScissors : MonoBehaviour
{
    Vector3 position;
    Vector3 pos;
    GameObject Result;
    public List<GameObject> rockPaperScissors;
    public List<GameObject> rockPaperScissorsResult;
    public List<GameObject> RPS;
    public List<GameObject> RPSResult;
    public Animator RPS_Animator;
    public Animator FourP1Ani, FourP2Ani, FourP3Ani, TwoPAni;
    void Awake()
    {
        GameEventCenter.AddEvent("FourPlayerRPS", FourPlayerRPS);
        GameEventCenter.AddEvent("TwoPlayerRPS", TwoPlayerRPS);
        GameEventCenter.AddEvent("FourPlayerShowRockResult", FourPlayerShowRockResult);
        GameEventCenter.AddEvent("FourPlayerShowPaperResult", FourPlayerShowPaperResult);
        GameEventCenter.AddEvent("FourPlayerShowScissorsResult", FourPlayerShowScissorsResult);
        GameEventCenter.AddEvent("TwoPlayerShowRockResult", TwoPlayerShowRockResult);
        GameEventCenter.AddEvent("TwoPlayerShowPaperResult", TwoPlayerShowPaperResult);
        GameEventCenter.AddEvent("TwoPlayerShowScissorsResult", TwoPlayerShowScissorsResult);
        GameEventCenter.AddEvent("CloseAnimator2P", CloseAnimator2P);
        GameEventCenter.AddEvent("CloseAnimator4P", CloseAnimator4P);
        GameEventCenter.AddEvent("FirstRoundCloseAnimator4PP1P2", FirstRoundCloseAnimatorP1P2);
        GameEventCenter.AddEvent("FirstRoundCloseAnimatorP3", FirstRoundCloseAnimatorP3);
        GameEventCenter.AddEvent("FirstRoundFourPlayerShowRockResultP1P2", FirstRoundFourPlayerShowRockResultP1P2);
        GameEventCenter.AddEvent("FirstRoundFourPlayerShowPaperResultP1P2", FirstRoundFourPlayerShowPaperResultP1P2);
        GameEventCenter.AddEvent("FirstRoundFourPlayerShowScissorsResultP1P2", FirstRoundFourPlayerShowScissorsResultP1P2);
        GameEventCenter.AddEvent("FirstRoundFourPlayerShowResultP3", FirstRoundFourPlayerShowResultP3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FourPlayerRPS()
    {
        BlockGameTask._userChooseRPS = false;
        //RPS_Animator
        position = new Vector3((float)-0.1, (float)1.3, (float)1.97);
        //RPS.Add(Instantiate(rockPaperScissors[0], position, Quaternion.identity));
        Instantiate(rockPaperScissors[1], position, Quaternion.Euler(0, 15, 0));

        position = new Vector3((float)2.804, (float)1.3, (float)1.75);
        //RPS.Add(Instantiate(rockPaperScissors[1], position, Quaternion.identity));
        Instantiate(rockPaperScissors[2], position, Quaternion.Euler(0, -15, 0));

        position = new Vector3((float)4.325, (float)1.3, (float)2.82);
        //RPS.Add(Instantiate(rockPaperScissors[2], position, Quaternion.identity));
        Instantiate(rockPaperScissors[3], position, Quaternion.Euler(0, -45, 0)); 

        //choose
        position = new Vector3(0, 5, 0);
        pos = rockPaperScissors[5].transform.position;
        //RPS.Add(Instantiate(rockPaperScissors[3], position, Quaternion.identity));
        Instantiate(rockPaperScissors[5], pos, Quaternion.identity);

        FourP1Ani = GameObject.Find("RockPaperScissors4P_1(Clone)").GetComponent<Animator>();
        FourP2Ani = GameObject.Find("RockPaperScissors4P_2(Clone)").GetComponent<Animator>();
        FourP3Ani = GameObject.Find("RockPaperScissors4P_3(Clone)").GetComponent<Animator>();
        FourP1Ani.SetBool("isRPS", true);
        FourP2Ani.SetBool("isRPS", true);
        FourP3Ani.SetBool("isRPS", true);    
    }
    public void TwoPlayerRPS()
    {
        BlockGameTask._userChooseRPS = false;
        position = new Vector3((float)1.378, (float)1.144, (float)3.468);
        //RPS.Add(Instantiate(rockPaperScissors[2], position, Quaternion.identity));
        Instantiate(rockPaperScissors[0], position, Quaternion.identity);

        //choose
        position = new Vector3(0, 5, 0);
        pos = rockPaperScissors[4].transform.position;
        //RPS.Add(Instantiate(rockPaperScissors[0], position, Quaternion.identity));
        Instantiate(rockPaperScissors[4], pos, Quaternion.identity);

        TwoPAni = GameObject.Find("RockPaperScissors2P(Clone)").GetComponent<Animator>();
        TwoPAni.SetBool("isRPS", true);
        //TwoPAni.GetComponent<Animator>().SetBool("isRPS", true);
        Debug.Log("StartAni");
    }
    public void FourPlayerShowRockResult()
    {

        //Rock
        //Result = Resources.Load<Sprite>("Animation/RockPaperScissors/RockPaperScissors_0");

        //Paper
        //Result = Resources.Load<Sprite>("Animation/RockPaperScissors/RockPaperScissors_1");

        //Scissors
        //Result = Resources.Load<Sprite>("Animation/RockPaperScissors/RockPaperScissors_2");
        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);

        
        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        
        position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, -45, 0));
        Debug.Log(Result);
    }
    public void FourPlayerShowPaperResult()
    {
        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);

        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.Euler(0, -45, 0));
        Debug.Log(Result);
    }
    public void FourPlayerShowScissorsResult()
    {
        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);

        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, -45, 0));
        Debug.Log(Result);
    }
    public void TwoPlayerShowRockResult()
    {
        TwoPAni.SetBool("isRPS", false);
        position = new Vector3((float)1.378, (float)1.0, (float)3.6);
        Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.identity);
        Debug.Log(Result);
    }
    public void TwoPlayerShowPaperResult()
    {
        TwoPAni.SetBool("isRPS", false);

        position = new Vector3((float)1.378, (float)1.12, (float)3.6);
        Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.identity);
        Debug.Log(Result);
    }
    public void TwoPlayerShowScissorsResult()
    {
        TwoPAni.SetBool("isRPS", false);

        position = new Vector3((float)1.378, (float)1.12, (float)3.6);
        Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.identity);
        Debug.Log(Result);
    }
    public void CloseAnimator2P()
    {
        TwoPAni.SetBool("isRPS", false);
        Debug.Log("2PAniStop");
    }
    public void CloseAnimator4P()
    {
        Debug.Log("AniStop!!!!");

        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);
        Debug.Log("4PAniStop");
    }  
    public void FirstRoundCloseAnimatorP1P2()
    {
        Debug.Log("AniStop!!!!");

        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        //FourP3Ani.SetBool("isRPS", false);
        Debug.Log("1Round P1 P2 AniStop");
    }
    public void FirstRoundCloseAnimatorP3()
    {
        Debug.Log("AniStop!!!!");

        //FourP1Ani.SetBool("isRPS", false);
        //FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);
        Debug.Log("1Round P3 AniStop");
    }
    public void FirstRoundFourPlayerShowRockResultP1P2()
    {
        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        //FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);


        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);


        //position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        //Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, -45, 0));
        //Debug.Log(Result);
    }
    public void FirstRoundFourPlayerShowPaperResultP1P2()
    {
        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        //FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);

        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        //position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        //Result = Instantiate(rockPaperScissorsResult[1], position, Quaternion.Euler(0, -45, 0));
        //Debug.Log(Result);
    }
    public void FirstRoundFourPlayerShowScissorsResultP1P2()
    {
        FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        //FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);

        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        //position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        //Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, -45, 0));
        //Debug.Log(Result);
    }
    public void FirstRoundFourPlayerShowResultP3()
    {
        Debug.Log("小花慢出");
        int Ran = Random.Range(0, 2);
        //FourP1Ani.SetBool("isRPS", false);
        //FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);

        //position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        //Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, 15, 0));
        //Debug.Log(Result);

        //position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        //Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, -15, 0));
        //Debug.Log(Result);

        position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        Result = Instantiate(rockPaperScissorsResult[Ran], position, Quaternion.Euler(0, -45, 0));
        Debug.Log(Result);
    }
}
