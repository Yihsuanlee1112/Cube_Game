using GameData;
using LabData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class GameApplication : MonoSingleton<GameApplication>
{
    public enum DisposeOptions
    {
        Default,
        Back2Ui,
        Quit
    }

    private bool _isOnApplicationQuit;
    /// <summary>
    /// 继承Manager的集合
    /// </summary>
    private List<IGameManager> _gameManagers;

    public static ApplicationConfig _appliacationConfig;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameApplicationInit();
        ApplicationRun();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void GameApplicationInit()
    {
        _isOnApplicationQuit = false;
        _appliacationConfig = LabTools.GetConfig<ApplicationConfig>();
        XRSettings.enabled = _appliacationConfig.IsOpenVR;
        GameEventCenter.EventCenterInit();
        _gameManagers = FindObjectsOfType<MonoBehaviour>().OfType<IGameManager>().ToList().OrderBy(m => m.Weight).ToList();
        _gameManagers.ForEach(p =>
        {
            p.ManagerInit();
        });
    }


    /// <summary>
    /// 销毁
    /// </summary>
    public void GameApplicationDispose(DisposeOptions options = DisposeOptions.Default)
    {
        StartCoroutine(GameApplicationDisposeEnumerator(options));
    }

    private IEnumerator GameApplicationDisposeEnumerator(DisposeOptions options = DisposeOptions.Default)
    {
        if (_gameManagers.Count <= 0)
        {
            yield break;
        }

        for (int i = 0; i < _gameManagers.Count; i++)
        {
            yield return StartCoroutine(_gameManagers[i].ManagerDispose());
        }
        _gameManagers.Clear();
        GameEventCenter.RemoveAllEvents();

        switch (options)
        {
            case DisposeOptions.Default:
                yield return null;
                break;
            case DisposeOptions.Back2Ui:
                GameApplicationInit();
                ApplicationRun();
                break;
            case DisposeOptions.Quit:
                Application.Quit();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        yield return null;
    }
    public void GameApplicationQuit()
    {
        if (!_isOnApplicationQuit)
        {
            GameApplicationDispose();
            Application.Quit();
            _isOnApplicationQuit = true;
        }
    }

    protected override void OnApplicationQuit()
    {
        GameApplicationQuit();
        base.OnApplicationQuit();
    }

    private void ApplicationRun()
    {

        if (!_appliacationConfig.OneSelf)
        {
            //string[] arguments = Environment.GetCommandLineArgs();

            //GameFlowData gameFlowData = new GameFlowData();

            //Debug.Log(arguments[1]);

            //var data = GetJsonData(GetUrl(arguments[1]));

            //Debug.Log(data);


            //var input = JsonConvert.DeserializeObject<Mindfrog.FingerPinch.FingerPinchScopeInput>(data);

            //gameFlowData.FingerpinchScopeinput = input;

            //gameFlowData.UserId = "Serve";

            //GameDataManager.FlowData = gameFlowData;

            //GameDataManager.LabDataManager.LabDataCollectInit(() => GameDataManager.FlowData.UserId);

            //GameSceneManager.Instance.Change2MainScene();
        }
        else
        {
            //從init scene 跳轉到 MainUI scene
            GameSceneManager.Instance.Change2MainUI();
        }
    }

    public string GetUrl(string url)
    {
        string outUrl = "";
        string[] str = url.Split('/');
        outUrl = str[1];
        return outUrl;
    }

    public string GetJsonData(string str)
    {
        var data = str.Replace("%7B", "{").Replace("%7D", "}").Replace("%22", "\"");
        return data;
    }

}


