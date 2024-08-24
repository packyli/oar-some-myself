using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 计时圈 : MonoBehaviour
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
        if (Time.time - countStart >= time)
        {
            return;
        }
        countdown.fillAmount = 1f - (Time.time - countStart) / time;
        CountdowndText.text = ((int)(time - (Time.time - countStart))).ToString()+" second";
    }
}
