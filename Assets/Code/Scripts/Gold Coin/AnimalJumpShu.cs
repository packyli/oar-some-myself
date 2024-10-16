using UnityEngine;

public class AnimalJumpShu : MonoBehaviour
{
    public float bounceHeight = 0.3f; // ��������߶�
    public float bounceSpeed = 0.8f;  // �����ٶ�
    public float moveDistance = 5.0f; // ǰ���ƶ��ľ���
    public float moveSpeed = 2.0f;    // �ƶ��ٶ�
    public float turnSpeed = 3f;    // ת���ٶ�
    public float startDelay = 0f;     // ��ʼ�ӳ�

    private float originalX;
    private float originalY;
    private float originalZ;
    private bool goingUp = true;       // ������������
    private bool movingForward = true; // ����ǰ���ƶ�����
    private Quaternion targetRotation; // Ŀ����ת
    private float delayCounter;        // �ӳټ�ʱ��

    void Start()
    {
        originalX = transform.position.x;
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
            if (transform.position.x < originalX + moveDistance) // ʹ��ԭʼ��Z�������洢X��ĳ�ʼλ��
            {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                movingForward = false;
                targetRotation = Quaternion.Euler(0, -90, 0); // �޸ĽǶȣ�ʹ���峯��X�Ḻ����
            }
        }
        else
        {
            if (transform.position.x > originalX) // ����ʹ��ԭʼZ����������Ӧ����originalX����Ҫ��Start�����г�ʼ��
            {
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                movingForward = true;
                targetRotation = Quaternion.Euler(0, 90, 0); // �޸ĽǶȣ�ʹ���峯��X��������
            }
        }
    }

    void SmoothTurn()
    {
        // ִ��ƽ��ת��
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
