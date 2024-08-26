using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityJIS;
using Timer = UnityJIS.Timer;

public class Engine : MonoBehaviour
{
    public bool IsStarted { get; private set; }
    public bool GameOver { get; private set; }
    public Text Text_time;
    public Image Image_gameOver;
    public Button button_reStart;
    private int timeCount = 0;
    public int countToWhen = 120;
    // Start is called before the first frame update

    private void Awake()
    {
        IsStarted = false;
        GameOver = false;
        Timer.Init();
        button_reStart.onClick.AddListener(ReStart);
    }

    private void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        
    }
    private TimerEvent timer;
   public void StartGame()
    {
        timer= Timer.CallBackOfIntervalTimer(1f,(object[] objs) => {
            timeCount++;

            Text_time.text = $"Countdown{(countToWhen - timeCount).ToString()}";
            if (countToWhen - timeCount == 0)
            {
                Timer.DestroyTimer("dtime");
                Image_gameOver.gameObject.SetActive(true);
            }
        });
        timer.m_timerName = "dtime";
        IsStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (s_OnApplicationUpdate != null)
            s_OnApplicationUpdate();
    }

    public static ApplicationVoidCallback s_OnApplicationModuleInitEnd = null;
    public static ApplicationVoidCallback s_OnApplicationQuit = null;
    public static ApplicationBoolCallback s_OnApplicationPause = null;
    public static ApplicationBoolCallback s_OnApplicationFocus = null;
    public static ApplicationVoidCallback s_OnApplicationUpdate = null;
    public static ApplicationVoidCallback s_OnApplicationFixedUpdate = null;
    public static ApplicationVoidCallback s_OnApplicationOnGUI = null;
    public static ApplicationVoidCallback s_OnApplicationOnDrawGizmos = null;
    public static ApplicationVoidCallback s_OnApplicationLateUpdate = null;
}

public delegate void ApplicationBoolCallback(bool status);
public delegate void ApplicationVoidCallback();
