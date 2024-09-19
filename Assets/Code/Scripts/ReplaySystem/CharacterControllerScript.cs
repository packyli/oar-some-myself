using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterControllerScript : MonoBehaviour
{
    public Animator _animator;
    public float standardForceByanimSpeed = 50;
    public float moveSpeedFactor = 5f;
    public float playerPowerFactor = 1f;

    private GameObject characterGameObject;
    private GameObject characterBody;
    private CharacterController charCont;
    private Vector3 initialPosition;
    private float horizontalValue;
    private float verticalValue;
    private bool buttonValue;
    private float currentForce;
    public float baseAmplitude = 1f;

    private void Awake()
    {
        characterGameObject = gameObject;
        characterBody = GameObject.FindWithTag("PlayerAvatarBody");
        charCont = GetComponent<CharacterController>();
    }

    // Start is called before the first frame updates
    void Start()
    {
        initialPosition = GetComponent<Transform>().position;
    }

    // Move the game object according to given parameters
    public void Move()
    {
        Vector3 motion = new Vector3(verticalValue, -2, horizontalValue);
        
        if (buttonValue == true)
        {
            //The button press has been received, row once
            _animator.SetTrigger("IsRow");
            AlterRowPower();
            AlterRowAmplitude();
        }
        
        //Actual Character Movement
        charCont.Move(motion * moveSpeedFactor);

    }

    public void AlterRowPower()
    {
        _animator.speed = currentForce / standardForceByanimSpeed;
        Debug.Log("Rowing Player Avatar's current animator speed: " + _animator.speed);
    }

    public void AlterRowAmplitude()
    {
        //// Modify size based on rowing power
        //float sizeFactor = baseAmplitude * currentForce * playerPowerFactor;
        //characterBody.transform.localScale = new Vector3(sizeFactor, sizeFactor, sizeFactor);
        //Debug.Log("Character body size adjusted to: " + sizeFactor);

        //// Get the Renderer component to modify the color
        //Renderer bodyRenderer = characterBody.GetComponent<Renderer>();
        //if (bodyRenderer != null)
        //{
        //    // Change the color based on power. Here, we're transitioning from blue to red as power increases.
        //    Color newColor = Color.Lerp(Color.blue, Color.red, currentForce);
        //    bodyRenderer.material.color = newColor;

        //    Debug.Log("Character body color adjusted to: " + newColor);
        //}
    }

    public void GivenInputs(PlayerInputStruct Inputs)
    {
        horizontalValue = Inputs.horizontalInput;
        verticalValue = Inputs.verticalInput;
        buttonValue = Inputs.buttonPressed;
        currentForce = Inputs.rowPowerInput;
    }

    public void ResetInputs()
    {
        horizontalValue = 0;
        verticalValue = 0;
        currentForce = 0;
    }

    public void Reset()
    {
        ResetInputs();
        charCont.enabled = false;
        charCont.transform.position = initialPosition;
        charCont.enabled = true;
    }
}
