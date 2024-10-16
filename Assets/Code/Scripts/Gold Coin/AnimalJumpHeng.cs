using UnityEngine;

public class AnimalJumpHeng : MonoBehaviour
{
    public float bounceHeight = 0.3f; // ��������߶�
    public float bounceSpeed = 0.8f;  // �����ٶ�
    public float moveDistance = 5.0f; // ǰ���ƶ��ľ���
    public float moveSpeed = 2.0f;    // �ƶ��ٶ�
    public float turnSpeed = 3f;    // ת���ٶ�
    public float startDelay = 0f;     // ��ʼ�ӳ�

    private float originalY;
    private float originalZ;
    private bool goingUp = true;       // ������������
    private bool movingForward = true; // ����ǰ���ƶ�����
    private Quaternion targetRotation; // Ŀ����ת
    private float delayCounter;        // �ӳټ�ʱ��

    void Start()
    {
        originalY = transform.position.y;
        originalZ = transform.position.z;
        targetRotation = transform.rotation;

        // ���������ʼ�ӳ�
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
                targetRotation = Quaternion.Euler(0, 180, 0); // ����ת��Ŀ��Ϊ180��
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
                targetRotation = Quaternion.Euler(0, 0, 0); // ����ת��Ŀ��Ϊ��ʼ����
            }
        }
    }

    void SmoothTurn()
    {
        // ִ��ƽ��ת��
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
