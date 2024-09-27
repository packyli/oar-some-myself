using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{

    //This class should include all the potential inputs that the player makes
    //These two are the movement inputs in both horizontal and vertical movement
    //private float horizontalValue;
    //private float verticalValue;

    private float horizontalValue;
    private float verticalValue;
    private uint rowPaceValue;
    private uint rowPowerValue;
    private uint rowDistanceValue;

    private bool keyPressed;

    private RowingMachineController rowingMachineCtlr;

    private void Awake()
    {
        rowingMachineCtlr = GameObject.FindObjectOfType<RowingMachineController>();
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
        rowPaceValue = (uint)rowingMachineCtlr.MeanRPM;
        rowPowerValue = rowingMachineCtlr.CurrentForce;
        rowDistanceValue = rowingMachineCtlr.DistanceTravelled;
    }

    public PlayerInputStruct GetInputStruct()
    {
        PlayerInputStruct playerInputs = new PlayerInputStruct(horizontalValue,
            verticalValue, rowPaceValue, rowPowerValue, rowDistanceValue, keyPressed);
        return playerInputs;
    }

    public void ResetInput()
    {
        horizontalValue = 0;
        verticalValue = 0;
        rowPaceValue = 0;
        rowPowerValue = 0;
        rowDistanceValue = 0;
        keyPressed = false;
    }

}


