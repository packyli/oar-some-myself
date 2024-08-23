using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float amplitude = 0.5f;  // �����ķ���
    public float frequency = 100f;    // ������Ƶ��
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ���㸡������λ�ã�ʹ�����Һ�����������Ȼ�������ƶ�Ч��
        float tempPos = amplitude * Mathf.Sin(Time.deltaTime * frequency);
        transform.position = startPosition + new Vector3(0, tempPos, 0);
    }
}
