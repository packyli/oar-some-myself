
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
    private int dropdownValue;
    // Start is called before the first frame update
    void Start()
    {
        engine = FindObjectOfType<Engine>();
        roundController = FindObjectOfType<RoundController>();
    }

    public void UpdateAvtarBar(PlayerInputStruct recordedInputs)
    {
        torsioninput = recordedInputs.rowPowerInput;
        float torsionvalue = (float)torsioninput / maxTorsion;
        

        freqinput = recordedInputs.rowPaceInput;
        float freqvalue = (float)freqinput / maxFreq;

        float PowerFactor = roundController.PowerFactor;
        float FrequencyFactor = roundController.FrequencyFactor;
        float SpeedFactor = roundController.SpeedFactor;

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Round2Config");

        foreach (GameObject obj in objectsWithTag)
        {
            Debug.Log("what I found!!!!!!!!!!!: " + obj.name);
        }

        int currentRound = engine.currentRound;
        string  tagName = "Round"+currentRound.ToString()+"Config";
        Debug.Log("My tage name is!!!!!!!!!!!!!!!!!! "+ tagName);
        GameObject dropdownObject = GameObject.Find("Round2Config");

        if (dropdownObject != null)
        {
            
            Dropdown dropdown = dropdownObject.GetComponent<Dropdown>();

            
            if (dropdown != null)
            {
                
                dropdownValue = dropdown.value;

                
                Debug.Log("Dropdown Value:!!!!!!!!!!!!!!!!!!!!!!!!!! " + dropdownValue);
            }
            else
            {
                //Debug.LogError("didn't find Dropdown component!!!!!!!!!!!!!!");
            }
        }
        else
        {
            //Debug.LogError("didn't find Tag object!!!!!!!!!!!!!!!!!");
        }

        float time = engine.timeCount;
        if (time > 0)
        {

            distance = recordedInputs.rowDistanceInput;
            speedinput = (uint)(distance / (float)time);
            float speedvalue = (float)speedinput / maxSpeed;
            switch (dropdownValue)
            {
                case (int)RoundController.RoundType.OnlySpeedChanged:
                    speedvalue *= SpeedFactor;
                    break;
                case (int)RoundController.RoundType.OnlyFrequencyChanged:
                    freqvalue *= FrequencyFactor;
                    break;
                case (int)RoundController.RoundType.OnlyPowerChanged:
                    torsionvalue *= PowerFactor;
                    break;
                case (int)RoundController.RoundType.AllChanged:
                    torsionvalue *= PowerFactor;
                    freqvalue *= FrequencyFactor;
                    speedvalue *= SpeedFactor;
                    break;
            }


            torsionImage.fillAmount = torsionvalue;
            freqImage.fillAmount = freqvalue;
            speedImage.fillAmount = speedvalue;
        }
    }
        
}
