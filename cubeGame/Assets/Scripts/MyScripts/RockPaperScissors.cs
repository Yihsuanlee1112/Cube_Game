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
        GameEventCenter.AddEvent("FourPlayerShowResult", FourPlayerShowResult);
        GameEventCenter.AddEvent("TwoPlayerShowResult", TwoPlayerShowResult);
        GameEventCenter.AddEvent("CloseAnimator2P", CloseAnimator2P);
        GameEventCenter.AddEvent("CloseAnimator4P", CloseAnimator4P);
        GameEventCenter.AddEvent("FirstRoundCloseAnimatorP1P3", FirstRoundCloseAnimatorP1P3);
        GameEventCenter.AddEvent("FirstRoundCloseAnimatorP2", FirstRoundCloseAnimatorP2);
        GameEventCenter.AddEvent("FirstRoundFourPlayerShowResultP1P3", FirstRoundFourPlayerShowResultP1P3);
        GameEventCenter.AddEvent("FirstRoundFourPlayerShowResultP2", FirstRoundFourPlayerShowResultP2);
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
    public void FourPlayerShowResult()
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
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);

        
        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        
        position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.Euler(0, -45, 0));
        Debug.Log(Result);
    }
    public void TwoPlayerShowResult()
    {
        TwoPAni.SetBool("isRPS", false);

        position = new Vector3((float)1.378, (float)1.12, (float)3.6);
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.identity);
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
    public void FirstRoundCloseAnimatorP1P3()
    {
        Debug.Log("AniStop!!!!");

        FourP1Ani.SetBool("isRPS", false);
        //FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);
        Debug.Log("1Round P1 P3 AniStop");
    }
    public void FirstRoundCloseAnimatorP2()
    {
        Debug.Log("AniStop!!!!");

        //FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        //FourP3Ani.SetBool("isRPS", false);
        Debug.Log("1Round P2 AniStop");
    }
    public void FirstRoundFourPlayerShowResultP1P3()
    {
        FourP1Ani.SetBool("isRPS", false);
        //FourP2Ani.SetBool("isRPS", false);
        FourP3Ani.SetBool("isRPS", false);

        position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.Euler(0, 15, 0));
        Debug.Log(Result);//


        ///position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        ///Result = Instantiate(rockPaperScissorsResult[0], position, Quaternion.Euler(0, -15, 0));
        //Debug.Log(Result);


        position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.Euler(0, -45, 0));
        Debug.Log(Result);//
    }
    
    public void FirstRoundFourPlayerShowResultP2()
    {
        Debug.Log("小花慢出");
        //FourP1Ani.SetBool("isRPS", false);
        FourP2Ani.SetBool("isRPS", false);
        //FourP3Ani.SetBool("isRPS", false);

        //position = new Vector3((float)-0.1, (float)1.3, (float)2.0);
        //Result = Instantiate(rockPaperScissorsResult[2], position, Quaternion.Euler(0, 15, 0));
        //Debug.Log(Result);

        position = new Vector3((float)2.804, (float)1.3, (float)1.8);
        Result = Instantiate(rockPaperScissorsResult[BlockGameTask._UsersChoice], position, Quaternion.Euler(0, -15, 0));
        Debug.Log(Result);

        //position = new Vector3((float)4.325, (float)1.3, (float)3.0);
        //Result = Instantiate(rockPaperScissorsResult[Ran], position, Quaternion.Euler(0, -45, 0));
        //Debug.Log(Result);
    }
}
