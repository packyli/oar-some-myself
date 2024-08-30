using UnityEngine;

public class AnimalJumpHeng : MonoBehaviour
{
    public float bounceHeight = 0.3f; // 最大跳动高度
    public float bounceSpeed = 0.8f;  // 跳动速度
    public float moveDistance = 5.0f; // 前后移动的距离
    public float moveSpeed = 2.0f;    // 移动速度
    public float turnSpeed = 3f;    // 转身速度
    public float startDelay = 0f;     // 初始延迟

    private float originalY;
    private float originalZ;
    private bool goingUp = true;       // 控制跳动方向
    private bool movingForward = true; // 控制前后移动方向
    private Quaternion targetRotation; // 目标旋转
    private float delayCounter;        // 延迟计时器

    void Start()
    {
        originalY = transform.position.y;
        originalZ = transform.position.z;
        targetRotation = transform.rotation;

        // 设置随机初始延迟
        startDelay = Random.Range(0.0f, 1.0f);
        delayCounter = startDelay;
    }

    void Update()
    {
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
            return;
        }

        HandleBouncing();
        HandleMoving();
        SmoothTurn();
    }

    void HandleBouncing()
    {
        if (goingUp)
        {
            if (transform.position.y < originalY + bounceHeight)
            {
                transform.position += new Vector3(0, bounceSpeed * Time.deltaTime, 0);
            }
            else
            {
                goingUp = false;
            }
        }
        else
        {
            if (transform.position.y > originalY)
            {
                transform.position -= new Vector3(0, bounceSpeed * Time.deltaTime, 0);
            }
            else
            {
                goingUp = true;
            }
        }
    }

    void HandleMoving()
    {
        if (movingForward)
        {
            if (transform.position.z < originalZ + moveDistance)
            {
                transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
            }
            else
            {
                movingForward = false;
                targetRotation = Quaternion.Euler(0, 180, 0); // 设置转身目标为180度
            }
        }
        else
        {
            if (transform.position.z > originalZ)
            {
                transform.position -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
            }
            else
            {
                movingForward = true;
                targetRotation = Quaternion.Euler(0, 0, 0); // 设置转身目标为初始方向
            }
        }
    }

    void SmoothTurn()
    {
        // 执行平滑转身
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
