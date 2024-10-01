
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    
    public uint torsioninput = 0;
    public uint freqinput = 0;
    public uint speedinput = 0;
    public uint distance = 0;

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

    public void UpdateAvtarBar(PlayerInputStruct recordedInputs)
    {
        torsioninput = recordedInputs.rowPowerInput;
        float torsionvalue = (float)torsioninput / maxTorsion;
        torsionImage.fillAmount = torsionvalue;

        freqinput = recordedInputs.rowSpeedInput;
        float freqvalue = (float)freqinput / maxFreq;
        freqImage.fillAmount = freqvalue;

        distance = recordedInputs.rowDistanceInput;
        speedinput = distance;
        float speedvalue = (float)speedinput / maxSpeed;
        speedImage.fillAmount = speedvalue;
    }

    
}
