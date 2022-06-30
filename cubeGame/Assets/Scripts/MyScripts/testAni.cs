using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testAni : MonoBehaviour
{
    //public Animator NPC, BlueCube;
    public GameObject Teacher;
    public GameObject BlueCube;
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

        Teacher.GetComponent<Animator>().SetBool("isTakeCube", true);
        Teacher.GetComponent<Animator>().SetBool("isTakeCubeWalking", true);
        Teacher.GetComponent<Animator>().SetBool("isPutingCube", true);
        yield return new WaitForSeconds(3.5f);
        TeacherMoveToXiaoMei = true;
        BlueCube.GetComponent<Animator>().Play("Teacher Take box walk");
        yield return new WaitForSeconds(10);
        TeacherMoveToXiaoMei = false;
        Debug.Log("老師走到小美");
        TeacherMoveBackFromXiaoMei = true;
        Teacher.GetComponent<Animator>().SetBool("isTakeCube", false);
        Teacher.GetComponent<Animator>().SetBool("isTakeCubeWalking", false);
        Teacher.GetComponent<Animator>().SetBool("isPutingCube", false);
        Debug.Log("老師走回去");
        yield return new WaitForSeconds(5);
        TeacherMoveBackFromXiaoMei = false;
        yield return null;


    }
    

}
