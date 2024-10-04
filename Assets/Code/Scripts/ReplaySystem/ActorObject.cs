using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorObject : MonoBehaviour
{
    public enum State
    {
        Playing,
        Playback,
        Reset
    }
    public State currentState;

    // UI Timer
    public Text timerText;

    //public float dragForce = 0.1f;
    public float frequencyFactor { get; set; }
    public float baseDrag = 0.1f;
    public float dragFactor = 0.05f;
    public float avatarSpeed { get; private set; }

    // 1. Player Input
    private PlayerRecorder playerInput;

    // 2. Object Controller
    private AvatarController objectController;

    // 3. Recording System
    private InputRecorder inputRec;

    // Booleans to check initial state changes
    private bool newPlayback = false;
    private float timer;
    private float playbackTimer;
    private Rigidbody rb;
    private Dashboard avatarDashboard;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the variables
        playerInput = GetComponent<PlayerRecorder>();
        objectController = GetComponent<AvatarController>();
        inputRec = GetComponent<InputRecorder>();
        avatarDashboard = GameObject.FindObjectOfType<Dashboard>();
        rb = GetComponent<Rigidbody>();

        if (playerInput == null)
        {
            Debug.LogError("PlayerRecorder is missing from this GameObject. Please attach the PlayerRecorder component.");
            return;
        }

        if (objectController == null)
        {
            Debug.LogError("AvatarController is missing from this GameObject. Please attach the AvatarController component.");
            return;
        }

        if (inputRec == null)
        {
            Debug.LogError("InputRecorder is missing from this GameObject. Please attach the InputRecorder component.");
            return;
        }

        // Player starts as idle (Reset state)
        currentState = State.Reset;
        timer = 0;
        playbackTimer = 0;
    }

    void Update()
    {
        // Capture key presses in the regular update loop
        playerInput.ListenForKeyPresses();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Playing:
                HandleRecording();
                break;
            case State.Playback:
                HandlePlayback();
                break;
            case State.Reset:
                HandleReset();
                break;
        }
    }

    private void HandleRecording()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");

        playerInput.GetInputs();
        PlayerInputStruct userInput = playerInput.GetInputStruct();
        inputRec.AddToDictionary(timer, userInput);

        playerInput.ResetInput();
    }

    private void HandlePlayback()
    {
        if (newPlayback)
        {
            playbackTimer = 0;
            newPlayback = false;
        }

        playbackTimer += Time.deltaTime;
        timerText.text = playbackTimer.ToString("F2");

        if (inputRec.KeyExists(playbackTimer))
        {
            PlayerInputStruct recordedInputs = inputRec.GetRecordedInputs(playbackTimer);

            if (recordedInputs.buttonPressed)
            {
                Debug.Log($"Row action triggered at {playbackTimer}, buttonPressed: {recordedInputs.buttonPressed}");
                objectController.GivenInputs(recordedInputs);
                objectController.Move();
            }

            // Don't move backwards
            if (rb.velocity.x <= 0) rb.velocity = new Vector3(0, rb.velocity.y);

            // Apply drag force to gradually slow the object down
            //rb.AddForce(-dragForce * Math.Abs(rb.velocity.x), 0, 0);

            //rb.drag = baseDrag + rb.velocity.magnitude * dragFactor;
            avatarSpeed = rb.velocity.x;
            //Debug.Log("Current Avatar Speed: "+ avatarSpeed);

            avatarDashboard.UpdateAvtarBar(recordedInputs);
        }
    }

    private void HandleReset()
    {
        timer = 0;
        timerText.text = "0.00";
    }

    public void Recording()
    {
        timer = 0;
        inputRec.ClearHistory();
        currentState = State.Playing;
    }

    public void Playback()
    {
        Debug.Log("Starting playback.");
        inputRec.PrintInputRecord();

        if (frequencyFactor > 1)
        {
            Debug.Log("Start altering frequency!!");
            AlterFrequency();
        }

        newPlayback = true;
        currentState = State.Playback;
    }

    public void Reset()
    {
        objectController.Reset();
        playerInput.ResetInput();
        currentState = State.Reset;
    }

    // This method doubles the frequency of `buttonPressed` occurrences in the input record
    private void AlterFrequency()
    {
        List<float> keys = new List<float>(inputRec.playerInputRecord.Keys);

        // Loop through all key-value pairs
        for (int i = 0; i < keys.Count - 1; i++)
        {
            float currentTime = keys[i];
            PlayerInputStruct currentInput = inputRec.playerInputRecord[currentTime];

            if (currentInput.buttonPressed)
            {
                // Find the next time where buttonPressed is true
                float nextRowTime = -1;
                for (int j = i + 1; j < keys.Count; j++)
                {
                    float nextTime = keys[j];
                    PlayerInputStruct nextInput = inputRec.GetRecordedInputs(nextTime);

                    if (nextInput.buttonPressed)
                    {
                        nextRowTime = nextTime;
                        break; // Exit the loop as soon as we find the next buttonPressed = true
                    }
                }

                // If no further row action (buttonPressed == true) is found, break the loop
                if (nextRowTime == -1) break;

                // Calculate the midTime between currentTime and nextRowTime
                float midTime = currentTime + (nextRowTime - currentTime) / 2;
                Debug.Log("midTime: " + midTime);

                // Check if midTime exists in the dictionary
                PlayerInputStruct midInput;
                if (inputRec.KeyExists(midTime))
                {
                    midInput = inputRec.GetRecordedInputs(midTime);
                    Debug.Log("KeyExists: " + midTime);

                    // Set buttonPressed to true at the midpoint
                    midInput.buttonPressed = true;
                    // Update the dictionary with the modified input
                    inputRec.UpdateDictionary(midTime, midInput);

                }
                else
                {
                    Debug.Log("KeyNotExists: " + midTime);
                    // If midTime does not exist, interpolate values between current and next inputs
                    midInput = InterpolateInputs(currentInput, inputRec.GetRecordedInputs(nextRowTime), 0.5f);
                    // Insert interpolated entry
                    inputRec.AddToDictionary(midTime, midInput);
                }
            }
        }

        // Print the altered record (optional)
        Debug.Log("Double frequency done!");
        inputRec.PrintInputRecord();
    }


    // Method to interpolate between two PlayerInputStructs
    private PlayerInputStruct InterpolateInputs(PlayerInputStruct input1, PlayerInputStruct input2, float factor)
    {
        return new PlayerInputStruct(
            Mathf.Lerp(input1.horizontalInput, input2.horizontalInput, factor),
            Mathf.Lerp(input1.verticalInput, input2.verticalInput, factor),
            (uint)Mathf.Lerp(input1.rowSpeedInput, input2.rowSpeedInput, factor),
            (uint)Mathf.Lerp(input1.rowFrequencyInput, input2.rowFrequencyInput, factor),  // Include rowFrequency interpolation
            (uint)Mathf.Lerp(input1.rowPowerInput, input2.rowPowerInput, factor),
            (uint)Mathf.Lerp(input1.rowDistanceInput, input2.rowDistanceInput, factor),
            input1.buttonPressed || input2.buttonPressed  // Preserve true if either input has buttonPressed true
        );
    }
}
