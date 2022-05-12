using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAni : MonoBehaviour
{
    public Animator NPC, BlueCube;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAni());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

   IEnumerator PlayAni()
    {
        //GameObject BlueCube = GameObject.Find("BlueCube");
        NPC.Play("坐在椅子上放積木(2D圖片) NPC用左手拿取桌上的積木，然後放在中間的圖片上");
        yield return new WaitForSeconds(1.5f);
        BlueCube.Play("Take Block");
        Debug.Log("PlayAni");
        //BlueCube.SetBool("isTake", true);
        yield return new WaitForSeconds(5);
    }
}
