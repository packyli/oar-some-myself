using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorObject : MonoBehaviour
{
    //What do actor objects need?
    //List-----------------------
    //1. Player Input
    //   Need a player recorder class to record inputs that are being sent to character.
    //2. Object Controller
    //   This object controller needs to read inputs and apply them to object.
    //3. Recording System / Playback System
    //   Recording system will need to record inputs from the player and then be able to play it back to the object


    //1 
    private PlayerRecorder playerInput;

    //2
    private RigidbodyController objectController;

    //3
    private InputRecorder inputRec;

    public enum State
    {
        Playing,
        Playback,
        Reset

    }
    public State currentState;


    //Booleans to check initial state changes
    private bool newPlayback = false;
    private float timer;
    private float playbackTimer;

    //UI Timer
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        //initialize the variables
        playerInput = GetComponent<PlayerRecorder>();
        objectController = GetComponent<RigidbodyController>();
        inputRec = GetComponent<InputRecorder>();

        if (playerInput == null)
        {
            Debug.LogError("PlayerRecorder is missing from this GameObject. Please attach the PlayerRecorder component.");
            return;
        }

        if (objectController == null)
        {
            Debug.LogError("RigidbodyController is missing from this GameObject. Please attach the RigidbodyController component.");
            return;
        }

        if (inputRec == null)
        {
            Debug.LogError("InputRecorder is missing from this GameObject. Please attach the InputRecorder component.");
            return;
        }

        //Player starts as idle until their clicked on, prob change this later
        currentState = State.Reset;
        timer = 0;
        playbackTimer = 0;
    }

    // Update is called once per frame
    // For button presses I noticed that if they're listened for in the fixed update they are somtimes missed
    // With this listening in a normal update loop, the button press is set to true. Then when the fixed update
    // goes over it, it will record the value as true, and then clear it for the next time the button is true.
    void Update()
    {
        playerInput.ListenForKeyPresses();
    }

    void FixedUpdate()
    {
        if ((int)currentState == 0)
        {
            timer = timer + Time.deltaTime;
            timerText.text = timer.ToString("F2");
            playerInput.GetInputs();
            PlayerInputStruct userInput = playerInput.GetInputStruct();

            inputRec.AddToDictionary(timer, userInput);
            objectController.GivenInputs(userInput);
            objectController.Move();
            playerInput.ResetInput();
        }

        if ((int)currentState == 1)
        {
            if (newPlayback == true)
            {
                playbackTimer = 0;
                newPlayback = false;
            }

            playbackTimer = playbackTimer + Time.deltaTime;
            timerText.text = playbackTimer.ToString("F2");
            if (inputRec.KeyExists(playbackTimer))
            {
                PlayerInputStruct recordedInputs = inputRec.GetRecordedInputs(playbackTimer);
                if (recordedInputs.buttonPressed == true)
                {
                    Debug.Log("At " + playbackTimer + " the value of the button press is " + recordedInputs.buttonPressed);
                }
                objectController.GivenInputs(recordedInputs);
                objectController.Move();
            }

        }

        if ((int)currentState == 2)
        {
            timer = 0;
            timerText.text = "0.00";
        }
    }

    public void Recording()
    {
        timer = 0;
        inputRec.ClearHistory();
        currentState = State.Playing;
    }


    public void Playback()
    {
        newPlayback = true;
        currentState = State.Playback;
    }

    public void Reset()
    {
        objectController.Reset();
        currentState = State.Reset;
        playerInput.ResetInput();
    }
}
