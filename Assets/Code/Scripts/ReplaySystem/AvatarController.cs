using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public Animator _animator;
    public float standardForceByanimSpeed = 50;
    public float moveSpeedFactor { get; set; }
    public float playerPowerFactor { get; set; }
    public float maxPowerOutput = 150;
    public float forceMultiplier = 5;
    public float dragForce = 0.1f;
    public Transform water;

    private float horizontalValue;
    private float verticalValue;
    private bool buttonValue;
    private float currentForce;
    private Rigidbody rb;
    private RowingMachineController rowingMachine;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Vector3 lastPlayerMoveXPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rowingMachine = GameObject.FindObjectOfType<RowingMachineController>();
    }

    // Start is called before the first frame updates
    void Start()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        lastPlayerMoveXPos = startingPosition;
    }

    // Move the game object according to given parameters
    public void Move()
    {
        if (buttonValue == true)
        {
            //The button press has been received, row once
            AlterRowPower();
            SpeedUpFoward();
        }

    }

    public void SpeedUpFoward()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        }

        rb.AddForce(Vector3.right * currentForce * forceMultiplier * moveSpeedFactor / maxPowerOutput, ForceMode.Impulse);

        if (!rowingMachine.DEBUG)
        {
            Debug.Log("Current proportionate speed-up force to the avatar: " + currentForce * forceMultiplier * moveSpeedFactor / maxPowerOutput);
        }

        // Don't move backwards
        if (rb.velocity.x <= 0) rb.velocity = new Vector3(0, rb.velocity.y);

        // Apply drag force to gradually slow the object down
        rb.AddForce(-dragForce * Math.Abs(rb.velocity.x), 0, 0);

        // Manage the movement of a player and update the position of water elements in the game,
        // to create the illusion of an infinite or continuously scrolling water surface.
        if ((transform.position.x - lastPlayerMoveXPos.x) > 600)
        {
            lastPlayerMoveXPos = transform.position;
            water.GetChild(0).transform.position = water.GetChild(1).transform.position + new Vector3(499, 0, 0);
            water.GetChild(0).SetAsLastSibling();
        }
    }

    public void AlterRowPower()
    {
        _animator.SetTrigger("IsRow");
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
        rb.velocity = Vector3.zero;
        transform.position = startingPosition;
        rb.rotation = startingRotation;
        rb.freezeRotation = true;
    }
}
