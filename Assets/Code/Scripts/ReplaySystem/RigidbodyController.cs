using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RigidbodyController : MonoBehaviour
{
    public Animator _animator;
    public float standardForceByanimSpeed = 50;
    public float maxPowerOutput;
    public float forceMultiplier;
    public float dragForce;
    public Transform water;

    private float currentForce;
    private bool buttonValue;
    private Rigidbody rb;
    private Engine engine;
    private RowingMachineController rowingMachine;

    private const float timeBetweenlogging = 1f;
    private float time;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Vector3 lastPlayerMoveXPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engine = GameObject.FindObjectOfType<Engine>();
        rowingMachine = GameObject.FindObjectOfType<RowingMachineController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        time = timeBetweenlogging;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        lastPlayerMoveXPos = startingPosition;
    }

    // Move is called once per frame
    public void Move()
    {
        time -= Time.deltaTime;

        if (buttonValue == true)
        {
            Debug.Log("The button press has been received, do additional functionality here");
            
            _animator.SetTrigger("IsRow");
            _animator.speed = currentForce / standardForceByanimSpeed;
            Debug.Log(1990678);
            Debug.Log(_animator.speed);

            rowingMachine.WaitingRow = false;

            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }

            rb.AddForce(Vector3.right * currentForce * forceMultiplier / maxPowerOutput, ForceMode.Impulse);

            if (!rowingMachine.DEBUG)
            {
                Debug.Log("Current proportionate force: " + currentForce * forceMultiplier / maxPowerOutput);
            }

            // Don't move backwards
            if (rb.velocity.x <= 0) rb.velocity = new Vector3(0, rb.velocity.y);

            rb.AddForce(-dragForce * Math.Abs(rb.velocity.x), 0, 0);

            if ((transform.position.x - lastPlayerMoveXPos.x) > 600)
            {
                lastPlayerMoveXPos = transform.position;
                water.GetChild(0).transform.position = water.GetChild(1).transform.position + new Vector3(499, 0, 0);
                water.GetChild(0).SetAsLastSibling();
            }
        }
    }

    public void GivenInputs(PlayerInputStruct Inputs)
    {
        currentForce = Inputs.rowPowerInput;
        buttonValue = Inputs.buttonPressed;
    }

    public void ResetInputs()
    {
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