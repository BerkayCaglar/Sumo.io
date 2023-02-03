using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 14, 10);
    [SerializeField] private float smoothSpeed = 0.01f;

    private void LateUpdate()
    {
        // If the target is null, return.
        if (GameManager.Instance.Player == null) return;

        // Move the camera to the target position with offset.
        Vector3 desiredPosition = GameManager.Instance.Player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
