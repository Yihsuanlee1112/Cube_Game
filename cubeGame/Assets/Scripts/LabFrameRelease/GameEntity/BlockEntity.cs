using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntity : GameEntityBase
{
    public bool _isChose = false;
    public bool _isUserColor = false;
    public bool _isOnUserTable = false;
    public GameObject ansTransform;
    public override void EntityDispose()
    {

    }

    public void ToAnsLv1()
    {
        string cubeName;
        //Get Cube name without "(Clone)"
        cubeName = gameObject.name;
        int delStr = cubeName.IndexOf("(Clone)");
        if (delStr >= 0)
        {
            cubeName = cubeName.Remove(delStr);
        }
        Debug.Log("In ToAnsLv1 RecentORder: " + BlockGameTask.RecentOrder);
        _isChose = true;
        if (gameObject.GetComponent<BlockEntity>()._isChose && gameObject.GetComponent<BlockEntity>()._isOnUserTable && !gameObject.GetComponent<BlockEntity>()._isUserColor)
        {
            
            //string cubeName = cube.name;
            Debug.Log(gameObject.GetComponent<BlockEntity>()._isChose);
            Debug.Log(gameObject.GetComponent<BlockEntity>()._isOnUserTable);
            Debug.Log(gameObject.GetComponent<BlockEntity>()._isUserColor);
            GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<Animator>().Play("Take Block");
            //GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<Animator>().SetBool("isTake", true);
            //GameObject.Find("desk/block/" + CubeName).GetComponent<Animator>().Play("Take Block");
        }
    }
    
    public void OnAnsLv1()
    {
        gameObject.transform.position = ansTransform.transform.position;
        gameObject.transform.rotation = ansTransform.transform.rotation;
        gameObject.transform.localScale = ansTransform.transform.localScale;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<Animator>().SetBool("isTake", false);
        //GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<MeshRenderer>().enabled = false;
        Debug.Log(gameObject.name);
        Debug.Log("********"+ansTransform.transform.position);
        BlockGameTask.RecentOrder++;
        Debug.Log(BlockGameTask.RecentOrder);
    }
    
    public void ToAnsLv2()
    {
        string cubeName;
        //Get Cube name without "(Clone)"
        cubeName = gameObject.name;
        int delStr = cubeName.IndexOf("(Clone)");
        if (delStr >= 0)
        {
            cubeName = cubeName.Remove(delStr);
        }
        Debug.Log("In ToAnsLv2 RecentORder: "+BlockGameTaskLv2.RecentOrder);
        _isChose = true;
        if (gameObject.GetComponent<BlockEntity>()._isChose && gameObject.GetComponent<BlockEntity>()._isOnUserTable && !gameObject.GetComponent<BlockEntity>()._isUserColor)
        {

            //string cubeName = cube.name;
            Debug.Log(gameObject.GetComponent<BlockEntity>()._isChose);
            Debug.Log(gameObject.GetComponent<BlockEntity>()._isOnUserTable);
            Debug.Log(gameObject.GetComponent<BlockEntity>()._isUserColor);
            GameObject.Find("Parents/Q" + BlockGameTaskLv2._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Parents/Q" + BlockGameTaskLv2._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<Animator>().Play("Take Block");
            //GameObject.Find("Parents/Q" + BlockGameTask._RandomQuestion + "_Parent/block/" + cubeName).GetComponent<Animator>().SetBool("isTake", true);
            //GameObject.Find("desk/block/" + CubeName).GetComponent<Animator>().Play("Take Block");
        }
    }
    
    public void OnAnsLv2()
    {
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
