using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinVanish : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame

    // 反馈材料
    public Material highlightMaterial;

    private Material originalMaterial;
    private GameObject lastHighlighted;

    public float requiredGazeTime = 0.5f;  // 需要凝视的时间长度（秒）
    private float gazeTimer = 0.0f;  // 凝视计时器
    private Transform lastGazedUpon;  // 上一次凝视的物体
    private bool gameStarted = false;  // 游戏是否已经开始

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
        }

        if (gameStarted)  // 只有在游戏开始后才执行以下逻辑
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var target = hit.collider.gameObject;


                if (hit.transform.CompareTag("Animal"))
                {
                    target.gameObject.SetActive(false);

                }
            }
            Transform gazedObject = GetGazedObject();
            if (gazedObject != null && gazedObject.CompareTag("Animal"))
            {
                if (lastGazedUpon != gazedObject)
                {
                    lastGazedUpon = gazedObject;
                    gazeTimer = 0.0f;
                }

                gazeTimer += Time.deltaTime;
                if (gazeTimer >= requiredGazeTime)
                {
                    gazedObject.gameObject.SetActive(false);  // 使物体消失
                }
            }
            else
            {
                gazeTimer = 0.0f;  // 重置计时器
                lastGazedUpon = null;
            }
        }
    }

    void ResetHighlight()
    {
        if (lastHighlighted != null)
        {
            lastHighlighted.GetComponent<Renderer>().material = originalMaterial;
            lastHighlighted = null;
        }
    }
    Transform GetGazedObject()
    {
        Ray centerEyeRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(centerEyeRay, out hit))
        {
            return hit.transform;  // 返回凝视的物体
        }
        return null;
    }
}

