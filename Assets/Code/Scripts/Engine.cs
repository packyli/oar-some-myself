using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityJIS;

public class Engine : MonoBehaviour
{
    public bool IsStarted { get; private set; }
    public bool GameOver { get; private set; }
    public Text Text_time;
    public Text Text_round;
    public Image Image_gameOver;
    public Button button_nextRound;
    private int timeCount = 0;
    public int countToWhen = 120;
    private int currentRound = 1;
    private int maxRounds = 5;

    private TimerEvent timer;

    private void Awake()
    {
        IsStarted = false;
        GameOver = false;
        Timer.Init();
        button_nextRound.onClick.AddListener(NextRound);
    }

    private void ReStart()
    {
        currentRound = 1;
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        // 游戏开始时不自动启动，而是等待玩家通过PlayerController按空格键来启动游戏
    }

    public void StartGame()
    {
        if (!IsStarted) // 确保游戏只启动一次
        {
            IsStarted = true;
            StartRound();
        }
    }

    private void StartRound()
    {
        timeCount = 0;
        Text_round.text = $"Round {currentRound}";
        Image_gameOver.gameObject.SetActive(false);

        timer = Timer.CallBackOfIntervalTimer(1f, (object[] objs) => {
            timeCount++;
            Text_time.text = $"Countdown: {(countToWhen - timeCount).ToString()}";

            if (countToWhen - timeCount == 0)
            {
                Timer.DestroyTimer("dtime");
                Image_gameOver.gameObject.SetActive(true);
                if (currentRound < maxRounds)
                {
                    // Show the next round button after a delay
                    Invoke("ShowNextRoundButton", 5f);
                }
                else
                {
                    // Automatically restart to round 1 after last round
                    Invoke("ReStart", 5f);
                }
            }
        });

        timer.m_timerName = "dtime";
    }

    private void ShowNextRoundButton()
    {
        button_nextRound.gameObject.SetActive(true);
    }

    private void NextRound()
    {
        if (currentRound < maxRounds)
        {
            currentRound++;
            StartRound();
            button_nextRound.gameObject.SetActive(false);
        }
        else
        {
            ReStart();
        }
    }

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
