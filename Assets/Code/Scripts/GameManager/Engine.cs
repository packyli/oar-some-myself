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
    public int currentRound = 1;

    private int countToWhen = 120;
    private int defaultCountToWhen;
    public int timeCount = 0;
    private int maxRounds = 5;
    private TimerEvent timer;
    private PlayerController playerController;
    private ReplayController replayController;
    private RoundController roundController;

    private void Awake()
    {
        IsStarted = false;
        GameOver = false;
        Timer.Init();
        button_nextRound.onClick.AddListener(NextRound);

        roundController = GetComponent<RoundController>();
        if (roundController == null)
        {
            Debug.LogError("RoundController is missing! Please attach it to the GameObject.");
        }
    }

    private void ReStart()
    {
        currentRound = 1;
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        // The game does not start automatically at the beginning of the game,
        // but waits for the player to start the game by pressing the spacebar
        // via the PlayerController
        playerController = GameObject.FindObjectOfType<PlayerController>();
        replayController = GameObject.FindObjectOfType<ReplayController>();
    }

    public void StartGame()
    {
        if (!IsStarted) // Ensure that the game is started only once
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
        IsStarted = true;

        // Apply round-specific configurations from RoundController
        // Use 1-based index as expected by RoundController
        if (roundController != null)
        {
            roundController.StartRound(currentRound);
        }

        if (int.TryParse(Text_time.text, out countToWhen))
        {
            Debug.Log("Conversion successful, countToWhen: " + countToWhen);
            defaultCountToWhen = countToWhen;
        }
        else
        {
            Debug.LogError("Conversion failed, could not convert text to integer.");
        }

        timer = Timer.CallBackOfIntervalTimer(1f, (object[] objs) => {
            timeCount++;
            Text_time.text = $"{(countToWhen - timeCount).ToString()}";

            if (countToWhen - timeCount == 0)
            {
                EndRound();
            }
        });

        timer.m_timerName = "dtime";
    }

    private void EndRound()
    {
        Timer.DestroyTimer("dtime");
        Text_time.text = defaultCountToWhen.ToString();
        Image_gameOver.gameObject.SetActive(true);
        playerController.PlayerReset();
        IsStarted = false;

        if (currentRound < maxRounds)
        {
            // Show the next round button after a delay
            Invoke(nameof(ShowNextRoundButton), 5f);
        }
        else
        {
            // Automatically restart to round 1 after last round
            Invoke(nameof(ReStart), 5f);
        }
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
            Text_time.text = countToWhen.ToString();
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

        if (Image_gameOver.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextRound();
                replayController.StartPlayback();
            }
        }

   
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
