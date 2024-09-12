using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinVanish : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame

    // ��������
    public Material highlightMaterial;

    private Material originalMaterial;
    private GameObject lastHighlighted;

    public float requiredGazeTime = 0.5f;  // ��Ҫ���ӵ�ʱ�䳤�ȣ��룩
    private float gazeTimer = 0.0f;  // ���Ӽ�ʱ��
    private Transform lastGazedUpon;  // ��һ�����ӵ�����
    private bool gameStarted = false;  // ��Ϸ�Ƿ��Ѿ���ʼ
    public AudioClip disappearSound;  // ��������Ч�ֶ�
    private AudioSource audioSource;  // AudioSource���

    void Start()
    {
        // ��ȡAudioSource���
        audioSource = GetComponent<AudioSource>();

      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
        }

        if (gameStarted)  // ֻ������Ϸ��ʼ���ִ�������߼�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var target = hit.collider.gameObject;


                if (hit.transform.CompareTag("Animal"))
                {
                    target.gameObject.SetActive(false);
                    PlayDisappearSound();  // ������ʧ������
                    

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
                    gazedObject.gameObject.SetActive(false);  // ʹ������ʧ
                    PlayDisappearSound();  // ������ʧ������
                    
                }
            }
            else
            {
                gazeTimer = 0.0f;  // ���ü�ʱ��
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
            return hit.transform;  // �������ӵ�����
        }
        return null;
    }
    void PlayDisappearSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);  // ������Ч
        }
    }
}

