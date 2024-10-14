using UnityEngine;

public class UIPositioning : MonoBehaviour
{
    public Transform XRCamera;
    public float distanceInFront = 0.5f;

    void Update()
    {
        PositionUIInFront();
    }

    void PositionUIInFront()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.position = XRCamera.position + XRCamera.forward * distanceInFront;

        rectTransform.rotation = Quaternion.LookRotation(rectTransform.position - XRCamera.position);
    }
}


