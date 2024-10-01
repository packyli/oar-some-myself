using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Transform player;
    private Transform playerAvatar;

    // Start is called before the first water
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        playerAvatar = GameObject.FindObjectOfType<AvatarController>().transform;
    }
    // Update is called once
    void Update()
    {
        float lastPlayer = 0;
        float firstPlayer = 0;
        if (player.transform.position.x - playerAvatar.transform.position.x > 0)
        {
            lastPlayer = playerAvatar.transform.position.x;
            firstPlayer = player.transform.position.x;
        }
        else
        {
            lastPlayer = player.transform.position.x;
            firstPlayer = playerAvatar.transform.position.x;
        }//see who is forward.

        if ((firstPlayer - transform.GetChild(transform.childCount - 2).position.x) > 350)
        {
            GameObject obj = transform.GetChild(0).gameObject;
            if (obj != null)
            {
                Vector3 pos = transform.GetChild(transform.childCount - 1).transform.position;
                GameObject clone = GameObject.Instantiate(obj, transform);
                clone.transform.localScale = Vector3.one;
                clone.transform.position = pos + new Vector3(499, 0, 0);
                clone.transform.localRotation = transform.GetChild(transform.childCount - 1).transform.localRotation;
            }
        }//see behind and forward player distance how long > 350 new water created.
        for (int i = 2; i < transform.childCount; i++)
        {
            if (lastPlayer - transform.GetChild(i).position.x > 400)
                DestroyImmediate(transform.GetChild(i).gameObject);
        }//see who is the last the water behind the last will be deleted.
    }
}
