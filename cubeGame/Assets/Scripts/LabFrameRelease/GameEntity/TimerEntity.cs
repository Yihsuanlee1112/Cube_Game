using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEntity : GameEntityBase
{
    public float Timer = 0;
    public bool _isRecord = false;

    public override void EntityDispose()
    {

    }

    private void Update()
    {
        if (_isRecord)
        {
            Timer += Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        Timer = 0;
    }
}
