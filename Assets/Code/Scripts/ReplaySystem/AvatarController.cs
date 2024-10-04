using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public Animator _animator;
    public float standardForceByanimSpeed = 50;
    public float moveSpeedFactor { get; set; }
    public float playerPowerFactor { get; set; }
    public float avatarSpeed { get; private set; }

    public float maxPowerOutput = 150;
    public float forceMultiplier = 5;

    private float horizontalValue;
    private float verticalValue;
    private bool buttonValue;
    private float currentForce;
    private Rigidbody rb;
    private RowingMachineController rowingMachine;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

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

        avatarSpeed = rb.velocity.x;
        //Debug.Log("Current Avatar Speed: "+ avatarSpeed);
    }

    public void AlterRowPower()
    {
        _animator.SetTrigger("IsRow");
        _animator.speed = playerPowerFactor * currentForce / standardForceByanimSpeed;
        //Debug.Log("Rowing Player Avatar's current animator speed: " + _animator.speed);
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
