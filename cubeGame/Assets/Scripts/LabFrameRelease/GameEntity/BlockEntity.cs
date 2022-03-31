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

    public void ToAns()
    {
        if(_isUserColor == false)
        {
            BlockGameTaskLv2._playerRound = false;
        }
        else
            BlockGameTaskLv2._playerRound = true;
        _isChose = true;
        gameObject.transform.position = ansTransform.position;
        gameObject.transform.rotation = ansTransform.rotation;
        gameObject.transform.localScale = ansTransform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(gameObject.name);
        Debug.Log("********"+ansTransform.position);
    }
    public void OtherSroupToAns()
    {
        Debug.Log("others put");
        _isChose = true;
        gameObject.transform.position = ansTransform.position;
        gameObject.transform.rotation = ansTransform.rotation;
        gameObject.transform.localScale = ansTransform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
    }
}
