public struct PlayerInputStruct
{
    public float horizontalInput;
    public float verticalInput;
    public uint rowSpeedInput;
    public uint rowFrequencyInput;
    public uint rowPowerInput;
    public uint rowDistanceInput;

    public bool buttonPressed;

    public PlayerInputStruct(
        float horizontalValue,
        float verticalValue,
        uint rowSpeedValue,
        uint rowFrequencyValue,
        uint rowPowerValue,
        uint rowDistanceValue,
        bool buttonValue)
    {
        horizontalInput = horizontalValue;
        verticalInput = verticalValue;
        rowSpeedInput = rowSpeedValue;
        rowFrequencyInput = rowFrequencyValue;
        rowPowerInput = rowPowerValue;
        rowDistanceInput = rowDistanceValue;
        buttonPressed = buttonValue;
    }
}
