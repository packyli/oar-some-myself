
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using static RoundController;

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
    private int dropdownValue2;
    private int dropdownValue3;
    private int dropdownValue4;
    private int dropdownValue5;

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
            Debug.Log("dropdownValue2 is working ？？？？？？？？？？？？？" + dropdownValue2);
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

                Debug.Log("dropdownValue2 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue2);
                Debug.Log("dropdownValue3 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue3);
                Debug.Log("dropdownValue4 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue4);
                Debug.Log("dropdownValue5 is working !!!!!!!!!!!!!!!!!!!!!!!!" + dropdownValue5);

                
            }
            else
            {
                Debug.LogError("未找到 Dropdown 组件!!!!!!!!!!!!！");
            }
        }
        else
        {
            Debug.LogError("未找到具有指定 Tag 的对象！!!!!!!!!!!!!!!!!!!!!!!");
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
        speedImage.fillAmount = speedvalue;
    }
}


