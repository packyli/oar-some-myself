using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Image countdown;
    public Text CountdowndText;
    public float time = 60f;
    float countStart = 0f;


    // Start is called before the first frame update
    void Start()
    {
        countStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsed = Time.time - countStart;
        if (Time.time - countStart >= time)
        {
            return;
        }
        float remainingTime = time - elapsed;
        countdown.fillAmount = 1f - (Time.time - countStart) / time;
        CountdowndText.text = ((int)(time - (Time.time - countStart))).ToString()+" second";
        if(remainingTime <= time * 0.1f)
        {
            countdown.color = Color.yellow;
        }
    }
}
