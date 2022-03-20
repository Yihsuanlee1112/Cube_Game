using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEntity : GameEntityBase
{
    private GameObject FollowCamera;
    private bool _isInit = false;
    // Start is called before the first frame update
    public override void EntityDispose()
    {

    }
    public void Init()
    {
        FollowCamera = GameObject.FindWithTag("MainCamera");
        _isInit = true;
        Debug.Log("CameraSetting");
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isInit)
        {
            gameObject.transform.position = FollowCamera.transform.position;
            gameObject.transform.rotation = FollowCamera.transform.rotation;
        }

    }
}
