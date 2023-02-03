using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostRotater : MonoBehaviour
{
    private float m_rotationSpeed = 100f;
    private void Update()
    {
        // Rotate the boost around the Y axis
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
    }
}
