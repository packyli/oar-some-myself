
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
    public float maxTorsion = 150f;
    public float maxFreq = 60f;
    public float maxSpeed = 20f;
    public Engine engine;
    // Start is called before the first frame update
    void Start()
    {
        engine = FindObjectOfType<Engine>();
    }

    public void UpdateAvtarBar(PlayerInputStruct recordedInputs)
    {
        torsioninput = recordedInputs.rowPowerInput;
        float torsionvalue = (float)torsioninput / maxTorsion;
        torsionImage.fillAmount = torsionvalue;

        freqinput = recordedInputs.rowPaceInput;
        float freqvalue = (float)freqinput / maxFreq;
        freqImage.fillAmount = freqvalue;

        float time = engine.timeCount;
        if (time > 0)
        {
            distance = recordedInputs.rowDistanceInput;
            speedinput = (uint)(distance / (float)time);
            float speedvalue = (float)speedinput / maxSpeed;
            speedImage.fillAmount = speedvalue;
        }
    }
}
