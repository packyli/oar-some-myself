﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{

    //This class should include all the potential inpuits that the player makes
    //These two are the movement inputs in both horizontal and vertical movement
    //private float horizontalValue;
    //private float verticalValue;
    
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
        if (Input.GetKeyDown("w"))
        {
            keyPressed = true;
        }
    }

    public void GetInputs()
    {
        rowPaceValue = 35; // Temp value for testing
        rowPowerValue = rowingMachineCtlr.CurrentForce;

        rowDistanceValue = rowingMachineCtlr.DistanceTravelled;
    }

    public PlayerInputStruct GetInputStruct()
    {
        PlayerInputStruct playerInputs = new PlayerInputStruct(rowPaceValue, rowPowerValue, rowDistanceValue, keyPressed);
        return playerInputs;
    }

    public void ResetInput()
    {
        rowPaceValue = 0;
        rowPowerValue = 0;
        rowDistanceValue = 0;
        keyPressed = false;
    }

}

