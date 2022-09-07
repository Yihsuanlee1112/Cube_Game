using GameData;
using LabData;
using System.Collections;
using System.Collections.Generic;

public class GameTaskManager : MonoSingleton<GameTaskManager>, IGameManager

{
    int IGameManager.Weight => GobalData.GameTaskManagerWeight;

    private List<TaskBase> _queuetasks;
    public static int task = 0;
    void IGameManager.ManagerInit()
    {
        _queuetasks = TaskFanctory.GetCurrentScopeTasks();
    }

    IEnumerator IGameManager.ManagerDispose()
    {
        _queuetasks.Clear();
        yield return null;
    }


    public void StartGameTask()
    {
        // _queuetasks.ForEach(p => { StartCoroutine(StartGameTaskEnumerator(p)); });
        if (GameDataManager.FlowData.Level == Level.Level1)
        {
            StartCoroutine(StartGameTaskEnumerator(_queuetasks[0]));
            task = 0;
        }
        else if (GameDataManager.FlowData.Level == Level.Level2)
        {
            StartCoroutine(StartGameTaskEnumerator(_queuetasks[1]));
            task = 1;
        }
        else if (GameDataManager.FlowData.Level == Level.Level3)
        {
            StartCoroutine(StartGameTaskEnumerator(_queuetasks[2]));
            task = 2;
        }
        else if (GameDataManager.FlowData.Level == Level.Level4)
        {
            StartCoroutine(StartGameTaskEnumerator(_queuetasks[3]));
            task = 3;
        }
    }



    private IEnumerator StartGameTaskEnumerator(TaskBase taskBase)
    {
        yield return StartCoroutine(taskBase.TaskInit());
        yield return StartCoroutine(taskBase.TaskStart());
        yield return StartCoroutine(taskBase.TaskStop());
    }
}
