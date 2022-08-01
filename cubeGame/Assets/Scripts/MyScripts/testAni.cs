using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testAni : MonoBehaviour
{
    //public Animator NPC, BlueCube;
    public GameObject Teacher, Teacher2;
    public GameObject BlueCube;
    public GameObject Hat1, Hat2, Green1, Green2, star1, star2,
        yoyo1, yoyo2, Red1, Red2, Mei1, Mei2, Hua1, Hua2;
    public static bool TeacherMoveToUser = false;
    public static bool TeacherMoveToXiaoMei = false;
    public static bool TeacherMoveBackFromUser = false;
    public static bool TeacherMoveBackFromXiaoMei = false;
    // Start is called before the first frame update
    void Start()
    {
       //StartCoroutine(PlayAni());
    }
    
    // Update is called once per frame
    void Update()
    {
        float speed = 0.7f;
        float rotateSpeed = 2f;
        if (TeacherMoveToUser)
        {
            Teacher.transform.position = Vector3.MoveTowards(Teacher.transform.position, 
                new Vector3(0.87f, 0.037f, 4.5f), speed * Time.deltaTime);
        }
        else if(TeacherMoveToXiaoMei)
        {
            Debug.Log("TeacherMoveToXiaoMei");
            Teacher.transform.position = Vector3.MoveTowards(Teacher.transform.position, 
                new Vector3(2.189f, 0.037f, 2.628f), speed * Time.deltaTime);
        }
        else if(TeacherMoveBackFromUser)
        {
            Debug.Log("TeacherMoveBackFromUser");
            Teacher.transform.rotation = Quaternion.Slerp(Teacher.transform.rotation, 
                Quaternion.Euler(0, 208f, 0), rotateSpeed * Time.deltaTime);
            Teacher.transform.position = Vector3.MoveTowards(Teacher.transform.position, 
                new Vector3(0.098f, 0.037f, 1.627f), speed * Time.deltaTime);
        }
        else if(TeacherMoveBackFromXiaoMei)
        {
            Debug.Log("TeacherMoveBackFromXiaoMei");
            Teacher.transform.rotation = Quaternion.Slerp(Teacher.transform.rotation, 
                Quaternion.Euler(0, 250f, 0), rotateSpeed * Time.deltaTime);
            Teacher.transform.position = Vector3.MoveTowards(Teacher.transform.position, 
                new Vector3(0.098f, 0.037f, 1.627f), speed * Time.deltaTime);
            
        }
        else if(!TeacherMoveBackFromXiaoMei || !TeacherMoveBackFromUser)
        {
            Teacher.transform.rotation = Quaternion.Slerp(Teacher.transform.rotation,
                Quaternion.Euler(0, 19f, 0), rotateSpeed * Time.deltaTime);
        }
        
    }

   IEnumerator PlayAni()
   {

        //Teacher.GetComponent<Animator>().SetBool("isTakeCube", true);
        //Teacher.GetComponent<Animator>().SetBool("isTakeCubeWalking", true);
        //Teacher.GetComponent<Animator>().SetBool("isPutingCube", true);
        //yield return new WaitForSeconds(3.5f);
        //TeacherMoveToXiaoMei = true;
        //BlueCube.GetComponent<Animator>().Play("Teacher Take box walk");
        //yield return new WaitForSeconds(10);
        //TeacherMoveToXiaoMei = false;
        //Debug.Log("老師走到小美");
        //TeacherMoveBackFromXiaoMei = true;
        //Teacher.GetComponent<Animator>().SetBool("isTakeCube", false);
        //Teacher.GetComponent<Animator>().SetBool("isTakeCubeWalking", false);
        //Teacher.GetComponent<Animator>().SetBool("isPutingCube", false);
        //Debug.Log("老師走回去");
        //yield return new WaitForSeconds(5);
        //TeacherMoveBackFromXiaoMei = false;
        //yield return null;

        Teacher2.GetComponent<Animator>().SetBool("isTakeCubeToUser", true);
        //BlueCube.GetComponent<Animator>().Play("拿積木走向 user後走回去");
        BlueCube.GetComponent<Animator>().SetBool("isToUser", true);
        yield return new WaitForSeconds(14);
        Teacher2.GetComponent<Animator>().SetBool("isTakeCubeToUser", false);
        //BlueCube.GetComponent<Animator>().SetBool("isToUser", false);
        //yield return new WaitForSeconds(0.005f);
        //star.GetComponent<Animator>().SetBool("isLeftClap", true);
        //yield return new WaitForSeconds(3);

        //yoyo1.GetComponent<Animator>().SetBool("isClapRightHand", true);
        //yield return new WaitForSeconds(3);
        //yoyo1.GetComponent<Animator>().SetBool("isClapRightHand", false);
        //yield return new WaitForSeconds(3);
        //yoyo2.GetComponent<Animator>().SetBool("isClapRightHand", true);
        ////yoyo2.GetComponent<Animator>().Play("右手擊掌");
        //yield return new WaitForSeconds(3);
        //yoyo2.GetComponent<Animator>().SetBool("isClapRightHand", false);

        //yield return new WaitForSeconds(3);
        //star1.GetComponent<Animator>().SetBool("isClapLeftHand", true);
        //yield return new WaitForSeconds(3);
        //star1.GetComponent<Animator>().SetBool("isClapLeftHand", false);
        //star1.GetComponent<Animator>().SetBool("isClapLeftHandInSpace", true);
        //yield return new WaitForSeconds(3);
        //star1.GetComponent<Animator>().SetBool("isClapLeftHandInSpace", false);

        //yield return new WaitForSeconds(3);
        //star2.GetComponent<Animator>().SetBool("isClapLeftHand", true);
        //yield return new WaitForSeconds(3);
        //star2.GetComponent<Animator>().SetBool("isClapLeftHand", false);
        //star2.GetComponent<Animator>().SetBool("isClapLeftHandInSpace", true);
        //yield return new WaitForSeconds(3);
        //star2.GetComponent<Animator>().SetBool("isClapLeftHandInSpace", false);
        //Debug.Log("老師走回去");


        //Teacher.GetComponent<Animator>().SetBool("isTalk", true);
        //Teacher2.GetComponent<Animator>().SetBool("isTalk", true);
        //yield return new WaitForSeconds(0.005f);
        //BlueCube.GetComponent<Animator>().SetBool("isToUser", true);
        //Teacher.GetComponent<Animator>().SetBool("isTakeCubeToUser", false);
        //star1.GetComponent<Animator>().SetBool("isClapLeftHand", true);
        //star2.GetComponent<Animator>().SetBool("isClapLeftHand", true);

        //Hat1.GetComponent<Animator>().SetBool("isClapRightHand", true);
        //Hat2.GetComponent<Animator>().SetBool("isClapRightHand", true);

        //Mei1.GetComponent<Animator>().SetBool("isRaiseHand", true);
        //yield return new WaitForSeconds(2);
        //Teacher2.GetComponent<Animator>().SetBool("isTalkingToXiaoMei", true);
        //yield return new WaitForSeconds(5);
        //Teacher2.GetComponent<Animator>().SetBool("isTalkingToXiaoMei", false);
        //Mei1.GetComponent<Animator>().SetBool("isRaiseHand", false);
        //Mei1.GetComponent<Animator>().SetBool("isRaiseHandAndTalkToTeacher", true);
        //yield return new WaitForSeconds(3);
        //Mei1.GetComponent<Animator>().SetBool("isRaiseHandAndTalkToTeacher", false);
        //Teacher2.GetComponent<Animator>().SetBool("isTakeCubeToXiaoMei", true);
        //yield return new WaitForSeconds(18);
        //Teacher2.GetComponent<Animator>().SetBool("isTakeCubeToXiaoMei", false) ;
        //Mei2.GetComponent<Animator>().SetBool("isClapRightHand", true);

        //Hua1.GetComponent<Animator>().SetBool("isClapRightHand", true);
        //Hua2.GetComponent<Animator>().SetBool("isClapRightHand", true);

        //yoyo1.GetComponent<Animator>().SetBool("isClapRightHand", true);
        //yoyo2.GetComponent<Animator>().SetBool("isClapRightHand", true);
        yield return null;

    }
    

}
