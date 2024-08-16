using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, 0.0f, verticalInput) * movementSpeed;
        movement *= Time.deltaTime;

        Vector3 position = transform.position;
        position += movement;
        transform.position = position;
    }
}
