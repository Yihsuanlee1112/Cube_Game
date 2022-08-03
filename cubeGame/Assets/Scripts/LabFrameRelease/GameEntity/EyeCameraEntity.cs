using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCameraEntity : GameEntityBase
{
    private GameObject FollowCamera;
    private bool _isInit = false;

    public override void EntityDispose()
    {

    }

    public void Init()
    {
        FollowCamera = GameObject.FindWithTag("MainCamera");
        _isInit = true;
        Debug.Log("CameraSetting");
    }

    private void Update()
    {
        if (_isInit)
        {
            gameObject.transform.position = FollowCamera.transform.position;
            gameObject.transform.rotation = FollowCamera.transform.rotation;
        }
    }
}
