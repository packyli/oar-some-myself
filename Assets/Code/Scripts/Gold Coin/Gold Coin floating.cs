using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float amplitude = 0.5f;  // 浮动的幅度
    public float frequency = 100f;    // 浮动的频率
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 计算浮动的新位置，使用正弦函数来创建自然的上下移动效果
        float tempPos = amplitude * Mathf.Sin(Time.deltaTime * frequency);
        transform.position = startPosition + new Vector3(0, tempPos, 0);
    }
}
