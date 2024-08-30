using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInputStruct
{
    public bool buttonPressed;

    public uint rowPaceInput;
    public uint rowPowerInput;
    public uint rowDistanceInput;

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
