using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinVanish : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame

    // ·´À¡²ÄÁÏ
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
            

            if ( hit.transform.CompareTag("Animal"))
            {
                target.gameObject.SetActive(false);
               
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
}

