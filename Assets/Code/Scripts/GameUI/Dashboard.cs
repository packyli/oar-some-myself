using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    public uint torsioninput = 0;
    public uint freqinput = 0;
    public float speedinput = 0;
    public uint distance = 0;
    public Image torsionImage;
    public Image freqImage;
    public Image speedImage;
    public float maxTorsion = 150f;
    public float maxFreq = 60f;
    public float maxSpeed = 20f;
    public Engine engine;
    public RoundController roundController;

    private AvatarController avatarController;
    public int dropdownValue2;
    public int dropdownValue3;
    public int dropdownValue4;
    public int dropdownValue5;

    // Start is called before the first frame update
    void Start()
    {
        engine = FindObjectOfType<Engine>();
        roundController = FindObjectOfType<RoundController>();
        avatarController = FindObjectOfType<AvatarController>();
    }
    
    void Update()
    {
        GameObject dropdownObject2 = GameObject.FindGameObjectWithTag("Round2Config");
        GameObject dropdownObject3 = GameObject.FindGameObjectWithTag("Round3Config");
        GameObject dropdownObject4 = GameObject.FindGameObjectWithTag("Round4Config");
        GameObject dropdownObject5 = GameObject.FindGameObjectWithTag("Round5Config");

        if (dropdownObject2 != null && dropdownObject3 != null && dropdownObject4 != null && dropdownObject5 != null)
        {
            Dropdown dropdown2 = dropdownObject2.GetComponent<Dropdown>();
            Dropdown dropdown3 = dropdownObject3.GetComponent<Dropdown>();
            Dropdown dropdown4 = dropdownObject4.GetComponent<Dropdown>();
            Dropdown dropdown5 = dropdownObject5.GetComponent<Dropdown>();

            if (dropdown2 != null && dropdown3 != null && dropdown4 != null && dropdown5 != null)
            {
                dropdownValue2 = dropdown2.value;
                dropdownValue3 = dropdown3.value;
                dropdownValue4 = dropdown4.value;
                dropdownValue5 = dropdown5.value;  
            }
            else
            {
                Debug.LogError("Dropdown not found or is null!");
            }
        }
        else
        {
            Debug.LogError("GameObject with specified tag Round2Config/Round3Config/Round4Config/Round5Config not found!");
        }
    }

    public void UpdateAvtarBar(PlayerInputStruct recordedInputs)
    {
        int currentRound = engine.currentRound;
        torsioninput = recordedInputs.rowPowerInput;
        float torsionvalue = (float)torsioninput / maxTorsion;

        freqinput = recordedInputs.rowFrequencyInput;
        float freqvalue = (float)freqinput / maxFreq;

        speedinput = avatarController.avatarSpeed;
        float speedvalue = (float)speedinput / maxSpeed;

        float PowerFactor = roundController.PowerFactor;
        float FrequencyFactor = roundController.FrequencyFactor;
        float SpeedFactor = roundController.SpeedFactor;
        float time = engine.timeCount;

        if (currentRound == 2)
        {
            switch (dropdownValue2)
            {
                case (int)RoundController.RoundType.OnlyFrequencyChanged:
                    freqvalue *= FrequencyFactor;
                    break;
                case (int)RoundController.RoundType.OnlyPowerChanged:
                    torsionvalue *= PowerFactor;
                    break;
                case (int)RoundController.RoundType.AllChanged:
                    torsionvalue *= PowerFactor;
                    freqvalue *= FrequencyFactor;
                    break;
            }
        }
        else if (currentRound == 3)
        {
            switch (dropdownValue3)
            {
                case (int)RoundController.RoundType.OnlyFrequencyChanged:
                    freqvalue *= FrequencyFactor;
                    break;
                case (int)RoundController.RoundType.OnlyPowerChanged:
                    torsionvalue *= PowerFactor;
                    break;
                case (int)RoundController.RoundType.AllChanged:
                    torsionvalue *= PowerFactor;
                    freqvalue *= FrequencyFactor;
                    break;
            }
        }
        else if (currentRound == 4)
        {
            switch (dropdownValue4)
            {
                case (int)RoundController.RoundType.OnlyFrequencyChanged:
                    freqvalue *= FrequencyFactor;
                    break;
                case (int)RoundController.RoundType.OnlyPowerChanged:
                    torsionvalue *= PowerFactor;
                    break;
                case (int)RoundController.RoundType.AllChanged:
                    torsionvalue *= PowerFactor;
                    freqvalue *= FrequencyFactor;
                    break;
            }
        }
        else if (currentRound == 5)
        {
            switch (dropdownValue5)
            {
                case (int)RoundController.RoundType.OnlyFrequencyChanged:
                    freqvalue *= FrequencyFactor;
                    break;
                case (int)RoundController.RoundType.OnlyPowerChanged:
                    torsionvalue *= PowerFactor;
                    break;
                case (int)RoundController.RoundType.AllChanged:
                    torsionvalue *= PowerFactor;
                    freqvalue *= FrequencyFactor;
                    break;
            }
        }

        torsionImage.fillAmount = torsionvalue;
        freqImage.fillAmount = freqvalue;
        speedImage.fillAmount = speedvalue;
    }
}