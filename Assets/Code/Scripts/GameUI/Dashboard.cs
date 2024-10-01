
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using static RoundController;

public class Dashboard : MonoBehaviour
{
    
    public uint torsioninput = 0;
    public uint freqinput = 0;
    public uint speedinput = 0;
    public uint distance = 0;

    public Image torsionImage;
    public Image freqImage;
    public Image speedImage;
    public float maxTorsion = 150f;
    public float maxFreq = 60f;
    public float maxSpeed = 20f;
    public Engine engine;
    public RoundController roundController;
    public RoundMonitor monitor;
    private int dropdownValue2;
    private int dropdownValue3;
    private int dropdownValue4;
    private int dropdownValue5;
    // Start is called before the first frame update
    void Start()
    {
        engine = FindObjectOfType<Engine>();
        roundController = FindObjectOfType<RoundController>();
        monitor = FindObjectOfType<RoundMonitor>();

    }


    public void UpdateAvtarBar(PlayerInputStruct recordedInputs)
    {
        int currentRound = engine.currentRound;
        torsioninput = recordedInputs.rowPowerInput;
        float torsionvalue = (float)torsioninput / maxTorsion;
        

        freqinput = recordedInputs.rowPaceInput;
        float freqvalue = (float)freqinput / maxFreq;

        float PowerFactor = roundController.PowerFactor;
        float FrequencyFactor = roundController.FrequencyFactor;
        float SpeedFactor = roundController.SpeedFactor;

        dropdownValue2 = monitor.dropdownValue2;
        dropdownValue3 = monitor.dropdownValue3;
        dropdownValue4 = monitor.dropdownValue4;
        dropdownValue5 = monitor.dropdownValue5;


        float time = engine.timeCount;
        
        



            if (currentRound == 2)
            {
                Debug.Log("currentRound ========== 2222222222222222  !!!!!!!!!!!!!!!!!!!!!!!!");
                switch (dropdownValue2)
                {
                    case (int)RoundController.RoundType.OnlyFrequencyChanged:
                        Debug.Log("freq ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        freqvalue *= FrequencyFactor;
                        break;
                    case (int)RoundController.RoundType.OnlyPowerChanged:
                        Debug.Log("power ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        break;
                    case (int)RoundController.RoundType.AllChanged:
                        Debug.Log("all ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        freqvalue *= FrequencyFactor;
                        break;
                }
            }
            else if (currentRound == 3)
            {
                Debug.Log("currentRound ========== 33333333333  !!!!!!!!!!!!!!!!!!!!!!!!");
                switch (dropdownValue3)
                {
                    case (int)RoundController.RoundType.OnlyFrequencyChanged:
                        Debug.Log("freq ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        freqvalue *= FrequencyFactor;
                        break;
                    case (int)RoundController.RoundType.OnlyPowerChanged:
                        Debug.Log("power ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        break;
                    case (int)RoundController.RoundType.AllChanged:
                        Debug.Log("all ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        freqvalue *= FrequencyFactor;
                        break;
                }
            }

            else if (currentRound == 4)
            {
                Debug.Log("currentRound ========== 444444444444444  !!!!!!!!!!!!!!!!!!!!!!!!");
                switch (dropdownValue4)
                {
                    case (int)RoundController.RoundType.OnlyFrequencyChanged:
                        Debug.Log("freq ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        freqvalue *= FrequencyFactor;
                        break;
                    case (int)RoundController.RoundType.OnlyPowerChanged:
                        Debug.Log("power ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        break;
                    case (int)RoundController.RoundType.AllChanged:
                        Debug.Log("all ==========   !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        freqvalue *= FrequencyFactor;
                        break;
                }
            }
            else if (currentRound == 5)
            {
                Debug.Log("currentRound ========== 45555555555555555  !!!!!!!!!!!!!!!!!!!!!!!!");
                switch (dropdownValue5)
                {
                    case (int)RoundController.RoundType.OnlyFrequencyChanged:
                        Debug.Log("freq ========== 222222222222222  !!!!!!!!!!!!!!!!!!!!!!!!");
                        freqvalue *= FrequencyFactor;
                        break;
                    case (int)RoundController.RoundType.OnlyPowerChanged:
                        Debug.Log("power ========== 111111111111111  !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        break;
                    case (int)RoundController.RoundType.AllChanged:
                        Debug.Log("all ========== 4444444444444444  !!!!!!!!!!!!!!!!!!!!!!!!");
                        torsionvalue *= PowerFactor;
                        freqvalue *= FrequencyFactor;
                        break;
                }
            }






            torsionImage.fillAmount = torsionvalue;
            freqImage.fillAmount = freqvalue;
            speedImage.fillAmount = 1;
        }
    }
        

