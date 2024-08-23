using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin消失test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 创建从摄像机到鼠标位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 进行射线投射，检测是否击中这个对象
        if (Physics.Raycast(ray, out hit))
        {
            // 检查射线是否击中了这个对象
            if (hit.collider.gameObject == gameObject)
            {
                // 使对象消失，可以通过禁用对象或其组件来实现
                DestroyImmediate(gameObject);
            }
        }
    }
}
