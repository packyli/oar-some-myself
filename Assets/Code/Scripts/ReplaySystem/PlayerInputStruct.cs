using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputStruct
{
    public float horizontalInput;
    public float verticalInput;
    public uint rowPaceInput;
    public uint rowPowerInput;
    public uint rowDistanceInput;

    public bool buttonPressed;

    public PlayerInputStruct(
        float horizontalValue,
        float verticalValue,
        uint rowPaceValue,
        uint rowPowerValue,
        uint rowDistanceValue,
        bool buttonValue)
    {
        horizontalInput = horizontalValue;
        verticalInput = verticalValue;
        rowPaceInput = rowPaceValue;
        rowPowerInput = rowPowerValue;
        rowDistanceInput = rowDistanceValue;
        buttonPressed = buttonValue;
    }
}
