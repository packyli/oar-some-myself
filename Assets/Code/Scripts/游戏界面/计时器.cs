
using UnityEngine;
using UnityEngine.UI;

public class 计时器 : MonoBehaviour
{
    public Text timerText; // 用于显示时间的UI文本
    private float startTime; // 计时器开始的时间

    void Start()
    {
        // 在游戏开始时立即启动计时器
        startTime = Time.time;
    }

    void Update()
    {
        // 计算经过的时间并更新UI文本
        float elapsedTime = Time.time - startTime;
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");
        timerText.text = minutes + ":" + seconds;
    }
}
