using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRPS : MonoBehaviour
{
    //GameObject[] RPS;
    //public Animator[] RPS = new Animator[3];
    public Button Rock_Button;
    public Button Paper_Button;
    public Button Scissors_Button;
    GameObject TwoP;
    GameObject FourP1;
    GameObject FourP2;
    GameObject FourP3;
    //public Animator RPS_Animator;
    
    // Start is called before the first frame update
    void Start()
    {
        TwoP = GameObject.Find("RockPaperScissors2P(Clone)");
        FourP1 = GameObject.Find("RockPaperScissors4P_1(Clone)");
        FourP2 = GameObject.Find("RockPaperScissors4P_2(Clone)");
        FourP3 = GameObject.Find("RockPaperScissors4P_3(Clone)");
        Rock_Button.onClick.AddListener(chooseRock);
        Paper_Button.onClick.AddListener(choosePaper);
        Scissors_Button.onClick.AddListener(chooseScissors);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void chooseRock()
    {
        print("RRR");

    }
    public void choosePaper()
    {
        print("PPP");
    }
    public void chooseScissors()
    {
        print("SSS");
    }
    public void CloseAnimator2P()
    {
        TwoP.GetComponent<Animator>().SetTrigger("Idle");
    }
    public void CloseAnimator4P()
    {
        FourP1.GetComponent<Animator>().SetTrigger("Idle");
        FourP2.GetComponent<Animator>().SetTrigger("Idle");
        FourP3.GetComponent<Animator>().SetTrigger("Idle");
    }
}
