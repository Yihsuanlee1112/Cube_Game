using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEntity : GameEntityBase
{
    public Action<GameEntityBase> Action;

    public VRIKInput vRIKInput;

    public static bool _take = false;
    public override void EntityDispose()
    {

    }
    public void Init(GameObject vrCamera)
    {
        vRIKInput = new VRIKInput();
        StartCoroutine(vRIKInput.VRIKStart(vrCamera, gameObject));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            BlockGameTask._playerRound = false;
            BlockGameTaskLv2._playerRound = false;
        }
    }
}
