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
    public Image torsionImage;
    public Image speedImage;
    public float maxTorsion = 100f;
    public float maxSpeed = 20f;

    private Rigidbody rb;
    private Engine engine;
    private const float timeBetweenlogging = 1f;
    private float time;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Vector3 lastPlayerMoveXPos;
    private RowingMachineController rowingMachine;

    private static LoggerService logger;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engine = GameObject.FindObjectOfType<Engine>();
        rowingMachine = GameObject.FindObjectOfType<RowingMachineController>();
        logger = new LoggerService();
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
            // Reset game variables when the key S pressed at start of game
            rb.velocity = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.S))
            {
                _startPanel.gameObject.SetActive(false);
                engine.StartGame();
            }
        }
        else
        {

            if (rowingMachine.WaitingRow)
            {
                _animator.SetTrigger("IsRow");
                _animator.speed = rowingMachine.CurrentForce / standardForceByanimSpeed;
                Debug.Log("Rowing Player's current animator speed: " + _animator.speed);

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

            }

            // Don't move backwards
            if (rb.velocity.x <= 0) rb.velocity = new Vector3(0, rb.velocity.y);

            rb.AddForce(-dragForce * Math.Abs(rb.velocity.x), 0, 0);
            Text_pace.text = rb.velocity.x.ToString("f2");

            float a = rowingMachine.CurrentForce;
            float torsionvalue = a / maxTorsion;
            torsionImage.fillAmount = torsionvalue;
            float speedvalue = rb.velocity.x / maxSpeed;
            speedImage.fillAmount = speedvalue;
            

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
        // 获取 Engine 实例
        Engine engine = GameObject.FindObjectOfType<Engine>();

        // 获取当前轮次的时间 (timeCount)
        int elapsedTime = engine.timeCount;

        // 获取当前系统时间 (Time.time)
        string systemTime = Time.time.ToString("F2");  // 保留两位小数

        // 获取当前轮次的数据
        var force = new Power(systemTime, rowingMachine.CurrentForce, false);  // 系统时间作为 Time 列
        var distance = new Distance(systemTime, transform.position.x - startingPosition.x);
        var rpm = new RPM(systemTime, rowingMachine.MeanRPM, false);

        // 将数据存入 LoggerService 的队列
        logger.power.Enqueue(force);
        logger.distance.Enqueue(distance);
        logger.rpm.Enqueue(rpm);

        // 调用 LoggerService 并传递当前轮次和 timeCount
        logger.Log(engine.currentRound, elapsedTime);  // 传递 timeCount 作为额外列
    }


    public void PlayerReset()
    {
        rb.velocity = Vector3.zero;
        transform.position = startingPosition;
        rb.rotation = startingRotation;
        rb.freezeRotation = true;

        water.Find("Background0").SetAsFirstSibling();
        water.GetChild(0).transform.localPosition = new Vector3(0, 0, 200);
        water.GetChild(1).transform.localPosition = new Vector3(499, 0, 200);
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
}