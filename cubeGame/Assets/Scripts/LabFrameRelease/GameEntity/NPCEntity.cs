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
    public List<AudioClip> ChineseSpeechList;
    public List<AudioClip> EnglishSpeechList;
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
        GameEventCenter.AddEvent("NPCRemindLv2", NPCRemindLv2);
        GameEventCenter.AddEvent("NPCRemind_OrderLv2", NPCRemind_OrderLv2);
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
        Debug.Log("NPCPUTOBJ!!!!!!");
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
        if (GameDataManager.FlowData.Language == Language.中文)
        {
            GameAudioController.Instance.PlayOneShot(ChineseSpeechList[0]);//NPC_Remind
        }
        else
        {
            GameAudioController.Instance.PlayOneShot(EnglishSpeechList[0]);//NPC_Remind
        }
    }
    public void NPCRemindLv2()
    {
        Remindcount++;
        Debug.Log("hello!!!!!");
        BlockGameTaskLv2._npcremind = true;
        animator.Play("Talk");
        //clip = Resources.Load<AudioClip>("AudioClip/NPC_Remind");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        if (GameDataManager.FlowData.Language == Language.中文)
        {
            GameAudioController.Instance.PlayOneShot(ChineseSpeechList[0]);//NPC_Remind
        }
        else
        {
            GameAudioController.Instance.PlayOneShot(EnglishSpeechList[0]);//NPC_Remind
        }
    }
    public void NPCRemind_Order()
    {
        Remindcount++;
        Debug.Log("hi!!!!!");
        BlockGameTask._npcremind = true;
        animator.Play("Talk");
        //clip = Resources.Load<AudioClip>("AudioClip/NPC_Remind2");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        if (GameDataManager.FlowData.Language == Language.中文)
        {
            GameAudioController.Instance.PlayOneShot(ChineseSpeechList[1]);//NPC_Remind_Order
        }
        else
        {
            GameAudioController.Instance.PlayOneShot(EnglishSpeechList[1]);//NPC_Remind_Order
        }
    }
    public void NPCRemind_OrderLv2()
    {
        Remindcount++;
        Debug.Log("hi!!!!!");
        BlockGameTaskLv2._npcremind = true;
        animator.Play("Talk");
        //clip = Resources.Load<AudioClip>("AudioClip/NPC_Remind2");
        //GameAudioController.Instance.PlayOneShot(clip);
        //yield return new WaitForSeconds(clip.length);
        GameAudioController.Instance.PlayOneShot(ChineseSpeechList[6]);//NPC_Remind_Order
        if (GameDataManager.FlowData.Language == Language.中文)
        {
            GameAudioController.Instance.PlayOneShot(ChineseSpeechList[1]);//NPC_Remind_Order
        }
        else
        {
            GameAudioController.Instance.PlayOneShot(EnglishSpeechList[1]);//NPC_Remind_Order
        }
    }
}
