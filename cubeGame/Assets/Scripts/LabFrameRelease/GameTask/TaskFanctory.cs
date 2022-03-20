﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFanctory 
{
    public static List<TaskBase> GetCurrentScopeTasks()
    {
        var temptasks = new List<TaskBase>
        {
            // 新增GameTask
            new BlockGameTask(),
            new BlockGameTaskLv2(),
        };
        return temptasks;
    }
}
