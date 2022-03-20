using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTriggerDisabled : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider answerCube)
    {
        string cubeName = answerCube.name;
        print(cubeName);
        int delStr = cubeName.IndexOf("(Clone)");
        print(cubeName + "I took");
        if (delStr >= 0)
        {
            cubeName = cubeName.Remove(delStr);
            print(cubeName + "again");
        }
        string quadName = gameObject.name;
        print(quadName);
        print(quadName.Length);
        string NewQuadName = quadName.Remove(quadName.Length - 9, 9);
        print(NewQuadName + "?????");
        if (NewQuadName == cubeName)
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
            print("My " + this.gameObject);
            print("Trigger off"); 
        }
        else if(NewQuadName != cubeName)
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
            print("Wrong " + cubeName +" Trigger still on!");
        }
        
    }
    /*public void OnCollisionEnter(Collision Quad)
    {
        
        if(Quad.gameObject.CompareTag("cube"))
        {

            string cubeName = Cube.name;
            string NewCubeName = cubeName.Remove(cubeName.Length - 1, 9);
            print(NewCubeName + "?????");
            this.GetComponent<BoxCollider>().enabled = false;
            print("My " + Quad);
            //print("my " + NewCubeName);
        }
        
        print("Collider off");
    }*/
}
