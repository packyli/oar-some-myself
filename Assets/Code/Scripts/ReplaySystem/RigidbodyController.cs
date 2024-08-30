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
        }

        if (time <= 0 && engine.IsStarted)
        {
            LogData();
            time = timeBetweenlogging;
        }

        if (!engine.IsStarted)
        {
            // Reset game variables when space pressed at start of game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //_startPanel.gameObject.SetActive(false);
                engine.StartGame();
            }
        }
        else
        {
            if (rowingMachine.WaitingRow)
            {
                _animator.SetTrigger("IsRow");
                _animator.speed = currentForce / standardForceByanimSpeed;
                //Debug.Log(_animator.speed);
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
                //engine.AddToCurrentScore(50);
            }

            // Don't move backwards
            if (rb.velocity.x <= 0) rb.velocity = new Vector3(0, rb.velocity.y);

            rb.AddForce(-dragForce * Math.Abs(rb.velocity.x), 0, 0);
            //Text_pace.text = rb.velocity.x.ToString("f2");
            //Text_dis.text = (transform.position.x - startingPosition.x).ToString("f2");
            if ((transform.position.x - lastPlayerMoveXPos.x) > 600)
            {
                lastPlayerMoveXPos = transform.position;
                water.GetChild(0).transform.position = water.GetChild(1).transform.position + new Vector3(499, 0, 0);
                water.GetChild(0).SetAsLastSibling();
            }
        }

    }

    void LogData()
    {
        
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

//[Serializable]
//public class RPM
//{
//    public string time;
//    public bool intervalType;
//    public double rpm;

//    public RPM(string time, double rpm, bool interval)
//    {
//        this.time = time;
//        this.rpm = rpm;
//        intervalType = interval;
//    }

//    public override string ToString()
//    {
//        return time + "," + rpm + "," + intervalType;
//    }
//}


//[Serializable]
//public class Power
//{
//    public String time;
//    public bool intervalType;
//    public double power;

//    public Power(String time, double data, bool interval)
//    {
//        this.time = time;
//        power = data;
//        intervalType = interval;
//    }

//    public override string ToString()
//    {
//        return time + "," + power + "," + intervalType;
//    }
//}

//[Serializable]
//public class Distance
//{
//    public String time;
//    public double distance;

//    public Distance(String time, double data)
//    {
//        this.time = time;
//        distance = data;
//    }

//    public override string ToString()
//    {
//        return time + "," + distance;
//    }
//}