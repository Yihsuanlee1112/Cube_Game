using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
//using SpeechLib;

public class NPCEntity : GameEntityBase
{
    public Action<GameEntityBase> Action;
    public Animator animator;
    public List<AudioClip> speechList;
    public GameObject npchand;
    private GameObject ObjectTaked;
    private AudioClip clip;
    public static int Remindcount = 0;

    public override void EntityDispose()
    {

    }

    public override void EntityInit()
    {
        GameEventCenter.AddEvent("NPCRemind", NPCRemind);
        GameEventCenter.AddEvent("NPCRemind_Order", NPCRemind_Order);
    }

    
    //public void NPCTalk()
    //{
    //    Debug.Log("講話!");
    //    SpVoice npcsay = new SpVoice();
    //    npcsay.Speak(GameDataManager.FlowData.UserName, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    //}
    
    public void NPCTakeObject(GameObject gameObject)//拼圖遊戲拿櫃子上的東西
    {
        Debug.Log("NPC touch block");
        Debug.Log(gameObject.name);
        gameObject.transform.position = npchand.transform.position;
        gameObject.transform.parent = null;
        gameObject.transform.SetParent(npchand.transform);
        ObjectTaked = gameObject;
    }

    public void NPCPutObject(Transform puttransform)
    {
        Debug.Log("NPC put block");
        ObjectTaked.transform.parent = null;
        ObjectTaked.transform.position = puttransform.position;
        ObjectTaked = null;
    }

    public void NPCRemind()
    {
        Remindcount++;
        Debug.Log("hello!!!!!");
        BlockGameTask._npcremind = true;
        animator.Play("Talk");
        //clip = Resources.Load<AudioClip>("AudioClip/NPC_Remind");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(speechList[3]);//NPC_Remind
        
    }
    public void NPCRemind_Order()
    {
        Remindcount++;
        Debug.Log("hi!!!!!");
        BlockGameTask._npcremind = true;
        animator.Play("Talk");
        clip = Resources.Load<AudioClip>("AudioClip/NPC_Remind2");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(speechList[6]);//NPC_Remind_Order
        Debug.Log("wait for clip.length sec");
        
    }
}
