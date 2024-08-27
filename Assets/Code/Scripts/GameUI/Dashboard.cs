using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    public uint torsioninput= 800;
    public uint freqinput = 50;
    public uint speedinput = 180;

    public Image torsionImage;
    public Image freqImage;
    public Image speedImage;
    public float maxTorsion = 1000;
    public float maxFreq = 60;
    public float maxSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float torsionvalue = (float)torsioninput/ maxTorsion;
        torsionImage.fillAmount = torsionvalue;

        float freqvalue = (float)freqinput / maxFreq;
        freqImage.fillAmount = freqvalue;

        float speedvalue = (float)speedinput / maxSpeed;
        speedImage.fillAmount = speedvalue;
    }
}
