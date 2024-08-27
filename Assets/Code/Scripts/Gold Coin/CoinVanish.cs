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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var target = hit.collider.gameObject;
            if (lastHighlighted != null && lastHighlighted != target)
            {
                // �����겻����ͣ��ԭ�����ϣ��ָ�ԭ����
                ResetHighlight();
            }

            if (lastHighlighted != target)
            {
                // ����ԭʼ����
                originalMaterial = target.GetComponent<Renderer>().material;
                // �ı��������ʾ����
                target.GetComponent<Renderer>().material = highlightMaterial;
                lastHighlighted = target;
            }
        }
        else
        {
            // ���û�����屻����Ͷ�У��ָ���һ������������Ĳ���
            ResetHighlight();
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
}

