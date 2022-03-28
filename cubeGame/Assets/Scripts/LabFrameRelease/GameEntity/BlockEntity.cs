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
        _isChose = true;
        gameObject.transform.position = ansTransform.position;
        gameObject.transform.rotation = ansTransform.rotation;
        gameObject.transform.localScale = ansTransform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(gameObject.name);
        Debug.Log("********"+ansTransform.position);
    }
}
