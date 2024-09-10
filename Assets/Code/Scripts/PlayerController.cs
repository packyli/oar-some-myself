using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;

public class PlayerController : MonoBehaviour
{
    public Image _startPanel;
    public float maxPowerOutput;
    public float forceMultiplier;
    public float dragForce;
    public Animator _animator;
    public float currentForce;
    public float standardForceByanimSpeed = 50;
    public Transform water;
    // Start is called before the first frame update
    public Text Text_pace;
    public Text Text_dis;

    private Rigidbody rb;
    private Engine engine;
    private const float timeBetweenlogging = 1f;
    private float time;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Vector3 lastPlayerMoveXPos;
    private RowingMachineController rowingMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engine = GameObject.FindObjectOfType<Engine>();
        rowingMachine = GameObject.FindObjectOfType<RowingMachineController>();
    }
    void Start()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        time = timeBetweenlogging;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        lastPlayerMoveXPos = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

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
                _startPanel.gameObject.SetActive(false);
                engine.StartGame();
            }
        }
        else
        {

            if (rowingMachine.WaitingRow && engine.isRoundStarted)
            {
                _animator.SetTrigger("IsRow");
                _animator.speed = rowingMachine.CurrentForce / standardForceByanimSpeed;
                Debug.Log("Rowing Player's current animator speed: ");
                Debug.Log(_animator.speed);
                rowingMachine.WaitingRow = false;

                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                }
                //currentForce += (Vector3.right * rowingMachine.CurrentForce * forceMultiplier / maxPowerOutput).x;
                rb.AddForce(Vector3.right * rowingMachine.CurrentForce * forceMultiplier / maxPowerOutput, ForceMode.Impulse);

                if (!rowingMachine.DEBUG)
                {
                    Debug.Log("Current proportionate force: " + rowingMachine.CurrentForce * forceMultiplier / maxPowerOutput);
                }
                //engine.AddToCurrentScore(50);
            }

            // Don't move backwards
            if (rb.velocity.x <= 0) rb.velocity = new Vector3(0, rb.velocity.y);

            rb.AddForce(-dragForce * Math.Abs(rb.velocity.x), 0, 0);
            Text_pace.text = rb.velocity.x.ToString("f2");
            Text_dis.text = (transform.position.x - startingPosition.x).ToString("f2");
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
        // 
        //var force = new Power(Time.time.ToString(), rowingMachine.CurrentForce, Section == Engine.Interval.HIGH_INTENSITY);
        //var distance = new Distance(Time.time.ToString(), rowingMachine.DistanceTravelled);
        //var heartRate = new HeartRate(Time.time.ToString(), hrService.heartRate);
        //var rpm = new RPM(Time.time.ToString(), rowingMachine.MeanRPM, Section == Engine.Interval.HIGH_INTENSITY);

        //logger.heartRate.Enqueue(heartRate);
        //logger.distance.Enqueue(distance);
        //logger.power.Enqueue(force);
        //logger.rpm.Enqueue(rpm);
        //logger.Log();
    }

    public void PlayerReset()
    {
        rb.velocity = Vector3.zero;
        transform.position = startingPosition;
        rb.rotation = startingRotation;
        rb.freezeRotation = true;
    }
}

[Serializable]
public class RPM
{
    public string time;
    public bool intervalType;
    public double rpm;

    public RPM(string time, double rpm, bool interval)
    {
        this.time = time;
        this.rpm = rpm;
        intervalType = interval;
    }

    public override string ToString()
    {
        return time + "," + rpm + "," + intervalType;
    }
}


[Serializable]
public class Power
{
    public String time;
    public bool intervalType;
    public double power;

    public Power(String time, double data, bool interval)
    {
        this.time = time;
        power = data;
        intervalType = interval;
    }

    public override string ToString()
    {
        return time + "," + power + "," + intervalType;
    }
}

[Serializable]
public class Distance
{
    public String time;
    public double distance;

    public Distance(String time, double data)
    {
        this.time = time;
        distance = data;
    }

    public override string ToString()
    {
        return time + "," + distance;
    }
}