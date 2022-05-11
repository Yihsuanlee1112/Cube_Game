using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public Text Text5sec;  // Timer5sec()
    public Text Text2sec;  // Timer2sec()
    public Text Text5secLv2;  // Timer5secLv2()
    public Text Text2secLv2;  // Timer2secLv2()
    public Text EyeText;    // EyeTimer()
    public Text HandText;   // HandTimer()
    public Text BreathText; // BreathTimer()
    public Text GameTimerText; // 遊戲時間
    public Text GameCoinText;  // 金幣數量
    public Text RubyText;  // 寶石數量
    public Text HeartText;  // 愛心數量

    public Animator LeftStar;
    public Animator MidStar;
    public Animator RightStar;
    public GameObject panel_LeftStar;
    public GameObject panel_MidStar;
    public GameObject panel_RightStar;

    public GameObject panel_ChooseFace;
    public GameObject canvas_ChooseFace;
    public Image angryImage_highlight;
    public Image happyImage_highlight;
    public static bool _isPickAngry = false;
    public static bool _isPickHappy = false;

    //public Image GIFImage_roar;
    //public Image GIFImage_punch;
    //public Image GIFImage_volcano;
    //public Image GIFImage_jump;

    public GameObject rebreathButton;
    public Button rebreathBtn;

    public GameObject wayPointArrowLook;
    public GameObject wayPointArrowGreenBall;

    //public Animator teacherAnimator;

    #region Timer5sec
    private float timer_f = 0f;
    private int timer_i = 0, minute = 0, second = 0, maxWaitingTime = 5;
    # endregion
    
    #region Timer2sec
    private float timer_f5 = 0f;
    private int timer_i5 = 0, minute_5 = 0, second_5 = 0, maxWaitingTime_5 = 2;
    # endregion

    #region HandTimer
    private float timer_f1 = 0f;
    private int timer_i1 = 0;
    #endregion

    #region EyeTimer
    private float timer_f2 = 0f;
    private int timer_i2 = 0;
    #endregion

    #region GameTimer
    private float timer_f3 = 0f;
    private int timer_i3 = 0, minute_3 = 0, second_3 = 0;

    private bool remindtimesup=false;
    #endregion

    #region BreathTimer
    private float timer_f4 = 0f;
    private int timer_i4 = 0;
    #endregion

    #region numberOfCoin 
    private int NumOfCoin = 0;
    public GameObject[] coinImage;
    #endregion

    #region numberOfRuby
    private int NumOfRuby = 0;
    #endregion
    
    #region numberOfHeart
    private int NumOfHeart = 0;
    #endregion

    /*
    #region RemindSound
    private float SoundLength = 0;
    #endregion
    */

    #region EyeFoucusTimer(眼動計時)
    private List<TimerEntity> TimerList = new List<TimerEntity>();
    #endregion

    #region Interference
    public GameObject Blueplane;
    public GameObject Redplane;
    public GameObject Round;
    public GameObject Seesaw;
    public GameObject Swing;
    public Animator RedplaneAni;
    public Animator BlueplaneAni;
    public Animator RoundAni;
    public Animator SeesawAni;
    public Animator Swing1Ani;
    public Animator Swing2Ani;

    public GameObject redplane;
    public GameObject greenplane;
    public GameObject roundabout;
    public GameObject roundwalkin;
    public GameObject Bicycle;
    public GameObject Bubble;
    public Animator redplaneAni;
    public Animator greenplaneAni;
    public Animator roundAni;
    public Animator roundwalkinAni;
    public Animator bicycleAni;
    public Animator bubbleAni;

    public ParticleSystem bubbles;

    public bool bicycleisover = false, roundisover = false, bubbleisover = false;
    public bool toshowgreenplane = false, toshowredplane = false, toshowbicycle = false, toshowbubble = false, toshowround = false;
    #endregion


    private void Awake()
    {
        GameEventCenter.AddEvent<bool>("Text5sec_isEnabledLv2", Text5sec_isEnabledLv2);
        GameEventCenter.AddEvent<bool>("Text2sec_isEnabledLv2", Text2sec_isEnabledLv2);
        GameEventCenter.AddEvent<bool>("Text5sec_isEnabled", Text5sec_isEnabled);
        GameEventCenter.AddEvent<bool>("Text2sec_isEnabled", Text2sec_isEnabled);
        GameEventCenter.AddEvent<bool>("HandText_isEnabled", HandText_isEnabled);
        GameEventCenter.AddEvent<bool>("EyeText_isEnabled", EyeText_isEnabled);
        //GameEventCenter.AddEvent<bool>("BreathText_isEnabled", BreathText_isEnabled); 
        
        GameEventCenter.AddEvent("Timer5secReset", Timer5secReset);
        GameEventCenter.AddEvent("Timer5secResetLv2", Timer5secResetLv2);
        GameEventCenter.AddEvent("Timer2secReset", Timer2secReset);
        GameEventCenter.AddEvent("Timer2secResetLv2", Timer2secResetLv2);
        GameEventCenter.AddEvent("HandTimerReset", HandTimerReset);
        GameEventCenter.AddEvent("EyeTimerReset", EyeTimerReset);
        //GameEventCenter.AddEvent("BreathTimerReset", BreathTimerReset);
        //GameEventCenter.AddEvent<float>("setBreathTimer", setBreathTimer);

        GameEventCenter.AddEvent("GameCoinCounter", GameCoinCounter);
        GameEventCenter.AddEvent("GameRubyCounter", GameRubyCounter);
        GameEventCenter.AddEvent("GameHeartCounter", GameHeartCounter);
        //GameEventCenter.AddEvent<float>("GetSoundLength", GetSoundLength);
        //GameEventCenter.AddEvent<bool>("ChooseFace_isEnabled", ChooseFace_isEnabled);
        //GameEventCenter.AddEvent<GameObject>("ChooseFace_setPosition", ChooseFace_setPosition);
        //GameEventCenter.AddEvent<string>("SetGIF", SetGIF);

        // 執行者
        GameEventCenter.AddEvent<bool>("GameTimerText_isEnabled", GameTimerText_isEnabled);

        //GameEventCenter.AddEvent<bool>("rebreathButton_isEnabled", rebreathButton_isEnabled);
        GameEventCenter.AddEvent<bool>("wayPointArrowLook_isEnabled", wayPointArrowLook_isEnabled);
        GameEventCenter.AddEvent<bool>("wayPointArrowGreenBall_isEnabled", wayPointArrowGreenBall_isEnabled);

        GameEventCenter.AddEvent<string>("EyeFocusTimer", EyeFocusTimer);
        //GameEventCenter.AddEvent("StoreEndData", StoreEndData);

        //GameEventCenter.AddEvent("Interference", Interference);
        //GameEventCenter.AddEvent("bicycle", bicycle);
        //GameEventCenter.AddEvent("rightplane", rightplane);
        //GameEventCenter.AddEvent("leftplane", leftplane);
        //GameEventCenter.AddEvent("round", round);
        //GameEventCenter.AddEvent("bubble", bubble);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 把MainSceneRes的TimerList給UI的TimerList
        TimerList = GameEntityManager.Instance.GetCurrentSceneRes<MainSceneRes>().TimerList;

        //rebreathBtn.onClick.AddListener(() =>
        //{
        //    // 再深呼吸一次
        //    MainGameTask._isRebreathButtonClick = true;
        //});
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("time: "+Time.deltaTime);
        //用Text是否被打開決定要開哪個計時器，不然一次開會亂掉
        if (Text5sec.enabled)
        {
            Timer5sec();
        }
        if (Text2sec.enabled)
        {
            Timer2sec();
        }
        if (Text5secLv2.enabled)
        {
            Timer5secLv2();
        }
        if (Text2secLv2.enabled)
        {
            Timer2secLv2();
        }
        //if (HandText.enabled)
        //{
        //    HandTimer();
        //}
        if (EyeText.enabled)
        {
            EyeTimer();
        }
        //if (BreathText.enabled)
        //{
        //    BreathTimer();
        //}
        if (GameTimerText.enabled)
        {
            GameTimer();
        }
        //if (panel_ChooseFace.activeSelf)
        //{
        //    ChooseFace();
        //}
        //if (MainGameTask.bicycleover && !bicycleisover)
        //{
        //    bicycleisover = true;
        //    StartCoroutine(stopbicycle());
        //}
        //if (MainGameTask.bubbleover && !bubbleisover)
        //{
        //    bubbleisover = true;
        //    StartCoroutine(stopbubble());
        //}

        if (minute_3 == GameDataManager.FlowData.GameTimeMin&&!remindtimesup)
        {
            Debug.Log("時間到");
            remindtimesup = true;
        }
    }

    void Text5sec_isEnabled(bool judge)
    {
        Text5sec.enabled = judge;
    }
    
    void Text2sec_isEnabled(bool judge)
    {
        Text2sec.enabled = judge;
    }
    
    void Text5sec_isEnabledLv2(bool judge)
    {
        Text5secLv2.enabled = judge;
    }
    
    void Text2sec_isEnabledLv2(bool judge)
    {
        Text2secLv2.enabled = judge;
    }

    void HandText_isEnabled(bool judge)
    {
        HandText.enabled = judge;
    }

    void EyeText_isEnabled(bool judge)
    {
        EyeText.enabled = judge;
        if (judge)
        {
            StartCoroutine("ShowStar");
        }
        else
        {
            panel_LeftStar.SetActive(false);
            panel_MidStar.SetActive(false);
            panel_RightStar.SetActive(false);
            StopCoroutine("ShowStar");
        }
    }

    //void BreathText_isEnabled(bool judge)
    //{
    //    BreathText.enabled = judge;
    //}

    void Timer5secReset()
    {
        //Debug.Log("5秒reset");
        BlockGameTask._is5secTimeUp = false;
        timer_f = 0;
    }
    void Timer5secResetLv2()
    {
        //Debug.Log("5秒reset");
        BlockGameTaskLv2._is5secTimeUp = false;
        timer_f = 0;
    }
    
    void Timer2secReset()
    {
        //Debug.Log("2秒reset");
        BlockGameTask._is2secTimeUp = false;
        timer_f5 = 0;
    }
    void Timer2secResetLv2()
    {
        //Debug.Log("2秒reset");
        BlockGameTaskLv2._is2secTimeUp = false;
        timer_f5 = 0;
    }

    void EyeTimerReset()
    {
        //Debug.Log("EyeReset");
        timer_f2 = 0;
    }

    void HandTimerReset()
    {
        timer_f1 = 0;
    }

    //void BreathTimerReset()
    //{
    //    timer_f4 = 0;
    //}

    void GameTimerText_isEnabled(bool judge)
    {
        GameTimerText.enabled = judge;
    }

    void GameCoinCounter()
    {
        // 獲得coin 就變色
        coinImage[NumOfCoin].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        NumOfCoin++;
        GameCoinText.text = NumOfCoin.ToString();
    }

    void GameRubyCounter()
    {
        NumOfRuby++;
        RubyText.text = NumOfRuby.ToString();
    }
    
    void GameHeartCounter()
    {
        NumOfHeart++;
        HeartText.text = NumOfHeart.ToString();
    }

    public void Timer5sec()
    {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        //turn_time();

        //if (minute == 0 && second >= maxWaitingTime) // 超過5秒，顯示時間到
        if (timer_i >= maxWaitingTime) // 超過5秒，顯示時間到
        {
            Text5sec.text = "5秒時間到";
            //Debug.Log("5秒時間到");
            BlockGameTask._is5secTimeUp = true;

            // 5秒到了都沒有深呼吸 rebreath改成true
            //BlockGameTask._isRebreathButtonClick = true;
        }
        else if (!BlockGameTask._is5secTimeUp) // 未達5秒，顯示時間
        {
            Text5sec.text = "5秒計時: " + timer_i;
            //Debug.Log("5秒計時: " + timer_i);

        }
        /*
        void turn_time()
        {
            // 顯示時間
            minute = timer_i / 60;
            second = timer_i % 60;

            //歸零
            if (MainGameTask._isHandTryAgain || MainGameTask._isEyeTryAgain)
            {
                // 如果秒數大於30 ex. 0:31 或是 ex. 1:30 就直接歸零
                // second = 31sec 因為大於30秒就會被歸零變成0秒，下一次就是1秒，所以不會有32秒
                if (second > maxWaitingTime || minute > 0)
                {
                    minute = 0;
                    second = 0;
                    timer_i = 0;
                    timer_f = 0;
                }
            }
        }
        */
    }
    
    public void Timer5secLv2()
    {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        //turn_time();

        //if (minute == 0 && second >= maxWaitingTime) // 超過5秒，顯示時間到
        if (timer_i >= maxWaitingTime) // 超過5秒，顯示時間到
        {
            Text5secLv2.text = "5秒時間到";
            //Debug.Log("5秒時間到");
            BlockGameTaskLv2._is5secTimeUp = true;

            // 5秒到了都沒有深呼吸 rebreath改成true
            //BlockGameTask._isRebreathButtonClick = true;
        }
        else if (!BlockGameTaskLv2._is5secTimeUp) // 未達5秒，顯示時間
        {
            Text5secLv2.text = "5秒計時: " + timer_i;
            //Debug.Log("5秒計時: " + timer_i);

        }
        /*
        void turn_time()
        {
            // 顯示時間
            minute = timer_i / 60;
            second = timer_i % 60;

            //歸零
            if (MainGameTask._isHandTryAgain || MainGameTask._isEyeTryAgain)
            {
                // 如果秒數大於30 ex. 0:31 或是 ex. 1:30 就直接歸零
                // second = 31sec 因為大於30秒就會被歸零變成0秒，下一次就是1秒，所以不會有32秒
                if (second > maxWaitingTime || minute > 0)
                {
                    minute = 0;
                    second = 0;
                    timer_i = 0;
                    timer_f = 0;
                }
            }
        }
        */
    }
    
    public void Timer2sec()
    {
        timer_f5 += Time.deltaTime;
        timer_i5 = (int)timer_f5;
        //turn_time();

        //if (minute == 0 && second >= maxWaitingTime) // 超過5秒，顯示時間到
        if (timer_i5 >= maxWaitingTime_5) // 超過2秒，顯示時間到
        {
            Text2sec.text = "2秒時間到";
            Debug.Log("2秒時間到");
            BlockGameTask._is2secTimeUp = true;

            // 5秒到了都沒有深呼吸 rebreath改成true
            //BlockGameTask._isRebreathButtonClick = true;
        }
        else if (!BlockGameTask._is2secTimeUp) // 未達5秒，顯示時間
        {
            Text2sec.text = "2秒計時: " + timer_i5;
            Debug.Log("2秒計時: " + timer_i5);

        }
        /*
        void turn_time()
        {
            // 顯示時間
            minute = timer_i / 60;
            second = timer_i % 60;

            //歸零
            if (MainGameTask._isHandTryAgain || MainGameTask._isEyeTryAgain)
            {
                // 如果秒數大於30 ex. 0:31 或是 ex. 1:30 就直接歸零
                // second = 31sec 因為大於30秒就會被歸零變成0秒，下一次就是1秒，所以不會有32秒
                if (second > maxWaitingTime || minute > 0)
                {
                    minute = 0;
                    second = 0;
                    timer_i = 0;
                    timer_f = 0;
                }
            }
        }
        */
    }
    
    public void Timer2secLv2()
    {
        timer_f5 += Time.deltaTime;
        timer_i5 = (int)timer_f5;
        //turn_time();

        //if (minute == 0 && second >= maxWaitingTime) // 超過5秒，顯示時間到
        if (timer_i5 >= maxWaitingTime_5) // 超過2秒，顯示時間到
        {
            Text2secLv2.text = "2秒時間到";
            Debug.Log("2秒時間到");
            BlockGameTaskLv2._is2secTimeUp = true;

            // 5秒到了都沒有深呼吸 rebreath改成true
            //BlockGameTask._isRebreathButtonClick = true;
        }
        else if (!BlockGameTaskLv2._is2secTimeUp) // 未達5秒，顯示時間
        {
            Text2secLv2.text = "2秒計時: " + timer_i5;
            Debug.Log("2秒計時: " + timer_i5);

        }
        /*
        void turn_time()
        {
            // 顯示時間
            minute = timer_i / 60;
            second = timer_i % 60;

            //歸零
            if (MainGameTask._isHandTryAgain || MainGameTask._isEyeTryAgain)
            {
                // 如果秒數大於30 ex. 0:31 或是 ex. 1:30 就直接歸零
                // second = 31sec 因為大於30秒就會被歸零變成0秒，下一次就是1秒，所以不會有32秒
                if (second > maxWaitingTime || minute > 0)
                {
                    minute = 0;
                    second = 0;
                    timer_i = 0;
                    timer_f = 0;
                }
            }
        }
        */
    }

    //public void HandTimer() // 因為雙手都在球裡所以開始手部計時
    //{
    //    timer_f1 += Time.deltaTime;

    //    // 超過4秒且進入丟球階段，歸零
    //    /*
    //    if ((int)timer_f1 > GameDataManager.FlowData.ThrowBallPracticeInput.HandTargetSetting && GameDataManager.FlowData.IsUserFinishCatchBallFromC == true)
    //    {
    //        timer_f1 = 0;
    //    }
    //    else 
    //    */
    //    if (!GameDataManager.FlowData.LeftHand || !GameDataManager.FlowData.RightHand)  //摸了又不摸，計時歸零
    //    {
    //        Debug.Log("摸，不摸");
    //        timer_f1 = 0;
    //        HandText.text = "";

    //        BlockGameTask._is5secTimeUp = true;
    //    }
    //    else if ((int)timer_f1 == GameDataManager.FlowData.HandSec) // 摸球時間達成，顯示成功訊息
    //    {
    //        HandText.text = "手finish!";
    //        BlockGameTask._isHandFinish = true;
    //        GameEventCenter.DispatchEvent("ShareTimerRecord", "StretchHand_CompletionTime");
    //    }
    //    else if ((int)timer_f1 < GameDataManager.FlowData.HandSec)// 顯示目前計時的秒數
    //    {
    //        timer_i1 = (int)timer_f1;
    //        HandText.text = timer_i1.ToString();
    //        //Debug.Log("timer_i1:" + timer_i1);
    //    }
    //}

    public void EyeTimer()
    {
        timer_f2 += Time.deltaTime;

        if ((int)timer_f2 == GameDataManager.FlowData.EyeSec) // 4秒達成，顯示成功訊息
        {
            EyeText.text = "眼睛finish!";
            BlockGameTask._isEyeFinish = true;
        }
        else // 未達4秒，顯示目前計時的秒數
        {
            timer_i2 = (int)timer_f2;
            EyeText.text = timer_i2.ToString();
            Debug.Log("timer_i2:" + timer_i2);
        }
    }
    
    public void EyeTimerLv2()
    {
        timer_f2 += Time.deltaTime;

        if ((int)timer_f2 == GameDataManager.FlowData.EyeSec) // 4秒達成，顯示成功訊息
        {
            EyeText.text = "眼睛finish!";
            BlockGameTaskLv2._isEyeFinish = true;
        }
        else // 未達4秒，顯示目前計時的秒數
        {
            timer_i2 = (int)timer_f2;
            EyeText.text = timer_i2.ToString();
            Debug.Log("timer_i2:" + timer_i2);
        }
    }

    //public void BreathTimer()
    //{
    //    timer_f4 += Time.deltaTime;

    //    if ((int)timer_f4 == GameDataManager.FlowData.BreathSec) // 4秒達成，顯示成功訊息
    //    {
    //        BreathText.text = "深呼吸finish!";
    //        MainGameTask._isBreathFinish = true;
    //    }
    //    else // 未達4秒，顯示目前計時的秒數
    //    {
    //        timer_i4 = (int)timer_f4;
    //        BreathText.text = timer_i4.ToString();
    //        Debug.Log("timer_i4:" + timer_i4);
    //    }
    //}

    //public void setBreathTimer(float startTime)
    //{
    //    timer_f4 = startTime;
    //}

    public void GameTimer()
    {
        timer_f3 += Time.deltaTime;
        timer_i3 = (int)timer_f3;
        turn_time();

        if (second_3 < 10)
        {
            GameTimerText.text = "" + minute_3 + ":0" + second_3;
        }
        else
        {
            GameTimerText.text = "" + minute_3 + ":" + second_3;
        }
        void turn_time()
        {
            minute_3 = timer_i3 / 60;
            second_3 = timer_i3 % 60;
        }
    }

    public string GetGameTime()
    {
        return GameTimerText.text.ToString();
    }

    //IEnumerator ShowStar()
    //{
    //    float tmp = GameDataManager.FlowData.EyeSec / 3.0f;

    //    LeftStar.speed = 1 / tmp;
    //    panel_LeftStar.SetActive(true);
    //    yield return new WaitForSeconds(tmp);

    //    MidStar.speed = 1 / tmp;
    //    panel_MidStar.SetActive(true);
    //    yield return new WaitForSeconds(tmp);

    //    RightStar.speed = 1 / tmp;
    //    panel_RightStar.SetActive(true);
    //    yield return new WaitForSeconds(tmp);
    //}

    //void ChooseFace_isEnabled(bool judge)
    //{
    //    panel_ChooseFace.SetActive(judge);
    //    if(!judge)
    //    {
    //        angryImage_highlight.enabled = false;
    //        happyImage_highlight.enabled = false;
    //    }
    //}

    //void ChooseFace_setPosition(GameObject User)
    //{
    //    // 要修位置
    //    //panel_ChooseFace.GetComponent<RectTransform>().localPosition = new Vector3(User.transform.position.x, 10, User.transform.position.z + (10f));
    //    //canvas_ChooseFace.GetComponent<RectTransform>().localPosition = new Vector3(User.transform.position.x, canvas_ChooseFace.GetComponent<RectTransform>().localPosition.y, -1200+User.transform.position.z);
    //    Debug.Log("canvas_ChooseFace: " + canvas_ChooseFace.GetComponent<RectTransform>().localPosition);
    //}

    //public void ChooseFace()
    //{
    //    if (_isPickAngry)     // pick angry
    //    {
    //        angryImage_highlight.enabled = true;
    //    }
    //    else if (_isPickHappy)    // pick happy
    //    {
    //        happyImage_highlight.enabled = true;
    //    }
    //}

    //public void SetGIF(string path)
    //{
    //    string judgeGIFImageName = path.Substring(23);

    //    if (judgeGIFImageName.Equals("roar.gif"))
    //    {
    //        GIFImage_roar.GetComponent<GIFManager>().SetGIFPath(path);
    //    }
    //    else if (judgeGIFImageName.Equals("punch.gif"))
    //    {
    //        GIFImage_punch.GetComponent<GIFManager>().SetGIFPath(path);
    //    }
    //    else if (judgeGIFImageName.Equals("volcano.gif"))
    //    {
    //        GIFImage_volcano.GetComponent<GIFManager>().SetGIFPath(path);
    //    }
    //    else if (judgeGIFImageName.Equals("jump.gif"))
    //    {
    //        GIFImage_jump.GetComponent<GIFManager>().SetGIFPath(path);
    //    }

    //}

    //public void rebreathButton_isEnabled(bool judge)
    //{
    //    rebreathButton.SetActive(judge);
    //}

    public void wayPointArrowLook_isEnabled(bool judge)
    {
        wayPointArrowLook.SetActive(judge);
    }

    public void wayPointArrowGreenBall_isEnabled(bool judge)
    {
        wayPointArrowGreenBall.SetActive(judge);
    }

    void EyeFocusTimer(string focusname)
    {
        int timerIndex;
        switch (focusname)
        {
            case "HostHead":
                timerIndex = 0;
                break;
            case "EyeDetectCube":
                timerIndex = 1;
                break;
            case "FlowerHead":
                timerIndex = 2;
                break;
            default:
                timerIndex = -1;
                break;
        }

        for (int i = 0; i < TimerList.Count; i++)
        {
            TimerList[i]._isRecord = i == timerIndex;
        }
    }

    //void StoreEndData()
    //{
    //    var eyedata = new EyeDataResult()
    //    {
    //        // 把Timer儲存的注視人物總時長，給對應的變數儲存
    //        FocusTime_HostHead = TimerList[0].Timer,
    //        FocusTime_StarHead = TimerList[1].Timer,
    //        FocusTime_FlowerHead = TimerList[2].Timer,
    //    };
    //    GameDataManager.LabDataManager.SendData(eyedata);

    //    var resultdata = new GameResult()
    //    {
    //        UserID = GameDataManager.FlowData.UserID,
    //        UserName = GameDataManager.FlowData.UserName,
    //        GameTime = GameTimerText.text.ToString(),

    //        EyeSec=GameDataManager.FlowData.EyeSec,
    //        HandSec=GameDataManager.FlowData.HandSec,
    //        BreathSec=GameDataManager.FlowData.BreathSec,
    //        GameTimeMin=GameDataManager.FlowData.GameTimeMin,
    //        IsAdvanced=(GameDataManager.FlowData.isAdvanced==1)?"True":"False",
    //        IsHaveInterference=(GameDataManager.FlowData._isHaveInterference==0)?"True":"False",
    //        Interference_Bicycle=(GameDataManager.FlowData.InterferenceStatus[0]==1)?"True":"False",
    //        Interference_Bubble = (GameDataManager.FlowData.InterferenceStatus[1] == 1) ? "True" : "False",
    //        Interference_Rides = (GameDataManager.FlowData.InterferenceStatus[2] == 1) ? "True" : "False",
    //        Interference_RedPlane = (GameDataManager.FlowData.InterferenceStatus[3] == 1) ? "True" : "False",
    //        Interference_GreenPlane = (GameDataManager.FlowData.InterferenceStatus[4] == 1) ? "True" : "False",
    //        LastPlayTime=MainUI.last_playtime
    //    };
    //    GameDataManager.LabDataManager.SendData(resultdata);
    //}

    /*
    public void Timer30sec()
    {
        timer_f += Time.deltaTime;
        timer_i = (int)timer_f;
        turn_time();

        if (minute == 0 && second >= maxWaitingTime) // 超過30秒，顯示時間到
        {
            Text30sec.text = "30秒時間到";
            MainGameTask._isHandOutOfWaitingTime = true;
            MainGameTask._isEyeOutOfWaitingTime = true;
            Debug.Log("30秒時間到");
        }
        else if (MainGameTask._isHandOutOfWaitingTime != true || MainGameTask._isEyeOutOfWaitingTime != true) // 未達30秒，顯示時間
        {
            if (second < 10)
            {
                Text30sec.text = "30秒計時:" + "" + minute + ":0" + second;
            }
            else
            {
                Text30sec.text = "30秒計時:" + "" + minute + ":" + second;
            }
        }

        if ((second == 10 || second == 20) && (!_isOnce)) // 在10秒跟20秒的時候也都提醒一次
        {
            _isOnce = true;
            StartCoroutine(AudioFor30secRemind());
        }

        void turn_time()
        {
            // 顯示時間
            minute = timer_i / 60;
            second = timer_i % 60;

            //歸零
            if (MainGameTask._isHandTryAgain || MainGameTask._isEyeTryAgain)
            {
                Debug.Log("動畫播完了");
                // 如果秒數大於30 ex. 0:31 或是 ex. 1:30 就直接歸零
                // second = 31sec 因為大於30秒就會被歸零變成0秒，下一次就是1秒，所以不會有32秒
                if (second > maxWaitingTime || minute > 0)
                {
                    minute = 0;
                    second = 0;
                    timer_i = 0;
                    timer_f = 0;
                }
            }
        }
    }
     */


    //public void GetSoundLength(float length)
    //{
    //    float SoundLength = length;
    //}


    /*
    IEnumerator AudioFor30secRemind() // 30秒到的時候要說的話, 依據user正處在丟球或接球的狀態而說不一樣的話
    {
        if (MainGameTask._userState == 0)
        {
            teacherAnimator.Play("站著說話+舉左手");
            GameEventCenter.DispatchEvent("PlayRemindSound", 24); // 24: 這時要看著小星的眼睛喔
            yield return new WaitForSeconds(SoundLength);
            teacherAnimator.SetBool("手部動作 to 東張西望", true);
        }
        else if (MainGameTask._userState == 1)
        {
            teacherAnimator.Play("站著說話+舉右手");
            GameEventCenter.DispatchEvent("PlayRemindSound", 25); // 25: 這時要將雙手放到綠色球球內準備接球喔
            yield return new WaitForSeconds(SoundLength);
            teacherAnimator.SetBool("手部動作 to 東張西望", true);
        }
        _isOnce = false;
    }
    */


    //void Interference()
    //{
    //    if (GameDataManager.FlowData.Mode == Mode.ChallengeMode)
    //    {
    //        if (GameDataManager.FlowData._isHaveInterference == 0)  //有干擾物
    //        {
    //            if (GameDataManager.FlowData.InterferenceStatus[0] == 1) //bicycle on
    //            {
    //                toshowbicycle = true;
    //                Debug.Log("bicycle on");
    //            }
    //            if (GameDataManager.FlowData.InterferenceStatus[1] == 1) //bubble on
    //            {
    //                toshowbubble = true;
    //                Debug.Log("bubble on");
    //            }
    //            if (GameDataManager.FlowData.InterferenceStatus[2] == 1) //round on
    //            {
    //                toshowround = true;
    //                roundabout.SetActive(true);
    //                roundabout.transform.GetChild(1).gameObject.SetActive(false);
    //                roundabout.transform.GetChild(2).gameObject.SetActive(false);
    //                roundabout.transform.GetChild(3).gameObject.SetActive(false);
    //                roundabout.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //                roundabout.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
    //                roundabout.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(false);
    //                roundwalkin.SetActive(false);
    //                Debug.Log("round on");
    //            }
    //            if (GameDataManager.FlowData.InterferenceStatus[3] == 1) //redplane on
    //            {
    //                toshowredplane = true;
    //                Debug.Log("redplane on");
    //            }
    //            if (GameDataManager.FlowData.InterferenceStatus[4] == 1) //greenplane on
    //            {
    //                toshowgreenplane = true;
    //                Debug.Log("greenplane on");
    //            }
    //        }
    //    }
    //}

    //void leftplane()
    //{
    //    if (toshowredplane)
    //    {
    //        redplane.SetActive(true);
    //        StartCoroutine(startleftplane());
    //    }
    //}
    //IEnumerator startleftplane()
    //{
    //    redplaneAni.Play("fly");
    //    yield return new WaitForSeconds(16);
    //    redplane.SetActive(false);
    //}
    //void rightplane()
    //{
    //    if (toshowgreenplane)
    //    {
    //        greenplane.SetActive(true);
    //        StartCoroutine(startrightplane());
    //    }
    //}
    //IEnumerator startrightplane()
    //{
    //    greenplaneAni.Play("fly");
    //    yield return new WaitForSeconds(16);
    //    greenplane.SetActive(false);
    //}
    //void bicycle()
    //{
    //    if (toshowbicycle)
    //    {
    //        Bicycle.SetActive(true);
    //        StartCoroutine(startbicycle());
    //    }
    //}
    //IEnumerator startbicycle()
    //{
    //    bicycleAni.Play("bicycle");
    //    yield return null;
    //}
    //IEnumerator stopbicycle()
    //{
    //    Debug.Log("bicycle position: "+Bicycle.transform.GetChild(0).gameObject.transform.position); //transform.position印出來是Vector3(x,y,z)
    //    Bicycle.transform.position = new Vector3(Bicycle.transform.GetChild(0).gameObject.transform.position.x - 1.2898f, 0, Bicycle.transform.GetChild(0).gameObject.transform.position.z - 5.3398f);
    //    bicycleAni.Play("goaway");
    //    yield return new WaitForSeconds(25);
    //    Bicycle.SetActive(false);
    //}
    //void round()
    //{
    //    if (toshowround)
    //    {
    //        roundwalkin.SetActive(true);
    //        StartCoroutine(startround());
    //    }
    //}

    IEnumerator startround()
    {
        roundwalkinAni.Play("roundwalkin");
        yield return new WaitForSeconds(7);
        roundwalkin.SetActive(false);
        roundabout.transform.GetChild(1).gameObject.SetActive(true);
        roundabout.transform.GetChild(2).gameObject.SetActive(true);
        roundabout.transform.GetChild(3).gameObject.SetActive(true);
        roundabout.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        roundabout.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        roundabout.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
        roundAni.Play("round");
    }

    //void bubble()
    //{
    //    StartCoroutine(startbubble());
    //}
    //IEnumerator startbubble()
    //{
    //    if (toshowbubble)
    //    {
    //        Bubble.SetActive(true);
    //        bubbleAni.Play("bubble");
    //    }
    //    yield return new WaitForSeconds(6); //走進場

    //    while (!BlockGameTask.bubbleover)
    //    {
    //        yield return new WaitForSeconds(2.5f);
    //        if (!MainGameTask.bubbleover)
    //        {
    //            bubbles.Play();
    //        }
    //        yield return new WaitForSeconds(1);
    //        if (!MainGameTask.bubbleover)
    //        {
    //            bubbles.Play();
    //        }
    //        yield return new WaitForSeconds(5.66f);
    //        if (!MainGameTask.bubbleover)
    //        {
    //            bubbles.Play();
    //        }
    //        yield return new WaitForSeconds(1);
    //        if (!MainGameTask.bubbleover)
    //        {
    //            bubbles.Play();
    //        }
    //        yield return new WaitForSeconds(4f);
    //        if (!MainGameTask.bubbleover)
    //        {
    //            bubbles.Play();
    //        }
    //        yield return new WaitForSeconds(1);
    //        if (!MainGameTask.bubbleover)
    //        {
    //            bubbles.Play();
    //        }
    //        yield return new WaitForSeconds(1.43f);
    //    }

    //}
    //IEnumerator stopbubble()
    //{
    //    Bubble.transform.position = new Vector3(/*-0.834*/-1.6f, 0.12f, /*0.67f*/2.5f);
    //    bubbleAni.Play("walkout");
    //    yield return new WaitForSeconds(8);
    //    Bubble.SetActive(false);
    //}
}
