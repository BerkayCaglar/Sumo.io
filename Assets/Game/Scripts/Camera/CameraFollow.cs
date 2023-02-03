using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.01f;

    private void Start()
    {
        // Find the player and set the offset
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        // If the target is null, return.
        if (target == null) return;

        // Move the camera to the target position with offset.
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
