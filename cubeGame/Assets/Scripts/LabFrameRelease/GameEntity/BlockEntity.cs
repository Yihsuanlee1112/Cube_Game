using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntity : GameEntityBase
{
    public bool _isChose = false;
    public bool _isUserColor = false;
    public GameObject ansTransform;
    public override void EntityDispose()
    {

    }

    public void ToAnsLv1()
    {
        Debug.Log("In ToAnsLv1 RecentORder: " + BlockGameTask.RecentOrder);
        _isChose = true;
        gameObject.transform.position = ansTransform.transform.position;
        gameObject.transform.rotation = ansTransform.transform.rotation;
        gameObject.transform.localScale = ansTransform.transform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(gameObject.name);
        Debug.Log("********"+ansTransform.transform.position);
        BlockGameTask.RecentOrder++;
        Debug.Log(BlockGameTask.RecentOrder);
    }
    public void ToAnsLv2()
    {
        Debug.Log("In ToAnsLv2 RecentORder: "+BlockGameTaskLv2.RecentOrder);
        _isChose = true;
        gameObject.transform.position = ansTransform.transform.position;
        gameObject.transform.rotation = ansTransform.transform.rotation;
        gameObject.transform.localScale = ansTransform.transform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(gameObject.name);
        Debug.Log("********" + ansTransform.transform.position);
        BlockGameTaskLv2.RecentOrder++;
        Debug.Log(BlockGameTaskLv2.RecentOrder);
    }
    public void OtherSroupToAns()
    {
        Debug.Log("others put");
        _isChose = true;
        gameObject.transform.position = ansTransform.transform.localPosition;
        gameObject.transform.rotation = ansTransform.transform.rotation;
        gameObject.transform.localScale = ansTransform.transform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
    }
}
