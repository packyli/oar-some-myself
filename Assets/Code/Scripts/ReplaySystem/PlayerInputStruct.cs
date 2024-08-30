using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputStruct
{

    //public float verticalInput;
    //public float horizontalInput;

    public bool buttonPressed;

    public uint rowPaceInput;
    public uint rowPowerInput;
    public uint rowDistanceInput;

    //public PlayerInputStruct(float horizontalValue, float verticalValue, bool buttonvalue)
    //{
    //    verticalInput = verticalValue;
    //    horizontalInput = horizontalValue;
    //    buttonPressed = buttonvalue;
    //}

    public PlayerInputStruct(
        uint rowPaceValue,
        uint rowPowerValue,
        uint rowDistanceValue,
        bool buttonValue)
    {
        rowPaceInput = rowPaceValue;
        rowPowerInput = rowPowerValue;
        rowDistanceInput = rowDistanceValue;
        buttonPressed = buttonValue;
    }
}
