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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var target = hit.collider.gameObject;
            if (lastHighlighted != null && lastHighlighted != target)
            {
                // 如果鼠标不再悬停在原对象上，恢复原材料
                ResetHighlight();
            }

            if (lastHighlighted != target)
            {
                // 保存原始材料
                originalMaterial = target.GetComponent<Renderer>().material;
                // 改变材料以显示高亮
                target.GetComponent<Renderer>().material = highlightMaterial;
                lastHighlighted = target;
            }
        }
        else
        {
            // 如果没有物体被射线投中，恢复上一个高亮的物体的材料
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

