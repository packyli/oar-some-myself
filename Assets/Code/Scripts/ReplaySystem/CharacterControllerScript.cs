using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterControllerScript : MonoBehaviour
{
    public Animator _animator;
    public float standardForceByanimSpeed = 50;
    public float moveSpeedFactor { get; set; }
    public float playerPowerFactor { get; set; }

    private CharacterController charCont;
    private Vector3 initialPosition;
    private float horizontalValue;
    private float verticalValue;
    private bool buttonValue;
    private float currentForce;

    private void Awake()
    {
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
        }
        
        //Actual Character Movement
        charCont.Move(motion * moveSpeedFactor);

    }

    public void AlterRowPower()
    {
        _animator.speed = playerPowerFactor * currentForce / standardForceByanimSpeed;
        Debug.Log("Rowing Player Avatar's current animator speed: " + _animator.speed);
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
