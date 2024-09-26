using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    // Enum to define the round configuration types
    public enum RoundType
    {
        OnlySpeedChanged,
        OnlyFrequencyChanged,
        OnlyPowerChanged,
        AllChanged
    }

    // Factors to modify base values
    public float SpeedFactor = 1.03f;  // Multiplies base speed
    public float FrequencyFactor = 2f;  // Multiplies base frequency
    public float PowerFactor = 2f;  // Multiplies base power

    // UI Dropdowns for rounds 2 to 5
    public Dropdown dropdownRound2;
    public Dropdown dropdownRound3;
    public Dropdown dropdownRound4;
    public Dropdown dropdownRound5;

    // Round configurations for each round
    [Header("Round Configurations (Editable in Unity)")]
    public List<RoundType> rounds = new List<RoundType>();

    private int currentRound = 1; // Start with round 1 (1-based index)

    void Start()
    {
        // Initialize dropdown listeners
        InitializeDropdowns();

        // Ensure at least some default round configurations are present for testing
        if (rounds.Count == 0)
        {
            // Add configurations for rounds 2 to 5 (since round 1 is not configurable)
            rounds.Add(RoundType.OnlySpeedChanged);   // Round 2
            rounds.Add(RoundType.OnlyFrequencyChanged);  // Round 3
            rounds.Add(RoundType.OnlyPowerChanged);   // Round 4
            rounds.Add(RoundType.AllChanged);         // Round 5
        }

        // Set dropdowns to the correct values based on initial configuration
        SetDropdownValues();
    }

    private void InitializeDropdowns()
    {
        // Add listeners to handle dropdown changes
        dropdownRound2.onValueChanged.AddListener(delegate { OnDropdownChanged(2, dropdownRound2.value); });
        dropdownRound3.onValueChanged.AddListener(delegate { OnDropdownChanged(3, dropdownRound3.value); });
        dropdownRound4.onValueChanged.AddListener(delegate { OnDropdownChanged(4, dropdownRound4.value); });
        dropdownRound5.onValueChanged.AddListener(delegate { OnDropdownChanged(5, dropdownRound5.value); });
    }

    private void SetDropdownValues()
    {
        // Set dropdown values based on initial round configurations
        dropdownRound2.value = (int)rounds[0];
        dropdownRound3.value = (int)rounds[1];
        dropdownRound4.value = (int)rounds[2];
        dropdownRound5.value = (int)rounds[3];
    }

    // Called when a dropdown value changes
    private void OnDropdownChanged(int roundNumber, int dropdownValue)
    {
        // Map the dropdown value to RoundType
        RoundType selectedType = (RoundType)dropdownValue;

        // Update the corresponding round configuration (rounds 2 to 5 map to index 0 to 3)
        rounds[roundNumber - 2] = selectedType;

        Debug.Log($"Round {roundNumber} configuration changed to {selectedType}");
    }

    // This function allows for user-defined rounds to start, based on a 1-based round index
    public void StartRound(int roundIndex)
    {
        // Handle case when round 1 is not configurable
        if (roundIndex == 1)
        {
            Debug.Log("Round 1 is not configurable, using default parameters.");
            ApplyParameters(1f, 1f, 1f); // Default parameters (no change)
            return;
        }

        // Only configure rounds between 2 and 5
        if (roundIndex < 2 || roundIndex > 5)
        {
            Debug.LogError($"Invalid round index! Provided: {roundIndex}, Expected between 2 and 5");
            return;
        }

        currentRound = roundIndex;  // Use the 1-based index for display purposes

        // Adjust the roundIndex to match the configuration list (0-based index)
        int adjustedIndex = roundIndex - 2;

        // Get the round type for the adjusted index
        RoundType roundType = rounds[adjustedIndex];

        // Determine factors based on the round type
        float speedFactor = 1f;
        float frequencyFactor = 1f;
        float powerFactor = 1f;

        switch (roundType)
        {
            case RoundType.OnlySpeedChanged:
                speedFactor = SpeedFactor;
                break;
            case RoundType.OnlyFrequencyChanged:
                frequencyFactor = FrequencyFactor;
                break;
            case RoundType.OnlyPowerChanged:
                powerFactor = PowerFactor;
                break;
            case RoundType.AllChanged:
                speedFactor = SpeedFactor;
                frequencyFactor = FrequencyFactor;
                powerFactor = PowerFactor;
                break;
        }

        ApplyParameters(speedFactor, frequencyFactor, powerFactor);
    }

    // Apply factors for the avatar controller/actor
    private void ApplyParameters(float speedFactor, float frequencyFactor, float powerFactor)
    {
        Debug.Log($"Applying Factors: SpeedFactor = {speedFactor}, FrequencyFactor = {frequencyFactor}, PowerFactor = {powerFactor}");

        var avatarController = FindObjectOfType<CharacterControllerScript>();
        var avatarActor = FindObjectOfType<ActorObject>();

        if (avatarController != null)
        {
            avatarController.moveSpeedFactor = speedFactor;
            avatarController.playerPowerFactor = powerFactor;
        }

        if (avatarActor != null)
        {
            avatarActor.frequencyFactor = frequencyFactor;
        }
    }
}
