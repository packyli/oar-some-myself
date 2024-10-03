using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{

    //This class should include all the potential inputs that the player makes
    //These two are the movement inputs in both horizontal and vertical movement
    //private float horizontalValue;
    //private float verticalValue;

    private float horizontalValue;
    private float verticalValue;
    private uint rowSpeedValue;
    private uint rowFrequencyValue;
    private uint rowPowerValue;
    private uint rowDistanceValue;

    private bool keyPressed;

    private RowingMachineController rowingMachineCtlr;
    private PlayerController playerCtlr;

    private void Awake()
    {
        rowingMachineCtlr = GameObject.FindObjectOfType<RowingMachineController>();
        playerCtlr = GameObject.FindObjectOfType<PlayerController>();
    }

    public void ListenForKeyPresses()
    {
        if (rowingMachineCtlr.DEBUG) {
            if (Input.GetKeyDown("w"))
            {
                keyPressed = true;
            }
        }
        else
        {
            if (rowingMachineCtlr.isRowed)
            {
                keyPressed = true;
                rowingMachineCtlr.isRowed = false;
            }
        }
    }

    public void GetInputs()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        rowSpeedValue = (uint)playerCtlr.rowSpeed;
        rowFrequencyValue = (uint)rowingMachineCtlr.MeanRPM;
        rowPowerValue = rowingMachineCtlr.CurrentForce;
        rowDistanceValue = rowingMachineCtlr.DistanceTravelled;
    }

    public PlayerInputStruct GetInputStruct()
    {
        PlayerInputStruct playerInputs = new PlayerInputStruct(horizontalValue,
            verticalValue, rowSpeedValue, rowFrequencyValue, rowPowerValue, rowDistanceValue, keyPressed);
        return playerInputs;
    }

    public void ResetInput()
    {
        horizontalValue = 0;
        verticalValue = 0;
        rowSpeedValue = 0;
        rowFrequencyValue = 0;
        rowPowerValue = 0;
        rowDistanceValue = 0;
        keyPressed = false;
    }
}


