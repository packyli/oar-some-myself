//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RoundController : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    // Enum for possible states of parameters
    public enum ParameterState
    {
        Default,
        Changed
    }

    // Struct to store configuration for each round
    [System.Serializable]
    public struct RoundConfig
    {
        public ParameterState speedState;
        public ParameterState frequencyState;
        public ParameterState powerState;
    }

    // UI elements
    public Text roundText;

    // Default and Changed values for Speed, Frequency, and Power
    public float DefaultSpeed = 5f;
    public float ChangedSpeed = 10f;

    public float DefaultFrequency = 1f;
    public float ChangedFrequency = 2f;

    public float DefaultPower = 50f;
    public float ChangedPower = 100f;

    // Round configurations (defined in the editor or programmatically)
    public List<RoundConfig> rounds = new List<RoundConfig>();

    private int currentRound = 0;

    void Start()
    {
        // Initialize rounds manually or programmatically here
        // Example:
        rounds.Add(new RoundConfig { speedState = ParameterState.Changed, frequencyState = ParameterState.Default, powerState = ParameterState.Default }); // Round 1
        rounds.Add(new RoundConfig { speedState = ParameterState.Default, frequencyState = ParameterState.Changed, powerState = ParameterState.Default }); // Round 2
        rounds.Add(new RoundConfig { speedState = ParameterState.Default, frequencyState = ParameterState.Default, powerState = ParameterState.Changed }); // Round 3
        rounds.Add(new RoundConfig { speedState = ParameterState.Changed, frequencyState = ParameterState.Changed, powerState = ParameterState.Changed }); // Round 4

        StartRound(0); // Start with the first round
    }

    // Method to start the round
    public void StartRound(int roundIndex)
    {
        if (roundIndex >= rounds.Count)
        {
            Debug.LogError("Invalid round index!");
            return;
        }

        currentRound = roundIndex;
        RoundConfig config = rounds[roundIndex];

        // Set values based on the round's configuration
        float speed = (config.speedState == ParameterState.Changed) ? ChangedSpeed : DefaultSpeed;
        float frequency = (config.frequencyState == ParameterState.Changed) ? ChangedFrequency : DefaultFrequency;
        float power = (config.powerState == ParameterState.Changed) ? ChangedPower : DefaultPower;

        // Apply the values (you can pass these values to the game mechanics, player controller, etc.)
        ApplyParameters(speed, frequency, power);

        // Update the UI
        roundText.text = $"Round {currentRound + 1}: Speed = {speed}, Frequency = {frequency}, Power = {power}";
    }

    // Method to apply the parameters (can be expanded for your game logic)
    private void ApplyParameters(float speed, float frequency, float power)
    {
        Debug.Log($"Applying Parameters: Speed = {speed}, Frequency = {frequency}, Power = {power}");
        // Here you can apply the parameters to your game objects or mechanics (e.g., PlayerController, enemy speed, etc.)
    }

    // Method to start the next round (e.g., called from UI button)
    public void NextRound()
    {
        currentRound++;
        if (currentRound < rounds.Count)
        {
            StartRound(currentRound);
        }
        else
        {
            Debug.Log("All rounds completed!");
            // Optionally, reset or loop rounds
        }
    }

    // Method to reset rounds or restart from round 1
    public void ResetRounds()
    {
        currentRound = 0;
        StartRound(currentRound);
    }
}

