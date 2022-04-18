using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntity : GameEntityBase
{
    public bool _isChose = false;
    public bool _isUserColor = false;
    public Transform ansTransform;
    public override void EntityDispose()
    {

    }

    public void ToAnsLv1()
    {
        if(_isUserColor == false)
        {
            BlockGameTask._playerRound = false;
        }
        else
        {
            BlockGameTask._playerRound = true;
        }
            
        _isChose = true;
        BlockGameTask.RecentOrder++;
        gameObject.transform.position = ansTransform.position;
        gameObject.transform.rotation = ansTransform.rotation;
        gameObject.transform.localScale = ansTransform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(gameObject.name);
        Debug.Log("********"+ansTransform.position);
    }
    public void ToAnsLv2()
    {
        Debug.Log(BlockGameTaskLv2.RecentOrder);
        if (_isUserColor == false)
        {
            BlockGameTaskLv2._playerRound = false;
        }
        else
        {
            BlockGameTaskLv2._playerRound = true;
        }
       
        _isChose = true;
        
        Debug.Log("RecentOrder: " + BlockGameTaskLv2.RecentOrder);
        gameObject.transform.position = ansTransform.position;
        gameObject.transform.rotation = ansTransform.rotation;
        gameObject.transform.localScale = ansTransform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(gameObject.name);
        Debug.Log("********" + ansTransform.position);
        BlockGameTaskLv2.RecentOrder++;
        Debug.Log(BlockGameTaskLv2.RecentOrder);
    }
    public void OtherSroupToAns()
    {
        Debug.Log("others put");
        _isChose = true;
        gameObject.transform.position = ansTransform.localPosition;
        gameObject.transform.rotation = ansTransform.rotation;
        gameObject.transform.localScale = ansTransform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
    }
}
